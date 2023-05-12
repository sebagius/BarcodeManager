using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace BarcodeManagerTest
{
    public class AlphanumericTest
    {
        char a = 'a', b = 'z', c = '!', d = '\t', e = ' ';

        [SetUp]
        public void Setup()
        {
            
        }

        //Second optimal approach 3-4ms for all assertions
        [Test]
        public void TestRegex()
        {
            Regex rg = new Regex(@"^[a-zA-Z0-9 ]");
            Assert.IsTrue(rg.IsMatch(a.ToString()));
            Assert.IsTrue(rg.IsMatch(b.ToString()));
            Assert.IsFalse(rg.IsMatch(c.ToString()));
            Assert.IsFalse(rg.IsMatch(d.ToString()));
            Assert.IsTrue(rg.IsMatch(e.ToString()));
        }

        //Most optimal approach < 1ms for all assertions
        [Test]
        public void TestString()
        {
            String x = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789 ";

            Assert.IsTrue(x.Contains(a));
            Assert.IsTrue(x.Contains(b));
            Assert.IsFalse(x.Contains(c));
            Assert.IsFalse(x.Contains(d));
            Assert.IsTrue(x.Contains(e));
        }


        //Least optimal approach 5ms for all assertions
        [Test]
        public void TestChar()
        {

            Assert.IsTrue(char.IsLetterOrDigit(a));
            Assert.IsTrue(char.IsLetterOrDigit(b));
            Assert.IsFalse(char.IsLetterOrDigit(c));
            Assert.IsFalse(char.IsLetterOrDigit(d));
            Assert.IsFalse(char.IsLetterOrDigit(e));
        }
    }
}
