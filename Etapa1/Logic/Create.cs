using DataAccess;
using Entities;

namespace Logic
{
    public class Create
    {
        private DAL repo;
        public Create()
        {
            repo = DAL.Factory();
        }        

        public void Playlist(string name,string desc)
        {
            Playlist p = new Playlist(name, desc);
            Playlist[] matchingPlaylists = repo.getAll(p);  //find all with same name and desc
            if (matchingPlaylists.Length <= 0)              //if no match is found...
            {              
                int id = repo.put(p);                       //add
                p.id = id;                                  //give it an ID
            }
        }


        public void Album(string name, uint year)
        {
            Album a = new Album(name, year);
            Album[] matchingAlbuns = repo.getAll(a);
            if (matchingAlbuns.Length <= 0)
            {
                int id = repo.put(a);
                a.id = id;
            }
        }

        public void Artist(string name)
        {
            Artist a = new Artist(name);
            Artist[] matchingArtists = repo.getAll(a);
            if (matchingArtists.Length <= 0)
            {
                int id = repo.put(a);
                a.id = id;
            }
        }

        public void Track(string name,uint duration)
        {
            Track t = new Track(name, duration);
            Track[] matchingTracks = repo.getAll(t);
            if (matchingTracks.Length <= 0)
            {
                int id = repo.put(t);
                t.id = id;
            }
        }
    }
}