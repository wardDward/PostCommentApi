using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PostCommentApi.DTOs.CommentDto
{
    public class CommentDTO
    {
        public int Id { get; set;}
        public string Body { get; set;} = string.Empty;
        public int PostId { get; set;}  
    }
}