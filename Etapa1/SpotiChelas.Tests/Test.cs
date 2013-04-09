using Entities;
using DataAccess;
using System.Collections.Generic;
using NUnit.Framework;
using System;
using System.Data.Entity;
using System.Linq;

namespace SpotiChelas.Tests
{
    public class Test_Repository
    {
        [TestFixture]
        public class testRepo
        {
            [Test]
            public void Test_DataAccessLayer_v1()
            {
                //setUp
                DAL repo = DAL.Factory(new FileLocalRepository());
                Playlist p1 = new Playlist("Playlist 1", "Music 4 ever");
                Playlist p2 = null;

                //test
                repo.put(p1);
                p2 = repo.get<Playlist>(p1.id);

                //Assert
                Assert.AreEqual(p1, p2);
            }
            private class Unknow : Identity{public override bool match(object o){return false;}}

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

            public static void Main()
            {
                new testRepo().Test_DataAccessLayer_v1();
            }
        }
    }
}
