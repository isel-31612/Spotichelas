using DataAccess;
using Entities;

namespace Logic
{
    public class Edit
    {
        DAL repo;
        public Edit()
        {
            repo = DAL.Factory();
        }
        public void PlaylistTo(int id,Playlist p)
        {
            Playlist edit;
            if (repo.get(id, out edit) != null)
            {
                if (p.Name != null)
                    edit.Name = p.Name;
                if (p.Description != null)
                    edit.Description = p.Description;
                if (p.Tracks.Count <= 0)
                    edit.Tracks = p.Tracks;
                repo.update(id,edit); // id == edit.id
            }
        }

        public void Album(Album a)
        {
        }

        public void Artist(Artist a)
        {
        }

        public void Track(Track t)
        {
        }
    }
}