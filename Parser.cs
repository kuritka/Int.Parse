using System;
using System.Globalization;
using System.Linq;
using System.Runtime.Remoting.Contexts;


namespace Int.Parse
{
    [Synchronization]
    public class Parser : ContextBoundObject
    {
       
        public int ParseInvariant(string number)
        {
            return Parse(number, CultureInfo.InvariantCulture);
        }


        public int Parse(string number, CultureInfo cultureInfo)
        {
           
            var decimalCounter = 1;
            var result = 0;

            if (string.IsNullOrEmpty(number))
            {
                throw new ArgumentNullException();
            }

            number = ProcessDecimalSeparator(number, cultureInfo);

            number = ProcessGroupSeparators(number, cultureInfo);       

           
            if (number.StartsWith("-"))
            {
                decimalCounter *= -1;
                number = number.Substring(1);
            }
            else if (number.StartsWith("+"))
            {
                number = number.Substring(1);
            }


            foreach (var charValue in number.Select(c => c - 48))
            {
                if (charValue < 0 || charValue > 9)
                {
                    throw new ArgumentException("Argument contains invalid characters");
                }
                checked
                {
                    result = 10*result + charValue;
                }
            }            
            return result * decimalCounter;
        }

        private static string ProcessDecimalSeparator(string number, CultureInfo cultureInfo)
        {
            if (number.Contains(cultureInfo.NumberFormat.NumberDecimalSeparator))
            {
                var decimalParts = number.Split(cultureInfo.NumberFormat.NumberDecimalSeparator.ToCharArray());
                if (decimalParts.Count() != 2)
                {
                    throw new ArgumentException("Invalid input");
                }
                if (decimalParts[1].Length > cultureInfo.NumberFormat.NumberDecimalDigits)
                {
                    throw new ArgumentException("Invalid input");
                }
                number = number.Split(cultureInfo.NumberFormat.NumberDecimalSeparator.ToCharArray())[0];
            }
            return number;
        }

        private static string ProcessGroupSeparators(string number, CultureInfo cultureInfo)
        {
            if (number.Contains(cultureInfo.NumberFormat.NumberGroupSeparator))
            {
                var groupParts = number.Split(cultureInfo.NumberFormat.NumberGroupSeparator.ToCharArray());
                if (groupParts.Skip(1).All(d => d.Count() == 3))
                {
                    number = number.Replace(cultureInfo.NumberFormat.NumberGroupSeparator, string.Empty);
                    if (number == string.Empty)
                    {
                        throw new ArgumentNullException();
                    }
                }
            }
            return number;
        }
    }
}
