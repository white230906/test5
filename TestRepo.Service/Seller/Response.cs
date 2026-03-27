namespace TetPee.Service.Seller;

public class Response
{
    public static class Message
    {
        public static string Created = "Created";
        public static string Failed = "Failed";
        public static string Deleted = "Deleted";
    }

    public class SellerResponse
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
    }
}