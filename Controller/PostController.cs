using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PostCommentApi.Data;
using PostCommentApi.DTOs.CommentDto;
using PostCommentApi.DTOs.PostDTO;
using PostCommentApi.Models;
using PostCommentApi.Repository;

namespace PostCommentApi.Controller
{
    [ApiController]
    [Route("api/posts")]
    public class PostController : ControllerBase
    {

        private readonly ApplicationDbContext _context;
        private readonly PostRepository _postRepository;
        public PostController(ApplicationDbContext context, PostRepository postRepository)
        {
            _context = context;
            _postRepository = postRepository;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var posts = await _postRepository.GetPostsAsync();
            return Ok(posts);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromForm] CreatePostDTO createPostDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _postRepository.AddPostAsync(createPostDTO);

            var newPost = new Post
            {
                Title = createPostDTO.Title,
                Body = createPostDTO.Body,
            };

            return CreatedAtAction(nameof(Show), new { Id = newPost.Id }, newPost);

        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Show([FromRoute] int id)
        {
            var post = await _postRepository.GetPostByIdAsync(id);

            if (post == null)
            {
                return NotFound();
            }

            var postDto = new PostDto
            {
                Id = post.Id,
                Title = post.Title,
                Body = post.Body,
                Comments = post.Comments.Select(c => new CommentDTO
                {
                    Id = c.Id,
                    Body = c.Body
                }).ToList()
            };

            return Ok(post);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromForm] UpdatePostDto updatePostDto)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var existingPost = await _postRepository.GetPostByIdAsync(id);

            if (existingPost == null)
            {
                return NotFound();
            }

            if (id != updatePostDto.Id)
            {
                return BadRequest("Post Id mismatch");
            }

            _postRepository.UpdatePostAsync(existingPost);
    


            await _context.SaveChangesAsync();

            var updatedPostDto = new PostDto
            {
                Id = existingPost.Id,
                Title = existingPost.Title,
                Body = existingPost.Body,
                Comments = existingPost.Comments.Select(c => new CommentDTO
                {
                    Id = c.Id,
                    Body = c.Body
                }).ToList()
            };

            return Ok(updatedPostDto);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            var post = await _postRepository.GetPostByIdAsync(id);

            if (post == null)
            {
                return NotFound();
            }

            await _postRepository.DeletePostAsync(post);

            return NoContent();

        }

    }
}