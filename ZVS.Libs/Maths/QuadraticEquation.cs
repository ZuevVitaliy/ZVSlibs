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
                return new double[0];
            }
            else
            {
                return new[] { -b / (2 * a) };
            }
        }

        public static string GetTextResolve(double a, double b, double c)
        {
            double[] result = GetResult(a, b, c);
            switch (result.Length)
            {
                case 0:
                    return $"Квадратное уравнение вида {a}x^2 + {b}x + {c} не имеет корней";
                case 1:
                    return $"Квадратное уравнение вида {a}x^2 + {b}x + {c} имеет дискриминант, равный D=0, с корнем X={result[0]}";
                default:
                    return $"Квадратное уравнение вида {a}x^2 + {b}x + {c} \n" +
                           $"имеет дискриминант, равный D={result[2]}, с корнями" +
                           $"X1={result[0]}" +
                           $"X2={result[1]}";
            }
        }

        private static double GetDiscriminant(double a, double b, double c)
        {
            return b * b - 4 * a * c;
        }
    }
}