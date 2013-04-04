using Entities;
using System;
using Controllers;

namespace SpotiChelas.Tests
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = new WebGarten2.HttpHost("http://localhost:8080");

            host.Add(WebGarten2.DefaultMethodBasedCommandFactory.GetCommandsFor(typeof(HomeController)));
            host.Add(WebGarten2.DefaultMethodBasedCommandFactory.GetCommandsFor(typeof(PlaylistController)));
            host.Open();
            Console.WriteLine("Server is running, press any key to continue...");
            Console.ReadKey();
            host.Close();
        }
    }
}
