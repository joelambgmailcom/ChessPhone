using ChessPhone.Domain;
using ChessPhone.Infrastructure.Repositories;

namespace ChessPhone.Application
{
    public class PhonePadService(IRepository<PhonePad> phonePadRepositoy) : IPhonePadService
    {
        private readonly IRepository<PhonePad> _PhonePadRepository = phonePadRepositoy;

        public async Task<List<PhonePad>> GetPhonePadsAsync()
        {
            return await _PhonePadRepository.GetAllAsync();
        }

        public async Task<PhonePad> GetPhonePadAsync(int id)
        {
            return await _PhonePadRepository.GetAsync(id);
        }
    }
}
