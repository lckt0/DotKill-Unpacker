using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using dnlib.DotNet;
using dnlib.DotNet.Emit;

namespace DotKill.KillProtect
{
    class AntiDe4Dot
    {
		public static int countofths = 0;

		public static int removed = 0;

		public static int removedantidedot = 0;

		// Token: 0x06000003 RID: 3 RVA: 0x00002084 File Offset: 0x00000284
		public static int ExecuteDe4Dot(ModuleDefMD module)
		{
			foreach (TypeDef type in (from t in module.GetTypes()
									  where t.FullName.Contains("Form") && t.HasInterfaces && t.Interfaces.Count == 2
									  select t).ToArray<TypeDef>())
			{
				module.Types.Remove(type);
				removedantidedot++;
			}
			return removedantidedot;
		}

		public static string Execute(ModuleDefMD module)
		{
			foreach (TypeDef type in from t in module.Types where t.HasMethods select t)
			{
				foreach (MethodDef method in type.Methods.Where((MethodDef m) => m.HasBody && m.Body.HasInstructions))
				{
					bool hasBody = method.HasBody;
					if (hasBody)
					{
						IList<Instruction> instr = method.Body.Instructions;
						for (int i = 0; i < instr.Count; i++)
						{
							bool flag = instr[i].OpCode == OpCodes.Nop && !IsNopBranchTarget(method, instr[i]) && !IsNopSwitchTarget(method, instr[i]) && !IsNopExceptionHandlerTarget(method, instr[i]);
							if (flag)
							{
								instr.RemoveAt(i);
								removed++;
								i--;
							}
						}
					}
				}
			}
			Execute2(module);
			return countofths.ToString() + "+" + removed.ToString();
		}

		private static IList<TypeDef> dgsjakjd(ModuleDef A_0)
		{
			return A_0.Types;
		}

