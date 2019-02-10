using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json.Linq;

namespace MovieSearch.Pages
{
    public class ResultModel : PageModel
    {
        public string JSONresponse { get; set; }
        public string Message { get; set; }
        public string Title;
        public string Rating;
        public string posterURL;
        public string Language;
        public string Overview;
        public string releaseDate;

        public void OnGet()
        {

            string userEntry = Request.QueryString.Value;
            userEntry = userEntry.Split("=")[1];

            if (userEntry == null) {
                Response.Redirect("Search");
            }
            else {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create("https://api.themoviedb.org/3/search/movie?api_key=a3bdaae66f8cf705750820e17c0e9471&query=" + userEntry);
                try
                {
                    WebResponse response = request.GetResponse();
                    using (Stream responseStream = response.GetResponseStream())
                    {
                        StreamReader reader = new StreamReader(responseStream, System.Text.Encoding.UTF8);
                        JSONresponse = reader.ReadToEnd();

                        dynamic parsing = JObject.Parse(JSONresponse);
                        try {

                            Title = parsing.results[0].original_title;
                            Rating = parsing.results[0].vote_average;
                            posterURL = "https://image.tmdb.org/t/p/w300/" + parsing.results[0].poster_path;
                            Language = parsing.results[0].original_language;
                            Overview = parsing.results[0].overview;
                            releaseDate = parsing.results[0].release_date;
                        }
                        catch (Exception e)
                        {
                            Message = "Sorry no movie with this name found. Please try again.";
                        }
                    }
                }
                catch (WebException ex)
                {
                    Message = "Something went wrong. Please try again.";
                }
            }
        }
    }
}
