using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Google.Apis.Services;
using Google.Apis.YouTube.v3;
using Google.Apis.YouTube.v3.Data;
namespace Rockola.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public ActionResult SearchList(string keywork)
        {
            var youtubeService = new YouTubeService(new BaseClientService.Initializer()
            {
                ApiKey = "AIzaSyC265U8n9gvpl4bFiW0Kl_eyMMSTe-7QPQ",
                ApplicationName = this.GetType().ToString()
            });
            var searchListRequest = youtubeService.Search.List("snippet");
            searchListRequest.Q = keywork; // Replace with your search term.
            searchListRequest.MaxResults = 10;
            var searchListResponse =  searchListRequest.Execute();
            return PartialView("Search", searchListResponse.Items);
        }
        public void Declare()
        {
            List<string> PLAYLISTVIDEOS = new List<string>();
            if (Session["Playlist"] == null)
            {
                Session["Playlist"] = PLAYLISTVIDEOS;

            }
        }
        public ActionResult AddVideoPlaylist(string Video)
        {
            Declare();

            List<string> auxList = (List<string>)Session["Playlist"];
            auxList.Add(Video);
            Session["Playlist"] = auxList;
            return PartialView("AddVideoPlaylist", auxList);
        }


        [HttpGet]
        public ActionResult VideoList(string id)
        {
            return PartialView("SearchVideo", id);
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