using BusinessRules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Utils;
using System.Web.Security;

namespace AppHarbor.Controllers
{
    [Authorize]
    public class SearchController : Controller
    {

        Logic Rules = Logic.Factory();
        public ActionResult Index()
        {
            return RedirectToAction("Query","Search");
        }

        //GET: root/search/Query
        [HttpGet, ActionName("Query")]
        public ActionResult QueryGet()
        {
            return View();
        }

        //GET: root/search/result
        [HttpGet, ActionName("Result")]
        public ActionResult ResultsGet(string query,string mode)
        {
            SearchInfo sf; 
            if (mode.Equals("track"))
                return View("TrackList",Rules.FindAll.Tracks(query, out sf));
            if(mode.Equals("album"))
                return View("AlbumList",Rules.FindAll.Albuns(query, out sf));
            if(mode.Equals("artist"))
                return View("ArtistList",Rules.FindAll.Artists(query, out sf));
            return new HttpStatusCodeResult(404);
        }

        //GET: root/search/track/{href}
        [HttpGet, ActionName("Track")]
        public ActionResult TrackGet(string href)
        {
            String user = GetCurrentUserName();
            ViewPlaylist[] playlists = Rules.FindAll.PlaylistsWithWriteAccess(user);//Brings both owner and permissions with write playlists

            ViewBag.Playlists = from ViewPlaylist p in playlists
                                select new SelectListItem { Text = p.Name, Value = p.Id+"" };
                
            ViewTrack track = Rules.Find.Track(href);
            if (track == null) return new HttpStatusCodeResult(504);
            return View(track);
        }

        //GET: root/search/Album/{href}
        [HttpGet, ActionName("Album")]
        public ActionResult AlbumGet(string href)
        {
            ViewAlbum album = Rules.Find.Album(href);
            if (album == null) return new HttpStatusCodeResult(504);
            return View(album);
        }

        //GET: root/search/Artist
        [HttpGet, ActionName("Artist")]
        public ActionResult ArtistGet(string href)
        {
            ViewArtist artist = Rules.Find.Artist(href);
            if (artist == null)return new HttpStatusCodeResult(504);
            return View(artist);
        }

        private string GetCurrentUserName()
        {
            object nickname = Profile.GetPropertyValue("Nickname");
            return (string)nickname;
        }
    }
}