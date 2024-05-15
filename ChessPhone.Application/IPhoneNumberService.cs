using ChessPhone.Domain;

namespace ChessPhone.Application
{
    public interface IPhoneNumberService
    {
        Task<PhoneNumberResult> GetPhoneNumbersCountAsync(int chessPieceId, int phonePadId, int lengthOfPhoneNumber, int phoneNumberGeneratorId);

        Task<PhoneNumberResult> GetPhoneNumbersAsync(int chessPieceId, int phonePadId, int lengthOfPhoneNumber, int phoneNumberGeneratorId);
    }
}