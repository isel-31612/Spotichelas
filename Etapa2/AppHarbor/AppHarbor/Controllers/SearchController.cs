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
        [HttpGet, ActionName("Track"),Authorize]
        public ActionResult TrackGet(string href)
        {
            ViewBag.Playlists = from ViewPlaylist p in Rules.FindAll.Playlists(Membership.GetUser().UserName)
                                select new SelectListItem { Text = p.Name, Value = p.Id+"" };
                
            var track = Rules.Find.Track(href);
            return View(track);
        }

        //GET: root/search/Album/{href}
        [HttpGet, ActionName("Album")]
        public ActionResult AlbumGet(string href)
        {
            var album = Rules.Find.Album(href);
            return View(album);
        }

        //GET: root/search/Artist
        [HttpGet, ActionName("Artist")]
        public ActionResult ArtistGet(string href)
        {
            var artist = Rules.Find.Artist(href);
            return View(artist);
        }
    }
}
