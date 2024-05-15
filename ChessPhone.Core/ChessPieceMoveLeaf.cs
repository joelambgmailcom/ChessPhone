namespace ChessPhone.Domain
{
    public class ChessPieceMoveLeaf(PhoneButton phoneButton, string currentPhoneNumber)
    {
        public PhoneButton PhoneButton { get; set; } = phoneButton;
        public string CurrentPhoneNumber { get; set; } = currentPhoneNumber;
    }
}