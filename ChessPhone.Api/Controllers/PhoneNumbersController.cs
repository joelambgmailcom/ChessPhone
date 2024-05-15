using ChessPhone.Application;
using ChessPhone.Domain;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ChessPhone.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PhoneNumbersController(IPhoneNumberService phoneNumberService) : ControllerBase
    {
        // GET: api/<PhoneNumbersController>
        [HttpGet("justTotal")]
        public async Task<PhoneNumberResult> GetPhoneNumbersCountAsync(int chessPieceId, int phonePadId, int lengthOfPhoneNumber, int phoneNumberGeneratorId)
        {
            return await phoneNumberService.GetPhoneNumbersCountAsync(chessPieceId, phonePadId, lengthOfPhoneNumber, phoneNumberGeneratorId);
        }

        // GET: api/<PhoneNumbersController>
        [HttpGet("withList")]
        public async Task<PhoneNumberResult> GetPhoneNumbersAsync(int chessPieceId, int phonePadId, int lengthOfPhoneNumber, int phoneNumberGeneratorId)
        {
            return await phoneNumberService.GetPhoneNumbersAsync(chessPieceId, phonePadId, lengthOfPhoneNumber, phoneNumberGeneratorId);
        }
    }
}