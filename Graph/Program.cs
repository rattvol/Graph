using System;
using System.IO;
using System.Text.RegularExpressions;

namespace Graph
{
    class Program
    {
        static void Main(string[] args)
        {
            int[] arrOfNodes;
            int numOfNodes, numOfRibs;
            int[,] arrOfRibs;
            string result = "";
            using (StreamReader streamReader = new StreamReader("input.txt"))
            {
                string line = ClearLine(streamReader.ReadLine());
                string[] objOfLine = line.Split(' ');
                numOfNodes = Convert.ToInt32(objOfLine[0]);//количество вершин
                numOfRibs = Convert.ToInt32(objOfLine[1]);//количество ребер
                //первичное заполнение массива степеней вершин
                arrOfNodes = new int[numOfNodes];
                for (int i = 0; i < numOfRibs; i++)
                {
                    do
                    {
                        line = ClearLine(streamReader.ReadLine());
                    }
                    while (line == "");//чтение с пропуском пустых строк
                    objOfLine = line.Split(' ');
                    arrOfNodes[Convert.ToInt32(objOfLine[0]) - 1]++;
                    arrOfNodes[Convert.ToInt32(objOfLine[1]) - 1]++;
                }
            }
            //Сортировка массива по убыванию
            SortFast(ref arrOfNodes, 0, arrOfNodes.Length - 1);
            //запись результата в строку
            result = ToLine(arrOfNodes, numOfNodes);
            //вывод в файл
            using (StreamWriter streamWriter = new StreamWriter("output.txt", false))
                streamWriter.WriteLine(result);
        }
        public static string ClearLine(string line)//пропуск пустых строк и двойных пробелов
        {
            return Regex.Replace(line, "[ ]+", " ").Trim();
        }

       
        public static void SortFast(ref int[] arr, int first, int last)
        {
            int p = arr[(last - first) / 2 + first];
            int temp;
            int i = first, j = last;
            while (i <= j)
            {
                while (arr[i] > p && i <= last) ++i;
                while (arr[j] < p && j >= first) --j;
                if (i <= j)
                {
                    temp = arr[i];
                    arr[i] = arr[j];
                    arr[j] = temp;
                    ++i; --j;
                }
            }
            if (j > first) SortFast(ref arr, first, j);
            if (i < last) SortFast(ref arr, i, last);
        }
        public static string ToLine(int[] array, int arrLength)
        {
            string output = "";
            for (int i = 0; i < arrLength; i++)
            {
                output += array[i].ToString() + " ";
            }
            return output;
        }
    }
}
