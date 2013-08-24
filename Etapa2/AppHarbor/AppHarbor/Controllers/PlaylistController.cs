using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

using Utils;
using BusinessRules;
using System.Collections;

namespace AppHarbor.Controllers
{
    [Authorize]
    public class PlaylistController : Controller
    {
        Logic Rules = Logic.Factory();

        // GET: root/playlist/index
        public ActionResult Index()
        {
            return RedirectToAction("List", "Playlist");
        }

        //GET: root/playlist/new
        [HttpGet, ActionName("New")]
        public ActionResult NewGet()
        {
            return View("Create");
        }

        //POST: root/playlist/new
        [HttpPost, ActionName("New")]
        public ActionResult NewPost(CreatePlaylist pl)
        {
            if (ModelState.IsValid)
            {
                int newId = Rules.Create.Playlist(pl, GetCurrentUserName());
                if (newId == 0)
                {
                    ModelState.AddModelError("", "Name must be Unique! Name and Description are required!");//TODO: check element name
                    return View("Create");
                }
                return RedirectToAction("Details", new { id = newId });
            }
            return View("Create");
        }

        //GET: root/playlist/{id}
        [HttpGet, ActionName("Details")]
        public ActionResult DetailsGet(int id)
        {
            ViewPlaylist playlist = Rules.Find.PlaylistWithReadAccess(id, GetCurrentUserName());
            if (playlist == null)
                return HttpNotFound();
            return View("Details", playlist);
        }

        // GET: root/playlist/list
        [HttpGet, ActionName("List")]
        public ActionResult List()
        {
            String user = GetCurrentUserName();
            ViewPlaylist[] list = Rules.FindAll.Playlists(user);
            ViewBag.User = user;
            return View("List", list);
        }

        //GET: root/playlist/{id}/edit
        [HttpGet, ActionName("Edit")]
        public ActionResult EditGet(int id)
        {
            String user = GetCurrentUserName();
            ViewPlaylist playlist = Rules.Find.PlaylistWithOwnerAccess(id, user);
            if (playlist == null)
                return HttpNotFound();
            return View("Edit", playlist);
        }

        //POST: root/playlist/{id}/edit
        [HttpPost, ActionName("Edit")]
        public ActionResult EditPost(int id)
        {
            if (ModelState.IsValid)
            {
                String user = GetCurrentUserName();
                ViewPlaylist playlist = Rules.Find.PlaylistWithOwnerAccess(id, user);
                if (playlist == null)
                    return HttpNotFound();
                if (TryUpdateModel<ViewPlaylist>(playlist))
                {
                    Rules.Edit.PlaylistTo(playlist, user);
                    return RedirectToAction("Details", new { id = id });
                }
                else
                {
                    return View("Edit");//TODO: suposedly should display edit form with erros.
                }
            }
            else
            {
                return View("Edit");//TODO: suposedly should display edit form with erros.
            }
        }

        //GET: root/playlist/{id}/delete
        [HttpGet, ActionName("Delete")]
        public ActionResult RemoveGet(int id)
        {
            String user = GetCurrentUserName();
            ViewPlaylist playlist = Rules.Find.PlaylistWithOwnerAccess(id, user);
            if (playlist == null)
                return HttpNotFound();
            return View("Delete", playlist);
        }

        //POST: root/playlist/{id}/delete
        [HttpPost, ActionName("Delete")]
        public ActionResult RemovePost(int id)
        {
            String user = GetCurrentUserName();
            if (Rules.Remove.Playlist(id, user))
                return HttpNotFound();
            return RedirectToAction("List");
        }

        //POST: root/playlist/{id}/add/{href}
        [HttpPost, ActionName("Add")]
        public ActionResult Add(int id, string href)
        {
            String user = GetCurrentUserName();
            if (Rules.Edit.AddTrack(id, href, user))
                return RedirectToAction("Details", new { id = id });
            return RedirectToAction("Track", "Search", new { href = href });
        }

        //POST: root/playlist/{id}/remove/{href}
        [HttpPost, ActionName("Remove")]
        public ActionResult RemovePost(int id, string href)
        {
            String user = GetCurrentUserName();
            if (Rules.Edit.RemoveTrack(id, href, user))
                return RedirectToAction("Details", new {id = id});
            return HttpNotFound();//TODO: not exactly the best error, but it should sufice for now
        }

        //GET: root/playlist/Permission/{id}
        [HttpGet, ActionName("Permission")]
        public ActionResult PermissionGet(int id)
        {
            String user = GetCurrentUserName();
            ViewPlaylist playlist = Rules.Find.PlaylistWithOwnerAccess(id, user);
            if (playlist == null)
                return HttpNotFound();
            
            var tmp = from MembershipUser u in Membership.GetAllUsers()
                      select new SelectListItem { Text = u.Comment, Value = u.Comment };
            ViewBag.Users = tmp.Where(u => !u.Text.Equals(user));
            return View(playlist);
        }

        //POST: root/playlist/Permission/{id}&{name}&{write}&{read}
        [HttpPost, ActionName("Permission")]
        public ActionResult PermissionPost(int id, string name, bool writePermission, bool readPermission)
        {
            String user = GetCurrentUserName();
            if (name == null)
                return HttpNotFound();
            if (Rules.Edit.AddUser(id, name, readPermission, writePermission, user))
                return RedirectToAction("Details", new { id = id });
            return HttpNotFound();
        }

        //POST: root/playlist/ChangeTrackNumber/{id}&{href}
        [HttpPost, ActionName("ChangeTrackNumber")]
        public ActionResult ChangeTrackNumberPost(int id, string href, int newTrackNumber)
        {
            String user = GetCurrentUserName();
            if (Rules.Edit.ChangeOrderTo(id, href, newTrackNumber, user))
                return HttpNotFound();
            return RedirectToAction("Details", new { id = id });
        }
        
        private string GetCurrentUserName()
        {
            return Membership.GetUser().UserName;
        }
    }
}