using BookApp.Models;

namespace BookApp.DTos
{
    public class CommentDto
       {
        public int Id { get; set; }
        public string Content { get; set; }
        public int UserId { get; set; } // Foreign key for User who made the comment
        public User? CommentedBy { get; set; } // Navigation property for the User who made the comment
        // public DateTime? DateCommented { get; set;}
        // Foreign key for Post
        public int PostId { get; set; }
        public Post? Post { get; set; } // Navigation property to access the related Post
    }
}