using ChessPhone.Application;
using ChessPhone.Domain;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ChessPhone.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PhonePadsController(IPhonePadService phonePadService) : ControllerBase
    {
        // GET: api/<PhonePadsController>
        [HttpGet]
        public async Task<IEnumerable<PhonePad>> GetPhonePadAsync()
        {
            return await phonePadService.GetPhonePadsAsync();
        }

        // GET api/<PhonePadsController>/5
        [HttpGet("{id}")]
        public async Task<PhonePad?> GetPhonePadAsync(int id)
        {
            return await phonePadService.GetPhonePadAsync(id);
        }
    }
}
