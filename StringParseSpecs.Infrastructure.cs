using System;
using System.Globalization;
using NUnit.Framework;

namespace Int.Parse
{
    partial class StringParseSpecs
    {
        private void ArgumentInput(string input)
        {
            Assert.Throws<ArgumentException>(() => Parse(input));
        }

        private void ArgumentNullInput(string input)
        {
            Assert.Throws<ArgumentNullException>(() => Parse(input));
        }


        private int Parse(string number)
        {
            return new Parser().ParseInvariant(number);
        }

        private int Parse(string number, CultureInfo cultureInfo)
        {
            return new Parser().Parse(number, cultureInfo);
        }
    }
}
