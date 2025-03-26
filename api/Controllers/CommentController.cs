using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using api.DTOs.Comment;
using api.Extensions;
using api.Helpers;
using api.Interfaces;
using api.mappers;
using Microsoft.AspNetCore.Authorization;
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
        private readonly IFMPService _FMPservice;

        public CommentController(IcommentService commentrepository, IaccountService accountservice, IstockService stockrepo,
        IFMPService fMPService)
        {
            _CommentRepository = commentrepository;
            _Accountservice = accountservice;
            _StockRepository = stockrepo;
            _FMPservice = fMPService;
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetAll(CommentQueryObject commentQueryObject)
        {
            var comments = await _CommentRepository.GetAllasync(commentQueryObject);

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
        public async Task<IActionResult> Create([FromRoute] string symbol, CreateCommentDto createCommentDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var stock = await _StockRepository.GetbySymbolAsync(symbol);

            if (stock == null)
            {
                var search_in_fmp = await _FMPservice.FindBySymbolAsync(symbol);

                if (search_in_fmp.Exit == false)
                {
                    return StatusCode(search_in_fmp.Errorcode, search_in_fmp.Errormessage);
                }
                else
                {
                    await _StockRepository.Createasync(search_in_fmp.Data);
                    stock = await _StockRepository.GetbySymbolAsync(symbol);
                }
            }

            var username = User.getUserName();
            var user = await _Accountservice.FindByname(username);

            var comment = createCommentDto.toCreateCommentdto(stock.ID, user.Id);

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