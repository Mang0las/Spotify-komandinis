using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Spotify_komandinis
{
    public class CallbackModel : PageModel
    {

        [BindProperty]
        public string token { get; set; }

        public IActionResult OnPost(string token)
        {
            //TempData["access_token"]
            TempData["access_token"] = (string)Request.Form["token"];
            TempData["access_token"] = (string)Request.Form["token"];

            //var thevalue = $('#token').val();

            //TODO: Figure out how to put the access token into TempData, it looks like it tries to
            //load the access token before the response, so it's just 
            return Page();
        }
    }
}
