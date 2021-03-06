﻿using System.Collections.Generic;

namespace Spotify_komandinis
{
    public class Track
    {
        public string album { get; set; }
        public List<Artist> artist { get; set; }
        public string id { get; set; }
        public string name { get; set; }
        public int popularity { get; set; }
        public string uri { get; set; }

        public Track(string album, List<Artist> artist, string id, string name, int popularity, string uri)
        {
            this.album = album;
            this.artist = artist;
            this.id = id;
            this.name = name;
            this.popularity = popularity;
            this.uri = uri;
        }

        public string ReturnArtist(int index, List<Track> trackList)
        {
            string line = "";
            for (int i = 0; i < trackList[index].artist.Count; i++)
            {
                if(i + 1 == trackList[index].artist.Count)
                    line += artist[i].name;
                else
                    line += artist[i].name + ", ";
            }
            return line;
        }
    }
}
