using System;

namespace ZVSlibs.Action3D
{
    /// <summary>
    /// Класс для работы с векторами в пространстве.
    /// </summary>
    public class Vector3D
    {
        /// <summary>
        /// Модуль (длина) вектора.
        /// </summary>
        private double module;

        /// <summary>
        /// Координата.
        /// </summary>
        private double x, y, z;

        /// <summary>
        /// Создание нового вектора.
        /// </summary>
        /// <param name="x">Координата x.</param>
        /// <param name="y">Координата y.</param>
        /// <param name="z">Координата z.</param>
        public Vector3D(double x, double y, double z)
        {
            this.x = x;
            this.y = y;
            this.z = z;
            Init();
        }

        /// <summary>
        /// Создание нового вектора из двух точек.
        /// </summary>
        /// <param name="start">Точка начала вектора.</param>
        /// <param name="end">Точка конца вектора.</param>
        public Vector3D(Point3D start, Point3D end)
        {
            Vector3D temp = end - start;
            this.x = temp.x;
            this.y = temp.y;
            this.z = temp.z;
            this.module = temp.module;
            this.H_Angle = temp.H_Angle;
            this.V_Angle = temp.V_Angle;
        }

        /// <summary>
        /// Создание нового вектора.
        /// </summary>
        /// <param name="coordinates">Массив из 3-х координат.</param>
        public Vector3D(double[] coordinates)
        {
            this.x = coordinates[0];
            this.y = coordinates[1];
            this.z = coordinates[2];
            Init();
        }

        /// <summary>
        /// Угол горизонтального склонения.
        /// </summary>
        public double H_Angle { get; private set; }

        /// <summary>
        /// Угол вертикального склонения.
        /// </summary>
        public double V_Angle { get; private set; }

        public static Vector3D operator -(Vector3D left, Vector3D right)
        {
            return left + right.Reverse();
        }

        public static Vector3D operator *(Vector3D left, double right)
        {
            double[] result = new double[3];
            result[0] = left.x * right;
            result[1] = left.y * right;
            result[2] = left.z * right;
            return new Vector3D(result);
        }

        public static Vector3D operator *(double left, Vector3D right)
        {
            return right * left;
        }

        public static Vector3D operator +(Vector3D left, Vector3D right)
        {
            double[] result = new double[3];
            result[0] = left.x + right.x;
            result[1] = left.y + right.y;
            result[2] = left.z + right.z;
            return new Vector3D(result);
        }

        /// <summary>
        /// Возвращает угол между векторами относительно одной плоскости.
        /// </summary>
        /// <param name="otherVector">Сравниваемый вектор.</param>
        /// <returns>Угол между векторами.</returns>
        public double GetAngleTo(Vector3D otherVector)
        {
            return Math.Acos((x * otherVector.x + y * otherVector.y + z * otherVector.z) / (module * otherVector.module));
        }

        /// <summary>
        /// Возвращает координаты вектора в виде массива чисел, где индексы
        /// [0] - x,
        /// [1] - y,
        /// [2] - z.
        /// </summary>
        /// <returns>Массив чисел с координатами вектора.</returns>
        public double[] GetCoordinates()
        {
            return new double[] { x, y, z };
        }

        /// <summary>
        /// Возващает противоположный вектор текущему.
        /// </summary>
        /// <returns>Противоположный вектор.</returns>
        public Vector3D Reverse()
        {
            return this * -1;
        }

        /// <summary>
        /// Вычисляет модуль и углы вектора, исходя из его координат.
        /// </summary>
        private void Init()
        {
            this.module = Math.Sqrt(x * x + y * y + z * z);
            this.V_Angle = Math.Acos(y / module);
            this.H_Angle = Math.Acos(z / module);
        }

        /// <summary>
        /// Выводит в консоль координаты вектора.
        /// </summary>
        public void PrintPoint()
        {
            Console.WriteLine($"x={x}, y={y}, z={z}");
        }
    }
}