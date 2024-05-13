namespace ChessPhone.Application
{
    public interface IPhoneNumberService
    {
        Task<int> GetPhoneNumbersCountAsync(int chessPieceId, int phonePadId, int lengthOfPhoneNumber);

        Task<List<string>> GetPhoneNumbersAsync(int chessPieceId, int phonePadId, int lengthOfPhoneNumber);
    }
}
