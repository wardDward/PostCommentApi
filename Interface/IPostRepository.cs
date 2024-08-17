using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PostCommentApi.Models;

namespace PostCommentApi.Interface
{
    public interface IPostRepository
    {
         Task<IEnumerable<Post>> GetPostsAsync(); 
         Task<Post> GetPostByIdAsync(int id);
         Task AddPostAsync(Post post);
         Task UpdatePostAsync(int id, Post post);   
         Task DeletePostAsync(Post post);
         

    }
}