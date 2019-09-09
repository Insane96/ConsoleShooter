using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shooter.Enemies
{
				class FastEnemy : Enemy
				{
								public FastEnemy(float health, float damage, Vector2 pos, Vector2 shootingPos, float movementSpeed, float maxMovementSpeed, float increaseMovementSpeed, float initialAttackSpeed, float maxAttackSpeed, float cooldownReduction, Vector2 size, string[,] shape) : base(health, damage, pos, shootingPos, movementSpeed, maxMovementSpeed, increaseMovementSpeed, initialAttackSpeed, maxAttackSpeed, cooldownReduction, size, shape)
								{

								}

				}
}
