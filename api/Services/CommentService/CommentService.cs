using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.DTOs.Comment;
using api.Helpers;
using api.Interfaces;
using api.mappers;
using api.Models;
using Microsoft.EntityFrameworkCore;

namespace api.Services.CommentService
{

    public class CommentService : ICommentService
    {
        private readonly ICommentRepository _CommentRepo;

        public CommentService(ICommentRepository commentrepo)
        {
            _CommentRepo = commentrepo;
        }

        public async Task<List<CommentDto>> GetAllCommentsAsync(CommentQueryObject commentQueryObject)
        {
            var consult = _CommentRepo.GetAll();

            if (!string.IsNullOrWhiteSpace(commentQueryObject.Symbol))
            {
                consult = consult.Where(s => s.Stock.Symbol == commentQueryObject.Symbol);
            }
            if (commentQueryObject.IsDecsending == true)
            {
                consult = consult.OrderByDescending(c => c.CreatedOn);
            }
            else
            {
                consult = consult.OrderBy(c => c.CreatedOn);
            }

            var comments = await consult.ToListAsync();

            return comments.Select(c => c.toCommentdto()).ToList();
        }
    }
}