using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PostCommentApi.Data;
using PostCommentApi.DTOs.PostDTO;
using PostCommentApi.Interface;
using PostCommentApi.Models;

namespace PostCommentApi.Repository
{
    public class PostRepository : IPostRepository
    {
        private readonly ApplicationDbContext _context;
        public PostRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task AddPostAsync(Post post)
        {

            _context.Posts.Add(post);
            await _context.SaveChangesAsync();

        }

        public async Task DeletePostAsync(Post post)
        {
            _context.Posts.Remove(post);
            await _context.SaveChangesAsync();
        }

        public async Task<Post> GetPostByIdAsync(int id)
        {
            return await _context.Posts.Include(x => x.Comments)
                         .FirstOrDefaultAsync(x => x.Id == id);

        }

        public async Task<IEnumerable<Post>> GetPostsAsync()
        {
            return await _context.Posts.Include(x => x.Comments).ToListAsync();
        }

        public async Task UpdatePostAsync(int id, Post post)
        {
            var existingPost = await _context.Posts.Include(x => x.Comments)
                         .FirstOrDefaultAsync(x => x.Id == id);

            if (existingPost == null)
            {
                throw new KeyNotFoundException($"Post with Id {id} not found.");
            }
            existingPost.Title = post.Title;
            existingPost.Body = post.Body;

            _context.Posts.Update(existingPost);

        }
    }


}