using System;
using System.Globalization;
using NUnit.Framework;

namespace Int.Parse
{
   
    public partial class StringParseSpecs
    {
        
        [Test]
        public void CorrectInvariantInput()
        {
            Assert.AreEqual(Parse("100"), 100);

            Assert.AreEqual(Parse("10501"), 10501);
        }

        [Test]
        public void WrongInvariantInput()
        {
            ArgumentNullInput(string.Empty);
            ArgumentNullInput(null);
            ArgumentInput("100as0");
            ArgumentInput("+ 100000 0000 ");
            ArgumentInput("528.22.12");
            ArgumentInput("528..11");
        }

       
        [Test]
        public void CorrectInvariantDecimalInput()
        {
            Assert.AreEqual(Parse("100.0"), 100);

            Assert.AreEqual(Parse("15142.2"), 15142); 
        }


        [Test]
        public void WrongInvariantDecimalNumberWithThreeDecimalDigits()
        {
            Assert.Throws<ArgumentException>(() => Parse("15142.005"));
        }

        [Test]
        public void WrongInvariantDecimalNumberWithWrongDecimalSeparator()
        {
            Assert.Throws<ArgumentException>(() => Parse("15142,05"));
        }


        [Test]
        public void CorrectCzechDecimalNumberWithRightDecimalSeparator()
        {
            Assert.AreEqual(Parse("15142,05",CultureInfo.GetCultureInfo("cs-CZ")),15142);
        }

        [Test]
        public void WrongCzechDecimalNumberWithWrongCountOfDecimalDigits()
        {
            Assert.Throws<ArgumentException>(() => Parse("15142,005", CultureInfo.GetCultureInfo("cs-CZ")));
        }


        [Test]
        public void InvariantGroupedNumberWithWrongGroupedSeparators()
        {
            Assert.Throws<ArgumentException>(() => Parse("15.142.005"));
        }



        [Test]
        public void InputWithGroupedInvarintNumber()
        {
            Assert.AreEqual(Parse("11,001.02"), 11001);
            Assert.AreEqual(Parse("11,001,001.02"), 11001001);       
        }

        [Test]
        public void CorrectNegativeInput()
        {
            Assert.AreEqual(Parse("-100"), -100);            
        }


        [Test]
        public void CorrectPositiveInput()
        {
            Assert.AreEqual(Parse("+100"), +100);
        }


        [Test]
        public void TooBigPositiveInput()
        {
            Assert.Throws<OverflowException>(() => Parse("1000000000000000000000"));
        }


        [Test]
        public void TooLowNegativeInput()
        {
            Assert.Throws<OverflowException>(() => Parse("-1000000000000000000000"));
        }

       

      

    }
}
