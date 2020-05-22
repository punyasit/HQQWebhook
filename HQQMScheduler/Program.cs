using System;
using HQQLibrary;
using HQQLibrary.Manager;

namespace HQQMScheduler
{
    public class Program
    {
        private static ShopeeDataExtraction shopeeDataExt;
        static void Main(string[] args)
        {
            InitVariable();
            StartProcess();
        }

        private static void InitVariable()
        {
            shopeeDataExt = new ShopeeDataExtraction();
        }

        private static void StartProcess()
        {
            DateTime startTime =  DateTime.Now;
            DateTime finishTime;

            Console.WriteLine("Process Start Time: {0:dd:MM:yyyy HH:mm:ss}", startTime);
            shopeeDataExt.ExecuteGatherShopInfo();
            finishTime = DateTime.Now;
            Console.WriteLine("Process Finish Time: {0:dd:MM:yyyy HH:mm:ss}", finishTime);
            Console.WriteLine("Elapsed Time: {0:0.00} Minutes", (finishTime - startTime).TotalMinutes);

        }
    }
}
