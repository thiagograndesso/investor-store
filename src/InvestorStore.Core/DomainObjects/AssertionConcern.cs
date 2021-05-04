using System.Text.RegularExpressions;

namespace InvestorStore.Core.DomainObjects
{
    public static class AssertionConcern
    {
        public static void ThrowIfEqual(object obj1, object obj2, string message)
        {
            if (obj1.Equals(obj2))
            {
                throw new DomainException(message);
            }
        }

        public static void ThrowIfDifferent(object obj1, object obj2, string message)
        {
            if (!obj1.Equals(obj2))
            {
                throw new DomainException(message);
            }
        }

        public static void ThrowIfDifferent(string pattern, string value, string message)
        {
            var regex = new Regex(pattern);

            if (!regex.IsMatch(value))
            {
                throw new DomainException(message);
            }
        }

        public static void ValidateLength(string value, int max, string message)
        {
            var length = value.Trim().Length;
            if (length > max)
            {
                throw new DomainException(message);
            }
        }

        public static void ValidateLength(string value, int min, int max, string message)
        {
            var length = value.Trim().Length;
            if (length < min || length > max)
            {
                throw new DomainException(message);
            }
        }
        
        public static void ThrowIfEmpty(string value, string message)
        {
            if (value == null || value.Trim().Length == 0)
            {
                throw new DomainException(message);
            }
        }

        public static void ThrowIfNull(object obj1, string message)
        {
            if (obj1 == null)
            {
                throw new DomainException(message);
            }
        }

        public static void ValidateMinMax(double value, double min, double max, string message)
        {
            if (value < min || value > max)
            {
                throw new DomainException(message);
            }
        }

        public static void ValidateMinMax(float value, float min, float max, string message)
        {
            if (value < min || value > max)
            {
                throw new DomainException(message);
            }
        }

        public static void ValidateMinMax(int value, int min, int max, string message)
        {
            if (value < min || value > max)
            {
                throw new DomainException(message);
            }
        }

        public static void ValidateMinMax(long value, long min, long max, string message)
        {
            if (value < min || value > max)
            {
                throw new DomainException(message);
            }
        }

        public static void ValidateMinMax(decimal value, decimal min, decimal max, string message)
        {
            if (value < min || value > max)
            {
                throw new DomainException(message);
            }
        }

        public static void ThrowIfLessThan(long value, long min, string message)
        {
            if (value < min)
            {
                throw new DomainException(message);
            }
        }

        public static void ThrowIfLessThan(double value, double min, string message)
        {
            if (value < min)
            {
                throw new DomainException(message);
            }
        }

        public static void ThrowIfLessThan(decimal value, decimal min, string message)
        {
            if (value < min)
            {
                throw new DomainException(message);
            }
        }

        public static void ThrowIfLessThan(int value, int min, string message)
        {
            if (value < min)
            {
                throw new DomainException(message);
            }
        }

        public static void ThrowIfFalse(bool boolValue, string message)
        {
            if (!boolValue)
            {
                throw new DomainException(message);
            }
        }

        public static void ThrowIfTrue(bool boolValue, string message)
        {
            if (boolValue)
            {
                throw new DomainException(message);
            }
        }
    }
}