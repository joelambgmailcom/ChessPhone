using ChessPhone.Application;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ChessPhone.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PhoneNumbersController(IPhoneNumberService phoneNumberService) : ControllerBase
    {
        // GET: api/<PhoneNumberssController>
        [HttpGet("justTotal")]
        public async Task<int> GetPhoneNumbersCountAsync(int chessPieceId, int phonePadId, int lengthOfPhoneNumber)
        {
            return await phoneNumberService.GetPhoneNumbersCountAsync(chessPieceId, phonePadId, lengthOfPhoneNumber);
        }

        // GET: api/<PhoneNumberssController>
        [HttpGet("withList")]
        public async Task<IEnumerable<string>> GetPhoneNumbersAsync(int chessPieceId, int phonePadId, int lengthOfPhoneNumber)
        {
            return await phoneNumberService.GetPhoneNumbersAsync(chessPieceId, phonePadId, lengthOfPhoneNumber);
        }
    }
}
