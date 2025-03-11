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
    public class RegionServices
    {
        //readonly so it can only be set in the constructor.
        private readonly WestWindContext _context;

        //internal can only be accessed inside WestWindSystem
        internal RegionServices(WestWindContext context)
        {
            _context = context;
        }

        #region Services

        /// <summary>
        /// Returns all records from Region.
        /// 
        /// This is a dangerous service. This will cause problems
        /// if your table has a large number of records.
        /// </summary>
        /// <returns>All records or an empty list.</returns>
        public List<Region> GetAllRegions() //RegionGet -or- Region_Get are also common naming schemes.
        {
            //IEnumerable is the base base base class of List
            //Used here to prevent a database query
            IEnumerable<Region> records = _context.Regions;

            return records.OrderBy(record => record.RegionDescription).ToList();
        }

        public Region GetRegionByID(int id)
        {
            Region region = null;

            //FirstOrDefault can take a predicate
            region = _context.Regions.FirstOrDefault(
                x => x.RegionID == id
                );

            return region;
        }
        #endregion
    }
}
