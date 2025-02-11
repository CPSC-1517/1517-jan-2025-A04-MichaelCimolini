using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOPsReview
{
    public static class Utilities
    {
        /// <summary>
        /// Checks if a double value is positive.
        /// 0.0 is assumed positive.
        /// </summary>
        /// <param name="value">The value to asses.</param>
        /// <returns>True if the value is positive or zero, false otherwise.</returns>
        public static bool IsPositive(double value)
        {
            /*
            if (value >= 0)
            {
                return true;
            }

            else
            {
                return false;
            }
            */

            /*
            bool valid = false;

            if (value >= 0.0)
            {
                valid = true;
            }

            return valid;
            */

            return value >= 0.0;
        }

        /// <summary>
        /// Checks if an int value is positive.
        /// 0 is assumed positive.
        /// </summary>
        /// <param name="value">The value to asses.</param>
        /// <returns>True if the value is positive or zero, false otherwise.</returns>
        public static bool IsPositive(int value) => value >= 0;

        /// <summary>
        /// Checks if a decimal value is positive.
        /// 0.0 is assumed positive.
        /// </summary>
        /// <param name="value">The value to asses.</param>
        /// <returns>True if the value is positive or zero, false otherwise.</returns>
        public static bool IsPoitive(decimal value) => value >= 0.0m;
    }
}
