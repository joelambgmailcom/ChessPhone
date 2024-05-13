using Microsoft.Extensions.DependencyInjection;

using ChessPhone.Application;
using ChessPhone.Domain;
using ChessPhone.Infrastructure.Repositories;

namespace ChessPhone.Console
{
    public static class Program
    {
        public static  void Main(string[] args)
        {
            //var serviceProvider = new ServiceCollection()
            //    .AddSingleton<IChessPieceService, ChessPieceService>()
            //    .AddSingleton<IPhonePadService, PhonePadService>()
            //    .AddSingleton<IPhoneNumberService, PhoneNumberService>()
            //    .BuildServiceProvider();

            //var chessPieceService = serviceProvider.GetRequiredService<IChessPieceService>();
            //var phonePadService = serviceProvider.GetRequiredService<IPhonePadService>();
            //var phoneNumberService = serviceProvider.GetRequiredService<IPhoneNumberService>();

            //List<ChessPiece> chessPieces = await chessPieceService.GetChessPiecesAsync();
            //List<PhonePad> phonePads = await phonePadService.GetPhonePadsAsync();

            //PhonePad phonePad = phonePads.First(pad => pad.Name == "Standard Phone Pad");

            //var phoneNumberDigits = 3;
            //foreach ( ChessPiece chessPiece in chessPieces ) 
            //{
            //    var list = await phoneNumberService.GetPhoneNumbersAsync(chessPiece.Id,  phonePad.Id, phoneNumberDigits);
            //    System.Console.WriteLine($"Chess Piece: {chessPiece.Name} PhonePad: {chessPiece.PhonePad.Name}  Number of Phone Digits: {phoneNumberDigits}");
            //    PrintAllValidMoves(chessPiece);
            //}





            //var justCount = await phoneNumberService.GetPhoneNumbersCountAsync(1, 2, 3);
            
        }

        public static void PrintAllValidMoves(IChessPiece chessPiece)
		{
            foreach (var buttonArray in chessPiece.PhonePad.PhoneButtons)
            {
                foreach (var button in buttonArray)
                {
					PrintValidMoves(button.ValidNextMoves, chessPiece, button.Column, button.Row);
                }
            }
        }

        public static void PrintValidMoves(HashSet<PhoneButton> valid, IChessPiece chessPiece, int pieceColumn, int pieceRow)
        {
            string NL = Environment.NewLine; // shortcut
            string NORMAL = System.Console.IsOutputRedirected ? "" : "\x1b[39m";
            string RED = System.Console.IsOutputRedirected ? "" : "\x1b[91m";
            string GREEN = System.Console.IsOutputRedirected ? "" : "\x1b[92m";
            string YELLOW = System.Console.IsOutputRedirected ? "" : "\x1b[93m";
            string BLUE = System.Console.IsOutputRedirected ? "" : "\x1b[94m";
            string MAGENTA = System.Console.IsOutputRedirected ? "" : "\x1b[95m";
            string CYAN = System.Console.IsOutputRedirected ? "" : "\x1b[96m";
            string GREY = System.Console.IsOutputRedirected ? "" : "\x1b[97m";
            string BOLD = System.Console.IsOutputRedirected ? "" : "\x1b[1m";
            string NOBOLD = System.Console.IsOutputRedirected ? "" : "\x1b[22m";
            string UNDERLINE = System.Console.IsOutputRedirected ? "" : "\x1b[4m";
            string NOUNDERLINE = System.Console.IsOutputRedirected ? "" : "\x1b[24m";
            string REVERSE = System.Console.IsOutputRedirected ? "" : "\x1b[7m";
            string NOREVERSE = System.Console.IsOutputRedirected ? "" : "\x1b[27m";


            System.Console.WriteLine(chessPiece.Name);
            for (int row = 0; row < chessPiece.PhonePad.RowCount; row++)
            {
                for (int column = 0; column < chessPiece.PhonePad.ColumnCount; column++)
                {
                    var label = chessPiece.PhonePad.GetPhoneButton(column, row).Label;
                    if (column == pieceColumn && row == pieceRow)
                        label = $"{REVERSE}{label}{NOREVERSE}";
                    if (valid.Any(m => m.Column == column && m.Row == row))
                        label = $"{BLUE}{label}{NORMAL}" ;
                    System.Console.Write(label);
                }
                System.Console.WriteLine();
            }
            System.Console.WriteLine();
        }
    }
}