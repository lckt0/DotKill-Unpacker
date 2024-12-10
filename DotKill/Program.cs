using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Threading;
using dnlib.DotNet;
using dnlib.DotNet.Writer;
using DotKill.KillProtect;

internal class Program
{
	private static IntPtr ThisConsole = GetConsoleWindow();

	[DllImport("kernel32.dll", ExactSpelling = true)]
	private static extern IntPtr GetConsoleWindow();

	[DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
	private static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

	public static string GetPath(string[] Arguments)
	{
		string lgo = "                                     ____        _   _  ___ _ _ \r\n                                    |  _ \\  ___ | |_| |/ (_) | |\r\n                                    | | | |/ _ \\| __| ' /| | | |\r\n                                    | |_| | (_) | |_| . \\| | | |\r\n                                    |____/ \\___/ \\__|_|\\_\\_|_|_|";
		string x = "                                    DOTNET CLEANER TOOL BY LOCKT";
		string ModulePath = "";
		ShowWindow(ThisConsole, 9);
		Console.SetWindowSize(102, 22);
		Console.SetBufferSize(102, 9001);
		Console.ForegroundColor = ConsoleColor.Cyan;
		Console.Write(lgo);
		Console.ForegroundColor = ConsoleColor.Red;
		Console.Write("\n" + x + "\n\n");
		Console.ForegroundColor = ConsoleColor.Cyan;
		Console.Title = "DotKill Unpacker by LockT#3341";
		ShowWindow(ThisConsole, 9);
		Console.SetWindowSize(102, 22);
		Console.SetBufferSize(102, 9001);
		if (Arguments.Length == 1)
		{
			ModulePath = Arguments[0];
		}
		if (Arguments.Length == 0)
		{
			ConsoleColor dsx = Console.ForegroundColor;
			Console.ForegroundColor = ConsoleColor.Green;
			Console.Write(" [+] ");
			Console.ForegroundColor = ConsoleColor.Blue;
			Console.Write("Path: ");
			Console.ForegroundColor = ConsoleColor.Yellow;
			ModulePath = Console.ReadLine().Replace("\"", "");
			Console.ForegroundColor = dsx;
			Console.Clear();
			Console.ForegroundColor = ConsoleColor.Cyan;
			Console.Write(lgo);
			Console.ForegroundColor = ConsoleColor.Red;
			Console.Write("\n" + x + "\n\n");
			Console.ForegroundColor = ConsoleColor.Cyan;
			ShowWindow(ThisConsole, 9);
			Console.SetWindowSize(102, 22);
			Console.SetBufferSize(102, 9001);
		}
		return ModulePath;
	}

	private static void printRemoved(string str)
	{
		ConsoleColor oldC = Console.ForegroundColor;
		Console.ForegroundColor = ConsoleColor.DarkGray;
		Console.Write("  [");
		Console.ForegroundColor = ConsoleColor.Red;
		Console.Write("Removed");
		Console.ForegroundColor = ConsoleColor.DarkGray;
		Console.Write("] ");
		Console.ForegroundColor = ConsoleColor.Green;
		string[] xd = str.Split(':');
		Console.Write(xd[0] + ":");
		Console.ForegroundColor = ConsoleColor.Magenta;
		Console.WriteLine(xd[1]);
		Console.ForegroundColor = oldC;
	}

	private static void printTimed(string str)
	{
		ConsoleColor oldC = Console.ForegroundColor;
		Console.ForegroundColor = ConsoleColor.DarkGray;
		Console.Write(" [");
		Console.ForegroundColor = ConsoleColor.Blue;
		Console.Write(DateTime.Now.ToString("hh:mm:ss"));
		Console.ForegroundColor = ConsoleColor.DarkGray;
		Console.Write("] ");
		Console.ForegroundColor = oldC;
		Console.Write(str);
	}

	private static void printPath(string str)
	{
		ConsoleColor oldC = Console.ForegroundColor;
		Console.ForegroundColor = ConsoleColor.DarkGray;
		Console.Write(" [");
		Console.ForegroundColor = ConsoleColor.Magenta;
		Console.Write("SavePath");
		Console.ForegroundColor = ConsoleColor.DarkGray;
		Console.Write("] ");
		Console.ForegroundColor = oldC;
		Console.Write(str);
	}

	private static void printExit()
	{
		ConsoleColor oldC = Console.ForegroundColor;
		Console.ForegroundColor = ConsoleColor.DarkGray;
		Console.Write("     [");
		Console.ForegroundColor = ConsoleColor.Red;
		Console.Write("Exit");
		Console.ForegroundColor = ConsoleColor.DarkGray;
		Console.Write("] ");
		Console.ForegroundColor = oldC;
	}

	private static void Main(string[] args)
	{
		string loadPath = GetPath(args);
		ModuleDefMD module = ModuleDefMD.Load(loadPath);
		string junkantide4dot = AntiDe4Dot.Execute(module);
		string dedot = junkantide4dot.Split('+')[0];
		string junk = junkantide4dot.Split('+')[1];
		printRemoved("Anti De4Dot ........: " + dedot);
		printRemoved("Junk ...............: " + junk);
		printRemoved($"Maths ..............: {MathProtection.Execute(module)}");
		printRemoved($"Anti Decompiler ....: {AntiDecompiler.Execute(module)}");
		printRemoved($"CFlow ..............: {CFlow.Execute(module)}");
		Console.ForegroundColor = ConsoleColor.Yellow;
		printTimed("Assembly is saving now");
		Thread.Sleep(200);
		Console.ForegroundColor = ConsoleColor.Green;
		Console.Write(".");
		Thread.Sleep(200);
		Console.ForegroundColor = ConsoleColor.Blue;
		Console.Write(".");
		Thread.Sleep(200);
		Console.ForegroundColor = ConsoleColor.Magenta;
		Console.WriteLine(".");
		string text2 = Path.GetDirectoryName(loadPath);
		if (text2 != null && !text2.EndsWith("\\"))
		{
			text2 += "\\";
		}
		string savePath = text2 + Path.GetFileNameWithoutExtension(loadPath) + "_dotkill" + Path.GetExtension(loadPath);
		module.Write(savePath, new ModuleWriterOptions(module)
		{
			PEHeadersOptions = 
			{
				NumberOfRvaAndSizes = 13u
			},
			Logger = DummyLogger.NoThrowInstance
		});
		Console.ForegroundColor = ConsoleColor.DarkGreen;
		printTimed("Assembly is saved:\n");
		Console.ForegroundColor = ConsoleColor.Yellow;
		printPath(savePath + "\n");
		Console.ForegroundColor = ConsoleColor.White;
		printExit();
		Console.WriteLine("Press any key for exit.");
		Console.ReadKey();
	}
}
