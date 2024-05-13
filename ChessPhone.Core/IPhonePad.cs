namespace ChessPhone.Domain
{
    public interface IPhonePad
    {
        int Id { get; } 

        string Name { get; set; }

        PhoneButton[][] PhoneButtons { get; set; }

        bool IsSkipMoveValid { get; }

        int ColumnCount { get; }

        int RowCount { get; }

        HashSet<PhoneButton> ValidMoveButtons { get; }

        PhoneButton GetPhoneButton(int colunmIndex, int rowIndex);
    }
}
