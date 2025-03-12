using Microsoft.EntityFrameworkCore;
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
    public class ShipmentServices
    {
        //readonly so it can only be set in the constructor.
        private readonly WestWindContext _context;

        //internal can only be accessed inside WestWindSystem
        internal ShipmentServices(WestWindContext context)
        {
            _context = context;
        }

        #region Services

        /// <summary>
        /// Returns any records from the given year and month.
        /// </summary>
        /// <param name="year">The year as yyyy (1950-Present)</param>
        /// <param name="month">The month as a 1-2 digit month (1-12)</param>
        /// <returns>List of matching records sorted by Date.</returns>
        public List<Shipment> GetShipmentByYearAndMonth(int year, int month) //ShipmentGet -or- Shipment_Get are also common naming schemes.
        {
            //IEnumerable is the base base base class of List
            //Used here to prevent a database query

            //Version 1
            //IEnumerable<Shipment> record = _context.Shipments
            //                                    //Equivalent to select * from Shipment
            //                                    //              where Year is year
            //                                    //                and Month is month
            //                                    .Where(shipment => shipment.ShippedDate.Year == year &&
            //                                                       shipment.ShippedDate.Month == month)
            //                                    .OrderBy(shipment => shipment.ShippedDate);

            /*
             *  If we want access to our foreign keys, we need to add them to our query.
             *  We can do this in 2 ways:
             *  1) Query both tables and then join them in our C# code. This is a lot of coding and
             *  is prone to errors.
             *  
             *  2) We can let SQL and LINQ do it for us. To do that, we use .Include()
             */

            //V2
            IEnumerable<Shipment> record = _context.Shipments
                                    .Include(shipment => shipment.ShipViaNavigation)
                                    //Equivalent to select * from Shipment
                                    //              where Year is year
                                    //                and Month is month
                                    .Where(shipment => shipment.ShippedDate.Year == year &&
                                                       shipment.ShippedDate.Month == month)
                                    .OrderBy(shipment => shipment.ShippedDate);
            return record.ToList();
        }
        #endregion
    }
}
