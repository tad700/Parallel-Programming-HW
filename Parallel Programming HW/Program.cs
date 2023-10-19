using System;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Threading;

namespace Parallel_Programming_HW
{
    internal class Program
    {
        public static StreamReader sr = new StreamReader("C:\\Users\\Todor\\source\\repos\\Parallel Programming HW\\Parallel Programming HW\\Book.txt");
        static char[] charToRemove = { ' ', '\n', '\r', '.', ',', '-', '!', '?', '(', ')','"',':' };
        static string[] Words = Text();
        static Stopwatch stopwatch = new Stopwatch();
        

        static string[] Text()
        {
            string text = sr.ReadToEnd();
            foreach (char charToRemoveItem in charToRemove)
            {
                text = text.Replace(charToRemoveItem.ToString(), " ");
            }
            string[] Words = text.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            Words = Array.FindAll(Words, word => word.Length >= 3);
            return Words;
        }

        public static int NumberOfWords()
        {
            int number = 0;
            foreach (string word in Words)
            {

                number++;
            }
            return number;
        }

        public static string ShortestWord()
        {
            string shortest = Words[0];
            foreach (string word in Words)
            {
                if (word.Length < shortest.Length)
                {
                    shortest = word;
                }
            }
            return shortest;
        }

        public static string LongestWord()
        {
            string longest = Words[0];
            foreach (string word in Words)
            {
                if (word.Length > longest.Length)
                {
                    longest = word;
                }
            }
            return longest;
        }

        static double AverageWordLength()
        {
            int allWords = 0;
            foreach (string word in Words)
            {
                allWords += word.Length;
            }
            double averageLength = (double)allWords / Words.Length;
            return averageLength;
        }

        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;

            Thread numOfTheWordsThread = new Thread(() =>
            {
                stopwatch.Start();
                Console.WriteLine($"Number of words is: {NumberOfWords()}");
            });

            Thread shortestWordThread = new Thread(() =>
            {
                Console.WriteLine($"Shortest word is: {ShortestWord()}");
            });

            Thread longestWordThread = new Thread(() =>
            {
                Console.WriteLine($"Longest word is: {LongestWord()}");
            });

            Thread averageWordLengthThread = new Thread(() =>
            {
                Console.WriteLine($"Average word length is: {AverageWordLength():N2}");
            });

            numOfTheWordsThread.Start();
            shortestWordThread.Start();
            longestWordThread.Start();
            averageWordLengthThread.Start();
/*
            numOfTheWordsThread.Join();
            shortestWordThread.Join();
            longestWordThread.Join();
            averageWordLengthThread.Join();*/
            stopwatch.Stop();

            Console.WriteLine($"Total Execution Time for All Threads: {stopwatch.Elapsed.TotalMilliseconds} ms");
     

            stopwatch.Restart();
            
            Console.WriteLine($"Method Number Of Words: {NumberOfWords()}");
            Console.WriteLine($"Method Shortest Word: {ShortestWord()}"); 
            Console.WriteLine($"Method Longest Word: {LongestWord()}");
            Console.WriteLine($"Method Average Word Length: {AverageWordLength():N2}");
            stopwatch.Stop();

           
            Console.WriteLine($"Execution Time for Method Calls: {stopwatch.Elapsed.TotalMilliseconds} ms");
    

        }
    }
}
