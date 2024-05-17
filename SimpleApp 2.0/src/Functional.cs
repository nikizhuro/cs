using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace SimpleApp_2._0.src
{
    static internal class Functional
    {
        public static void ApplyPosition(
            this Window window, 
            KeyValuePair<double, double> position
        )
        {
            window.Left = position.Key;
            window.Top  = position.Value;
        }

        public static void toCenter(
            this Window window,
            Window centerPoint
        )
        {
            window.ApplyPosition(Algorithms.toCenterOf(
                new KeyValuePair<double, double>(centerPoint.Left, centerPoint.Top),
                new KeyValuePair<double, double>(centerPoint.Width, centerPoint.Height),
                new KeyValuePair<double, double>(window.Width, window.Height)
            ));
        }
    }
}
