using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.DTOs.Comment;
using api.Helpers;
using api.Models;

namespace api.Services
{
    public interface ICommentService
    {
        Task<List<CommentDto>> GetAllCommentsAsync(CommentQueryObject commentQueryObject);

    }
}