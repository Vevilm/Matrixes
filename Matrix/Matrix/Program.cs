using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Matrixes
{
    class Program
    {
        static void Main(string[] args)
        {
            Matrix x = new Matrix(4,4), y = new Matrix(2,1);
            x.Randfill();
            x.Output();

            y.Randfill();
            y.Output();

            Console.WriteLine(x.Determinant());
        }
    }
    class Matrix
    {
        public double[,] m;
        readonly int lenX,lenY;
        readonly bool isSquare;
        public Matrix(int lenX, int lenY) {
            this.lenX = lenX;
            this.lenY = lenY;
            m = new double[lenX, lenY];
            if (lenX == lenY) {
                isSquare = true;
            }
        }//Конструктор для создания прямоугольной матрицы
        public Matrix(int len)//Сокращённый конструктор для создания квадратной матрицы
        {
            lenX = len;
            lenY = len;
            m = new double[len, len];
            isSquare = true;
        }

        public int LengthX
        {
            get {return lenX;}
        }//Свойство - количество строк
        public int LengthY
        {
            get { return lenY; }
        }//Свойство - количество столбцов
        public bool IsSquare { get { return isSquare; } } //Свойство - является ли матрица квадратной
        
        public void Randfill()
        {
            Random rnd = new Random(DateTime.Now.Second*DateTime.Now.Millisecond);
            for (int i = 0; i < lenX; i++)
            {
                for (int j = 0; j < lenY; j++)
                {
                    m[i, j] = rnd.Next(-10, 10);
                }
            }
        }//Тестовый метод для случайного заполнения матрицы
        public void Output()
        {
            for (int i = 0; i < lenX; i++)
            {
                for (int j = 0; j < lenY; j++)
                {
                    Console.Write(m[i, j] + "\t");
                }
                Console.WriteLine();
            }
            Console.WriteLine("============================");

        }//Метод вывода матрицы. В нём есть разделитель, чтобы различать матрицы
        public double this[int indexX, int indexY]
        {
            get { return m[indexX, indexY]; }
            set { m[indexX, indexY] = value; }
        }//Индексатор

        //Перегрузка арифметических операторов
        public static Matrix operator +(Matrix cm1, Matrix cm2)
        {
            Matrix cm3 = new Matrix(cm1.LengthX,cm1.LengthY);
            if (cm1.LengthX == cm2.LengthX && cm2.LengthY == cm2.LengthX)
            {
                for (int i = 0; i < cm3.LengthX; i++)
                {
                    for (int j = 0; j < cm3.LengthY; j++)
                    {
                        cm3[i, j] = cm1[i, j] + cm2[i, j];
                    }
                }
                return cm3;
            }
            else
            {
                return null;
            }
        }
        public static Matrix operator -(Matrix cm1, Matrix cm2)
        {
            Matrix cm3 = new Matrix(cm1.LengthX, cm1.LengthY);
            if (cm1.LengthX == cm2.LengthX && cm2.LengthY == cm2.LengthX)
            {
                for (int i = 0; i < cm3.LengthX; i++)
                {
                    for (int j = 0; j < cm3.LengthY; j++)
                    {
                        cm3[i, j] = cm1[i, j] - cm2[i, j];
                    }
                }
                return cm3;
            }
            else
            {
                return null;
            }
        }
        public static Matrix operator *(Matrix A, Matrix B)
        {
            if (A.LengthY != B.LengthX)
            {
                Console.WriteLine("Ошибка! Невозможно умножить матрицы!");
                return null;
            }
            Matrix r = new Matrix(A.LengthX, B.LengthY);
            for (int i = 0; i < A.LengthX; i++)
            {
                for (int j = 0; j < B.LengthY; j++)
                {
                    for (int k = 0; k < A.LengthY; k++)
                    {
                        r[i, j] += A[i, k] * B[k, j];
                    }
                }
            }
            return r;
        }
        public static Matrix operator *(Matrix A, double k)
        {
            Matrix r = new Matrix(A.LengthX, A.LengthY);
            for (int i = 0; i < A.LengthX; i++)
            {
                for (int j = 0; j < A.LengthY; j++)
                {
                    r[i, j] = A[i, j] * k;
                }
            }
            return r;
        }
        public static Matrix operator *(double k, Matrix A)
        {
            Matrix r = new Matrix(A.LengthX, A.LengthY);
            for (int i = 0; i < A.LengthX; i++)
            {
                for (int j = 0; j < A.LengthY; j++)
                {
                    r[i, j] = A[i, j] * k;
                }
            }
            return r;
        }

        //Перегрузка логических операторов == и !=
        public static bool operator ==(Matrix cm1, Matrix cm2)
        {
            if (cm1.LengthX != cm2.LengthX || cm1.LengthY != cm2.LengthY)
            {
                return false;
            }
            for (int i = 0; i < cm1.LengthX; i++)
            {
                for (int j = 0; j < cm1.LengthY; j++)
                {
                    if (cm1[i, j] != cm2[i, j])
                        return false;
                }
            }
            return true;
        }
        public static bool operator !=(Matrix cm1, Matrix cm2)
        {
            if (cm1.LengthX != cm2.LengthX || cm1.LengthY != cm2.LengthY)
            {
                return true;
            }
            for (int i = 0; i < cm1.LengthX; i++)
            {
                for (int j = 0; j < cm1.LengthY; j++)
                {
                    if (cm1[i, j] != cm2[i, j])
                        return true;
                }
            }
            return false;
        }

        public double Determinant() {
            double r = 0;
            int blockedColumn = 0, subMatrixRow = 0, subMatrixColumn = 0;
            if (!IsSquare)
                return 0.0;
            if (LengthX == 2)
            {
                r = (this[0, 0] * this[1, 1]) - (this[1, 0] * this[0, 1]);
                return r;
            }
            else if (LengthX > 2)
            {
                Matrix[] cm1 = new Matrix[LengthX];
                for (int l = 0; l < LengthX; l++)
                {
                    blockedColumn = l;
                    cm1[l] = new Matrix(LengthX - 1);
                    for (int i = 1; i < LengthX; i++)
                    {
                        for (int j = 0; j < LengthX; j++)
                        {
                            if (j != blockedColumn)
                            {
                                cm1[l][subMatrixRow, subMatrixColumn] = this[j, i];
                                subMatrixRow++;
                            }
                        }
                        subMatrixColumn++;
                        subMatrixRow = 0;
                    }

                    subMatrixColumn = 0;
                    subMatrixRow = 0;

                    r += this[blockedColumn, 0] * Math.Pow(-1, blockedColumn) * MatrixAddition.Determinant(cm1[l]);
                }
            }
            return r;
        }//Метод для рассчёта определителя матрицы
        public Matrix Transposition()
        {
            if (!IsSquare)
                return null;
            Matrix r = new Matrix(LengthX);
            for (int i = 0; i < LengthX; i++)
            {
                for (int j = 0; j < LengthX; j++)
                {
                    r[i, j] = this[j, i];
                }
            }
            return r;
        }//Метод для транспонирования матрицы
        
    }
    //Дополнительный класс c методом для конвертации двумерного массива в матрицу
    static class Matrixes {
        public static Matrix Convert(double[,] a) {
            Matrix newMatrix = new Matrix(a.GetLength(0), a.GetLength(1));
            for (int i = 0; i < newMatrix.LengthX; i++)
            {
                for (int j = 0; j < newMatrix.LengthY; j++)
                {
                    newMatrix[i, j] = a[i, j];
                }
            }
            return newMatrix;
        }
    }
}
