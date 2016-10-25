using UnityEngine;
using System.Collections;

namespace Automation
{
		/// <summary>
		/// Steers the entity away from the player, if the player is within sight range.
		/// </summary>
		public class Flee : AIBehaviour
		{
				private static readonly string SCRIPT_NAME = typeof(Flee).Name;

				public override Vector2 GetForce ()
				{
						var player = GetEntityInSight (entity.SightRadius, PLAYER_TAG_NAME, SCRIPT_NAME, LOGGING_ENABLED);

						if (!player) {
								return Vector2.zero;
						}

						return transform.position - player.transform.position;
				}
		}
}
