using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace MovieSearch.Pages
{
    public class SearchModel : PageModel
    {
        public string Message { get; set; }

        public void OnGet()
        {
            Message = "ENTER A MOVIE NAME";
        }
    }
}
