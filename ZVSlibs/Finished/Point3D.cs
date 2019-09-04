using System;

namespace ZVSlibs.Action3D
{
    /// <summary>
    /// Класс для работы с точками в пространстве.
    /// </summary>
    public class Point3D
    {
        /// <summary>
        /// Координата.
        /// </summary>
        private double x, y, z;

        /// <summary>
        /// Создание новой точки.
        /// </summary>
        /// <param name="x">Координата x.</param>
        /// <param name="y">Координата y.</param>
        /// <param name="z">Координата z.</param>
        public Point3D(double x, double y, double z)
        {
            this.x = x;
            this.y = y;
            this.z = z;
        }

        /// <summary>
        /// Создание новой точки.
        /// </summary>
        /// <param name="coordinates">Массив из 3-х координат.</param>
        public Point3D(double[] coordinates)
        {
            this.x = coordinates[0];
            this.y = coordinates[1];
            this.z = coordinates[2];
        }

        public static Vector3D operator -(Point3D left, Point3D right)
        {
            double[]
                start = left.GetCoordinates(),
                end = right.GetCoordinates(),
                result = new double[3];
            for (int i = 0; i < result.Length; i++)
            {
                result[i] = end[i] - start[i];
            }
            return new Vector3D(result);
        }

        /// <summary>
        /// Возвращает координаты точки в виде массива чисел, где индексы
        /// [0] - x,
        /// [1] - y,
        /// [2] - z.
        /// </summary>
        /// <returns>Массив чисел с координатами точки.</returns>
        public double[] GetCoordinates()
        {
            return new double[] { x, y, z };
        }

        /// <summary>
        /// Возвращает расстояние от текущей точки до другой.
        /// </summary>
        /// <param name="otherPoint">Точка, до которой измеряется расстояние.</param>
        /// <returns>Расстояние между точками.</returns>
        public double GetDistance(Point3D otherPoint)
        {
            double
                _x = otherPoint.x - x,
                _y = otherPoint.y - y,
                _z = otherPoint.z - z;
            return Math.Sqrt(_x * _x + _y * _y + _z * _z);
        }

        /// <summary>
        /// Перемещает точку из одной позиции в другую.
        /// </summary>
        /// <param name="vector">Вектор смещения точки.</param>
        public void MovePoint(Vector3D vector)
        {
            double[] v = vector.GetCoordinates();
            this.x += v[0];
            this.y += v[1];
            this.z += v[2];
        }

        /// <summary>
        /// Возвращает точку, между текущей и другой, находящейся на желаемом расстоянии от второй.
        /// </summary>
        /// <param name="otherPoint">Точка, от которой будет найдена новая точка.</param>
        /// <param name="distance">Расстояние от второй точки.</param>
        /// <returns>Новая точка между текущей и другой.</returns>
        public Point3D NearPoint(Point3D otherPoint, double distance)
        {
            double
                l = otherPoint.x - x,
                m = otherPoint.y - y,
                n = otherPoint.z - z;
            double t1, t2;
            t1 = Math.Sqrt(distance * distance / (l * l + m * m + n * n));
            t2 = t1 * -1;
            Point3D[] result = new Point3D[] {
            new Point3D(l * t1 + otherPoint.x, m * t1 + otherPoint.y, n * t1 + otherPoint.z),
            new Point3D(l * t2 + otherPoint.x, m * t2 + otherPoint.y, n * t2 + otherPoint.z)
        };
            if (GetDistance(result[0]) < GetDistance(result[1])) return result[0];
            else return result[1];
        }

        /// <summary>
        /// Выводит в консоль координаты точки.
        /// </summary>
        public void PrintPoint()
        {
            Console.WriteLine($"x={x}, y={y}, z={z}");
        }
    }
}