using System;
using System.Text.Json.Serialization;
using Microsoft.Extensions.Hosting;

namespace BlogWebApi.Models
{
	public class Comment
	{
        public int CommentId { get; set; }

        public int PostId { get; set; }

        public string Text { get; set; } = null!;

        public DateTime CreatedAt { get; set; }
        [JsonIgnore]
        public virtual Post Post { get; set; } = null!;
    }
}


