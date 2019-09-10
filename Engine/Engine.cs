using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shooter.Engine
{
				class Engine
				{
								/// <summary>
								/// Needs to be called in the Main() do..while 
								/// Takes care of updating Input, Time and Renderer
								/// </summary>
								public static void MainLoop()
								{
												Input.UpdateKeyPress();
												Time.Update();
												Renderer.Draw();
								}

				}
}
