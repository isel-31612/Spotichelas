using System;
using System.Collections.Generic;

namespace Entities
{
    public class Album : Identity
    {
        public string Name { get; set; }        
        public uint Year { get; set; }
        //TODO: public List<Track> Tracks  { get; set; }

        public Album(string name, uint year)
        {
            Name = name;
            Year = year;
            //Tracks = new List<Track>();
        }

        public override bool match(Object o)
        {
            Album al = o as Album;
            if (al == null)
                throw new InvalidCastException();
            return (al.Name==null)      || al.Name.Equals(Name) &&
                   (al.Year==0)      || al.Year.Equals(Year)/*  &&
                   ((al.Tracks.Count==0)||(al.Tracks.Equals(Tracks)))*/;
        }
    }
}
