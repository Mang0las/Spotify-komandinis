using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Http.Extensions;
using System.Net.Http;
using SpotifyApi.NetCore;

namespace Spotify_komandinis
{
    public class OverviewModel : PageModel
    {

        public async Task<IActionResult> OnGetAsync()
        {
            string token = (string)TempData["access_token"];

            //Console.WriteLine(test.Result.Items.Length);
            var mostPlayedTracks = await GetMostPlayedtracks(token); //sarasas dainu kurias displayint
            Console.WriteLine("test");
            return Page();
        }

        public async Task<PagedTracks> GetMostPlayedtracks(string access_token)
        {
            var http = new HttpClient();
            var personal = new PersonalizationApi(http, access_token);
            var tracks = await personal.GetUsersTopTracks();

            //var tracks = new List<Track>();

            return tracks;
        }

       
    }
}
