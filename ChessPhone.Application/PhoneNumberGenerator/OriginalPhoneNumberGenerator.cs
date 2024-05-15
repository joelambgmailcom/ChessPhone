using ChessPhone.Domain;
using ChessPhone.Infrastructure.Repositories;
using System.Collections.Generic;
using System;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;

namespace ChessPhone.Application.PhoneNumberGenerator
{
    internal class OriginalPhoneNumberGenerator(IRepository<ChessPiece> chessPieceRepository, IRepository<PhonePad> phonePadRepository) : IPhoneNumberGenerator
    {
        public async Task<PhoneNumberResult> GetPhoneNumbersCountAsync(int chessPieceId, int phonePadId, int lengthOfPhoneNumber)
        {
            ArgumentOutOfRangeException.ThrowIfGreaterThan(lengthOfPhoneNumber, 12);
            Console.WriteLine("OriginalPhoneNumberGenerator.GetPhoneNumbersCountAsync");

            var timer = new Stopwatch();
            timer.Start();
            var result = await GetPhoneNumbersButtonListAsync(chessPieceId, phonePadId, lengthOfPhoneNumber);
            timer.Stop();

            return new PhoneNumberResult
            {
                PhoneNumberList = null,
                PhoneNumberCount = result.Count,
                GenerationTimeInMilliseconds = timer.ElapsedMilliseconds
            };
        }

        public async Task<PhoneNumberResult> GetPhoneNumbersAsync(int chessPieceId, int phonePadId, int lengthOfPhoneNumber)
        {
            Console.WriteLine("OriginalPhoneNumberGenerator.GetPhoneNumbersAsync");
            ArgumentOutOfRangeException.ThrowIfGreaterThan(lengthOfPhoneNumber, 7);

            var timer = new Stopwatch();
            timer.Start();
            var result = await GetPhoneNumbersButtonListAsync(chessPieceId, phonePadId, lengthOfPhoneNumber);
            timer.Stop();

            return new PhoneNumberResult
            {
                PhoneNumberList = ConvertPhoneNumberButtonsToStrings(result),
                PhoneNumberCount = result.Count,
                GenerationTimeInMilliseconds = timer.ElapsedMilliseconds
            };
        }

        private async Task<List<List<PhoneButton>>> GetPhoneNumbersButtonListAsync(int chessPieceId, int phonePadId, int lengthOfPhoneNumber)
        {
            var phoneNumbers = new List<List<PhoneButton>>();
            var newPhoneNumbers = new List<List<PhoneButton>>();
            var chessPiece = await chessPieceRepository.GetAsync(chessPieceId);
            var phonePad = await phonePadRepository.GetAsync(phonePadId);
            chessPiece?.SetPhonePad(phonePad);

            if (chessPiece?.PhonePad != null)
                for (var row = 0; row < chessPiece.PhonePad.RowCount; row++)
                {
                    for (var column = 0; column < chessPiece.PhonePad.ColumnCount; column++)
                    {
                        var button = chessPiece.PhonePad.GetPhoneButton(column, row);
                        if (chessPiece.IsLocationValidNow(button.Column, button.Row, 0))
                            phoneNumbers.Add([button]);
                    }
                }

            for (var iterations = 1; iterations < lengthOfPhoneNumber; iterations++)
            {
                foreach (var buttonList in phoneNumbers)
                {
                    var lastButton = buttonList.Last();
                    foreach (var newButton in lastButton.ValidNextMoves)
                    {
                        if (chessPiece != null && !chessPiece.IsLocationValidNow(newButton.Column, newButton.Row, iterations))
                            continue;

                        var cloneList = buttonList.ToList();
                        cloneList.Add(newButton);
                        newPhoneNumbers.Add(cloneList);
                    }
                }
                phoneNumbers = newPhoneNumbers;
                newPhoneNumbers = [];
            }

            return phoneNumbers;
        }

        private static List<string> ConvertPhoneNumberButtonsToStrings(List<List<PhoneButton>> result)
        {
            var results = new List<string>();
            foreach (List<PhoneButton> list in result)
            {
                var builder = new StringBuilder();
                foreach (PhoneButton phoneButton in list)
                    builder.Append(phoneButton.Label);

                results.Add(builder.ToString());
            }

            results.Sort();
            return results;
        }
    }
}
