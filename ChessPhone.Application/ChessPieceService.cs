using ChessPhone.Domain;
using ChessPhone.Infrastructure.Repositories;

namespace ChessPhone.Application
{
    public class ChessPieceService(IRepository<ChessPiece> chessPieceRepositoy) : IChessPieceService
    {
        private readonly IRepository<ChessPiece> _chessPieceRepositoy = chessPieceRepositoy;

        public async Task<List<ChessPiece>> GetChessPiecesAsync()
        {
            return await _chessPieceRepositoy.GetAllAsync();
        }

        public async Task<ChessPiece> GetChessPieceAsync(int Id)
        {
            return await _chessPieceRepositoy.GetAsync(Id);
        }
    }
}
