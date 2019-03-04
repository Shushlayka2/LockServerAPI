using System.Text.RegularExpressions;

namespace LockMobileClient.Validations
{
    public class CodeValidator : IValidationRule<string>
    {
        const string codePattern = @"\w{3}-\w{3}-\w{3}";

        public bool Validate(string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                return false;
            }
            else
            {
                var regex = new Regex(codePattern, RegexOptions.IgnoreCase);
                return regex.IsMatch(value);
            }
        }
    }
}
