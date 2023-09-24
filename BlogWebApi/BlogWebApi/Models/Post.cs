using System;
using System.Text.Json.Serialization;

namespace BlogWebApi.Models
{
	public class Post
	{
        public int PostId { get; set; }

        public string Title { get; set; } = null!;

        public string Content { get; set; } = null!;
        public DateTime CreatedAt { get; set; }
        [JsonIgnore]
        public virtual ICollection<Comment> Comments { get; set; } = new List<Comment>();

   
    }
}

