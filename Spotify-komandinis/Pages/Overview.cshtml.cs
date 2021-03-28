using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using SpotifyApi.NetCore;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace Spotify_komandinis
{
    [BindProperties]
    public class OverviewModel : PageModel
    {
        public List<Track> trackList = new List<Track>();
        public List<Artist> artistList = new List<Artist>();
        public string accessToken;
        public bool generateTable;
        [BindProperty(SupportsGet = true)]
        public string? range { get; set; }
        [BindProperty(SupportsGet = true)]
        public string? displaySelection { get; set; }
        public List<SelectListItem> rangeSelections { get; set; }
        public List<SelectListItem> displaySelections { get; set; }


        public IActionResult OnGet()
        {

            PopulateSelectionLists();

            generateTable = false;
            string token = (string)TempData["access_token"];
            this.HttpContext.Session.SetString("access_token", token);
            //var mostPlayedTracks = await GetMostPlayedtracks(token); //sarasas dainu kurias displayint
            //trackList = PutTracksIntoList(mostPlayedTracks);
            //var mostPlayedArtists = await GetMostPlayedArtists(token);
            //artistList = PutArtistsIntoList(mostPlayedArtists);
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            PopulateSelectionLists();

            var timeRangeSelection = Request.Form["menuRange"];
            string token = this.HttpContext.Session.GetString("access_token");
            var mostPlayedTracks = await GetMostPlayedtracks(token); //sarasas dainu kurias displayint
            trackList = PutTracksIntoList(mostPlayedTracks);
            var mostPlayedArtists = await GetMostPlayedArtists(token);
            artistList = PutArtistsIntoList(mostPlayedArtists);
                var selectiontest = Request.Form["displaySelection"];
            generateTable = true;
            return Page();

        }

        public void PopulateSelectionLists()
        {
            rangeSelections = new List<SelectListItem>
            {
                new SelectListItem {Value = "1", Text = "4 weeks"},
                new SelectListItem {Value = "2", Text = "6 months"},
                new SelectListItem {Value = "3", Text = "All time"}
            };
            displaySelections = new List<SelectListItem>
            {
                new SelectListItem {Value = "1", Text = "Artists"},
                new SelectListItem {Value = "2", Text = "Tracks"},
                new SelectListItem {Value = "3", Text = "Recommended"}
            };
        }

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
            

            for (int i = 0; i < tracks.Items.Length; i++)
            {
                List<Artist> artistList = new List<Artist>();
                string album = tracks.Items[i].Album.Name;

                for (int j = 0; j < tracks.Items[i].Artists.Length; j++)
                {
                    string artistid = tracks.Items[i].Artists[j].Id;
                    string artistname = tracks.Items[i].Artists[j].Name;
                    Artist artist = new Artist(artistid, artistname);
                    artistList.Add(artist);
                }

                List<Artist> artists = artistList;
                string id = tracks.Items[i].Id;
                string name = tracks.Items[i].Name;
                int popularity = tracks.Items[i].Popularity;
                string uri = tracks.Items[i].Uri;

                Track track = new Track(album, artists, id, name, popularity, uri);
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
