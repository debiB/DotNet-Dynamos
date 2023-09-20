using System;
using System.Collections.Generic;
using BlogConsoleApp.Model;

namespace BlogConsoleApp.Model;

public partial class Comment
{
    public int CommentId { get; set; }

    public int PostId { get; set; }

    public string Text { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public virtual Post Post { get; set; } = null!;

    public override string ToString()
    {
        return $"Comment Id: {CommentId}\nPost Id: {PostId}\nText: \n\t{Text}\nDate: {CreatedAt}";
    }
    public string ToStringSmall()
    {
        return $"Comment Id: {CommentId}\n\tText: \n\t\t{Text}";
    }
}

