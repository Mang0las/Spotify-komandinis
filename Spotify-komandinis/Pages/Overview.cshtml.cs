using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Http.Extensions;

namespace Spotify_komandinis
{
    public class OverviewModel : PageModel
    {

        public TokenResponse GetToken() { return new TokenResponse(); }
        public IActionResult OnGet()
        {
           
            return Page();
        }
    }
}
