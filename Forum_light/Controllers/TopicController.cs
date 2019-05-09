using Forum_light.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Forum_light.Controllers
{
    public class TopicController : Controller
    {
        ForumDB db = new ForumDB();
        HttpCookie Cookie = new HttpCookie("UserId", "1");
        // GET: Topic
        public ActionResult Index()
        {
            var topics = db.Topics.ToList<Topic>();
            return View(topics);
        }
        public ActionResult ShowPosts(int id)
        {
            ViewBag.TopicName = db.Topics.Find(id).TopicName;
            var posts = (from post in db.Posts
                         where post.TopicId == id
                         select post).ToList<Post>();
            return View(posts);
        }
    }
}