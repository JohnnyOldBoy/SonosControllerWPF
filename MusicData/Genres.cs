using System;
using System.Collections.Generic;

namespace MusicData
{
    [Serializable()]
    public class Genres
    {
        private List<string> genreList = new List<string>();
        public List<string> GenreList 
        { 
            get => genreList; 
            set => genreList = value; 
        }
    }
}
