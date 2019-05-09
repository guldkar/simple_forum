using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace Forum_light.Models
{
    public class Post
    {
        public int ID { get; set; }
        public int UserId { get; set; }
        public int TopicId { get; set; }

        public Topic Topic { get; set; }
        public string UserName { get; set; }

        [DisplayName("Subject")]
        [Required(ErrorMessage = "You need a subject")]
        public string Subject { get; set; }
        [DataType(DataType.MultilineText)]
        [Required(ErrorMessage = "Didn't you forget to write your post?")]
        public string Content { get; set; }

        public int CommentId { get; set; }
        public List<Comment> Comments { get; set; }

    }

    
}