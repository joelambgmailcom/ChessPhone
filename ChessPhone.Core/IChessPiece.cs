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

        bool IsLocationValidNow(int newColumn, int newRow, int iteration);
    }
}
