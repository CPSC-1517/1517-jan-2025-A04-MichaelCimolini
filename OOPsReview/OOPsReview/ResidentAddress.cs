using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOPsReview
{
    /// <summary>
    /// A Record for holding information about a Person(s) physical address.
    /// </summary>
    /// <param name="Number">House Number.</param>
    /// <param name="Street">Street Name/Number.</param>
    /// <param name="City">City of residence.</param>
    /// <param name="Province">Province of Residence (Canadian Province).</param>
    /// <param name="PostalCode">Postal Code (X#X #X#)</param>
    public record ResidentAddress(int Number, String Street, String City, String Province, String PostalCode)
    {
        /// <summary>
        /// Returns a CSV representation of the data in this record.
        /// </summary>
        /// <returns>The CSV string created (Number,Street,City,Province,Postal Code).</returns>
        public override string ToString()
        {
            return $"{Number},{Street},{City},{Province},{PostalCode}";
        }
    }
}
