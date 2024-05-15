using ChessPhone.Domain;
using System.Threading.Tasks;

namespace ChessPhone.Application.PhoneNumberGenerator
{
    public interface IPhoneNumberGenerator
    {
        Task<PhoneNumberResult> GetPhoneNumbersCountAsync(int chessPieceId, int phonePadId, int lengthOfPhoneNumber);

        Task<PhoneNumberResult> GetPhoneNumbersAsync(int chessPieceId, int phonePadId, int lengthOfPhoneNumber);
    }
}