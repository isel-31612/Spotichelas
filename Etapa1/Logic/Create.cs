using DataAccess;
using Entities;

namespace Logic
{
    public class Create
    {
        private static DAL repo = DAL.Factory();

        public static void Playlist(string name,string desc)
        {
            Playlist p = new Playlist(name, desc);
            Playlist[] matchingPlaylists = repo.getAll(p);  //find all with same name and desc
            if (matchingPlaylists.Length<=0)                //if no match is found...
                repo.put(p);                                //add
        }


        public static void Album(string name, uint year)
        {
            Album a = new Album(name, year);
            //search if exists a deleted
            //Add a to repository
        }

        public static void Artist(string name)
        {
            Artist a = new Artist(name);
            //search if exists a deleted
            //add a to repository
        }

        public static void Track(string name,uint duration)
        {
            Track t = new Track(name, duration);
            //search if exists a deleted
            //add t to repository
        }
    }
}