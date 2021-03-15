using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http.Extensions;


namespace Spotify_komandinis
{
    public class IndexModel : PageModel
    {

        [BindProperty]
        public string parameters { get; set; }

        private readonly ILogger<IndexModel> _logger;

        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }

        
        public IActionResult OnGet()
        {
            var authorisation = new Auth();
            var randomiser = new Random();
            string state = randomiser.Next(100000, 999999).ToString();
            var query = new QueryBuilder();
            query.Add("client_id", authorisation.clientID);
            query.Add("response_type", "token");
            query.Add("redirect_uri", authorisation.redirectURL);
            query.Add("state", state);
            query.Add("scope", "user-top-read");
            query.Add("show_dialog", "true");
            TempData["state"] = state;
            parameters = query.ToQueryString().ToString();

            return Page();
        }

    }
}
