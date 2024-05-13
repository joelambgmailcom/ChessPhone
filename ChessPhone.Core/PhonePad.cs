namespace ChessPhone.Domain
{
    public class PhonePad : IPhonePad, IRepositoryEntity
    {
        public int Id { get; set; }

        public required string Name { get; set; }

        public int NumberOfTurnsMaximum { get; set; }

        public int NumberOfTurnsWithListMaximum { get; set; }

        public PhoneButton[][] PhoneButtons { get; set; } = null!;

        public bool IsSkipMoveValid { get; set; }

        public int ColumnCount { get; set; }

        public int RowCount { get; set; }

        public PhoneButton GetPhoneButton(int columnIndex, int rowIndex) => PhoneButtons[columnIndex][rowIndex];
    }
}