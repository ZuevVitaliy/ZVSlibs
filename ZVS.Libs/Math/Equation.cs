namespace ZVS.Libs.Math
{
    public class Equation
    {
        private double a, b, c;

        public Equation(double a, double b, double c)
        {
            this.a = a;
            this.b = b;
            this.c = c;
        }

        public double[] SolveTheSystem(Equation other)
        {
            double[] roots = new double[2];
            roots[0] = (other.b * c / b - other.c) / (other.a - other.b * a / b);
            roots[1] = -(a * roots[0] + c) / b;
            return roots;
        }
    }
}