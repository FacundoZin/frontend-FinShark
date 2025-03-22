using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using api.DTOs.Comment;
using api.Extensions;
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
        private readonly IcommentService _CommentRepository;
        private readonly IaccountService _Accountservice;
        private readonly IstockService _StockRepository;

        public CommentController(IcommentService commentrepository,IaccountService accountservice ,IstockService stockrepo)
        {
            _CommentRepository = commentrepository;
            _Accountservice = accountservice;
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

            var username = User.getUserName();
            var user = await _Accountservice.FindByname(username);

            var comment = createCommentDto.toCreateCommentdto(stockid,user.Id);

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