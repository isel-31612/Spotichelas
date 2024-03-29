﻿using System;
using System.Collections.Specialized;
using System.Net;
using System.Net.Http;
using DataAccess;
using Utils;
using Views;
using WebGarten2;
using WebGarten2.Html;
using BusinessRules;

namespace Controllers
{
    public class TrackController
    {
        private readonly Logic Rules;
        public TrackController()
        {
            Rules = Logic.Factory(); //TODO: inject Logic or subclass
        }

        [HttpMethod("GET", "/track/new")]
        public HttpResponseMessage New()
        {
            return new HttpResponseMessage
            {
                Content = new PlaylistNewView(null).AsHttpContent("text/html")
            };
        }

        [HttpMethod("GET", "/track/{id}")]
        public HttpResponseMessage Get(string id)
        {
            var t = Rules.Find.Track(id); 

            return t == null ? new HttpResponseMessage(HttpStatusCode.NotFound) :
                new HttpResponseMessage
                {
                   Content = new TrackView(t).AsHttpContent("text/html")
                };
        }

        [HttpMethod("GET", "/track/{href}/add")]
        public HttpResponseMessage GetAdd(string href)
        {
            var playlists = Rules.FindAll.Playlists();
            return new HttpResponseMessage
            {
                Content = new AddTrackView(href, playlists).AsHttpContent("text/html")
            };
        }
    }
}
