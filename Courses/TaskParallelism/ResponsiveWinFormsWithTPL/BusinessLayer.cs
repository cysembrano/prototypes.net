using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ResponsiveWinFormsWithTPL
{
    internal class BusinessLayer
    {
        /// <summary>
        /// Method will update the UI once the task is entirely complete
        /// </summary>
        /// <param name="data"></param>
        /// <param name="updateUICallBack"></param>
        public static void ProcessData(int data, Action<string> updateUICallBack)
        {
            Task.Factory.StartNew(
                () =>
                {
                    Thread.Sleep(2000); //simulateWork, do something with the data received
                })
                .ContinueWith(
                (cw) =>
                {
                    updateUICallBack(string.Format("Finished step {0}", data));
                }
            );
        }

        /// <summary>
        /// Method will update the UI while the task is running
        /// </summary>
        /// <param name="data"></param>
        /// <param name="updateUICallBack"></param>
        public static void PerformInternalValidationsOnData(object data, Action<string, bool> updateUICallBack)
        {
            Task.Factory.StartNew(
                () =>
                {
                    Thread.Sleep(10); //simulateWork, do something with the data received
                    updateUICallBack("Running validation 20%", false);
                    Thread.Sleep(1000); //simulateWork, do something with the data received
                    updateUICallBack("Running validation 40%", false);
                    Thread.Sleep(800); //simulateWork, do something with the data received
                    updateUICallBack("Running validation 60%", false);
                    Thread.Sleep(700); //simulateWork, do something with the data received
                    updateUICallBack("Running validation 80%", false);
                    Thread.Sleep(1000); //simulateWork, do something with the data received
                    updateUICallBack("Validations complete - 100%", true);
                });
        }

        /// <summary>
        /// Method will update the UI very frequently while the task is running
        /// </summary>
        /// <param name="data"></param>
        /// <param name="updateUICallBack"></param>
        public static void PrepareTransformationsForProcessing(object data, Action<string, bool> updateUICallBack)
        {
            Task.Factory.StartNew(
                () =>
                {
                    for (int i = 0; i <= 100; i++)
                    {
                        Thread.Sleep(50); //simulateWork, do something with the data received
                        updateUICallBack(string.Format("Preparing transformations for processing {0}% done", i), false);
                    }
                    updateUICallBack("Preparing transformations for processing 100% complete", true);
                });
        }

        /// <summary>
        /// Method will update UI very frequently with new task while the original task is running
        /// </summary>
        /// <param name="data"></param>
        /// <param name="updateUICallBack"></param>
        public static void PrepareLaunchSequenceForData(object data, Action<string, bool> updateUICallBack)
        {
            Random rand = new Random();
            Task.Factory.StartNew(
                () =>
                {
                    for (int i = 0; i <= 100; i++)
                    {
                        Thread.Sleep(rand.Next(10, 11)); //simulateWork, do something with the data received

                        Task.Factory.StartNew(
                            (state) =>
                            {
                                updateUICallBack(string.Format("Preparing launch requence for data {0}% done", state), false);
                            }, i);

                        //Incorrect way of passing data to a task. 
                        //
                        //The value of variable i may be different from the time the task was scheduled
                        //and from the time the task actually runs
                        //
                        //If you comment the above task, and run the below task, sometimes you may see 101% done in the UI, instead of 100% done.
                        //
                        //Task.Factory.StartNew(
                        //   () =>
                        //   {
                        //       updateUICallBack(string.Format("Preparing launch requence for data {0}% done", i), false);
                        //   });
                    }

                        Task.Factory.StartNew(
                                () =>
                                {
                                    updateUICallBack("Preparing launch requence for data 100% complete", true);
                                });
                    
                });
        }
    }
}
