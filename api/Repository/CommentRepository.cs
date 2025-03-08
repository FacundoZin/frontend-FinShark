using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
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

        public async Task<List<Comment>> GetAllasync()
        {
            return await _Context.comments.ToListAsync();
        }

        public async Task<Comment> Getbyid(int id)
        {
            return await _Context.comments.FindAsync(id);
        }
    }
}