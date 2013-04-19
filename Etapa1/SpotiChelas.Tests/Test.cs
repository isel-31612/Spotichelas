using Entities;
using DataAccess;
using System.Collections.Generic;
using NUnit.Framework;
using System;
using System.Data.Entity;
using System.Linq;
using System.Data.SqlClient;
using SpotifyBridge;
using BusinessRules;

namespace SpotiChelas.Tests
{
    [TestFixture]
    public class Test_DataAccess
    {
        [Test]
        public void Test_DataAccessLayer_With_Memory_Database_With_Playlist()
        {
            //setUp
            DAL repo = DAL.Factory(new MemoryLocalRepository());
            Playlist p1 = new Playlist("Playlist 1", "Music 4 ever");
            Playlist p2 = null;
            Playlist p3 = new Playlist("Playlist 3", "Music is fun");
            Playlist p4 = null;

            //test
            repo.put(p1);
            p2 = repo.get<Playlist>(p1.id);
            p4 = repo.update(p1.id, p3);
            repo.remove<Playlist>(p4.id);

            //Assert
            Assert.AreEqual(p1, p2);
            Assert.IsNull(repo.get<Playlist>(p4.id));
            Assert.IsNull(repo.get<Playlist>(p1.id));
        }

        [Test]
        public void Test_DataAccessLayer_With_Persistent_Database_With_Playlist()
        {
            //setUp
            DAL repo = DAL.Factory(new FileLocalRepository("Test"));
            Playlist p1 = new Playlist("Playlist 1", "Music 4 ever");
            Playlist p2 = null;
            Playlist p3 = new Playlist("Playlist 3", "Music is fun");
            Playlist p4 = null;
            //test

            repo.put(p1);
            p2 = repo.get<Playlist>(p1.id);
            p4 = repo.update<Playlist>(p1.id, p3);
            repo.remove<Playlist>(p4.id);
            
            //Assert
            Assert.AreEqual(p1, p2);
            Assert.IsNull(repo.get<Playlist>(p4.id));
            Assert.IsNull(repo.get<Playlist>(p3.id));
            Assert.IsNull(repo.get<Playlist>(p1.id));
        }

        private class Unknow : Identity { public override bool match(object o) { return false; } }
        [Test]
        public void test_DataAccessLayer_With_Unknow_Type()
        {
            //setUp
            DAL repo = DAL.Factory(new FileLocalRepository());
            Identity v = new Unknow();
		
            //Assert
            Assert.Throws<InvalidOperationException>(() => { repo.get<Unknow>(0); });
            Assert.Throws<InvalidOperationException>(() => { repo.put(v); });
        }
    }

    [TestFixture]
    public class SpotifyBridge
    {
        [Test]
        public void Test_Lookup_Usage()
        {
            //Setup
            string artistQuery = "30 Seconds to Mars";//"Adema";
            Logic logic = Logic.Factory();
            string artistId = logic.FindAll.Artists(artistQuery).First().Href;

            //Test
            var va = logic.Find.Artist(artistId);
            var vb = logic.Find.Album(va.Albuns.First().Key);
            var vt = logic.Find.Track(vb.Tracks.First().Key);

            //Assert
            Assert.NotNull(vb);
            Assert.NotNull(va);
            Assert.NotNull(vt);
        }
        [Test]
        public void Test_Searcher_Usage()
        {
            //Setup
            Logic logic = Logic.Factory();
            string AlName = "Topple the Giants";
            string ArName = "Adema";
            string TName = "Resolution";
            
            //Test
            var list1 = logic.FindAll.Albuns(AlName);
            var list2 = logic.FindAll.Artists(ArName);
            var list3 = logic.FindAll.Tracks(TName);

            //Assert
            Assert.IsTrue(list1 != null && list1.Count != 0);
            Assert.IsTrue(list2 != null && list2.Count != 0);
            Assert.IsTrue(list3 != null && list3.Count != 0);

        }
    }

    public class Debug
    {
        public static void Main()
        {
            new SpotifyBridge().Test_Lookup_Usage();
        }
    }
}
