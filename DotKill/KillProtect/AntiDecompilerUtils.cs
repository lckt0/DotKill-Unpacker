using System;
using System.Collections.Generic;
using System.Linq;
using dnlib.DotNet;
using dnlib.DotNet.Emit;

namespace DotKill.KillProtect
{
	public static class AntiDecompilerUtils
	{
		internal static bool DetectCallSizeOfCalli(MethodDef method)
		{
			IList<ExceptionHandler> body = method.Body.ExceptionHandlers;
			foreach (ExceptionHandler exceptionHandler in body.ToArray<ExceptionHandler>())
			{
				bool flag = AntiDecompilerUtils.List.Contains(exceptionHandler.TryStart.OpCode) && exceptionHandler.TryStart.Operand == null;
				if (flag)
				{
					return true;
				}
			}
			return false;
		}

		internal static bool DetectCallUnaligned(MethodDef method)
		{
			IList<Instruction> instr = method.Body.Instructions;
			for (int i = 0; i < instr.Count; i++)
			{
				bool flag = !instr[i].IsBr();
				if (!flag)
				{
					bool flag2 = instr[i + 1].OpCode.Code != Code.Unaligned;
					if (!flag2)
					{
						return true;
					}
				}
			}
			return false;
		}

		internal static bool DetectCallConstrained(MethodDef method)
		{
			IList<Instruction> instr = method.Body.Instructions;
			for (int i = 0; i < instr.Count; i++)
			{
				bool flag = instr[i].IsBr() && instr[i + 1].OpCode == OpCodes.Constrained;
				if (flag)
				{
					return true;
				}
			}
			return false;
		}

		internal static void CallSizeOfCalli(MethodDef method)
		{
			bool hasprotection = AntiDecompilerUtils.DetectCallSizeOfCalli(method);
			bool flag = !hasprotection;
			if (!flag)
			{
				IList<ExceptionHandler> body = method.Body.ExceptionHandlers;
				foreach (ExceptionHandler exceptionHandler in body.ToArray<ExceptionHandler>())
				{
					bool flag2 = AntiDecompilerUtils.List.Contains(exceptionHandler.TryStart.OpCode) && exceptionHandler.TryStart.Operand == null;
					if (flag2)
					{
						IList<Instruction> instr = method.Body.Instructions;
						int endIndex = instr.IndexOf(exceptionHandler.TryEnd);
						for (int i = 0; i < endIndex; i++)
						{
							instr[i].OpCode = OpCodes.Nop;
						}
						body.Remove(exceptionHandler);
					}
				}
			}
		}

		internal static void CallUnaligned(MethodDef method)
		{
			bool hasprotection = AntiDecompilerUtils.DetectCallUnaligned(method);
			bool flag = !hasprotection;
			if (!flag)
			{
				IList<Instruction> instr = method.Body.Instructions;
				for (int i = 0; i < instr.Count; i++)
				{
					bool flag2 = !instr[i].IsBr();
					if (!flag2)
					{
						bool flag3 = instr[i + 1].OpCode.Code != Code.Unaligned;
						if (!flag3)
						{
							instr.RemoveAt(i + 1);
						}
					}
				}
			}
		}

		internal static void CallConstrained(MethodDef method)
		{
			bool hasprotection = AntiDecompilerUtils.DetectCallConstrained(method);
			bool flag = !hasprotection;
			if (!flag)
			{
				IList<Instruction> instr = method.Body.Instructions;
				for (int i = 0; i < instr.Count; i++)
				{
					bool flag2 = instr[i].IsBr() && instr[i + 1].OpCode == OpCodes.Constrained;
					if (flag2)
					{
						instr.RemoveAt(i + 1);
					}
				}
			}
		}

		private static readonly OpCode[] List = new OpCode[]
		{
			OpCodes.Call,
			OpCodes.Sizeof,
			OpCodes.Calli
		};
	}
}
