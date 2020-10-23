using System;

namespace ArrayFinder
{
    class Program
    {
        static void Main(string[] args)
        {
            //Тестовые входные данные
            int[,] input1 = { { 0, 1, 2 }, { 3, 4, 5 } };
            int[,,,] input2 = { { { { 1, 1, 1, 1 }, { 1, 1, 1, 2 } }, { { 1, 1, 1, 3 }, { 1, 1, 1, 4 } }, { { 1, 1, 1, 0 }, { 1, 1, 1, 0 } } }, { { { 1, 1, 1, 5 }, { 1, 1, 1, 6 } }, { { 1, 1, 1, 7 }, { 1, 1, 1, 8 } }, { { 1, 1, 1, 9 }, { 1, 1, 1, 10 } } } };

            int[] desiredImput1 = { 1, 2, 3 };
            int[,] desiredImput2 = { { 1, 1, 1, 0 }, { 1, 1, 1, 0 } };
            //Тестовые входные данные

            Console.WriteLine("Первый метод: " + CheckCoincidence1(input2, desiredImput2));
            Console.WriteLine("Второй метод: " + CheckCoincidence2(input2, desiredImput2));

            //Первый метод для [[0, 1, 2],[3, 4, 5]] и [1, 2, 3] выдаст true, тогда как второый выдаст false
            //Note: Я не понял, как должно быть правильно, поэтому сделал оба метода
        }

        ////////////////////////////////////////////////////ПЕРВЫЙ МЕТОД//////////////////////////////////////////////////////
        static bool CheckCoincidence1(Array arr1, Array arr2)
        {
            bool output = false;

            //перевод многомерных массивов в одномерные
            int[] arrLocal1 = MoveNArrTo1Arr(arr1);
            int[] arrLocal2 = MoveNArrTo1Arr(arr2);
            
            //сравнивание полученных одномерных массивов
            for (int i = 0; i < arrLocal1.Length; i++)
            {
                if (arrLocal1[i] == arrLocal2[0])
                    for (int ii = 0; ii < arrLocal2.Length; ii++)
                    {
                        if (arrLocal1[i + ii] != arrLocal2[ii]) break;
                        if (ii + 1 >= arrLocal2.Length)
                        {
                            output = true;
                            break;
                        }
                        if (ii + i + 1 >= arrLocal1.Length) break;
                    }
            }

            return output;
        }

        static int[] MoveNArrTo1Arr(Array arr)
        {
            int[] arrLocal = new int[arr.Length];

            int i = 0;
            foreach (int i1 in (Array)arr)
            {
                arrLocal[i] = i1;
                i++;
            }

            return arrLocal;
        }

        /////////////////////////////////////////////////////ВТОРОЙ МЕТОД////////////////////////////////////////////////////////////
        static bool CheckCoincidence2(Array arr1, Array arr2)
        {
            return MoveArrayToString(arr1).Contains(MoveArrayToString(arr2));
        }

        static string MoveArrayToString(Array arr)
        {
            string output = "";

            //создание строки из числ и запятых
            foreach (int value in (Array)arr)
            {
                output += Convert.ToString(value) + ',';
            }

            //оформление первого измерения
            output = output.Insert(0, "[");
            int commaNum = 0;
            for (int i = 0; i < output.Length; i++)
            {
                if (output[i] == ',') commaNum++;
                if (commaNum == arr.GetLength(arr.Rank - 1))
                {
                    output = output.Insert(i, "]");
                    if (i + 2 < output.Length) output = output.Insert(i + 2, "[");
                    commaNum = 0;
                    i++;
                }
            }
            output = output.Remove(output.Length - 1);

            //оформление остальных измерений
            for (int rankNow = arr.Rank - 2; rankNow >= 0; rankNow--)
            {
                int bracketNum = 0;
                int bracketLvl = 0;
                for (int i = 0; i < output.Length; i++)
                {
                    if (output[i] == '[') bracketLvl++;
                    if (output[i] == ']')
                    {
                        bracketLvl--;
                        if (bracketLvl == 0) bracketNum++;
                    }
                    if (bracketNum == arr.GetLength(rankNow))
                    {
                        output = output.Insert(i, "]");
                        if (i + 3 < output.Length) output = output.Insert(i + 3, "[");
                        bracketNum = 0;
                        i += 3;
                    }
                }
                output = output.Insert(0, "[");
            }

            return output;
        }
            
    }
}
