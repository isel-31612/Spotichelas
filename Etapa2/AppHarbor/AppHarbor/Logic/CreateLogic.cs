using DataAccess;
using Entities;
using System;
using System.Collections.Generic;
using Utils;

namespace BusinessRules
{
    public class CreateLogic
    {
        private DAL repo;

        public CreateLogic(DAL d)
        {
            repo = d;
        }

        public ViewPlaylist Playlist(CreatePlaylist createPlaylist, string user)
        {
            string name = createPlaylist.Name;
            string desc = createPlaylist.Description;
            if (name.Equals("") || desc.Equals(""))
                return null;
            Playlist p = new Playlist(name, desc, user);
            Playlist[] matchingPlaylists = repo.getAll(p);
            if (matchingPlaylists.Length <= 0)
            {
                int id = repo.put(p);
                p.id = id;
                ViewPlaylist ret = new ViewPlaylist(p.id,p.Name,p.Description,p.Owner,p.Tracks);
                return ret;
            }
            return null;
        }
    }
}