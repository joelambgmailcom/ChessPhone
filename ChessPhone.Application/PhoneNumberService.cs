using ChessPhone.Application.PhoneNumberGenerator;
using ChessPhone.Domain;
using ChessPhone.Infrastructure.Repositories;

namespace ChessPhone.Application
{
    public class PhoneNumberService(IRepository<ChessPiece> chessPieceRepository, IRepository<PhonePad> phonePadRepository) : IPhoneNumberService
    {
        public async Task<PhoneNumberResult> GetPhoneNumbersAsync(int chessPieceId, int phonePadId, int lengthOfPhoneNumber, int phoneNumberGeneratorId) =>
            await GetPhoneNumberGenerator(phoneNumberGeneratorId).GetPhoneNumbersAsync(chessPieceId, phonePadId, lengthOfPhoneNumber);

        public async Task<PhoneNumberResult> GetPhoneNumbersCountAsync(int chessPieceId, int phonePadId, int lengthOfPhoneNumber, int phoneNumberGeneratorId) =>
            await GetPhoneNumberGenerator(phoneNumberGeneratorId).GetPhoneNumbersCountAsync(chessPieceId, phonePadId, lengthOfPhoneNumber);

        private IPhoneNumberGenerator GetPhoneNumberGenerator(int phoneNumberGeneratorId)
        {
            var originalGenerator = new OriginalPhoneNumberGenerator(chessPieceRepository, phonePadRepository);
            var betterGenerator = new BetterStringHandlingPhoneNumberGenerator(chessPieceRepository, phonePadRepository);
            var summationGenerator = new SummationPhoneNumberGenerator(chessPieceRepository, phonePadRepository);

            IPhoneNumberGenerator generator;
            switch (phoneNumberGeneratorId)
            {
                case 1:
                    generator = originalGenerator;
                    break;
                case 2:
                    generator = betterGenerator;
                    break;
                case 3:
                    generator = summationGenerator;
                    break;

                default:
                    throw new ArgumentException(nameof(phoneNumberGeneratorId), $" not found");
            }

            return generator;
        }
    }
}
