using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.Helpers;
using api.Interfaces;
using api.Models;
using Microsoft.EntityFrameworkCore;

namespace api.Repository
{
    public class CommentRepository : IcommentService
    {
        private readonly ApplicationDBcontext _Context;
        public CommentRepository(ApplicationDBcontext dBcontext)
        {
            _Context = dBcontext;
        }

        public async Task<Comment> Createasync(Comment comment)
        {
            await _Context.comments.AddAsync(comment);
            await _Context.SaveChangesAsync();
            return comment;
        }

        public async Task<Comment?> Deleteasync(int id)
        {
            var comment = await _Context.comments.FindAsync(id);

            if (comment == null)
            {
                return null;
            }

            _Context.comments.Remove(comment);
            await _Context.SaveChangesAsync();

            return comment;
        }

        public async Task<List<Comment>> GetAllasync(CommentQueryObject commentQueryObject)
        {
            var comment = _Context.comments.Include(a => a.AppUser).AsQueryable();

            if (!string.IsNullOrWhiteSpace(commentQueryObject.Symbol))
            {
                comment = comment.Where(s => s.Stock.Symbol == commentQueryObject.Symbol);
            }
            if (commentQueryObject.IsDecsending == true)
            {
                comment = comment.OrderByDescending(c => c.CreatedOn);
            }
            else
            {
                comment = comment.OrderBy(c => c.CreatedOn);
            }
            return await comment.ToListAsync();
        }

        public async Task<Comment> Getbyid(int id)
        {
            return await _Context.comments.Include(a => a.AppUser).FirstOrDefaultAsync(c => c.ID == id);
        }
    }
}