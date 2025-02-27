using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using api.DTOs.Comment;
using api.Interfaces;
using api.mappers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata;

namespace api.Controllers
{
    [Route("api/comment")]
    [ApiController]
    public class CommentController : ControllerBase
    {
        private readonly Icomment _CommentRepository;
        private readonly Istock _StockRepository;

        public CommentController(Icomment commentrepository, Istock stockrepo)
        {
            _CommentRepository = commentrepository;
            _StockRepository = stockrepo;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {

            var comments = await _CommentRepository.GetAllasync();

            var commentdto = comments.Select(s => s.toCommentdto());

            return Ok(commentdto);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> Getbyid([FromRoute] int id)
        {
            var comment = await _CommentRepository.Getbyid(id);

            if (comment == null)
            {
                return NotFound();
            }

            return Ok(comment.toCommentdto());
        }

        [HttpPost("{stockid:int}")]
        public async Task<IActionResult> Create([FromRoute] int stockid, CreateCommentDto createCommentDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (false == await _StockRepository.StockExist(stockid))
            {
                return BadRequest("stock does not exist");
            }

            var comment = createCommentDto.toCreateCommentdto(stockid);

            await _CommentRepository.Createasync(comment);

            return CreatedAtAction(nameof(Getbyid), new { id = comment.ID }, comment.toCommentdto());

        }

        [HttpDelete]
        [Route("{id:int}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            var comment = await _CommentRepository.Deleteasync(id);

            if (comment == null)
            {
                return NotFound();
            }

            return NoContent();
        }

    }
}