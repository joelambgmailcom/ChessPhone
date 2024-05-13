using ChessPhone.Domain;
using ChessPhone.Infrastructure.Repositories;

namespace ChessPhone.Application
{
    public class ChessPieceService(IRepository<ChessPiece> chessPieceRepository) : IChessPieceService
    {
        public async Task<List<ChessPiece>> GetChessPiecesAsync()
        {
            return await chessPieceRepository.GetAllAsync();
        }

        public async Task<ChessPiece?> GetChessPieceAsync(int id)
        {
            return await chessPieceRepository.GetAsync(id);
        }
    }
}
