using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Forum_light.Models
{
    public class ForumDdInitializer : DropCreateDatabaseAlways<ForumDB>
    {
        protected override void Seed(ForumDB context)
        {
            var topic1 = new Topic { TopicName = "The first topic" };
            var topic2 = new Topic { TopicName = "The second topic" };

            context.Posts.Add(new Post
            {
                Content = "strong content",
                Subject = "Strong Subject",
                Topic = topic1,
                UserName = "Tina" 
            });
            context.Posts.Add(new Post
            {
                Content = "Somewhat weaker content",
                Subject = "Weak subject",
                Topic = topic2,
                UserName ="Henrik" 
            });
            context.Posts.Add(new Post
            {
                Content = "look at my car, my car is amazing",
                Subject = "Fast car",
                Topic = topic1,
                UserName = "Torben" 
            });
            context.Posts.Add(new Post
            {
                Content = "slow car",
                Subject = "This car is reasonably priced",
                Topic = topic2,
                UserName = "Kristoffer" 
            });
            context.SaveChanges();
            base.Seed(context);
        }
    }
}