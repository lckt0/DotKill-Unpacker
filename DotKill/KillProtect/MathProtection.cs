using dnlib.DotNet;
using dnlib.DotNet.Emit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DotKill.KillProtect
{
    class MathProtection
    {
		public static int removed = 0;

		public static int Execute(ModuleDefMD module)
		{
			foreach (TypeDef type in (from x in module.Types
									  where x.HasMethods
									  select x).ToArray<TypeDef>())
			{
				foreach (MethodDef method in (from x in type.Methods
											  where x.HasBody && x.Body.HasInstructions
											  select x).ToArray<MethodDef>())
				{
					for (int i = 0; i < method.Body.Instructions.Count; i++)
					{
						bool flag = method.Body.Instructions[i].OpCode == OpCodes.Call && method.Body.Instructions[i].Operand.ToString().Contains("System.Math::") && method.Body.Instructions[i].Operand.ToString().Contains("(System.Double)") && method.Body.Instructions[i - 1].OpCode == OpCodes.Ldc_R8;
						if (flag)
						{
							MemberRef memberRef = (MemberRef)method.Body.Instructions[i].Operand;
							MethodBase invoke = typeof(Math).GetMethod(memberRef.Name, new Type[] { typeof(double) });
							double arg = (double)method.Body.Instructions[i - 1].Operand;
							double result = (double)invoke.Invoke(null, new object[] { arg });
							method.Body.Instructions[i].OpCode = OpCodes.Ldc_R8;
							method.Body.Instructions[i].Operand = result;
							method.Body.Instructions[i - 1].OpCode = OpCodes.Nop;
							removed++;
						}
						bool flag2 = method.Body.Instructions[i].OpCode == OpCodes.Call && method.Body.Instructions[i].Operand.ToString().Contains("System.Math::") && method.Body.Instructions[i].Operand.ToString().Contains("(System.Single)") && method.Body.Instructions[i - 1].OpCode == OpCodes.Ldc_R4;
						if (flag2)
						{
							MemberRef memberRef2 = (MemberRef)method.Body.Instructions[i].Operand;
							MethodBase invoke2 = typeof(Math).GetMethod(memberRef2.Name, new Type[] { typeof(float) });
							float arg2 = (float)method.Body.Instructions[i - 1].Operand;
							float result2 = (float)invoke2.Invoke(null, new object[] { arg2 });
							method.Body.Instructions[i].OpCode = OpCodes.Ldc_R4;
							method.Body.Instructions[i].Operand = result2;
							method.Body.Instructions[i - 1].OpCode = OpCodes.Nop;
							removed++;
						}
						bool flag3 = method.Body.Instructions[i].OpCode == OpCodes.Call && method.Body.Instructions[i].Operand.ToString().Contains("System.Math::") && method.Body.Instructions[i].Operand.ToString().Contains("(System.Int32)") && method.Body.Instructions[i - 1].OpCode == OpCodes.Ldc_I4;
						if (flag3)
						{
							MemberRef memberRef3 = (MemberRef)method.Body.Instructions[i].Operand;
							MethodBase invoke3 = typeof(Math).GetMethod(memberRef3.Name, new Type[] { typeof(int) });
							int arg3 = (int)method.Body.Instructions[i - 1].Operand;
							int result3 = (int)invoke3.Invoke(null, new object[] { arg3 });
							method.Body.Instructions[i].OpCode = OpCodes.Ldc_I4;
							method.Body.Instructions[i].Operand = result3;
							method.Body.Instructions[i - 1].OpCode = OpCodes.Nop;
							removed++;
						}
						bool flag4 = method.Body.Instructions[i].OpCode == OpCodes.Call && method.Body.Instructions[i].Operand.ToString().Contains("System.Math::") && method.Body.Instructions[i].Operand.ToString().Contains("(System.Double,System.Double)") && method.Body.Instructions[i - 1].OpCode == OpCodes.Ldc_R8 && method.Body.Instructions[i - 2].OpCode == OpCodes.Ldc_R8;
						if (flag4)
						{
							MemberRef memberRef4 = (MemberRef)method.Body.Instructions[i].Operand;
							MethodBase invoke4 = typeof(Math).GetMethod(memberRef4.Name, new Type[]
							{
								typeof(double),
								typeof(double)
							});
							double arg4 = (double)method.Body.Instructions[i - 1].Operand;
							double arg5 = (double)method.Body.Instructions[i - 2].Operand;
							double result4 = (double)invoke4.Invoke(null, new object[] { arg4, arg5 });
							method.Body.Instructions[i].OpCode = OpCodes.Ldc_R8;
							method.Body.Instructions[i].Operand = result4;
							method.Body.Instructions[i - 1].OpCode = OpCodes.Nop;
							method.Body.Instructions[i - 2].OpCode = OpCodes.Nop;
							removed++;
						}
						bool flag5 = method.Body.Instructions[i].OpCode == OpCodes.Call && method.Body.Instructions[i].Operand.ToString().Contains("System.Math::") && method.Body.Instructions[i].Operand.ToString().Contains("(System.Single,System.Single)") && method.Body.Instructions[i - 1].OpCode == OpCodes.Ldc_R4 && method.Body.Instructions[i - 2].OpCode == OpCodes.Ldc_R4;
						if (flag5)
						{
							MemberRef memberRef5 = (MemberRef)method.Body.Instructions[i].Operand;
							MethodBase invoke5 = typeof(Math).GetMethod(memberRef5.Name, new Type[]
							{
								typeof(float),
								typeof(float)
							});
							float arg6 = (float)method.Body.Instructions[i - 1].Operand;
							float arg7 = (float)method.Body.Instructions[i - 2].Operand;
							float result5 = (float)invoke5.Invoke(null, new object[] { arg6, arg7 });
							method.Body.Instructions[i].OpCode = OpCodes.Ldc_R4;
							method.Body.Instructions[i].Operand = result5;
							method.Body.Instructions[i - 1].OpCode = OpCodes.Nop;
							method.Body.Instructions[i - 2].OpCode = OpCodes.Nop;
							removed++;
						}
						bool flag6 = method.Body.Instructions[i].OpCode == OpCodes.Call && method.Body.Instructions[i].Operand.ToString().Contains("System.Math::") && method.Body.Instructions[i].Operand.ToString().Contains("(System.Int32,System.Int32)") && method.Body.Instructions[i - 1].OpCode == OpCodes.Ldc_I4 && method.Body.Instructions[i - 2].OpCode == OpCodes.Ldc_I4;
						if (flag6)
						{
							MemberRef memberRef6 = (MemberRef)method.Body.Instructions[i].Operand;
							MethodBase invoke6 = typeof(Math).GetMethod(memberRef6.Name, new Type[]
							{
								typeof(int),
								typeof(int)
							});
							int arg8 = (int)method.Body.Instructions[i - 1].Operand;
							int arg9 = (int)method.Body.Instructions[i - 2].Operand;
							int result6 = (int)invoke6.Invoke(null, new object[] { arg8, arg9 });
							method.Body.Instructions[i].OpCode = OpCodes.Ldc_I4;
							method.Body.Instructions[i].Operand = result6;
							method.Body.Instructions[i - 1].OpCode = OpCodes.Nop;
							method.Body.Instructions[i - 2].OpCode = OpCodes.Nop;
							removed++;
						}
					}
				}
			}
			return removed;
		}
	}
}
