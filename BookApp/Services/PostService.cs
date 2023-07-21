using AutoMapper;
using BookApp.Context;
using BookApp.Models;
using Microsoft.EntityFrameworkCore;

namespace BookApp.Services
{
    public class PostService
    {

        private readonly IMapper _mapper;
        private ApplicationDbContext _dbContext;
        public PostService(ApplicationDbContext context, IMapper mapper)
        {
            _dbContext = context;
            _mapper = mapper;
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


        public async Task<Post> GetPostById(int Id)
        {
            var post = await _dbContext.Posts.FindAsync(Id);


            return post;
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