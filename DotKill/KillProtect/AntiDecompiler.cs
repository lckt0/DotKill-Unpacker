using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using dnlib.DotNet;
using dnlib.DotNet.Emit;

namespace DotKill.KillProtect
{
    class AntiDecompiler
	{
		public static int count = 0;

		public static int Execute(ModuleDefMD module)
		{
			foreach (TypeDef type in module.GetTypes())
			{
				foreach (MethodDef method in type.Methods)
				{
					if (method != null && method.HasBody && method.Body.HasInstructions)
					{
						try
						{
							AntiDecompilerPhase.Execute(method);
							count++;
						}
						catch (Exception ex)
						{

						}
					}
				}
			}
			return count;
		}
	}
}
