using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Threading;
using System.Speech;
using System.Speech.Synthesis;﻿

namespace CpuMonitor
{
    class Program
    {
        static void Main(string[] args)
        {

            #region Performance Counters 
            //Pulls cpu load in percentage
            PerformanceCounter perfCpuCount = new PerformanceCounter("Processor Information", "% Processor Time", "_Total");

            //Pulls Available Ram in Megabytes 
            PerformanceCounter perfMemCount = new PerformanceCounter("Memory", "Available MBytes");
            #endregion

            
            SpeechSynthesizer synth = new SpeechSynthesizer();
            synth.SelectVoiceByHints(VoiceGender.Male);
            synth.Speak("This is cpu monitor alpha 1.4");

            while(true)
            {
                int currentCpuPercentage = (int)perfCpuCount.NextValue();
                int currentAvailableMemory = (int)perfMemCount.NextValue();

             

                    Console.WriteLine("Cpu Load: {0}%", currentCpuPercentage);
                    Console.WriteLine("Available Memory: {0}MB", currentAvailableMemory);

                if ( currentCpuPercentage > 80)
                {
                    if (currentCpuPercentage == 100)
                    {
                        synth.Speak("Warning cpu load is at 100 percent");
                    }
                    else
                    {
                        string cpuLoadVocalMessage = String.Format("The current cpu load is {0} percent", currentCpuPercentage);
                        synth.Speak(cpuLoadVocalMessage);
                    }
                }

               if (currentAvailableMemory < 4024)
               {
                   string memAvailableVocalMessage = String.Format("The currently have {0} gigabytes of memory available", currentAvailableMemory / 1024);
                   synth.Speak(memAvailableVocalMessage);

               }
                    Thread.Sleep(1000);
                
            }











        }
    }
}
