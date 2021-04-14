using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZVS.Libs.Physics
{
    public class Volume
    {
        public static double[] MethodOfConnectedVolumes(IEnumerable<double> source)
        {
            if (!(source is ICollection<double>)) source = source.ToList();
            int lenght = source.Count();
            double average = source.Average(x => x);
            double[] result = new double[lenght];
            for (int i = 0; i < lenght; i++)
            {
                result[i] = average;
            }

            return result;
        }
    }
}
