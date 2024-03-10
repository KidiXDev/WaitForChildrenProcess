
# Wait For Children Process

A simple tools to find and wait for children process to exit

# How it works?

Uses a ManagementObjectSearcher to retrieve all processes with the specified parent process ID using a WMI query. Then iterates through each child process and retrieves the process ID of each child process.

For each child process, it gets the Process object representing the child process using ``Process.GetProcessById`` and then waits for the child process to exit using ``childProcess.WaitForExit()``

Additionally, it recursively calls the ``WaitForAllChildProcesses()`` method for each child process to wait for any child processes of the current child process.