using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Forum_light.Models
{
    public class Comment
    {
        public int ID { get; set; }
        [DisplayName("Comment")]
        [DataType(DataType.MultilineText)]
        public string Content { get; set; }
        public int PostId { get; set; }
    }
}