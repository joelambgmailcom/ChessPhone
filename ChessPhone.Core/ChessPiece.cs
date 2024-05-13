namespace ChessPhone.Domain
{
    public class ChessPiece : IChessPiece, IRepositoryEntity
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public IPhonePad PhonePad { get; private set; }

        public bool IsCanOnlyMoveSingleSpace { get; set; }

        public HashSet<Location> Vectors { get; set; } = [];



        public void SetPhonePad(IPhonePad phonePad)
        {
            PhonePad = phonePad;
            foreach (var buttonArray in phonePad.PhoneButtons)
            {
                foreach (var button in buttonArray)
                {
                    button.ValidNextMoves = GetValidMoves(button.Column, button.Row);
                }
            }
        }



        public bool IsLocationGeneralValid(int newColumn, int newRow)
        {
            if (newColumn >= PhonePad.ColumnCount)
                return false;

            if (newRow >= PhonePad.RowCount)
                return false;

            if (PhonePad.GetPhoneButton(newColumn, newRow).IsAlwaysInvalid)
                return false;

            return true;
        }

        public bool IsLocationValidNow(int newColumn, int newRow, int iteration)
        {
            if (!IsLocationGeneralValid(newColumn, newRow))
                return false;

            if (PhonePad.GetPhoneButton(newColumn, newRow).InvalidOnIteration.Contains(iteration))
                return false;

            return true;
        }

        public bool IsLocationValidNow(int newColumn, int newRow, int currentColumn, int currentRow, int iteration)
        {
            if (!IsLocationValidNow(newColumn, newRow, iteration))
                return false;

            if (!PhonePad.IsSkipMoveValid &&  newColumn == currentColumn && newRow == currentRow)
                return false;

            return true;
        }

        private bool HasVectorMovedOffPad(int columnIndex, int rowIndex, int iterations)
        {
            return columnIndex < 0
                || columnIndex >= PhonePad.ColumnCount
                || rowIndex < 0
                || rowIndex >= PhonePad.RowCount
                || IsCanOnlyMoveSingleSpace && iterations > 1;
        }

        private HashSet<PhoneButton> GetValidMoves(int column, int row)
        {
            var validButtons = new HashSet<PhoneButton>();
            if (PhonePad.GetPhoneButton(column, row).IsAlwaysInvalid)
                return validButtons;

            foreach (var vector in Vectors)
            {
                int currentColumnOffset = column;
                int currentRowOffset = row;
                int iterations = 0;
                while (!HasVectorMovedOffPad(currentColumnOffset, currentRowOffset, iterations))
                {
                    var button = PhonePad.GetPhoneButton(currentColumnOffset, currentRowOffset);
                    if (IsLocationGeneralValid(currentColumnOffset, currentRowOffset))
                        validButtons.Add(button);

                    currentColumnOffset += vector.Column;
                    currentRowOffset += vector.Row;
                    iterations++;
                }
            }

            return validButtons;
        }
    }
}