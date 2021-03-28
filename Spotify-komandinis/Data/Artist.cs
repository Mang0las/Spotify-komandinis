namespace Spotify_komandinis
{
    public class Artist
    {
        public string id { get; set; }
        public string name { get; set; }

        public Artist(string id, string name)
        {
            this.id = id;
            this.name = name;
        }
    }
}
