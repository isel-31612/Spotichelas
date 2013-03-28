using SpotiChelas.DomainModel.Data;
using SpotiChelas.DomainModel.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace SpotiChelasTests
{
    public class MemoryRepository
    {
        public void test_basic_fuctions()
        {
            Track t = new Track("Musica1",100);
            Album al = new Album("Album1",2013);
            Artist ar = new Artist("Artista1");
            Playlist p = new Playlist("Playlist1","Porreiras");
            Repository r = new MemoryLocalRepository();
            int tidx=r.setT(t);
            int alidx = r.setT(al);
            int aridx = r.setT(ar);
            int pidx = r.setT(p);
            Assert.Equal(t, r.getT<Track>(tidx));
            Assert.Equal(al, r.getT<Album>(alidx));
            Assert.Equal(ar, r.getT<Artist>(aridx));
            Assert.Equal(p, r.getT<Playlist>(pidx));
        }

    }
}
