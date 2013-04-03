﻿using SpotiChelas.Views;
using System.Net.Http;
using WebGarten2;
using WebGarten2.Html;


namespace SpotiChelas.Controller
{
    class HomeController
    {
        //Apenas para testar as views, podem alterar a vontade
        [HttpMethod("GET", "/")]
        public HttpResponseMessage Get()
        {
            return new HttpResponseMessage
            {
                Content = new HomeView().AsHttpContent("text/html")
            };
        }
    }
}
