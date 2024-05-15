using ChessPhone.Application;
using ChessPhone.Domain;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ChessPhone.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChessPiecesController(IChessPieceService chessPieceService) : ControllerBase
    {
        // GET: api/<ChessPiecesController>
        [HttpGet]
        public async Task<IEnumerable<ChessPiece>> Get()
        {
            return await chessPieceService.GetChessPiecesAsync();
        }

        // GET api/<ChessPiecesController>/5
        [HttpGet("{id}")]
        public async Task<ChessPiece?> Get(int id)
        {
            return await chessPieceService.GetChessPieceAsync(id);
        }
    }
}
