namespace TetPee.Service.Seller;

public class Request
{
    public class SellerRequest: User.Request.UserRequest
    {
        public string TaxCode { get; set; }
        public string CompanyName { get; set; }
        public string CompanyAddress { get; set; }
    }
}