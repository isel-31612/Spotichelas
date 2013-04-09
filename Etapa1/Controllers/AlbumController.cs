﻿using System;
using System.Collections.Specialized;
using System.Net;
using System.Net.Http;
using DataAccess;
using BusinessRules;
using Utils;
using Views;
using WebGarten2;
using WebGarten2.Html;

namespace Controllers
{
    public class AlbumController
    {
        private readonly Logic Rules;
        public AlbumController()
        {
            Rules = Logic.Factory(); //TODO: inject Logic or subclass
        }

        [HttpMethod("GET", "/album/{id}")]
        public HttpResponseMessage Get(int id)
        {
            var album = Rules.Find.Album(id);

            return album == null ? new HttpResponseMessage(HttpStatusCode.NotFound) :
                new HttpResponseMessage
                {
                    Content = new AlbumView(album).AsHttpContent("text/html")
                };
        }

        [HttpMethod("POST", "/album")]
        public HttpResponseMessage Post(NameValueCollection content)
        {
            CreateAlbum ca = new CreateAlbum(content["name"],content["year"]);
            var album = Rules.Create.Album(ca);
            if (album == null)
            {
                return new HttpResponseMessage(HttpStatusCode.BadRequest);
            }
            var resp = new HttpResponseMessage(HttpStatusCode.Redirect);
            resp.Headers.Location = new Uri(ResolveUri.For(album));
            return resp;
        }
    }
}
