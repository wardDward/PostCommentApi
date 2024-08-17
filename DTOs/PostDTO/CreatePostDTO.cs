using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PostCommentApi.DTOs.PostDTO
{
    public class CreatePostDTO
    {

        public string Title { get; set; } = string.Empty;
        public string Body { get; set; } = string.Empty;
    }
}