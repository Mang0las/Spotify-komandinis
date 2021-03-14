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
            TempData["access_token"] = (string)Request.Form["token"]; //Access token saugomas TempData

            //Redirectinam i overview kur bus visa informacija
            return Redirect("/Overview");
        }
    }
}
