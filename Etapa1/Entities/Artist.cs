using System;
using System.Collections.Generic;

namespace Entities
{
    public class Artist : Identity
    {
        public String Name { get; set; }
        //TODO: public List<Album> Albuns { get; set; }

        public Artist(String name)
        {
            Name = name;
            //Albuns = new List<Album>();
        }

        public override bool match(object o)
        {
            Artist ar = o as Artist;
            if (ar == null)
                throw new InvalidCastException();
            return ((ar.Name == null)    || ar.Name.Equals(Name))/* &&
                    ((ar.Albuns.Count==0)|| (ar.Albuns.Equals(Albuns)))*/;
        }
    }
}