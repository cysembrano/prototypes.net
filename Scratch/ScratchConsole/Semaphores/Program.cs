/*
 * SEMAPHORES
 * Think of semaphores as bouncers at a nightclub. There are a dedicated number of people that are allowed in the club at once. 
 * If the club is full no one is allowed to enter, but as soon as one person leaves another person might enter.
 * It's simply a way to limit the number of consumers for a specific resource. 
 * For example, to limit the number of simultaneous calls to a database in an application.
 * */
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace TheNightclub
{
    public class Program
    {
        public static Semaphore Bouncer { get; set; }

        public static void Main(string[] args)
        {
            // Create the semaphore with 3 slots, where 3 are available.
            Bouncer = new Semaphore(3, 3);

            // Open the nightclub.
            OpenNightclub();
        }

        public static void OpenNightclub()
        {
            for (int i = 1; i <= 50; i++)
            {
                // Let each guest enter on an own thread.
                Thread thread = new Thread(new ParameterizedThreadStart(Guest));
                thread.Start(i);
            }
        }

        public static void Guest(object args)
        {
            // Wait to enter the nightclub (a semaphore to be released).
            Console.WriteLine("Guest {0} is waiting to entering nightclub.", args);
            Bouncer.WaitOne();

            // Do some dancing.
            Console.WriteLine("Guest {0} is doing some dancing.", args);
            Thread.Sleep(500);

            // Let one guest out (release one semaphore).
            Console.WriteLine("Guest {0} is leaving the nightclub.", args);
            Bouncer.Release(1);
        }
    }
}