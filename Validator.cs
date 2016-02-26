using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Gulliver
{
    public static class Validator
    {
        public static bool ValidPrice(string price)
        {
           string regexPattern = @"^\d{1,3}(\.\d{1,2})?$";
           return Regex.IsMatch(price, regexPattern);
        }

        public static bool ValidateDate(string price)
        {
           string regexPattern = @"(0[1-9]|[12][0-9]|3[01])/(0[1-9]|1[012])/20\d{2}";
           return Regex.IsMatch(price, regexPattern);
        }

        public static bool ValidPlusAndMinusPrice(string price)
        {
           string regexPattern = @"^([-+] ?)?\d{1,3}(\.\d{1,2})?$";
           return Regex.IsMatch(price, regexPattern);
        }

        public static string Plural(string word, int number)
        {
           return (number + " " + word + ((number > 1) ? "s" : string.Empty));
        }

        public static bool isDecimal(string str)
        {
           string regexPattern = @"^\d{1,3}(\.\d{1,2})?$";
           return Regex.IsMatch(str, regexPattern);
        }
    }
}
