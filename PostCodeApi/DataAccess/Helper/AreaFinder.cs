using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Helper
{
  

    public class AreaFinder : IAreaFinder
    {
        /// <summary>
        /// This action used to get Area details based on latitude
        /// </summary>
        /// <param name="latitude">latitude</param>
        /// <returns></returns>
        public string GetArea(double latitude)
        {
            if (latitude < Constants.Constants.LatitudeSouth) return Constants.Constants.South;
            if (latitude >= Constants.Constants.LatitudeNorth) return Constants.Constants.North;
            if (Constants.Constants.LatitudeSouth <= latitude && latitude < Constants.Constants.LatitudeNorth) return Constants.Constants.Midlands;
            return string.Empty;
        }
    }
}
