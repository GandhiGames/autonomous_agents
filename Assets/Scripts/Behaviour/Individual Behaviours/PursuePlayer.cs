using UnityEngine;
using System.Collections;

namespace Automation
{
		/// <summary>
		/// Attempts to intercept player, taking into account the players heading. 
		/// </summary>
		public class PursuePlayer : AIBehaviour
		{
				private static readonly string SCRIPT_NAME = typeof(PursuePlayer).Name;

				void Start ()
				{
						Initialise ();
				}
		
				public override Vector2 GetForce ()
				{
						var player = GetEntityWithTagName (PLAYER_TAG_NAME, SCRIPT_NAME, LOGGING_ENABLED);

						if (!player)
								return Vector2.zero;

						Vector2 toPlayer = player.transform.position - transform.position;

						PlayerController playerMovement = GetPlayerMovement (player);

						if (!playerMovement)
								return Vector2.zero;

						float relativeHeading = Vector2.Dot (entity.heading, playerMovement.heading);

						if ((Vector2.Dot (toPlayer, entity.heading) > 0)
								&& (relativeHeading < -0.95)) {
								return Arrive (player.transform.position, 10f);
						}

						float LookAheadTime = toPlayer.magnitude
								/ (entity.MaxVelocity + playerMovement.velocity.magnitude);

						Vector2 pos = (Vector2)player.transform.position;

						Vector2 seekPos = (pos + (playerMovement.velocity * LookAheadTime));

						return Arrive (seekPos, 10f);
				}
		

				private PlayerController GetPlayerMovement (GameObject obj)
				{
						var entity = obj.GetComponent<PlayerController> ();
			
						if (!entity) {
								Debug.Log (SCRIPT_NAME + ": PlayerController script not found for object " + obj.name);
						}
			
						return entity;
				}
		
		}
}
