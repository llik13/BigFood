namespace AgregatorGrpc.Protos
{
    public partial class ReviewIdRequest
    {
        public static implicit operator int(ReviewIdRequest message)
        {
            return message.Id;
        }

        public static implicit operator ReviewIdRequest(int value)
        {
            return new ReviewIdRequest { Id = value };
        }
    }
}
