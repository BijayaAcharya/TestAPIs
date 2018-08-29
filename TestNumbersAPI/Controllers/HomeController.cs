using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json.Linq;

namespace TestNumbersAPI.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {

            HttpWebRequest apiRequest =
                WebRequest.CreateHttp("https://numbersapi.p.mashape.com/42/trivia?fragment=true&json=true "); // url


            apiRequest.Headers.Add("X-Mashape-Key", ConfigurationManager.AppSettings["X-Mashape-Key"]); // adding keys getting from web.config file to hide the key

            apiRequest.UserAgent = "Mozilla/5.0 (Windows NT 6.1; Win64; x64; rv:47.0) Gecko/20100101 Firefox/47.0";

            HttpWebResponse apiResponse = (HttpWebResponse) apiRequest.GetResponse(); // represents the response we get from API
            if(apiResponse.StatusCode == HttpStatusCode.OK) // if we got a status == 200
            {

                // get the data and then parse it
                StreamReader responsedata = new StreamReader (apiResponse.GetResponseStream());

                string trivia = responsedata.ReadToEnd(); // reads the data from the response
                //TODo: parse the Json data
                JObject jsonTrivia = JObject.Parse(trivia);
                ViewBag.trivia = jsonTrivia["text"]; // I want only text
                ViewBag.triviaDate = jsonTrivia["date"]; // to get the date

                ViewBag.trivia = trivia;

            }

            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}