		public static void Execute2(ModuleDefMD module)
		{
			for (int i = 0; i < module.Types.Count; i++)
			{
				TypeDef typeDef = module.Types[i];
				bool hasInterfaces = typeDef.HasInterfaces;
				if (hasInterfaces)
				{
					for (int jic = 0; jic < typeDef.Interfaces.Count; jic++)
					{
						bool flag19 = typeDef.Interfaces[jic].Interface != null;
						if (flag19)
						{
							bool flag20 = typeDef.Interfaces[jic].Interface.Name.Contains(typeDef.Name) || typeDef.Name.Contains(typeDef.Interfaces[jic].Interface.Name);
							if (flag20)
							{
								module.Types.RemoveAt(i);
								countofths++;
							}
						}
					}
				}
			}

			for (int j = 0; j < module.CustomAttributes.Count; j++)
			{
				CustomAttribute attribute = module.CustomAttributes[j];
				bool flag = attribute == null;
				bool flag21 = !flag;
				if (flag21)
				{
					TypeDef type = attribute.AttributeType.ResolveTypeDef();
					bool flag2 = type == null;
					bool flag22 = !flag2;
					if (flag22)
					{
						bool flag3 = type.Name == "ConfusedByAttribute";
						bool flag23 = flag3;
						if (flag23)
						{
							dgsjakjd(module).Remove(type);
							module.CustomAttributes.Remove(attribute);
							countofths++;
						}
						bool flag2123132123123213 = type.Name == "ZYXDNGuarder";
						bool flag24 = flag2123132123123213;
						if (flag24)
						{
							dgsjakjd(module).Remove(type);
							module.CustomAttributes.Remove(attribute);
							countofths++;
						}
						bool flag4 = type.Name == "YanoAttribute";
						bool flag25 = flag4;
						if (flag25)
						{
							dgsjakjd(module).Remove(type);
							module.CustomAttributes.Remove(attribute);
							countofths++;
						}
						bool flag5 = type.Name == "Xenocode.Client.Attributes.AssemblyAttributes.ProcessedByXenocode";
						bool flag26 = flag5;
						if (flag26)
						{
							dgsjakjd(module).Remove(type);
							module.CustomAttributes.Remove(attribute);
							countofths++;
						}
						bool flag6 = type.Name == "SmartAssembly.Attributes.PoweredByAttribute";
						bool flag27 = flag6;
						if (flag27)
						{
							dgsjakjd(module).Remove(type);
							module.CustomAttributes.Remove(attribute);
							countofths++;
						}
						bool flag7 = type.Name == "SecureTeam.Attributes.ObfuscatedByAgileDotNetAttribute";
						bool flag28 = flag7;
						if (flag28)
						{
							dgsjakjd(module).Remove(type);
							module.CustomAttributes.Remove(attribute);
							countofths++;
						}
						bool flag8 = type.Name == "ObfuscatedByGoliath";
						bool flag29 = flag8;
						if (flag29)
						{
							dgsjakjd(module).Remove(type);
							module.CustomAttributes.Remove(attribute);
							countofths++;
						}
						bool flag9 = type.Name == "NineRays.Obfuscator.Evaluation";
						bool flag30 = flag9;
						if (flag30)
						{
							dgsjakjd(module).Remove(type);
							module.CustomAttributes.Remove(attribute);
							countofths++;
						}
						bool flag10 = type.Name == "EMyPID_8234_";
						bool flag31 = flag10;
						if (flag31)
						{
							dgsjakjd(module).Remove(type);
							module.CustomAttributes.Remove(attribute);
							countofths++;
						}
						bool flag11 = type.Name == "DotfuscatorAttribute";
						bool flag32 = flag11;
						if (flag32)
						{
							dgsjakjd(module).Remove(type);
							module.CustomAttributes.Remove(attribute);
							countofths++;
						}
						bool flag12 = type.Name == "CryptoObfuscator.ProtectedWithCryptoObfuscatorAttribute";
						bool flag33 = flag12;
						if (flag33)
						{
							dgsjakjd(module).Remove(type);
							module.CustomAttributes.Remove(attribute);
							countofths++;
						}
						bool flag13 = type.Name == "BabelObfuscatorAttribute";
						bool flag34 = flag13;
						if (flag34)
						{
							dgsjakjd(module).Remove(type);
							module.CustomAttributes.Remove(attribute);
							countofths++;
						}
						bool flag14 = type.Name == ".NETGuard";
						bool flag35 = flag14;
						if (flag35)
						{
							dgsjakjd(module).Remove(type);
							module.CustomAttributes.Remove(attribute);
							countofths++;
						}
						bool flag15 = type.Name == "OrangeHeapAttribute";
						bool flag36 = flag15;
						if (flag36)
						{
							dgsjakjd(module).Remove(type);
							module.CustomAttributes.Remove(attribute);
							countofths++;
						}
						bool flag16 = type.Name == "WTF";
						bool flag37 = flag16;
						if (flag37)
						{
							dgsjakjd(module).Remove(type);
							module.CustomAttributes.Remove(attribute);
							countofths++;
						}
						bool flag17 = type.Name == "<ObfuscatedByDotNetPatcher>";
						bool flag38 = flag17;
						if (flag38)
						{
							dgsjakjd(module).Remove(type);
							module.CustomAttributes.Remove(attribute);
							countofths++;
						}
						bool duwggdyq3e6f7yqwsdas = type.Name == "SecureTeam.Attributes.ObfuscatedByCliSecureAttribute";
						bool flag39 = duwggdyq3e6f7yqwsdas;
						if (flag39)
						{
							dgsjakjd(module).Remove(type);
							module.CustomAttributes.Remove(attribute);
							countofths++;
						}
						bool sajdha8edy7128 = type.Name == "SmartAssembly.Attributes.PoweredByAttribute";
						bool flag40 = sajdha8edy7128;
						if (flag40)
						{
							dgsjakjd(module).Remove(type);
							module.CustomAttributes.Remove(attribute);
							countofths++;
						}
						bool flag18 = type.Name == "Xenocode.Client.Attributes.AssemblyAttributes.ProcessedByXenocode";
						bool flag41 = flag18;
						if (flag41)
						{
							dgsjakjd(module).Remove(type);
							module.CustomAttributes.Remove(attribute);
							countofths++;
						}
						bool flag42 = type.Name == "OiCuntJollyGoodDayYeHavin_____________________________________________________";
						if (flag42)
						{
							dgsjakjd(module).Remove(type);
							module.CustomAttributes.Remove(attribute);
							countofths++;
						}
						bool flag43 = type.Name == "ProtectedWithCryptoObfuscatorAttribute";
						if (flag43)
						{
							dgsjakjd(module).Remove(type);
							module.CustomAttributes.Remove(attribute);
							countofths++;
						}
						bool flag44 = type.Name == "NetGuard";
						if (flag44)
						{
							dgsjakjd(module).Remove(type);
							module.CustomAttributes.Remove(attribute);
							countofths++;
						}
						bool flag45 = type.Name == "ZYXDNGuarder";
						if (flag45)
						{
							dgsjakjd(module).Remove(type);
							module.CustomAttributes.Remove(attribute);
							countofths++;
						}
						bool flag46 = type.Name == "DotfuscatorAttribute";
						if (flag46)
						{
							dgsjakjd(module).Remove(type);
							module.CustomAttributes.Remove(attribute);
							countofths++;
						}
						bool flag47 = type.Name == "SecureTeam.Attributes.ObfuscatedByAgileDotNetAttribute";
						if (flag47)
						{
							dgsjakjd(module).Remove(type);
							module.CustomAttributes.Remove(attribute);
							countofths++;
						}
					}
				}
			}
			foreach (TypeDef type2 in from t in module.Types.ToList<TypeDef>()
									  where t.HasInterfaces
									  select t)
			{
				for (int k = 0; k < type2.Interfaces.Count; k++)
				{
					bool flag48 = type2.Interfaces[k].Interface.Name.Contains(type2.Name) || type2.Name.Contains(type2.Interfaces[k].Interface.Name);
					if (flag48)
					{
						module.Types.Remove(type2);
						countofths++;
					}
				}
			}
			List<string> fakeObfuscators = new List<string>
			{
				"DotNetPatcherObfuscatorAttribute", "DotNetPatcherPackerAttribute", "DotfuscatorAttribute", "ObfuscatedByGoliath", "dotNetProtector", "PoweredByAttribute", "AssemblyInfoAttribute", "BabelAttribute", "CryptoObfuscator.ProtectedWithCryptoObfuscatorAttribute", "Xenocode.Client.Attributes.AssemblyAttributes.ProcessedByXenocode",
				"NineRays.Obfuscator.Evaluation", "YanoAttribute", "SmartAssembly.Attributes.PoweredByAttribute", "NetGuard", "SecureTeam.Attributes.ObfuscatedByCliSecureAttribute", "Reactor", "ZYXDNGuarder", "CryptoObfuscator", "MaxtoCodeAttribute", ".NETReactorAttribute",
				"BabelObfuscatorAttribute"
			};
			for (int l = 0; l < module.Types.ToList<TypeDef>().Count; l++)
			{
				bool flag49 = fakeObfuscators.Contains(module.Types[l].Name);
				if (flag49)
				{
					module.Types.Remove(module.Types[l]);
					countofths++;
				}
			}
		}

