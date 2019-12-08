using System;
using System.IO;
using System.Text.RegularExpressions;

namespace Graph
{
    class Program
    {
        static void Main(string[] args)
        {
            int numOfNodes, numOfRibs;
            int[,] arrOfRibs;
            string result = "";
            using (StreamReader streamReader = new StreamReader("input.txt"))
            {
                string line = ClearLine(streamReader.ReadLine());
                string[] objOfLine = line.Split(' ');
                numOfNodes = Convert.ToInt32(objOfLine[0]);//количество вершин
                numOfRibs = Convert.ToInt32(objOfLine[1]);//количество ребер
                //загрузка массива ребер
                arrOfRibs = new int[numOfRibs, 2];
                for (int i = 0; i < numOfRibs; i++)
                {
                    do{
                    line = ClearLine(streamReader.ReadLine());
                    }
                    while (line == "") ;//чтение с пропуском пустых строк
                    objOfLine = line.Split(' ');
                    arrOfRibs[i, 0] = Convert.ToInt32(objOfLine[0]);
                    arrOfRibs[i, 1] = Convert.ToInt32(objOfLine[1]);
                }

            }
            //первичное заполнение массива степеней вершин
            int[] arrOfNodes = new int[numOfNodes];
            MassFiling(ref arrOfNodes, arrOfRibs, numOfNodes, numOfRibs);
            //Сортировка массива по убыванию
            Sort(ref arrOfNodes, numOfNodes);
            //запись результата в строку
           result = ToLine(arrOfNodes, numOfNodes);
            //вывод в файл
            using (StreamWriter streamWriter = new StreamWriter("output.txt", false))
                streamWriter.WriteLine(result);
        }
        public static string ClearLine (string line)//пропуск пустых строк и двойных пробелов
        {
            return Regex.Replace(line, "[ ]+", " ").Trim();
        }
        public static void MassFiling(ref int[] array, int[,] readArray, int numOfNodes, int numOfRibs)
        {
            for (int i = 0; i < numOfNodes; i++)
            {
                array[i] = 0;//по умолчанию степень 0
                for (int n = 0; n < numOfRibs; n++)
                {
                    if (readArray[n, 0] == i + 1 || readArray[n, 1] == i + 1) array[i]++;//если ребро содержит номер вершины добавляем в массив
                }

            }
        }
        public static void Sort (ref int[] array, int arrLength)
        {
            for (int i = 0; i< arrLength - 1; i++)
            {
                for (int n = 0; n<(arrLength - (i+1)); n++)
                {
                    if (array[n] < array[n + 1])
                    {
                        int temp = array[n + 1];
                        array[n + 1] = array[n];
                        array[n] = temp;
                    }
}
            }
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
