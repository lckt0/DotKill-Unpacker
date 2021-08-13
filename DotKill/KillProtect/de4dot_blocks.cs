using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using dnlib.DotNet;
using dnlib.DotNet.Emit;
using de4dot.blocks;
using de4dot.blocks.cflow;

namespace DotKill.KillProtect
{
    class de4dot_blocks
	{
		public static string Execute(ModuleDefMD module)
		{
            foreach (TypeDef type in module.GetTypes())
            {
                foreach (MethodDef method in type.Methods)
                {
                    if (method != null)
                    {
                        try
                        {
                            BlocksCflowDeobfuscator blocksCflowDeobfuscator = new BlocksCflowDeobfuscator();
                            Blocks blocks = new Blocks(method);
                            blocksCflowDeobfuscator.Initialize(blocks);
                            blocksCflowDeobfuscator.Deobfuscate();
                            blocks.RepartitionBlocks();
                            IList<Instruction> list;
                            IList<ExceptionHandler> exceptionHandlers;
                            blocks.GetCode(out list, out exceptionHandlers);
                            DotNetUtils.RestoreBody(method, list, exceptionHandlers);
                        }
                        catch (Exception)
                        {
                            break;
                        }
					}
				}
            }
			return "N/A";
		}
	}
}
