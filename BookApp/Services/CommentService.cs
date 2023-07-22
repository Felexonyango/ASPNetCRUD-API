using BookApp.Context;
using BookApp.Models;
using Microsoft.EntityFrameworkCore;

namespace BookApp.Services
{
    public class CommentService
    {
    

        private ApplicationDbContext _dbContext;
        public CommentService(ApplicationDbContext context)
        {
            _dbContext = context;
        
        }
    public async Task<Comment> AddComment(int postId, Comment comment, User user)
{
    var post = await _dbContext.Posts.FindAsync(postId);

    if (post == null) return null;
    comment.PostId = postId;
    comment.CommentedBy = user;
    await _dbContext.Comments.AddAsync(comment);
    await _dbContext.SaveChangesAsync();

    return comment;
}
  public async Task<IEnumerable<Comment>> GetAllComments() {
    
            var comment = await  _dbContext.Comments
            .Include(x=>x.CommentedBy)
            .ToListAsync();

            return comment;

     }


        public async Task<Comment> GetCommentById(int Id)
        {
            var comment = await _dbContext.Comments.FindAsync(Id);
            
            return comment;
        }



        public void deleteComment(int Id)
        {
            var comment = _dbContext.Comments.FirstOrDefault(comment => comment.Id == Id);

            if (comment != null)
            {
                _dbContext.Comments.Remove(comment);
                _dbContext.SaveChanges();
            }
        }
        public async Task<Comment?> UpdateComment(Comment comment, int Id)
        {
            var commentId = await _dbContext.Comments.FindAsync(Id);
            if (commentId != null)
            {
                commentId.Content = comment.Content;

                _dbContext.Update(commentId);
                await _dbContext.SaveChangesAsync();
            }
            return commentId;
        }


    }


}