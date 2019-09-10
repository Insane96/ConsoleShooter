using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shooter.Engine
{
				class Time
				{
								static Stopwatch sw = new Stopwatch();

								public static float deltaTime;

								public static void Update()
								{
												sw.Stop();
												deltaTime = sw.ElapsedMilliseconds / 1000f;
												sw.Reset();

												sw.Start();
								}
				}
}
