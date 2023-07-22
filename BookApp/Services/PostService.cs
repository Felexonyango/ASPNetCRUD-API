
using BookApp.Context;
using BookApp.DTos;
using BookApp.Models;
using Mapster;
using Microsoft.EntityFrameworkCore;

namespace BookApp.Services
{
    public class PostService
    {
        private ApplicationDbContext _dbContext;
        public PostService(ApplicationDbContext context)
        {
            _dbContext = context;
        
        }
        public async Task<Post> AddPost(Post post, User user)
        {

            post.PostedBy = user;
            await _dbContext.Posts.AddAsync(post);
            await _dbContext.SaveChangesAsync();

            return post;
        }

        public async Task<IEnumerable<Post>> GetAllPosts()
        {
            var posts = await _dbContext.Posts
            .Include(post => post.PostedBy)
            .ToListAsync();

            return posts;
        }


        public async Task<PostDto> GetPostById(int id)
        {
            var post = await _dbContext.Posts
                .Include(post => post.PostedBy)
                .Include(x => x.Comments)
                .FirstOrDefaultAsync(u => u.Id == id);

            var postDto = post.Adapt<PostDto>();

            return postDto;
        }

        public void deletePost(int Id)
        {
            var post = _dbContext.Posts.FirstOrDefault(post => post.Id == Id);

            if (post != null)
            {
                _dbContext.Posts.Remove(post);
                _dbContext.SaveChanges();
            }
        }
        public async Task<Post?> UpdatePost(Post post, int Id)
        {
            var _post = await _dbContext.Posts.FindAsync(Id);
            if (_post != null)
            {
                _post.Name = post.Name;

                _dbContext.Update(_post);
                await _dbContext.SaveChangesAsync();
            }
            return _post;
        }


    }


}