using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PostCommentApi.DTOs.CommentDto;
using PostCommentApi.Models;

namespace PostCommentApi.DTOs.PostDTO
{
    public class PostDto
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Body { get; set; } = string.Empty;
        public ICollection<CommentDTO> Comments = new List<CommentDTO>();
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; } = DateTime.Now;
    }
}