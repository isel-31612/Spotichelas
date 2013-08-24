using System;
using System.Collections.Generic;
using System.Linq;

using DataAccess;
using Entities;
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

        public int Playlist(CreatePlaylist createPlaylist, string user)
        {
            string name = createPlaylist.Name;
            string desc = createPlaylist.Description;
            if (name.Equals("") || desc.Equals(""))
                return 0;
            Playlist p = new Playlist(name, desc, user);
            if (!repo.getAll().Any(existingPlaylist => existingPlaylist.Name.Equals(name)))
            {
                int id = repo.put(p);
                p.id = id;
                return id;
            }
            return 0;
        }
    }
}