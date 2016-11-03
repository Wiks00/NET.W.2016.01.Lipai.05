using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Task1
{
    public class Polinome : IEnumerable<double>
    {
        /// <summary>
        /// Polinom coefficients
        /// </summary>
        private readonly double[] coefficients;

        /// <summary>
        /// Count of coefficients
        /// </summary>
        public int Count => coefficients.Length;

        #region Constructors
        /// <summary>
        /// Default constructor
        /// </summary>
        public Polinome()
        {
            coefficients = new double[0];
        }

        /// <summary>
        /// Constructor create polinom with set size
        /// </summary>
        /// <param name="length">length of created polinom</param>
        public Polinome(int length)
        {
            if(length < 0)
                throw new ArgumentOutOfRangeException();
            coefficients = new double[length];
        }

        /// <summary>
        /// Constructor create polinom with set values
        /// </summary>
        /// <param name="values">values of created polinom</param>
        public Polinome(params double[] values)
        {
            if (ReferenceEquals(null, values))
                throw new NullReferenceException();

            coefficients = new double[values.Length];
            values.CopyTo(coefficients, 0);
        }

        /// <summary>
        /// Constructor create polinom with set values by given enumerable type
        /// </summary>
        /// <param name="enumerator">enumirable type</param>
        public Polinome(IEnumerable<double> enumerator)
        {
            if (ReferenceEquals(null, enumerator))
                throw new NullReferenceException();

            coefficients = new double[enumerator.Count()];

            enumerator.ToArray().CopyTo(coefficients,0);
        }
        #endregion

        #region Enumerators
        /// <summary>
        /// Create enumerator for enumeration
        /// </summary>
        /// <returns>enumirator</returns>
        public IEnumerator<double> GetEnumerator()
        {
            return ((IEnumerable<double>)coefficients).GetEnumerator();
        }

        /// <summary>
        /// Create enumerator for enumeration
        /// </summary>
        /// <returns>enumirator</returns>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
        #endregion

        #region Operators
        /// <summary>
        /// Calculate sum of two polynomial
        /// </summary>
        /// <param name="lhs">first polynomial</param>
        /// <param name="rhs">second polynomial</param>
        /// <returns>calculated polynomial</returns>
        public static Polinome operator +(Polinome lhs, Polinome rhs)
        {
            if (ReferenceEquals(null, lhs) || ReferenceEquals(null, rhs))
                throw new NullReferenceException();

                Polinome min, max;

            if (lhs.Count >= rhs.Count)
            {
                min = rhs;
                max = lhs;
            }
            else
            {
                min = lhs;
                max = rhs;
            }

            double[] reuslPolinome = new double[max.Count];

            for (var i = 0; i < max.Count; i++)
            {
                if (i < min.Count)
                    reuslPolinome[i] = min[i] + max[i];
                else
                    reuslPolinome[i] = max[i];
            }

            return new Polinome(reuslPolinome);
        }

        /// <summary>
        /// Calculate difference of two polynomials
        /// </summary>
        /// <param name="lhs">first polynomial</param>
        /// <param name="rhs">second polynomial</param>
        /// <returns>calculated polynomial</returns>
        public static Polinome operator -(Polinome lhs, Polinome rhs)
        {
            if (ReferenceEquals(null, lhs) || ReferenceEquals(null, rhs))
                throw new NullReferenceException();

            Polinome min, max;

            if (lhs.Count >= rhs.Count)
            {
                min = rhs;
                max = lhs;
            }
            else
            {
                min = lhs;
                max = rhs;
            }

            double[] reuslPolinome = new double[max.Count];

            for (var i = 0; i < max.Count; i++)
            {
                if (i < min.Count)
                    reuslPolinome[i] = min[i] - max[i];
                else
                    reuslPolinome[i] = max[i];
            }

            return new Polinome(reuslPolinome);

        }

        /// <summary>
        /// Calculate multiple of two polynomials
        /// </summary>
        /// <param name="lhs">first polynomial</param>
        /// <param name="rhs">second polynomial</param>
        /// <returns>calculated polynomial</returns>
        public static Polinome operator *(Polinome lhs, Polinome rhs)
        {
            if (ReferenceEquals(null, lhs) || ReferenceEquals(null, rhs))
                throw new NullReferenceException();

            double[] reuslPolinome = new double[lhs.Count + rhs.Count - 1];

            for (var i = 0; i < lhs.Count; i++)
            {
                for (int j = 0; j < rhs.Count; j++)
                {
                    reuslPolinome[i + j] += lhs[i] * rhs[i];
                }      
            }

            return new Polinome(reuslPolinome);
        }

        /// <summary>
        /// Check on equals polynomials
        /// </summary>
        /// <param name="lhs">first polynomial</param>
        /// <param name="rhs">second polynomial</param>
        /// <returns>true or false</returns>
        public static bool operator ==(Polinome lhs, Polinome rhs)
        {
            if (ReferenceEquals(null, lhs) || ReferenceEquals(null, rhs))
                return false;
            return ReferenceEquals(lhs, rhs) || lhs.Equals(rhs);
        }

        /// <summary>
        /// Check on not equals polynomials
        /// </summary>
        /// <param name="lhs">first polynomial</param>
        /// <param name="rhs">second polynomial</param>
        /// <returns>true or false</returns>
        public static bool operator !=(Polinome lhs, Polinome rhs)
        {
            return !(lhs == rhs);
        }
        #endregion

        #region Object methods
        /// <summary>
        /// Check on equals polynomial and object
        /// </summary>
        /// <param name="obj">inspected object</param>
        /// <returns>true or false</returns>
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj))
                return false;
            return ReferenceEquals(this, obj) || Equals(obj as Polinome);
        }

        /// <summary>
        /// Check on equals polynomials
        /// </summary>
        /// <param name="obj">inspected polynomial</param>
        /// <returns>true or false</returns>
        public bool Equals(Polinome polinome)
        {
            if (ReferenceEquals(null, polinome))
                return false;
            if (Count != polinome.Count)
                return false;
            for (int i = 0; i < Count -1; i++)
            {
                if (coefficients[i] != polinome[i])
                    return false;
            }
            return true;
        }

        /// <summary>
        /// Generate hash code for polynomial
        /// </summary>
        /// <returns>hash code of polynomial</returns>
        public override int GetHashCode()
        {
            int hashCode = 1;
            for (int i = 0; i <= coefficients.Length - 1; i++)
                hashCode = hashCode * 8 + coefficients[i].GetHashCode();
            return hashCode * 174;
        }

        /// <summary>
        /// Convert  polynomial to string
        /// </summary>
        /// <returns>string representation of polynomial</returns>
        public override string ToString()
        {
            var str = string.Empty;
            for (var i = coefficients.Length - 1; i != -1; i--)
            {
                if (coefficients[i] < 0)
                    str += $"{coefficients[i]}x^{i} ";
                else
                    str += $"+{coefficients[i]}x^{i} ";
            }
            return str.TrimEnd(" "[0]);
        }
        #endregion

        private double this[int index]
        {
            get
            {
                if (index < 0 || index > Count)
                    throw new IndexOutOfRangeException();
                return coefficients[index];
            }
        }
    }
}
