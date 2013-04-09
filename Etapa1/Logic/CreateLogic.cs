using DataAccess;
using Entities;
using System;

namespace BusinessRules
{
    public class CreateLogic
    {
        private DAL repo;

        public CreateLogic(DAL d)
        {
            repo = d;
        }

        public Playlist Playlist(CreatePlaylist createPlaylist)
        {
            string name = createPlaylist.Name;
            string desc = createPlaylist.Description;
            if (name.Equals("") || desc.Equals(""))
                return null;
            Playlist p = new Playlist(name, desc);
            Playlist[] matchingPlaylists = repo.getAll(p);
            if (matchingPlaylists.Length <= 0)
            {
                int id = repo.put(p);
                p.id = id;
                return p;
            }
            return null;
        }


        public Album Album(CreateAlbum createAlbum)
        {
            string name = createAlbum.Name;
            uint year = UInt32.Parse(createAlbum.Year);
            if (name.Equals("") || year == 0)
                return null;
            Album a = new Album(name, year);
            Album[] matchingAlbuns = repo.getAll(a);
            if (matchingAlbuns.Length <= 0)
            {
                int id = repo.put(a);
                a.id = id;
                return a;
            }
            return null;
        }

        public Artist Artist(CreateArtist createArtist)
        {
            string name = createArtist.Name;
            if (name.Equals(""))
                return null;
            Artist a = new Artist(name);
            Artist[] matchingArtists = repo.getAll(a);
            if (matchingArtists.Length <= 0)
            {
                int id = repo.put(a);
                a.id = id;
                return a;
            }
            return null;
        }

        public Track Track(CreateTrack createTrack)
        {
            string name = createTrack.Name;
            UInt32 duration = UInt32.Parse(createTrack.Duration);
            if (name.Equals("") || duration == 0)
                return null;
            Track t = new Track(name, duration);
            Track[] matchingTracks = repo.getAll(t);
            if (matchingTracks.Length <= 0)
            {
                int id = repo.put(t);
                t.id = id;
                return t;
            }
            return null;
        }
    }
}