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
    public class BuildVersionServices
    {
        //readonly so it can only be set in the constructor.
        private readonly WestWindContext _context;

        //internal can only be accessed inside WestWindSystem
        internal BuildVersionServices(WestWindContext context)
        {
            _context = context;
        }

        #region Services

        /// <summary>
        /// Returns a single record from BuildVersion.
        /// </summary>
        /// <returns>First record or null.</returns>
        public BuildVersion? GetBuildVersion() //BuildVersionGet -or- BuildVersion_Get are also common naming schemes.
        {
            //IEnumerable is the base base base class of List
            //Used here to prevent a database query
            IEnumerable<BuildVersion> record = _context.BuildVersions;

            return record.FirstOrDefault();
        }
        #endregion
    }
}
