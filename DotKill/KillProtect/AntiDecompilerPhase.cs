using System;
using dnlib.DotNet;
using DotKill.KillProtect;

namespace DotKill.KillProtect
{
	internal static class AntiDecompilerPhase
	{
		public static void Execute(MethodDef method)
		{
			AntiDecompilerUtils.CallSizeOfCalli(method);
			AntiDecompilerUtils.CallUnaligned(method);
			AntiDecompilerUtils.CallConstrained(method);
		}
	}
}
