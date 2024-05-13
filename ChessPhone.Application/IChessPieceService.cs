using ChessPhone.Domain;

namespace ChessPhone.Application
{
    public interface IChessPieceService
    {
        Task<List<ChessPiece>> GetChessPiecesAsync();

        Task<ChessPiece> GetChessPieceAsync(int Id);
    }
}