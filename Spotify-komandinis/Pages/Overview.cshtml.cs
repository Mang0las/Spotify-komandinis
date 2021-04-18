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
        public List<Track> trackListShort = new List<Track>();
        public List<Track> trackListMedium = new List<Track>();
        public List<Track> trackListLong = new List<Track>();

        public List<Artist> artistListShort = new List<Artist>();
        public List<Artist> artistListMedium = new List<Artist>();
        public List<Artist> artistListLong = new List<Artist>();


        public List<Track> recommendationList = new List<Track>();

        public async Task<IActionResult> OnGetAsync()
        {  
            string token = (string)TempData["access_token"];

            var mostPlayedTracksShort = await GetMostPlayedtracks(token, TimeRange.ShortTerm);
            trackListShort = PutTracksIntoList(mostPlayedTracksShort);
            var mostPlayedTracksMedium = await GetMostPlayedtracks(token, TimeRange.MediumTerm);
            trackListMedium = PutTracksIntoList(mostPlayedTracksMedium);
            var mostPlayedTracksLong = await GetMostPlayedtracks(token, TimeRange.LongTerm);
            trackListLong = PutTracksIntoList(mostPlayedTracksLong);

            var mostPlayedArtistsShort = await GetMostPlayedArtists(token,TimeRange.ShortTerm);
            artistListShort = PutArtistsIntoList(mostPlayedArtistsShort);
            var mostPlayedArtistsMedium = await GetMostPlayedArtists(token, TimeRange.MediumTerm);
            artistListMedium = PutArtistsIntoList(mostPlayedArtistsMedium);
            var mostPlayedArtistsLong = await GetMostPlayedArtists(token, TimeRange.LongTerm);
            artistListLong = PutArtistsIntoList(mostPlayedArtistsLong);

            var recommendedTracks = await GetTrackRecommendations(token);
                recommendationList = PutRecommendationsIntoList(recommendedTracks);

            Console.WriteLine("test");
            return Page();
        }   

        public async Task<PagedTracks> GetMostPlayedtracks(string access_token, TimeRange timeRange = TimeRange.LongTerm)
        {
            var http = new HttpClient();
            var personal = new PersonalizationApi(http, access_token);
            var tracks = await personal.GetUsersTopTracks(10, timeRange : timeRange);

            return tracks;
        }

        public async Task<PagedArtists> GetMostPlayedArtists(string access_token, TimeRange timeRange=TimeRange.LongTerm)
        {
            var http = new HttpClient();
            var personal = new PersonalizationApi(http, access_token);
            var artists = await personal.GetUsersTopArtists(10, timeRange: timeRange);

            return artists;
        }

        public async Task<RecommendationsResult> GetTrackRecommendations(string access_token)
        {
            var http = new HttpClient();
            var browse = new BrowseApi(http, access_token);
            string id1 = artistListLong[0].id;
            string id2 = artistListLong[1].id;
            RecommendationsResult result = await browse.GetRecommendations(new[] { id1, id2 }, null, null);
            return result;
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

        public List<Track> PutRecommendationsIntoList(RecommendationsResult tracks)
        {
            List<Track> trackList1 = new List<Track>();

            for (int i = 0; i < tracks.Tracks.Length; i++)
            {
                List<Artist> artistList = new List<Artist>();
                string album = tracks.Tracks[i].Album.Name;

                for (int j = 0; j < tracks.Tracks[i].Artists.Length; j++)
                {
                    string artistid = tracks.Tracks[i].Artists[j].Id;
                    string artistname = tracks.Tracks[i].Artists[j].Name;
                    Artist artist = new Artist(artistid, artistname);
                    artistList.Add(artist);
                }

                List<Artist> artists = artistList;
                string id = tracks.Tracks[i].Id;
                string name = tracks.Tracks[i].Name;
                int popularity = tracks.Tracks[i].Popularity;
                string uri = tracks.Tracks[i].Uri;

                Track track = new Track(album, artists, id, name, popularity, uri);
                trackList1.Add(track);
            }

            return trackList1;
        }
    }
}
