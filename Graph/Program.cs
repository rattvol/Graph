using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace Graph
{
    class Program
    {
        static void Main(string[] args)
        {
            string inputPath = Properties.Settings.Default.inputPath;
            string outputPath = Properties.Settings.Default.outputPath;
            FileInfo inputFile = new FileInfo(inputPath);
            if (inputFile.Exists)
            {
                try
                {
                    //подключение файла
                    FileStream fs = inputFile.OpenRead();
                    StreamReader streamReader = new StreamReader(fs);
                    string line = streamReader.ReadLine();
                    line = Regex.Replace(line, "[ ]+", " ").Trim();
                    string[] objOfLine = line.Split(' ');
                    int numOfNodes = Convert.ToInt32(objOfLine[0]);//количество вершин
                    int numOfRibs = Convert.ToInt32(objOfLine[1]);//количество ребер
                    //загрузка массива ребер
                    int[,] arrOfRibs = new int[numOfRibs, 2];
                    for (int i = 0; i < numOfRibs; i++)
                    {
                        do{
                        line = streamReader.ReadLine();
                            line = Regex.Replace(line, "[ ]+", " ").Trim();
                        }
                        while (line == "") ;//пропуск пустых строк
                        objOfLine = line.Split(' ');
                        arrOfRibs[i, 0] = Convert.ToInt32(objOfLine[0]);
                        arrOfRibs[i, 1] = Convert.ToInt32(objOfLine[1]);
                    }
                    streamReader.Close();
                    //первичное заполнение массива степеней вершин
                    int[] arrOfNodes = new int[numOfNodes];
                    for (int i = 0; i < numOfNodes; i++)
                    {
                        arrOfNodes[i] = 0;//по умолчанию степень 0
                        for (int n = 0; n < numOfRibs; n++)
                        {
                            if (arrOfRibs[n, 0] == i + 1 || arrOfRibs[n, 1] == i + 1) arrOfNodes[i]++;//если ребро содержит номер вершины добавляем в массив
                        }

                    }
                    //Сортировка массива по убыванию
                    for (int i = 0; i < numOfNodes-1; i++)
                    {
                        for (int n = 0; n < (numOfNodes - (i+1)); n++)
                        {
                            if (arrOfNodes[n] < arrOfNodes[n+1])
                            {
                                int temp = arrOfNodes[n + 1];
                                arrOfNodes[n + 1] = arrOfNodes[n];
                                arrOfNodes[n] = temp;
                            }
                        }
                    }

                    //вывод в файл
                    StreamWriter streamWriter = new StreamWriter(outputPath, false);
                    for (int i = 0; i < numOfNodes; i++)
                    {
                        streamWriter.Write(arrOfNodes[i].ToString() +" ");
                    }
                    streamWriter.WriteLine();
                    Console.WriteLine("Результат записан в файл:" + outputPath);
                    streamWriter.Close();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }

            }
            else
            {
                Console.WriteLine("Файл "+inputPath+" не найден");
            }
            Console.ReadKey();
        }
    }
}
