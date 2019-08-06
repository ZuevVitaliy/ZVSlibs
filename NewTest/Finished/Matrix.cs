using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZVSlibs.Matrix
{
    class NotSquareException : Exception { }

    /// <summary>
    /// Класс, предоставляющий операции для работы с матрицами.
    /// </summary>
    public class Matrix
    {
        /// <summary>
        /// Создает матрицу из массива-сетки.
        /// </summary>
        /// <param name="matrix">Массив-сетка.</param>
        public Matrix(byte[,] matrix)
        {
            this.MatrixGrid = new double[matrix.GetLength(0), matrix.GetLength(1)];
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    this.MatrixGrid[i, j] = matrix[i, j];
                }
            }
        }

        /// <summary>
        /// Создает матрицу из массива-сетки.
        /// </summary>
        /// <param name="matrix">Массив-сетка.</param>
        public Matrix(int[,] matrix)
        {
            this.MatrixGrid = new double[matrix.GetLength(0), matrix.GetLength(1)];
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    this.MatrixGrid[i, j] = matrix[i, j];
                }
            }
        }

        /// <summary>
        /// Создает матрицу из массива-сетки.
        /// </summary>
        /// <param name="matrix">Массив-сетка.</param>
        public Matrix(float[,] matrix)
        {
            this.MatrixGrid = new double[matrix.GetLength(0), matrix.GetLength(1)];
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    this.MatrixGrid[i, j] = matrix[i, j];
                }
            }
        }

        /// <summary>
        /// Создает матрицу из массива-сетки.
        /// </summary>
        /// <param name="matrix">Массив-сетка.</param>
        public Matrix(double[,] matrix)
        {
            this.MatrixGrid = matrix;
        }

        /// <summary>
        /// Создает матрицу из массива-сетки.
        /// </summary>
        /// <param name="matrix">Массив-сетка.</param>
        public Matrix(byte[][] matrix)
        {
            int maxRowLength = 0;
            foreach (byte[] item in matrix)
            {
                if (item.Length > maxRowLength) maxRowLength = item.Length;
            }
            this.MatrixGrid = new double[matrix.Length, maxRowLength];
            for (int i = 0; i < matrix.Length; i++)
            {
                for (int j = 0; j < maxRowLength; j++)
                {
                    try
                    {
                        this.MatrixGrid[i, j] = matrix[i][j];
                    }
                    catch
                    {
                        this.MatrixGrid[i, j] = 0;
                    }
                }
            }
        }

        /// <summary>
        /// Создает матрицу из массива-сетки.
        /// </summary>
        /// <param name="matrix">Массив-сетка.</param>
        public Matrix(int[][] matrix)
        {
            int maxRowLength = 0;
            foreach (int[] item in matrix)
            {
                if (item.Length > maxRowLength) maxRowLength = item.Length;
            }
            this.MatrixGrid = new double[matrix.Length, maxRowLength];
            for (int i = 0; i < matrix.Length; i++)
            {
                for (int j = 0; j < maxRowLength; j++)
                {
                    try
                    {
                        this.MatrixGrid[i, j] = matrix[i][j];
                    }
                    catch
                    {
                        this.MatrixGrid[i, j] = 0;
                    }
                }
            }
        }

        /// <summary>
        /// Создает матрицу из массива-сетки.
        /// </summary>
        /// <param name="matrix">Массив-сетка.</param>
        public Matrix(float[][] matrix)
        {
            int maxRowLength = 0;
            foreach (float[] item in matrix)
            {
                if (item.Length > maxRowLength) maxRowLength = item.Length;
            }
            this.MatrixGrid = new double[matrix.Length, maxRowLength];
            for (int i = 0; i < matrix.Length; i++)
            {
                for (int j = 0; j < maxRowLength; j++)
                {
                    try
                    {
                        this.MatrixGrid[i, j] = matrix[i][j];
                    }
                    catch
                    {
                        this.MatrixGrid[i, j] = 0;
                    }
                }
            }
        }

        /// <summary>
        /// Создает матрицу из массива-сетки.
        /// </summary>
        /// <param name="matrix">Массив-сетка.</param>
        public Matrix(double[][] matrix)
        {
            int maxRowLength = 0;
            foreach (double[] item in matrix)
            {
                if (item.Length > maxRowLength) maxRowLength = item.Length;
            }
            this.MatrixGrid = new double[matrix.Length, maxRowLength];
            for (int i = 0; i < matrix.Length; i++)
            {
                for (int j = 0; j < maxRowLength; j++)
                {
                    try
                    {
                        this.MatrixGrid[i, j] = matrix[i][j];
                    }
                    catch
                    {
                        this.MatrixGrid[i, j] = 0;
                    }
                }
            }
        }

        /// <summary>
        /// Создает единичную матрицу размерностью size строк и столбцов.
        /// </summary>
        /// <param name="size">Размерность матрицы.</param>
        public Matrix(int size)
        {
            this.MatrixGrid = new double[size, size];
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    if (i == j) MatrixGrid[i, j] = 1;
                    else MatrixGrid[i, j] = 0;
                }
            }
        }

        /// <summary>
        /// Создает матрицу размерностью size[0] строк и size[1] столбцов, заполненную нулями.
        /// </summary>
        /// <param name="size">Размерность матрицы</param>
        public Matrix(byte[] size)
        {
            double[,] m = new double[size[0], size[1]];
            for (int i = 0; i < size[0]; i++)
            {
                for (int j = 0; j < size[1]; j++)
                {
                    m[i, j] = 0;
                }
            }
            this.MatrixGrid = m;
        }

        /// <summary>
        /// Создает матрицу размерностью size[0] строк и size[1] столбцов, заполненную нулями.
        /// </summary>
        /// <param name="size">Размерность матрицы</param>
        public Matrix(int[] size)
        {
            this.MatrixGrid = new double[size[0], size[1]];
            for (int i = 0; i < size[0]; i++)
            {
                for (int j = 0; j < size[1]; j++)
                {
                    MatrixGrid[i, j] = 0;
                }
            }
        }

        /// <summary>
        /// Получить сетку матрицы.
        /// </summary>
        public double[,] MatrixGrid { get; }

        public static Matrix operator -(Matrix left, Matrix right)
        {
            Matrix[] both = ToEqualSize(left, right);
            left = both[0];
            right = both[1];
            int[] maxSize = left.GetSize();
            Matrix result = new Matrix(maxSize);
            for (int i = 0; i < maxSize[0]; i++)
            {
                for (int j = 0; j < maxSize[1]; j++)
                {
                    result.MatrixGrid[i, j] = left.MatrixGrid[i, j] - right.MatrixGrid[i, j];
                }
            }
            return result;
        }

        public static Matrix operator *(Matrix left, double right)
        {
            for (int i = 0; i < left.MatrixGrid.GetLength(0); i++)
            {
                for (int j = 0; j < left.MatrixGrid.GetLength(1); j++)
                {
                    left.MatrixGrid[i, j] *= right;
                }
            }
            return left;
        }

        public static Matrix operator *(double left, Matrix right)
        {
            return right * left;
        }

        public static Matrix operator *(Matrix left, Matrix right)
        {
            if (left.MatrixGrid.GetLength(1) == right.MatrixGrid.GetLength(0))
            {
                Matrix result = new Matrix(new byte[left.MatrixGrid.GetLength(0), right.MatrixGrid.GetLength(1)]);
                for (int i = 0; i < result.MatrixGrid.GetLength(0); i++)
                {
                    for (int j = 0; j < result.MatrixGrid.GetLength(1); j++)
                    {
                        for (int n = 0; n < left.MatrixGrid.GetLength(1); n++)
                        {
                            result.MatrixGrid[i, j] += left.MatrixGrid[i, n] * right.MatrixGrid[n, j];
                        }
                    }
                }
                return result;
            }
            else throw new Exception("Операция невозможна, несопоставимая размерность матриц");
        }

        public static Matrix operator +(Matrix left, Matrix right)
        {
            Matrix[] both = ToEqualSize(left, right);
            left = both[0];
            right = both[1];
            int[] maxSize = left.GetSize();
            Matrix result = new Matrix(maxSize);
            for (int i = 0; i < maxSize[0]; i++)
            {
                for (int j = 0; j < maxSize[1]; j++)
                {
                    result.MatrixGrid[i, j] = left.MatrixGrid[i, j] + right.MatrixGrid[i, j];
                }
            }
            return result;
        }

        /// <summary>
        /// Возвращает текущую матрицу с новой размерностью.
        /// Примечание: при увеличении размерности новые позиции заполняются нулями, при уменьшении - лишние позиции удаляются (потеря данных).
        /// </summary>
        /// <param name="size">Новая размерность, где [0] - количество строк, [1] - количество столбцов.</param>
        /// <returns>Матрицу с новой размерностью.</returns>
        public Matrix ChangeMatrixSize(int[] size)
        {
            byte[] dim = { (byte)this.MatrixGrid.GetLength(0), (byte)this.MatrixGrid.GetLength(1) };
            if (size[0] == dim[0] && size[1] == dim[1]) return this;
            else
            {
                Matrix result = new Matrix(size);
                for (int i = 0; i < size[0]; i++)
                {
                    for (int j = 0; j < size[1]; j++)
                    {
                        if (i < dim[0] && j < dim[1]) result.MatrixGrid[i, j] = MatrixGrid[i, j];
                        else result.MatrixGrid[i, j] = 0;
                    }
                }
                return result;
            }
        }

        /// <summary>
        /// Сравнение матриц поэлементно.
        /// </summary>
        /// <param name="other">Целевая матрица для сравнения.</param>
        /// <returns>Логическое значение равны ли текущая матрица с целевой.</returns>
        public bool Equals(Matrix other)
        {
            int[] thisSize = this.GetSize();
            int[] otherSize = other.GetSize();
            if (thisSize[0] != otherSize[0] || thisSize[1] != otherSize[1]) return false;
            else
            {
                for (int i = 0; i < thisSize[0]; i++)
                {
                    for (int j = 0; j < thisSize[1]; j++)
                    {
                        if (this.MatrixGrid[i, j] != other.MatrixGrid[i, j]) return false;
                    }
                }
                return true;
            }
        }

        /// <summary>
        /// Получить алгебраическое дополнение элемента матрицы.
        /// </summary>
        /// <param name="row">Строка, в которой расположен элемент.</param>
        /// <param name="column">Столбец, в котором расположен элемент.</param>
        /// <returns>Алгебраическое дополнение элемента матрицы.</returns>
        public double GetCofactor(int row, int column)
        {
            return Math.Pow(-1, row + column) * GetMinor(row, column);
        }

        /// <summary>
        /// Получить детерминант квадратной матрицы. Примечание: только для квадратных матриц.
        /// </summary>
        /// <returns>Детерминант.</returns>
        /// <exception cref="NotSquareException">Если матрица не квадратная</exception>
        public double GetDeterminant()
        {
            int[] size = this.GetSize();
            bool isSquare = size[0] == size[1];
            if (isSquare)
            {
                double[,] temp = new double[size[0], size[1]];
                int[] check_column = new int[size[1]];
                //копирование исходной матрицы и проверка на вырожденность
                for (int i = 0; i < size[0]; i++)
                {
                    int check_row = 0;
                    for (int j = 0; j < size[1]; j++)
                    {
                        temp[i, j] = this.MatrixGrid[i, j];
                        if (temp[i, j] == 0)
                        {
                            check_row++;
                            check_column[j]++;
                            if (check_row == size[1] || check_column[j] == size[1]) return 0;
                        }
                    }
                }
                int counter = 1;
                sbyte sign = 1;
                //приведение матрицы к треугольной
                while (counter < size[1])
                {
                    if (temp[counter - 1, counter - 1] == 0)
                    {
                        for (int i = counter; i < size[0]; i++)
                        {
                            if (temp[i, counter - 1] != 0)
                            {
                                for (int j = counter - 1; j < size[1]; j++)
                                {
                                    double t = temp[i, j];
                                    temp[i, j] = temp[i - 1, j];
                                    temp[i - 1, j] = t;
                                }
                                sign *= -1;
                                break;
                            }
                        }
                    }
                    else
                    {
                        for (int i = counter; i < size[0]; i++)
                        {
                            double coefficient = -temp[i, counter - 1] / temp[counter - 1, counter - 1];
                            for (int j = counter - 1; j < size[1]; j++)
                            {
                                temp[i, j] += temp[counter - 1, j] * coefficient;
                            }
                        }
                        counter++;
                    }
                }
                double result = temp[0, 0];
                //вычисление определителя треугольной матрицы

                for (int i = 1; i < size[0]; i++)
                {
                    result *= temp[i, i];
                }
                return result * sign;
            }
            else throw new Exception("Матрица не является квадратной");
        }

        /// <summary>
        /// Получить минор элемента матрицы. Примечание: только для квадратных матриц.
        /// </summary>
        /// <param name="row">Строка, в которой расположен элемент.</param>
        /// <param name="column">Столбец, в котором расположен элемент.</param>
        /// <returns>Минор элемента матрицы.</returns>
        /// <exception cref="NotSquareException">Если матрица не квадратная</exception>
        public double GetMinor(int row, int column)
        {
            int[] size = this.GetSize();
            if (row >= size[0] || column >= size[1]) throw new ArgumentOutOfRangeException();
            double[,] temp = new double[size[0] - 1, size[1] - 1];
            int counter = 0;
            for (int i = 0; i < size[0]; i++)
            {
                for (int j = 0; j < size[1]; j++)
                {
                    if (i != row && j != column)
                    {
                        temp[counter / temp.GetLength(0), counter % temp.GetLength(1)] = MatrixGrid[i, j];
                        counter++;
                    }
                }
            }
            return new Matrix(temp).GetDeterminant();
        }

        /// <summary>
        /// Получить размерность матрицы.
        /// </summary>
        /// <returns>Размернсть матрицы, где [0] - количество строк, [1] - количество столбцов.</returns>
        public int[] GetSize()
        {
            int[] size = new int[2];
            size[0] = this.MatrixGrid.GetLength(0);
            size[1] = this.MatrixGrid.GetLength(1);
            return size;
        }

        /// <summary>
        /// Представляет матрицу в виде массива строк для вывода на экран.
        /// </summary>
        /// <returns>Массив строк матрицы.</returns>
        public string[] ToStringArray()
        {
            string[] str = new string[MatrixGrid.GetLength(0)];
            for (int i = 0; i < MatrixGrid.GetLength(0); i++)
            {
                for (int j = 0; j < MatrixGrid.GetLength(1); j++)
                {
                    str[i] += string.Format("{0:0.##} ", MatrixGrid[i, j]);
                }
            }
            return str;
        }

        /// <summary>
        /// Обращение матрицы. Примечание: только для квадратных матриц.
        /// </summary>
        /// <returns>Новую обращенную матрицу из текущей.</returns>
        /// <exception cref="NotSquareException">Если матрица не квадратная.</exception>
        public Matrix Reverse()
        {
            Matrix result = new Matrix(this.GetSize());
            for (int i = 0; i < MatrixGrid.GetLength(0); i++)
            {
                for (int j = 0; j < MatrixGrid.GetLength(1); j++)
                {
                    result.MatrixGrid[i, j] = this.GetCofactor(i, j);
                }
            }
            result = 1 / this.GetDeterminant() * result.Transposition();
            return result;
        }

        /// <summary>
        /// Транспонирование матрицы.
        /// </summary>
        /// <returns>Новую транспонированную матрицу из текущей.</returns>
        public Matrix Transposition()
        {
            Matrix result = new Matrix(new double[MatrixGrid.GetLength(1), MatrixGrid.GetLength(0)]);
            for (int i = 0; i < MatrixGrid.GetLength(0); i++)
            {
                for (int j = 0; j < MatrixGrid.GetLength(1); j++)
                {
                    result.MatrixGrid[j, i] = MatrixGrid[i, j];
                }
            }
            return result;
        }

        /// <summary>
        /// Приведение двух матриц к одной размерности (к наибольшей).
        /// </summary>
        /// <param name="left">Левая матрица.</param>
        /// <param name="right">Правая матрица.</param>
        /// <returns>Массив из двух матриц где [0] - левая матрица, [1] - правая матрица.</returns>
        private static Matrix[] ToEqualSize(Matrix left, Matrix right)
        {
            int[] maxSize = new int[2];
            int[] leftSize = left.GetSize();
            int[] rightSize = right.GetSize();
            if (leftSize[0] < rightSize[0]) maxSize[0] = rightSize[0];
            else maxSize[0] = leftSize[0];
            if (leftSize[1] < rightSize[1]) maxSize[1] = rightSize[1];
            else maxSize[1] = leftSize[1];
            return new Matrix[] { left.ChangeMatrixSize(maxSize), right.ChangeMatrixSize(maxSize) };
        }
    }
}
