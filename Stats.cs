using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace Shooter
{
				public class Stats
				{
								public float experience;
								public float damageTaken;
								public float damageDealt;

								public Stats()
								{
												experience = 0f;
												damageTaken = 0f;
												damageDealt = 0f;
								}
				}
}
