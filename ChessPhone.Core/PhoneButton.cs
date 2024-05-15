namespace ChessPhone.Domain
{
    public class PhoneButton
    {
        public int Column { get; set; }

        public int Row { get; set; }

        public string? Label { get; set; }

        public bool IsAlwaysInvalid { get; set; }

        public int[] InvalidOnIteration { get; set; } = [];

        public HashSet<PhoneButton> ValidNextMoves { get; set; } = [];

        public int TotalMoves { get; set; }

        public int TotalMovesLastIteration { get; set; }
    }
}
