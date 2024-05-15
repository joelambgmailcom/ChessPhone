using ChessPhone.Domain;
using ChessPhone.Infrastructure.Repositories;
using System.Collections.Generic;
using System;
using System.Diagnostics;
using System.Threading.Tasks;


namespace ChessPhone.Application.PhoneNumberGenerator
{
    internal class BetterStringHandlingPhoneNumberGenerator
            (IRepository<ChessPiece> chessPieceRepository, IRepository<PhonePad> phonePadRepository) : IPhoneNumberGenerator
    {
        public async Task<PhoneNumberResult> GetPhoneNumbersCountAsync(int chessPieceId, int phonePadId, int lengthOfPhoneNumber)
        {
            Console.WriteLine("BetterStringHandlingPhoneNumberGenerator.GetPhoneNumbersCountAsync");
            ArgumentOutOfRangeException.ThrowIfGreaterThan(lengthOfPhoneNumber, 12);
            var timer = new Stopwatch();
            timer.Start();
            var result = await GetPhoneNumbersButtonListAsync(chessPieceId, phonePadId, lengthOfPhoneNumber);
            timer.Stop();
            return new PhoneNumberResult { PhoneNumberList = null, PhoneNumberCount = result.Count, GenerationTimeInMilliseconds = timer.ElapsedMilliseconds };
        }

        public async Task<PhoneNumberResult> GetPhoneNumbersAsync(int chessPieceId, int phonePadId, int lengthOfPhoneNumber)
        {
            Console.WriteLine("BetterStringHandlingPhoneNumberGenerator.GetPhoneNumbersAsync");
            ArgumentOutOfRangeException.ThrowIfGreaterThan(lengthOfPhoneNumber, 7);
            var timer = new Stopwatch();
            timer.Start();
            var result = await GetPhoneNumbersButtonListAsync(chessPieceId, phonePadId, lengthOfPhoneNumber);
            timer.Stop();
            return new PhoneNumberResult
            {
                PhoneNumberList = result.Select(phoneNumber => phoneNumber.CurrentPhoneNumber).ToList(),
                PhoneNumberCount = result.Count,
                GenerationTimeInMilliseconds = timer.ElapsedMilliseconds
            };
        }

        private async Task<List<ChessPieceMoveLeaf>> GetPhoneNumbersButtonListAsync(int chessPieceId, int phonePadId, int lengthOfPhoneNumber)
        {
            var lastPhoneNumbers = new List<ChessPieceMoveLeaf>();
            var chessPiece = await chessPieceRepository.GetAsync(chessPieceId)
                             ?? throw new ArgumentOutOfRangeException(nameof(chessPieceId), $"chessPieceId {chessPieceId} not found");
            var phonePad = await phonePadRepository.GetAsync(phonePadId)
                           ?? throw new ArgumentOutOfRangeException(nameof(phonePadId), $"phonePadId {phonePadId} not found");
            chessPiece.SetPhonePad(phonePad);

            if (chessPiece.PhonePad != null)
                for (var row = 0; row < chessPiece.PhonePad.RowCount; row++)
                {
                    for (var column = 0; column < chessPiece.PhonePad.ColumnCount; column++)
                    {
                        var button = chessPiece.PhonePad.GetPhoneButton(column, row);
                        if (button.Label != null && chessPiece.IsLocationValidNow(button.Column, button.Row, 0))
                            lastPhoneNumbers.Add(new ChessPieceMoveLeaf(button, button.Label));
                    }
                }

            for (var iterations = 1; iterations < lengthOfPhoneNumber; iterations++)
            {
                var phoneNumbers = new List<ChessPieceMoveLeaf>();
                foreach (var lastButtonLeaf in lastPhoneNumbers)
                {
                    foreach (var validMoveButton in lastButtonLeaf.PhoneButton.ValidNextMoves
                                 .Where(validMoveButton =>
                                     chessPiece.IsLocationValidNow(
                                         validMoveButton.Column,
                                         validMoveButton.Row,
                                         iterations)))
                    {
                        phoneNumbers.Add(new ChessPieceMoveLeaf(validMoveButton,
                            lastButtonLeaf.CurrentPhoneNumber + validMoveButton.Label));
                    }

                    lastPhoneNumbers = phoneNumbers;
                }
            }

            return lastPhoneNumbers;
        }

    }
}
