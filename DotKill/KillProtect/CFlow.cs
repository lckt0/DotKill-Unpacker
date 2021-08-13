using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using dnlib.DotNet;
using dnlib.DotNet.Emit;

namespace DotKill.KillProtect
{
    class CFlow
    {
		public static int removed = 0;

		public static void RemoveUselessNops(MethodDef method)
		{
			IList<Instruction> instr = method.Body.Instructions;
			for (int i = 0; i < instr.Count; i++)
			{
				while (instr[i].OpCode == OpCodes.Nop && !IsNopBranchTarget(method, instr[i]) && !IsNopSwitchTarget(method, instr[i]) && !IsNopExceptionHandlerTarget(method, instr[i]))
				{
					method.Body.Instructions.RemoveAt(i);
					if (instr[i] == instr.Last())
					{
						break;
					}
				}
			}
		}

		private static bool IsNopExceptionHandlerTarget(MethodDef method, Instruction nopInstr)
		{
			if (!method.Body.HasExceptionHandlers)
			{
				return false;
			}
			return method.Body.ExceptionHandlers.Any((ExceptionHandler exceptionHandler) => exceptionHandler.FilterStart == nopInstr || exceptionHandler.HandlerEnd == nopInstr || exceptionHandler.HandlerStart == nopInstr || exceptionHandler.TryEnd == nopInstr || exceptionHandler.TryStart == nopInstr);
		}


		private static bool IsNopSwitchTarget(MethodDef method, Instruction nopInstr)
		{
			return (from t in method.Body.Instructions
					where t.OpCode.OperandType == OperandType.InlineSwitch && t.Operand != null
					select (Instruction[])t.Operand).Any((Instruction[] source) => source.Contains(nopInstr));
		}

		private static bool IsNopBranchTarget(MethodDef method, Instruction nopInstr)
		{
			return (from t in method.Body.Instructions
					where t.OpCode.OperandType == OperandType.InlineBrTarget || (t.OpCode.OperandType == OperandType.ShortInlineBrTarget && t.Operand != null)
					select (Instruction)t.Operand).Any((Instruction instruction2) => instruction2 == nopInstr);
		}


		public static int Execute(ModuleDefMD module)
		{
			TypeDef[] array = (from t in module.GetTypes()
							   where t.HasMethods
							   select t).ToArray();
			for (int j = 0; j < array.Length; j++)
			{
				foreach (MethodDef method in array[j].Methods.Where((MethodDef m) => m.HasBody && m.Body.HasInstructions && m.Body.HasVariables))
				{
					Local[] array2 = method.Body.Variables.Where((Local v) => v.Type == module.ImportAsTypeSig(typeof(InsufficientMemoryException))).ToArray();
					foreach (Local variable in array2)
					{
						method.Body.Variables.Remove(variable);
						removed++;
					}
					RemoveUselessNops(method);
					method.Body.SimplifyBranches();
					IList<Instruction> instr = method.Body.Instructions;
					for (int i = 0; i < method.Body.Instructions.Count; i++)
					{
						if (instr[i].IsLdcI4() && instr[i + 1].IsStloc() && instr[i + 2].IsLdcI4() && instr[i + 3].IsLdcI4() && instr[i + 4].IsLdcI4() && instr[i + 5].OpCode == OpCodes.Xor && instr[i + 6].IsLdcI4() && instr[i + 8].IsLdcI4() && instr[i + 9].IsStloc() && instr[i + 12].OpCode == OpCodes.Nop)
						{
							i++;
							do
							{
								method.Body.Instructions.RemoveAt(i);
								removed++;
							}
							while (instr[i].OpCode != OpCodes.Nop);
						}
					}
					method.Body.OptimizeBranches();
					RemoveUselessNops(method);
				}
			}
			return removed;
		}
	}
}
