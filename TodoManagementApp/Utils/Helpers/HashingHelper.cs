namespace TodoManagementApp.Utils.Helpers
{
    public static class HashingHelper
    {
        public static string Hash(string input) => BCrypt.Net.BCrypt.HashPassword(input);
        public static bool Verify(string input, string hash) => BCrypt.Net.BCrypt.Verify(input, hash);
    }
}
