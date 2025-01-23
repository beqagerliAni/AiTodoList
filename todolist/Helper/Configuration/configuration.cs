namespace todolist.Helper.Configuration
{
    public class Configuration
    {
        public static IConfiguration? _configuration { get; set; }
        public static Task<string> Connection(string text)
        {

            if (_configuration == null) { throw new NotImplementedException(); }

            string? connection = _configuration[text];

            if (connection == null) { throw new Exception("connection not found"); }

            return Task.FromResult(connection);
        }
    }
}
