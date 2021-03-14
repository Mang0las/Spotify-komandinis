using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Spotify_komandinis
{
    public class Track
    {
        public string album { get; set; }
        public Artist[] artists { get; set; }
        public string id { get; set; }
        public string name { get; set; }
        public int popularity { get; set; }
        public string uri { get; set; }
        
    }
}
