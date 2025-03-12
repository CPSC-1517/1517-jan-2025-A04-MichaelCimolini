using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using WestWindSystem.DAL;
using WestWindSystem.Entities;

namespace WestWindSystem.BLL
{
    /// <summary>
    /// Public so it can be accessed from WestWindApp.
    /// </summary>
    public class ShipperServices
    {
        //readonly so it can only be set in the constructor.
        private readonly WestWindContext _context;

        //internal can only be accessed inside WestWindSystem
        internal ShipperServices(WestWindContext context)
        {
            _context = context;
        }

        #region Services

        /// <summary>
        /// Returns a single record from Shipper.
        /// 
        /// A Reminder that returning all is dangerous if you have a large dataset.
        /// </summary>
        /// <returns>List of Shippers or an empty list.</returns>
        public List<Shipper> GetAllShippers() //ShipperGet -or- Shipper_Get are also common naming schemes.
        {
            //IEnumerable is the base base base class of List
            //Used here to prevent a database query
            IEnumerable<Shipper> record = _context.Shippers;

            return record.ToList();
        }
        #endregion
    }
}
