using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace Task1.Tests
{
    [TestFixture]
    public class PolinomeTests
    {
        [Test]
        public void Polinome_SumTest()
        {
            Polinome pol1 = new Polinome(1,2,3);
            Polinome pol2 = new Polinome(1,2,3,4);

            Polinome expected = new Polinome(2,4,6,4);

            Polinome result = pol1 + pol2;           

            Assert.AreEqual(expected,result);
        }

        [Test]
        public void Polinome_DifferenceTest()
        {
            Polinome pol1 = new Polinome(1, 2, 3);
            Polinome pol2 = new Polinome(1, 2, 3, 4);

            Polinome expected = new Polinome(0, 0, 0, 4);

            Polinome result = pol1 - pol2;

            Assert.AreEqual(expected, result);
        }

        [Test]
        public void Polinome_MultipleTest()
        {
            Polinome pol1 = new Polinome(1, 2, 3);
            Polinome pol2 = new Polinome(1, 2, 3, 4);

            Polinome expected = new Polinome(1,5,14,14,13,9);

            Polinome result = pol1 * pol2;

            Assert.AreEqual(expected, result);
        }


        [Test]
        public void Polinome_StringTest()
        {
            Polinome pol1 = new Polinome(1, 2, 3);

            string expected = "+3x^2 +2x^1 +1x^0";

            string result = pol1.ToString();

            Assert.AreEqual(expected, result);
        }

        [Test]
        public void Polinome_EqualsAndHashCodeTest()
        {
            Polinome pol1 = new Polinome(1, 2, 3);
            Polinome pol2 = new Polinome(1, 2, 3);
            object pol3 = pol2;

            bool expected = true;

            bool result1 = pol1.Equals(pol2);
            bool result2 = pol1.Equals(pol3);
            bool result3 = pol2.Equals(pol3);
            bool result4 = pol2 == new Polinome(1,2,3);

            bool result5 = pol1.GetHashCode() == pol2.GetHashCode();
            
            Assert.AreEqual(expected, result1);
            Assert.AreEqual(expected, result2);
            Assert.AreEqual(expected, result3);
            Assert.AreEqual(expected, result4);
            Assert.AreEqual(expected, result5);
        }

    }
}
