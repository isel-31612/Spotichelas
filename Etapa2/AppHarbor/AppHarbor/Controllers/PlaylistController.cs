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
                var playlist = Rules.Create.Playlist(pl, GetCurrentUserName());
                return RedirectToAction("Details", new { id = playlist.Id });
            }
            return View("New");
        }

        //GET: root/playlist/{id}
        [HttpGet, ActionName("Details")]
        public ActionResult DetailsGet(int id)
        {
            var playlist = Rules.Find.PlaylistWithReadAccess(id, GetCurrentUserName());
            if (playlist == null)
                return HttpNotFound();
            return View("Details", playlist);
        }

        // GET: root/playlist/list
        [HttpGet, ActionName("List")]
        public ActionResult List()
        {
            String user = GetCurrentUserName();
            var list = Rules.FindAll.Playlists(user);
            ViewBag.User = user;
            return View("List", list);
        }

        //GET: root/playlist/{id}/edit
        [HttpGet, ActionName("Edit")]
        public ActionResult EditGet(int id)
        {
            String user = GetCurrentUserName();
            var playlist = Rules.Find.PlaylistWithWriteAccess(id, user);
            if (playlist == null)
                return HttpNotFound();
            if (!playlist.Owner.Equals(user))
                return new HttpStatusCodeResult(403);
            return View("Edit", playlist);
        }

        //POST: root/playlist/{id}/edit
        [HttpPost, ActionName("Edit")]
        public ActionResult EditPost(int id)
        {
            if (ModelState.IsValid)
            {
                String user = GetCurrentUserName();
                var playlist = Rules.Find.PlaylistWithWriteAccess(id, user);
                if (playlist == null)
                    return HttpNotFound();
                if (!playlist.Owner.Equals(user))
                    return new HttpStatusCodeResult(403);
                if (TryUpdateModel<ViewPlaylist>(playlist))
                {
                    Rules.Edit.PlaylistTo(playlist, user);
                    return RedirectToAction("Details", new { id = playlist.Id });
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
            var playlist = Rules.Find.PlaylistWithOwnerAccess(id, user);
            if (playlist == null)
                return HttpNotFound();
            if (!playlist.Owner.Equals(user))
                return new HttpStatusCodeResult(403);
            return View("Delete", playlist);
        }

        //POST: root/playlist/{id}/delete
        [HttpPost, ActionName("Delete")]
        public ActionResult RemovePost(int id)
        {
            String user = GetCurrentUserName();
            var playlist = Rules.Remove.Playlist(id, user);
            if (playlist == null)
                return HttpNotFound();
            if (!playlist.Owner.Equals(user))
                return new HttpStatusCodeResult(403);
            return RedirectToAction("List");
        }

        //POST: root/playlist/{id}/add/{href}
        [HttpPost, ActionName("Add")]
        public ActionResult Add(int id, string href)
        {
            String user = GetCurrentUserName();
            var playlist = Rules.Find.PlaylistWithWriteAccess(id, user);
            var track = Rules.Find.Track(href);
            if (playlist == null || track == null)
                return HttpNotFound();
            if (Rules.Edit.AddTrack(playlist, track, user))
                return RedirectToAction("Details", new { id = playlist.Id });
            return RedirectToAction("Track", "Search", new { id = playlist.Id });
        }

        //POST: root/playlist/{id}/remove/{href}
        [HttpPost, ActionName("Remove")]
        public ActionResult RemovePost(int id, string href)
        {
            String user = GetCurrentUserName();
            var playlist = Rules.Find.PlaylistWithReadAccess(id, user);
            var track = Rules.Find.Track(href);
            if (playlist == null || track == null)
                return HttpNotFound();
            if (Rules.Edit.RemoveTrack(playlist, href, GetCurrentUserName()))
                return RedirectToAction("Details", new { id = playlist.Id });
            return View("Details", new { id = playlist.Id });
        }

        //GET: root/playlist/Permission/{id}
        [HttpGet, ActionName("Permission")]
        public ActionResult PermissionGet(int id)
        {
            String user = GetCurrentUserName();
            ViewPlaylist playlist = Rules.Find.PlaylistWithOwnerAccess(id, user);
            if (playlist == null)
                return HttpNotFound();
            if (!playlist.Owner.Equals(user))
                return new HttpStatusCodeResult(403);
            
            var tmp = from MembershipUser u in Membership.GetAllUsers()
                      select new SelectListItem { Text = u.Comment, Value = u.Comment };
            ViewBag.Users = tmp.Where(u => !u.Text.Equals(user));
            return View(playlist);
        }

        //POST: root/playlist/Permission/{id}&{name}&{write}&{read}
        [HttpPost, ActionName("Permission")]
        public ActionResult PermissionPost(int id, string name, bool writePermission, bool readPermission)
        {
            if (name == null)
            {
                return HttpNotFound();
            }
            String user = GetCurrentUserName();
            ViewPlaylist playlist = Rules.Find.PlaylistWithOwnerAccess(id, user);
            if (playlist == null)
                return HttpNotFound();
            if (!playlist.Owner.Equals(user))
                return new HttpStatusCodeResult(403);
            if (Rules.Edit.AddUser(playlist, name, readPermission, writePermission, user))
                return RedirectToAction("Details", new { id = playlist.Id });//TODO: why would it fail?
            return RedirectToAction("Permission");
        }

        private string GetCurrentUserName()
        {
            return Membership.GetUser().Comment;
        }
    }
}