using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Task1
{
    public class Polinome : IEquatable<Polinome> , ICloneable
    {
        /// <summary>
        /// Polinom coefficients
        /// </summary>
        private readonly double[] coefficients;

        /// <summary>
        /// Rounding precision
        /// </summary>
        private static double epsilon;

        /// <summary>
        /// Count of coefficients
        /// </summary>
        public int Count => coefficients.Length;

        public static double Epsilon
        {
            get { return epsilon; }
            private set
            {
                if (value <= 0 || value >= 1)
                    throw new ArgumentOutOfRangeException();

                epsilon = value;
            }

        }

        #region Constructors
        /// <summary>
        /// Default constructor
        /// </summary>
        public Polinome()
        {
            coefficients = new double[0];
        }

        /// <summary>
        /// Constructor create polinom with set values
        /// </summary>
        /// <param name="values">values of created polinom</param>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public Polinome(params double[] values)
        {
            if (ReferenceEquals(null, values))
                throw new ArgumentNullException();
            if (values.Length == 0)
                throw new ArgumentOutOfRangeException();

            coefficients = new double[values.Length];
            values.CopyTo(coefficients, 0);
        }

        static Polinome()
        {
            try
            {
                string epsStr = ConfigurationManager.AppSettings["epsilon"];
                Epsilon = double.Parse(epsStr);
            }
            catch (ConfigurationErrorsException ex)
            {
                throw new ConfigurationErrorsException("Can't get epsilon", ex);
            }
            catch (FormatException ex)
            {
                throw new ConfigurationErrorsException("Invalid format", ex);
            }
            catch (Exception ex)
            {
                throw new ConfigurationErrorsException("Invalid value", ex);
            }

        }
        #endregion

        #region Operators
        public static Polinome operator +(Polinome polinome)
        {
            if (ReferenceEquals(polinome, null))
                throw new ArgumentNullException();

            return polinome;
        }

        public static Polinome operator -(Polinome polinome)
        {
            if (ReferenceEquals(polinome, null))
                throw new ArgumentNullException();

            var rev = polinome.Clone();
            for (int i = 0; i < rev.Count; i++)
            {
                rev[i] = rev[i]*-1;
            }

            return rev;
        }

        /// <summary>
        /// Calculate sum of two polynomial
        /// </summary>
        /// <param name="lhs">first polynomial</param>
        /// <param name="rhs">second polynomial</param>
        /// <returns>calculated polynomial</returns>
        /// <exception cref="ArgumentNullException"></exception>
        public static Polinome operator +(Polinome lhs, Polinome rhs)
        {
            if (ReferenceEquals(null, lhs) || ReferenceEquals(null, rhs))
                throw new ArgumentNullException();

            Polinome min = lhs.Count >= rhs.Count ? rhs : lhs;
            Polinome max = lhs.Count >= rhs.Count ? lhs : rhs;

            var reuslPolinome = max.Clone();

            for (var i = 0; i < max.Count; i++)
            {
                if (i < min.Count)
                    reuslPolinome[i] += min[i];
            }

            return reuslPolinome;
        }

        /// <summary>
        /// Calculate difference of two polynomials
        /// </summary>
        /// <param name="lhs">first polynomial</param>
        /// <param name="rhs">second polynomial</param>
        /// <returns>calculated polynomial</returns>
        /// <exception cref="ArgumentNullException"></exception>
        public static Polinome operator -(Polinome lhs, Polinome rhs)
        {
            if (ReferenceEquals(null, lhs) || ReferenceEquals(null, rhs))
                throw new ArgumentNullException();

            return lhs + -rhs;

        }

        /// <summary>
        /// Calculate multiple of two polynomials
        /// </summary>
        /// <param name="lhs">first polynomial</param>
        /// <param name="rhs">second polynomial</param>
        /// <returns>calculated polynomial</returns>
        /// <exception cref="ArgumentNullException"></exception>
        public static Polinome operator *(Polinome lhs, Polinome rhs)
        {
            if (ReferenceEquals(null, lhs) || ReferenceEquals(null, rhs))
                throw new ArgumentNullException();

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

        public static Polinome Add(Polinome lhs, Polinome rhs)
        {
            return lhs + rhs;
        }

        public static Polinome Subtract(Polinome lhs, Polinome rhs)
        {
            return lhs - rhs;
        }

        public static Polinome Multiple(Polinome lhs, Polinome rhs)
        {
            return lhs * rhs;
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
            if (ReferenceEquals(null, obj) || obj.GetType() != GetType())
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
        /// Clone polynomial 
        /// </summary>
        /// <returns>new polynomial</returns>
        public Polinome Clone()
        {
            return new Polinome(coefficients);
        }

        /// <summary>
        /// Interface method , clone polynomial 
        /// </summary>
        /// <returns>new polynomial</returns>
        object ICloneable.Clone()
        {
            return Clone();
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
                    str += $" + {(Math.Abs(coefficients[i] - 1) <= Epsilon ? "" : coefficients[i].ToString("##,###"))}x^{i}";
                else
                    str += $" - {(Math.Abs(coefficients[i] + 1) <= Epsilon ? "" : (-coefficients[i]).ToString("##,###"))}x^{i}";
            }
            return str.TrimEnd(" "[0]);
        }
        #endregion

        private double this[int index]
        {
            get
            {
                if (index < 0 || index > Count)
                    throw new ArgumentOutOfRangeException();
                return coefficients[index];
            }

            set
            {
                if (index < 0 || index > Count)
                    throw new ArgumentOutOfRangeException();
                coefficients[index] = value;
            }
        }
    }
}
