using Forum_light.Models;
//using Microsoft.AspNet.Identity;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using static System.Collections.Specialized.BitVector32;

namespace Forum_light.Controllers
{
    public class PostController : Controller
    {
        ForumDB db = new ForumDB();
        HttpCookie Cookie = new HttpCookie("UserId", "1");

        // GET: Post
        public ActionResult Index()
        {
            Session["UserId"] = 1;
            var posts = db.Posts.ToList<Post>();
            return View(posts);
        }

        public ActionResult ShowPost(int id)
        {
        #region adding test-comments
            db.Comments.Add(new Comment
            {
                PostId = 1,
                Content = "this is an anonymous comment"
            });
            db.Comments.Add(new Comment
            {
                PostId = 1,
                Content = "this is another anonymous comment"
            });
            db.Comments.Add(new Comment
            {
                PostId = 2,
                Content = "this is an anonymous comment for another post"
            });
            db.SaveChanges();
            #endregion
            var selectedPost = (from post in db.Posts
                                where post.ID == id
                                select post).FirstOrDefault();
            if (selectedPost != null)
                return View(selectedPost);

            return RedirectToAction("Index");
        }
        
        // GET: create post
        [Authorize]
        public ActionResult Create(int topicId)
        {
            TempData["topicId"] = topicId;
            Post newPost = new Post();
            return View(newPost);
        }
        [Authorize]
        [HttpPost]
        public ActionResult Create(Post post)
        {
            int topicId = (int) TempData["topicId"];
            post.TopicId = topicId;
            post.UserName = User.Identity.Name;
            if (ModelState.IsValid)
            {
                db.Posts.Add(post);
                db.SaveChanges();
                return RedirectToAction("topic","ShowPosts",new { TopicId = topicId });
            }
            else
            {
                return View("Create", post);
            }
        }

        public ActionResult Comments(int id)
        {
            var comments = from comment in db.Comments
                           where comment.PostId == id
                           select comment;
            return PartialView("_comments", comments.ToList<Comment>());
        }
        [Authorize]
        public ActionResult AddComment(int postId)
        {
            Comment newComment = new Comment();
            ViewBag.PostSubject = db.Posts.Find(postId).Subject;
            TempData["PostId"] = postId;
            return View(newComment);
        }
        [Authorize]
        [HttpPost]
        public ActionResult AddComment(Comment comment)
        {
            if (ModelState.IsValid)
            {
                comment.PostId = (int) TempData["PostId"];
                db.Comments.Add(comment);
                db.SaveChanges();
            }
            else
            {
                int postId = (int)TempData["PostId"];
                return RedirectToAction("ShowPost",new { id = postId });
            }

            return RedirectToAction("ShowPost", new { id = comment.PostId });
        }
    }
}