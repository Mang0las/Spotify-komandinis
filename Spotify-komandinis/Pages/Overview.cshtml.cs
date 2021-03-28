using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SpotifyApi.NetCore;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace Spotify_komandinis
{
    public class OverviewModel : PageModel
    {
        public List<Track> trackList = new List<Track>();
        public List<Artist> artistList = new List<Artist>();

        public async Task<IActionResult> OnGetAsync()
        {  
            string token = (string)TempData["access_token"];
            //Console.WriteLine(test.Result.Items.Length);
            var mostPlayedTracks = await GetMostPlayedtracks(token); //sarasas dainu kurias displayint
            trackList = PutTracksIntoList(mostPlayedTracks);
            var mostPlayedArtists = await GetMostPlayedArtists(token);
            artistList = PutArtistsIntoList(mostPlayedArtists);
            Console.WriteLine("test");
            return Page();
        }

        //public async Task<IActionResult> OnPostAsync()
        //{
            
        //    return Page();
        //}

        public async Task<PagedTracks> GetMostPlayedtracks(string access_token)
        {
            var http = new HttpClient();
            var personal = new PersonalizationApi(http, access_token);
            var tracks = await personal.GetUsersTopTracks(10, timeRange: TimeRange.LongTerm);

            return tracks;
        }

        public async Task<PagedArtists> GetMostPlayedArtists(string access_token)
        {
            var http = new HttpClient();
            var personal = new PersonalizationApi(http, access_token);
            var artists = await personal.GetUsersTopArtists(10, timeRange: TimeRange.LongTerm);

            return artists;
        }

        public List<Track> PutTracksIntoList(PagedTracks tracks)
        {
            List<Track> trackList = new List<Track>();
            //List<Artist> artistList = new List<Artist>();

            for (int i = 0; i < tracks.Items.Length; i++)
            {
                string album = tracks.Items[i].Album.Name;

                //artistList.Clear();

                //for (int j = 0; j < tracks.Items[i].Artists.Length; j++)
                //{
                //    string artistid = tracks.Items[i].Artists[j].Id;
                //    string artistname = tracks.Items[i].Artists[j].Name;
                //    Artist artist = new Artist(artistid, artistname);
                //    artistList.Add(artist);
                //}


                //List<Artist> artists = artistList;
                string artist = tracks.Items[i].Artists[0].Name;
                string id = tracks.Items[i].Id;
                string name = tracks.Items[i].Name;
                int popularity = tracks.Items[i].Popularity;
                string uri = tracks.Items[i].Uri;

                Track track = new Track(album, artist, id, name, popularity, uri);
                trackList.Add(track);
            }

            return trackList;
        }

        public List<Artist> PutArtistsIntoList(PagedArtists artists)
        {
            List<Artist> artistList = new List<Artist>();
            for (int i = 0; i < artists.Items.Length; i++)
            {
                string id = artists.Items[i].Id;
                string name = artists.Items[i].Name;
                Artist artist = new Artist(id, name);
                artistList.Add(artist);
            }
            return artistList;
        }

    }
}
