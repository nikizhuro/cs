using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleApp_2._0
{
    internal static class Algorithms
    {
        public static KeyValuePair<double,double> toCenterOf(
            KeyValuePair<double, double> parentCoordinates, 
            KeyValuePair<double, double> parentSize,
            KeyValuePair<double, double> currentSize
        )
        {
            return new KeyValuePair<double, double>(
                parentCoordinates.Key + (parentSize.Key - currentSize.Value)/2,
                parentCoordinates.Value + (parentSize.Value - currentSize.Value)/2
            );
        }

    }
}
