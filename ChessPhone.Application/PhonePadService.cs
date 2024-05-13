using ChessPhone.Domain;
using ChessPhone.Infrastructure.Repositories;

namespace ChessPhone.Application
{
    public class PhonePadService(IRepository<PhonePad> phonePadRepository) : IPhonePadService
    {
        public async Task<List<PhonePad>> GetPhonePadsAsync()
        {
            return await phonePadRepository.GetAllAsync();
        }

        public async Task<PhonePad?> GetPhonePadAsync(int id)
        {
            return await phonePadRepository.GetAsync(id);
        }
    }
}
