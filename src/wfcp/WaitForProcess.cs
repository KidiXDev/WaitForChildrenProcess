using System;
using System.Diagnostics;
using System.Management;

namespace wfcp
{
    public class WaitForProcess
    {
        /// <summary>
        /// Launches the parent process, and or then waits for all child processes to exit.
        /// </summary>
        /// <param name="parentProcess">The parent process to be launched and waited for</param>
        public static void LaunchAndWait(Process parentProcess)
        {
            // Start the parent process
            parentProcess.Start();
            int parentProcessId = parentProcess.Id;

            // Wait for the parent process to exit
            parentProcess.WaitForExit();

            WaitForAllChildProcesses(parentProcessId);
        }


        /// <summary>
        /// Recursively waits for all child processes of the specified parent process ID to exit.
        /// </summary>
        /// <param name="parentId">The process ID of the parent process.</param>
        private static void WaitForAllChildProcesses(int parentId)
        {
            try
            {
                // Create a ManagementObjectSearcher to retrieve all processes with the specified parent process ID
                using (ManagementObjectSearcher searcher = new ManagementObjectSearcher($"Select * From Win32_Process Where ParentProcessId={parentId}"))
                {
                    // Iterate through each management object representing a child process
                    foreach (ManagementObject managementObject in searcher.Get())
                    {
                        // Retrieve the process ID of the child process
                        int childProcessId = Convert.ToInt32(managementObject["ProcessId"]);

                        // Get the Process object representing the child process
                        Process childProcess = Process.GetProcessById(childProcessId);

                        childProcess.WaitForExit();

                        // Recursively call WaitForAllChildProcesses to wait for any child processes of the current child process
                        WaitForAllChildProcesses(childProcessId);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }

    }
}
