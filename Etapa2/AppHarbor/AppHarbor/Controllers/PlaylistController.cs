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
            return RedirectToAction("List","Playlist");
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
            var playlist = Rules.Create.Playlist(pl, GetCurrentUserName());
            return RedirectToAction("Details", new { id = playlist.Id });
        }

        //GET: root/playlist/{id}
        [HttpGet, ActionName("Details")]
        public ActionResult DetailsGet(int id)
        {
            var playlist = Rules.Find.Playlist(id, GetCurrentUserName());
            if (playlist == null)
                return new HttpStatusCodeResult(404);
            return View("Details",playlist);
        }

        // GET: root/playlist/list
        [HttpGet, ActionName("List")]
        public ActionResult List()
        {
            var list = Rules.FindAll.Playlists(GetCurrentUserName());
            return View("List",list);
        }

        //GET: root/playlist/{id}/edit
        [HttpGet, ActionName("Edit")]
        public ActionResult EditGet(int id)
        {
            var playlist = Rules.Find.Playlist(id, GetCurrentUserName());
            return View("Edit",playlist);
        }
        
        //POST: root/playlist/{id}/edit
        [HttpPost, ActionName("Edit")]
        public ActionResult EditPost(int id)
        {
            var playlist = Rules.Find.Playlist(id, GetCurrentUserName());
            if (TryUpdateModel<ViewPlaylist>(playlist))
            {
                Rules.Edit.PlaylistTo(playlist, GetCurrentUserName());
                return RedirectToAction("Details", new { id = playlist.Id });
            }
            else
                return View("Edit");//TODO: suposedly should display edit form with erros. Test it
        }

        //GET: root/playlist/{id}/delete
        [HttpGet, ActionName("Delete")]
        public ActionResult RemoveGet(int id)
        {
            var playlist = Rules.Find.Playlist(id, GetCurrentUserName());
            return View("Delete",playlist);
        }

        //POST: root/playlist/{id}/delete
        [HttpPost, ActionName("Delete")]
        public ActionResult RemovePost(int id)
        {
            var playlist = Rules.Remove.Playlist(id, GetCurrentUserName());
            if (playlist != null)
                return RedirectToAction("List");
            return View("Delete",playlist);
        }

        //POST: root/playlist/{id}/add/{href}
        [HttpPost, ActionName("Add")]
        public ActionResult Add(int id, string href)
        {
            var playlist = Rules.Find.Playlist(id, GetCurrentUserName());
            var track = Rules.Find.Track(href);
            if (Rules.Edit.AddTrack(playlist, track))
                return RedirectToAction("Details", new { id = playlist.Id });
            return View("Add");//TODO: i didnt come from here
        }

        //GET: root/playlist/{id}/remove/{href}
        [HttpPost, ActionName("Remove")]
        public ActionResult Remove(int id, string href)
        {
            var playlist = Rules.Find.Playlist(id, GetCurrentUserName());
            var track = Rules.Find.Track(href);
            if (Rules.Edit.RemoveTrack(playlist, track))
                return RedirectToAction("Details", new { id = playlist.Id });
            return View("Remove");
        }

        //GET: root/playlist/Permission/{id}
        [HttpGet, ActionName("Permission")]
        public ActionResult PermissionGet(int id)
        {
            ViewBag.Users = from MembershipUser u in Membership.GetAllUsers()
                        select new SelectListItem { Text = u.UserName, Value = u.UserName.ToLower() };
            
            var playlist = Rules.Find.Playlist(id, GetCurrentUserName());
            return View(playlist);
        }

        //POST: root/playlist/Permission/{id}&{name}&{write}&{read}
        [HttpPost, ActionName("Permission")]
        public ActionResult PermissionPost(int id, string name, bool writePermission, bool readPermission)
        {
            var playlist = Rules.Find.Playlist(id, GetCurrentUserName());
            if(Rules.Edit.AddUser(playlist, name, readPermission, writePermission))
                return RedirectToAction("Details", new { id = playlist.Id });
            return View("Permission");
        }
        
        private string GetCurrentUserName()
        {
            return Membership.GetUser().UserName;
        }
    }
}
