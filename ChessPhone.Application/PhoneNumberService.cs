using ChessPhone.Domain;
using ChessPhone.Infrastructure.Repositories;
using System.Diagnostics;

namespace ChessPhone.Application
{
    public class PhoneNumberService(IRepository<ChessPiece> chessPieceRepository, IRepository<PhonePad> phonePadRepository) : IPhoneNumberService
    {
        public async Task<PhoneNumberResult> GetPhoneNumbersCountAsync(int chessPieceId, int phonePadId, int lengthOfPhoneNumber)
        {
            ArgumentOutOfRangeException.ThrowIfGreaterThan(lengthOfPhoneNumber, 12);

            var timer = new Stopwatch();
            timer.Start();
            var result = await GetPhoneNumbersButtonListAsync(chessPieceId, phonePadId, lengthOfPhoneNumber);
            timer.Stop();

            return new PhoneNumberResult
            {
                PhoneNumberCount = result,
                GenerationTimeInMilliseconds = timer.ElapsedMilliseconds
            };
        }

        private async Task<long> GetPhoneNumbersButtonListAsync(int chessPieceId, int phonePadId,
            int lengthOfPhoneNumber)
        {
            var chessPiece = await chessPieceRepository.GetAsync(chessPieceId)
                             ?? throw new ArgumentOutOfRangeException(nameof(chessPieceId),
                                 $"chessPieceId {chessPieceId} not found");
            var phonePad = await phonePadRepository.GetAsync(phonePadId)
                           ?? throw new ArgumentOutOfRangeException(nameof(phonePadId),
                               $"phonePadId {phonePadId} not found");
            chessPiece.SetPhonePad(phonePad);


            var phonePadButtonList = new List<PhoneButton>();
            for (var row = 0; row < phonePad.RowCount; row++)
            {
                for (var column = 0; column < phonePad.ColumnCount; column++)
                {
                    var button = phonePad.GetPhoneButton(column, row);
                    button.TotalMoves = 0;
                    button.TotalMovesLastIteration = 0;
                    if (button.IsAlwaysInvalid)
                        continue;

                    phonePadButtonList.Add(button);
                    if (chessPiece.IsLocationValidNow(button.Column, button.Row, 0))
                        button.TotalMoves = 1;
                }
            }

            MoveTotalMovesToTotalMovesLastIteration(phonePadButtonList);

            for (var iterations = 1; iterations < lengthOfPhoneNumber; iterations++)
            {
                foreach (var currentButton in phonePadButtonList)
                {
                    foreach (var targetButton in currentButton.ValidNextMoves)
                    {
                        if (!chessPiece.IsLocationValidNow(targetButton.Column, targetButton.Row, iterations))
                            continue;

                        targetButton.TotalMoves += currentButton.TotalMovesLastIteration;
                    }
                }

                MoveTotalMovesToTotalMovesLastIteration(phonePadButtonList);
            }

            var answer = phonePadButtonList.Select(button => button.TotalMovesLastIteration).Sum();
            return answer;
        }


        private static void MoveTotalMovesToTotalMovesLastIteration(List<PhoneButton> phonePadButtonList)
        {
            foreach (var currentButton in phonePadButtonList)
            {
                currentButton.TotalMovesLastIteration = currentButton.TotalMoves;
                currentButton.TotalMoves = 0;
            }
        }
    }
}
