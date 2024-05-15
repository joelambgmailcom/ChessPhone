using ChessPhone.Application;
using ChessPhone.Domain;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860
 namespace ChessPhone.Api.Controllers;
 [Route("api/[controller]")]
 [ApiController]
public class PhoneNumbersController(IPhoneNumberService phoneNumberService) : ControllerBase
{
    // GET: api/<PhoneNumbersController>
    [HttpGet]
    public async Task<PhoneNumberResult> GetPhoneNumbersCountAsync(int chessPieceId, int phonePadId, int lengthOfPhoneNumber)
    {
        return await phoneNumberService.GetPhoneNumbersCountAsync(chessPieceId, phonePadId, lengthOfPhoneNumber);
    }
}