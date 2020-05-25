namespace User.Helpers
{
    using System.Text;

    public static class EmailComposer
    {
        public static string ComposeEmail(string name, int verificationCode)
        {
            var stringBuilder = new StringBuilder();
            stringBuilder.Append($"Hello, {name}");
            stringBuilder.Append("Welcome to User Management System");
            stringBuilder.Append($"Your Verification Code is {verificationCode}");
            return stringBuilder.ToString();
        }
    }
}
