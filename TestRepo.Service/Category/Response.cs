namespace TetPee.Service.Category;

public class Response
{
    public static class Message
    {
        public static string Created = "Created";
        public static string Updated = "Updated";
        public static string Deleted = "Deleted";
    }

    public class CategoryResponse
    {
        public string Name { get; set; } 
        public Guid Id { get; set; }
    }
}