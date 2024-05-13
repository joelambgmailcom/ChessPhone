﻿using ChessPhone.Domain;
using ChessPhone.Infrastructure.Repositories;
using System.Text;

namespace ChessPhone.Application
{
    public class PhoneNumberService(IRepository<ChessPiece> chessPieceRepository, IRepository<PhonePad> phonePadRepository) : IPhoneNumberService
    {
        private readonly IRepository<ChessPiece> _chessPieceRepository = chessPieceRepository;

        private readonly IRepository<PhonePad> _phonePadRepository = phonePadRepository;

        public async Task<int> GetPhoneNumbersCountAsync(int chessPieceId, int phonePadId, int lengthOfPhoneNumber)
        {
            ArgumentOutOfRangeException.ThrowIfGreaterThan(lengthOfPhoneNumber, 25);

            var result = await GetPhoneNumbersButtonListAsync(chessPieceId, phonePadId, lengthOfPhoneNumber);
            return result.Count;

        }

        public async Task<List<string>> GetPhoneNumbersAsync(int chessPieceId, int phonePadId, int lengthOfPhoneNumber)
        {
            ArgumentOutOfRangeException.ThrowIfGreaterThan(lengthOfPhoneNumber, 7);

            var result = await GetPhoneNumbersButtonListAsync(chessPieceId, phonePadId, lengthOfPhoneNumber);
            return ConvertPhoneNumberButtonsToStrings(result);
        }

        private async Task<List<List<PhoneButton>>> GetPhoneNumbersButtonListAsync(int chessPieceId, int phonePadId, int lengthOfPhoneNumber)
        {
            var phoneNumbers = new List<List<PhoneButton>>();
            var newPhoneNumbers = new List<List<PhoneButton>>();
            var chessPiece = await _chessPieceRepository.GetAsync(chessPieceId);
            var phonePad = await _phonePadRepository.GetAsync(phonePadId);
            chessPiece.SetPhonePad(phonePad);

            for (int row = 0; row < chessPiece.PhonePad.RowCount; row++)
            {
                for (int column = 0; column < chessPiece.PhonePad.ColumnCount; column++)
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
                        if (!chessPiece.IsLocationValidNow(newButton.Column, newButton.Row, lastButton.Column, lastButton.Row, iterations))
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
                foreach (PhoneButton button in list)
                    builder.Append(button.Label);

                results.Add(builder.ToString());
            }

            results.Sort();
            return results;
        }
    }
}