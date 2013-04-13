using DataAccess;
using Entities;
using System;
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

        public ViewPlaylist Playlist(CreatePlaylist createPlaylist)
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
                ViewPlaylist ret = new ViewPlaylist(p.id,p.Name,p.Description,null);
                foreach (var track in p.Tracks)
                    ret.Tracks.Add(track.Name);
                return ret;
            }
            return null;
        }


        public ViewAlbum Album(CreateAlbum createAlbum)
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
                ViewAlbum ret = new ViewAlbum(a.id,a.Name,(int)a.Year,a.Artist.Name,null);
                foreach (var track in a.Tracks)
                    ret.Tracks.Add(track.Name);
                return ret;
            }
            return null;
        }

        public ViewArtist Artist(CreateArtist createArtist)
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
                ViewArtist ret = new ViewArtist(a.id,a.Name,null);
                foreach (var album in a.Albuns)
                    ret.Albuns.Add(album.Name);
                return ret;
            }
            return null;
        }

        public ViewTrack Track(CreateTrack createTrack)
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
                ViewTrack ret = new ViewTrack(t.id,t.Name,(int)t.Duration,t.Artist.Name,t.Album.Name);
                return ret;
            }
            return null;
        }
    }
}