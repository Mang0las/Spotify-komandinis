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
        public IActionResult OnGet(string code, string state)
        {
            TempData["access_token"] = (string)HttpContext.Request.Query["access_token"];
            //TODO: Figure out how to put the access token into TempData, it looks like it tries to
            //load the access token before the response, so it's just null
            return Page();
        }
    }
}
