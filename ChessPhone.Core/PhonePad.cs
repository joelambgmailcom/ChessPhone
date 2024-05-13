namespace ChessPhone.Domain
{
    public class PhonePad : IPhonePad, IRepositoryEntity
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int NumberOfTurnsMaximum { get; set; }

        public int NumberOfTurnsWithListMaximum { get; set; }

        public PhoneButton[][] PhoneButtons { get; set; }

        public bool IsSkipMoveValid { get; set; }

        public int ColumnCount { get; set; }

        public int RowCount { get; set; }

        public HashSet<PhoneButton> ValidMoveButtons { get; set; } = [];

        public PhoneButton GetPhoneButton(int colunmIndex, int rowIndex) => PhoneButtons[colunmIndex][rowIndex];
    }
}