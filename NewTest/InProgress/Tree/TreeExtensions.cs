using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewTest.InProgress.Tree
{
    internal static class TreeExtensions
    {
        public static StringBuilder RemoveLastIndex(this StringBuilder positionBuilder)
        {
            positionBuilder.Remove(positionBuilder.Length - 1, 1);
            while (positionBuilder.Length != 0 && positionBuilder.ToString()[positionBuilder.Length - 1] != '.')
            {
                positionBuilder.Remove(positionBuilder.Length - 1, 1);
            }
            return positionBuilder;
        }
    }
}
