using System.Linq;
using System.Text.RegularExpressions;
namespace iprod
{
    public static class Validations
    {
        private const string ForbiddenWords = "!@#$^_+[]'?<>,`.№;:";

        public static bool ValidRegister(string login, string password)
        {
            return !string.IsNullOrEmpty(login) && !string.IsNullOrEmpty(password) &&
                   (IsValidPassword(password) && IsValidLogin(login));
        }

        private static bool IsValidPassword(string password)
        {
            var hasNumber = new Regex(@"[0-9]+");
            var hasUpperChar = new Regex(@"[A-Z]+");
            var hasMinimum8Chars = new Regex(@".{8,}");
            var hasForbiddenSymbols = new Regex($"[{ForbiddenWords}]");
            
            
            return !hasForbiddenSymbols.IsMatch(password) && (hasNumber.IsMatch(password) &&
                                                              hasUpperChar.IsMatch(password) &&
                                                              hasMinimum8Chars.IsMatch(password));
        }

        private static bool IsValidLogin(string login)
        {
            var hasMinimum8Chars = new Regex(@".{8,}");
            var hasForbiddenSymbols = new Regex($"[{ForbiddenWords}]");

            return !hasForbiddenSymbols.IsMatch(login) && hasMinimum8Chars.IsMatch(login);
        }

        public static bool IsNull(params string[] mass)
        {
            return mass.Any(t => t == null);
        }

        
            
            
    }
}