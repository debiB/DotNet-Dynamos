using System;


using System.Collections.Generic;

namespace BlogConsoleApp.Model;

public partial class Post
{
    public int PostId { get; set; }

    public string Title { get; set; } = null!;

    public string Content { get; set; } = null!;
    public DateTime CreatedAt { get; set; }

    public virtual ICollection<Comment> Comments { get; set; } = new List<Comment>();

    public override string ToString()
    {
        return $"Post Id: {PostId}\nPost Title: {Title}\nPost Content: \n\t{Content}\nDate: {CreatedAt}";
    }
    public string ToStringSmall()
    {
        return $"Post Title: {Title}\nPost Content: \n\t{Content}";
    }
}