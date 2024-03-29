﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Security;
using AppHarbor.Models;
using System.Security.Cryptography;
using System.Text;

using Utils;
using System.Net.Mail;
using System.Configuration;
using System.Net;

namespace AppHarbor.Controllers
{
    public class AccountController : Controller
    {
        //
        // GET: /Account/LogOn

        public ActionResult LogOn()
        {
            return View();
        }

        //
        // POST: /Account/LogOn

        [HttpPost]
        public ActionResult LogOn(LogOnModel model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                if (Membership.ValidateUser(model.LoginName, model.Password))
                {
                    FormsAuthentication.SetAuthCookie(model.LoginName, model.RememberMe);
                    if (Url.IsLocalUrl(returnUrl) && returnUrl.Length > 1 && returnUrl.StartsWith("/")
                        && !returnUrl.StartsWith("//") && !returnUrl.StartsWith("/\\"))
                    {
                        return Redirect(returnUrl);
                    }
                    else
                    {
                        return RedirectToAction("Index", "Home");
                    }
                }
                else
                {
                    MembershipUser u = Membership.GetUser(model.LoginName);
                    if (u != null)
                    {
                        if (!u.IsApproved)
                            ModelState.AddModelError("", "Account not yet validated, please validate your account before logging in.");
                        if (u.IsLockedOut)
                            ModelState.AddModelError("", "User was denied access. Contact an administrator to find out the reason.");
                    }else
                        ModelState.AddModelError("", "The user name or password provided is incorrect.");
                }
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        //
        // GET: /Account/LogOff

        public ActionResult LogOff()
        {
            FormsAuthentication.SignOut();

            return RedirectToAction("Index", "Home");
        }

        //
        // GET: /Account/Register

        public ActionResult Register()
        {
            return View();
        }

        //
        // POST: /Account/Register

        [HttpPost]
        public ActionResult Register(RegisterModel model)
        {
            if (ModelState.IsValid)
            {
                // Attempt to register the user
                MembershipCreateStatus createStatus;
                var user = Membership.CreateUser(model.LoginName, model.Password, model.Email, model.SecurityQuestion, model.SecurityAnswer
                                        , false, out createStatus);//isApproved=false user cannot log in

                if (createStatus == MembershipCreateStatus.Success)
                {
                    string challenge = ChallengeGenerator(DateTime.Now.Ticks.ToString());
                    FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(
                        1,
                        "Challenge",
                        DateTime.Now,
                        DateTime.Now.AddMinutes(30),
                        false,
                        "HiIAMaCookie!Do you want to eat me?");
                    string encryption = FormsAuthentication.Encrypt(ticket);
                    Response.Cookies.Add(new HttpCookie(".SPOTICHELASANONYMOUS", encryption));
            
                    Membership.UpdateUser(user);
                    Utils.Emailer.SendEmail(model.LoginName, model.Email, " Validation Code: " + challenge, "Account Validation!");
                    return RedirectToAction("Validate");
                }
                else
                {
                    ModelState.AddModelError("", ErrorCodeToString(createStatus));
                }
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        //
        // GET: /Account/ChangePassword

        [Authorize]
        public ActionResult ChangePassword()
        {
            return View();
        }

        //
        // POST: /Account/ChangePassword

        [Authorize]
        [HttpPost]
        public ActionResult ChangePassword(ChangePasswordModel model)
        {
            if (ModelState.IsValid)
            {

                // ChangePassword will throw an exception rather
                // than return false in certain failure scenarios.
                bool changePasswordSucceeded;
                try
                {
                    MembershipUser currentUser = Membership.GetUser(User.Identity.Name, true /* userIsOnline */);
                    changePasswordSucceeded = currentUser.ChangePassword(model.OldPassword, model.NewPassword);
                }
                catch (Exception)
                {
                    changePasswordSucceeded = false;
                }

                if (changePasswordSucceeded)
                {
                    return RedirectToAction("ChangePasswordSuccess");
                }
                else
                {
                    ModelState.AddModelError("", "The current password is incorrect or the new password is invalid.");
                }
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        //
        // GET: /Account/ChangePasswordSuccess

        public ActionResult ChangePasswordSuccess()
        {
            return View();
        }

        //GET: /Account/UserCP
        [Authorize]
        [HttpGet, ActionName("UserCP")]
        public ActionResult UserCPGet()
        {
            object avatar = Profile.GetPropertyValue("AvatarUrl");
            ViewBag.Image = avatar == null || avatar.Equals(string.Empty) ? string.Empty : avatar.ToString();
            ViewBag.Email = Membership.GetUser().Email;
            return View("UserCP");
        }

        //GET: /Account/Validate
        [HttpGet, ActionName("Validate")]
        public ActionResult ValidateGet()
        {
            return View("Validate");
        }

        //POST: /Account/Validate
        [HttpPost, ActionName("Validate")]
        public ActionResult ValidatePost(ValidateAccountModel model)
        {
            if (ModelState.IsValid)
            {

                MembershipUser user = Membership.GetUser(model.LoginName);
                IEnumerable<string> count = from MembershipUser u in Membership.GetAllUsers()
                            where model.Nickname.Equals(u.Comment)
                            select u.Comment;
                if (count.Count() == 0)
                {
                    HttpCookie cookie = Request.Cookies[".SPOTICHELASANONYMOUS"];
                    if (cookie == null)
                        ModelState.AddModelError("", "Validation code expired! Please Register again. Also allow cookies in order to complete the regristration");
                    //Note: Due to the challenge code being kept in a cookie, they must be activated in the browser. Embebing into Url would be an option but a security risk
                    FormsAuthenticationTicket ticket = FormsAuthentication.Decrypt(cookie.Value);
                    string challenge = ticket.UserData;
                    if (model.Challenge.Equals(challenge))
                    {
                        user.IsApproved = true;
                        user.Comment = model.Nickname;
                        Roles.AddUserToRole(user.UserName, "User");
                        Membership.UpdateUser(user);
                        FormsAuthentication.SetAuthCookie(user.UserName, true /* createPersistentCookie */);
                        return RedirectToAction("UserCP");
                    }
                    else
                    {
                        ModelState.AddModelError("challenge", "Invalid Challenge Code!");
                    }
                }else
                    ModelState.AddModelError("nickname", "Public name already taken!");
            }
            return View("Validate");
            }

        //GET: /Account/Edit
        [Authorize]
        [HttpGet, ActionName("Edit")]
        public ActionResult EditGet()
        {
            return View("Edit");
        }

        //POST: /Account/Edit
        [Authorize]
        [HttpPost, ActionName("Edit")]
        public ActionResult EditPost(EditAccountModel model)
        {
            if (ModelState.IsValid)
            {
                MembershipUser user = Membership.GetUser();
                if (model.Email != null)
                    user.Email = model.Email;
                if (model.Image != null)
                {
                    Profile.SetPropertyValue("AvatarUrl", model.Image);
                    Profile.Save();
                }
            }
            return View("Edit",model);
        }

        //POST: /Account/Erase
        [Authorize]
        [HttpPost, ActionName("Erase")]
        public ActionResult ErasePost() 
        {
            var user = Membership.GetUser();
            Membership.DeleteUser(user.UserName, true);
            return RedirectToAction("LogOff", "Account");
        }

        //GET: /Account/Promote
        [Authorize(Roles="Admin")]
        [HttpGet, ActionName("Promote")]
        public ActionResult PromoteGet()
        {
            ViewBag.Users = (from MembershipUser user in Membership.GetAllUsers()
                             select user.Comment).ToList();
            ViewBag.Roles = Roles.GetAllRoles().ToList();
            return View("Promote");
        }

        //POST: /Account/Promote
        [Authorize(Roles = "Admin")]
        [HttpPost, ActionName("Promote")]
        public ActionResult PromotePost(PromoteAccountModel model)
        {
            IEnumerable<MembershipUser> users = from MembershipUser u in Membership.GetAllUsers()
                                  where u.Comment.Equals(model.Nickname)
                                  select Membership.GetUser(u.UserName);
            MembershipUser user = users.FirstOrDefault();
            if (user != null)
                if (Roles.RoleExists(model.Role))
                    if (!Roles.IsUserInRole(user.UserName, model.Role))
                    {
                        Roles.AddUserToRole(user.UserName, model.Role);
                        return RedirectToAction("UserCP");
                    }
                    else
                    {
                        ModelState.AddModelError("Username", "User is already in that role!");
                    }
                else
                {
                    ModelState.AddModelError("Role", "Role doesnt exist");
                }
            else
            {
                ModelState.AddModelError("Usename", "User not found!");
            }
            return View("Promote");
        }

        //GET: /Account/Delete
        [Authorize(Roles = "Admin")]
        [HttpGet, ActionName("Delete")]
        public ActionResult DeleteGet()
        {
            ViewBag.Users = (from MembershipUser user in Membership.GetAllUsers()
                                select user.UserName).ToList();
            return View("Delete");
        }

        //POST: /Account/Delete
        [Authorize(Roles = "Admin")]
        [HttpPost, ActionName("Delete")]
        public ActionResult DeletePost(string user)
        {
            if (user!=null)
                if(Membership.GetUser(user) != null)
                {
                    Membership.DeleteUser(user, true);
                    return RedirectToAction("UserCP");
                }else
                    ModelState.AddModelError("user", "User not found!");
            else
                ModelState.AddModelError("user", "User not specified!");
            return View("Delete");
        }

        private string ChallengeGenerator(string random)
        {
            MD5 md5Hasher = MD5.Create();
            byte[] data = md5Hasher.ComputeHash(Encoding.UTF8.GetBytes(random));
            StringBuilder builder = new StringBuilder();
            for (int i = 0; i < data.Length; i++)
            {
                builder.Append(data[i].ToString("x2"));
            }
            return builder.ToString();
        }

        #region Status Codes
        private static string ErrorCodeToString(MembershipCreateStatus createStatus)
        {
            // See http://go.microsoft.com/fwlink/?LinkID=177550 for
            // a full list of status codes.
            switch (createStatus)
            {
                case MembershipCreateStatus.DuplicateUserName:
                    return "User name already exists. Please enter a different user name.";

                case MembershipCreateStatus.DuplicateEmail:
                    return "A user name for that e-mail address already exists. Please enter a different e-mail address.";

                case MembershipCreateStatus.InvalidPassword:
                    return "The password provided is invalid. Please enter a valid password value.";

                case MembershipCreateStatus.InvalidEmail:
                    return "The e-mail address provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.InvalidAnswer:
                    return "The password retrieval answer provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.InvalidQuestion:
                    return "The password retrieval question provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.InvalidUserName:
                    return "The user name provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.ProviderError:
                    return "The authentication provider returned an error. Please verify your entry and try again. If the problem persists, please contact your system administrator.";

                case MembershipCreateStatus.UserRejected:
                    return "The user creation request has been canceled. Please verify your entry and try again. If the problem persists, please contact your system administrator.";

                default:
                    return "An unknown error occurred. Please verify your entry and try again. If the problem persists, please contact your system administrator.";
            }
        }
        #endregion
    }
}
