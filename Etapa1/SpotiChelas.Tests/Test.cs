using Entities;
using DataAccess;
using System.Collections.Generic;
using NUnit.Framework;
using System;
using System.Data.Entity;
using System.Linq;
using System.Data.SqlClient;
using SpotifyBridge;

namespace SpotiChelas.Tests
{
    [TestFixture]
    public class Test_DataAccess
    {
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
        public void Test_Lookup()
        {
            //Setup
            string artistId = "1aJO58SQeP4NlFVMxNpofa";
            string albumID  = "5NH94cATqx5fjBE794xZLy";
            string trackID  = "0qSSbcZ2bvjJuB3Wy5lzK5";
            LookUp snatch = new LookUp();

            //Test
            //Artist ar = snatch.Artist(artistId);
            Album al = snatch.Album(albumID);
            Track t = snatch.Track(trackID);

            //Assert
            //Assert.NotNull(ar);
            //Assert.NotNull(al);
            
        }
    }

    public class Debug
    {
        public static void Main()
        {
            new SpotifyBridge().Test_Lookup();
        }
    }
}
