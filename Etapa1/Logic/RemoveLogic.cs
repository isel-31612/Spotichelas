using DataAccess;
using Entities;

namespace BusinessRules
{
    public class RemoveLogic
    {
        private DAL repo;
        public RemoveLogic(DAL d)
        {
            repo = d;
        }

        public Playlist Playlist(int id)
        {
            //TODO: verificaçoes
            return repo.remove<Playlist>(id);
        }
    }
}
