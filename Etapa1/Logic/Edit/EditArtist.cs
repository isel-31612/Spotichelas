﻿namespace BusinessRules
{
    public class EditArtist
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public EditArtist(int id, string name)
        {
            Id = id;
            Name = name;
        }
    }
}