		private static bool IsNopExceptionHandlerTarget(MethodDef method, Instruction nopInstr)
		{
			bool flag = !method.Body.HasExceptionHandlers;
			bool result;
			if (flag)
			{
				result = false;
			}
			else
			{
				IList<ExceptionHandler> exceptionHandlers = method.Body.ExceptionHandlers;
				foreach (ExceptionHandler exceptionHandler in exceptionHandlers)
				{
					bool flag2 = exceptionHandler.FilterStart == nopInstr || exceptionHandler.HandlerEnd == nopInstr || exceptionHandler.HandlerStart == nopInstr || exceptionHandler.TryEnd == nopInstr || exceptionHandler.TryStart == nopInstr;
					if (flag2)
					{
						return true;
					}
				}
				result = false;
			}
			return result;
		}

		private static bool IsNopSwitchTarget(MethodDef method, Instruction nopInstr)
		{
			IList<Instruction> instr = method.Body.Instructions;
			for (int i = 0; i < instr.Count; i++)
			{
				bool flag = instr[i].OpCode.OperandType == OperandType.InlineSwitch && instr[i].Operand != null;
				if (flag)
				{
					Instruction[] source = (Instruction[])instr[i].Operand;
					bool flag2 = source.Contains(nopInstr);
					if (flag2)
					{
						return true;
					}
				}
			}
			return false;
		}

		private static bool IsNopBranchTarget(MethodDef method, Instruction nopInstr)
		{
			IList<Instruction> instr = method.Body.Instructions;
			for (int i = 0; i < instr.Count; i++)
			{
				bool flag = instr[i].OpCode.OperandType == OperandType.InlineBrTarget || (instr[i].OpCode.OperandType == OperandType.ShortInlineBrTarget && instr[i].Operand != null);
				if (flag)
				{
					Instruction instruction2 = (Instruction)instr[i].Operand;
					bool flag2 = instruction2 == nopInstr;
					if (flag2)
					{
						return true;
					}
				}
			}
			return false;
		}
	}
}
