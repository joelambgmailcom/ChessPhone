namespace ChessPhone.Domain
{
    public class PhoneNumberResult
    {
        public List<string>? PhoneNumberList { get; set; }

        public int? PhoneNumberCount { get; set; }

        public long GenerationTimeInMilliseconds { get; set; }
    }
}