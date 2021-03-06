using ESRI.ArcGIS.Geometry;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Phonatic
{
   public class Tower
    {

        /// <summary>
        /// This is the id of the tower
        /// </summary>
        public string ID { get; set; }

        /// <summary>
        /// Tower type
        /// </summary>
        public string TowerType { get; set; }

        /// <summary>
        /// The network band
        /// </summary>
        public string NetworkBand { get; set; }


        /// <summary>
        /// The tower cost in dollars
        /// </summary>
        public double TowerCost { get; set; }

        /// <summary>
        /// How far the tower can reach
        /// </summary>
        public double TowerCoverage { get; set; }

        /// <summary>
        /// The height of the tower
        /// </summary>
        public double TowerHeight { get; set; }

        /// <summary>
        /// The area of the tower 
        /// </summary>
        public double TowerBaseArea { get; set; }

        /// <summary>
        /// get twoer location of  the tower 
        /// </summary>
        public IPoint TowerLocation { get; set; }
    }
}
