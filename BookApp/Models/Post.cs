namespace BookApp.Models
{
    public class Post
    {
     public int Id { get; set; }
        public string Name { get; set; }
        public string? UserId { get; set; } // Foreign key for User who made the post
        //  public DateTime? DatePosted { get; set;}
        public User? PostedBy { get; set; } // Navigation property for the User who made the post

        // Navigation property to access the related comments
         public IList<Comment>? Comments { get; set; }
    }
}
