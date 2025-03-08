using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Models;

namespace api.Interfaces
{
    public interface IcommentService
    {
        Task<List<Comment>> GetAllasync();
        Task<Comment> Getbyid(int id);
        Task<Comment> Createasync(Comment comment);
        Task<Comment?> Deleteasync(int id);
    }
}