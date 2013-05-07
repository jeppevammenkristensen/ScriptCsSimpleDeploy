using System;
using System.Diagnostics;
using System.IO;


// This needs to be changed. It not sure that exitcode 0 indicates success
public static bool Run(string fileName, string parameters)
{
	Console.WriteLine("Starting {0}", fileName);

	var process = new Process();
	process.StartInfo.UseShellExecute = false;

	process.StartInfo.FileName = fileName;
	process.StartInfo.Arguments = parameters; 

	process.Start();

    process.WaitForExit();

    Console.WriteLine("{0} Exicited with code {0}", fileName);
    

    return process.ExitCode == 0;
}