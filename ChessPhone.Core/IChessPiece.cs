namespace ChessPhone.Domain
{
    public interface IChessPiece
    {
        int Id { get; }

        string Name { get; set; }

        IPhonePad? PhonePad { get; }

        bool IsCanOnlyMoveSingleSpace { get; set; }

        HashSet<Location> Vectors { get; set; }

        void SetPhonePad(IPhonePad? phonePad);

        bool IsLocationGeneralValid(int newColumn, int newRow);

        bool IsLocationValidNow(int newColumn, int newRow, int iteration);

        bool IsLocationValidNow(int newColumn, int newRow, int currentColumn, int currentRow, int iteration);
    }
}
