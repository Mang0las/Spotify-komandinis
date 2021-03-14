using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Spotify_komandinis
{
    public class Track
    {
        public string album { get; set; }
        public string artist { get; set; }
        public string id { get; set; }
        public string name { get; set; }
        public int popularity { get; set; }
        public string uri { get; set; }

        public Track(string album, string artist, string id, string name, int popularity, string uri)
        {
            this.album = album;
            this.artist = artist;
            this.id = id;
            this.name = name;
            this.popularity = popularity;
            this.uri = uri;
        }
    }
}
