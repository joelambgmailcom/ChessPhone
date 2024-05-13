using ChessPhone.Domain;

namespace ChessPhone.Application
{
    public interface IPhonePadService
    {
        Task<List<PhonePad>> GetPhonePadsAsync();

        Task<PhonePad> GetPhonePadAsync(int id);
    }
}