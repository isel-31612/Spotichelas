using Entities;
using System.Collections.Generic;

namespace SpotifyBridge
{
    public class Searcher
    {
        public Result<Track> Track(Track t) //TODO: Falta fazer
        {
            //extract the request arguments
            //create the request
            //send request
            //receive reply
            //convert to Result
            return null;
        }

        public Result<Album> Album(Album a) //TODO: Falta fazer
        {
            //extract the request arguments
            //create the request
            //send request
            //receive reply
            //convert to Result
            return null;
        }

        public Result<Artist> Artist(Artist a) //TODO: Falta fazer
        {
            //extract the request arguments
            //create the request
            //send request
            //receive reply
            //convert to Result
            return null;
        }

        public class Result<T> where T : Identity
        {
            private List<T> result;

            public Result(List<T> list)
            {
                result = list;
            }
        }
    }
}
