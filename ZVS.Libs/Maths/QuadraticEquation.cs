using System;

namespace ZVS.Libs.Maths
{
    public static class QuadraticEquation
    {
        public static double[] GetResult(double a, double b, double c)
        {
            double D = GetDiscriminant(a, b, c);
            if (D > 0)
            {
                double sqrtD = Math.Sqrt(D);
                return new[]
                {
                    (-b + sqrtD) / (2 * a),
                    (-b - sqrtD) / (2 * a),
                    D
                };
            }
            else if (D < 0)
            {
                return null;
            }
            else
            {
                return new[] { -b / (2 * a) };
            }
        }

        public static string GetTextResolve(double a, double b, double c)
        {
            double[] result = GetResult(a, b, c);
            if (result == null)
            {
                return string.Format(@"Квадратное уравнение вида {0}x^2 + {1}x + {2} не имеет корней", a, b, c);
            }
            else if (result.Length == 1)
            {
                return string.Format(@"Квадратное уравнение вида {0}x^2 + {1}x + {2} 
                                     имеет дискриминант, равный D=0, с корнем X={3}",
                    a, b, c, result[0]);
            }
            else
            {
                return string.Format(@"Квадратное уравнение вида {0}x^2 + {1}x + {2} 
                                     имеет дискриминант, равный D={3}, с корнями
                                     X1={4}
                                     X2={5}",
                    a, b, c, result[2], result[0], result[1]);
            }
        }

        private static double GetDiscriminant(double a, double b, double c)
        {
            return b * b - 4 * a * c;
        }
    }
}