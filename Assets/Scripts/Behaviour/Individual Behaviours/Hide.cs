using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Automation
{
		/// <summary>
		/// Attempts to position the character so that an obstacle is between the character and the player. 
		/// </summary>
		public class Hide : AIBehaviour
		{

				// Script name - used in GetEntitiesWithTagName for debugging.
				private static readonly string SCRIPT_NAME = typeof(Hide).Name;

				void Start ()
				{
						Initialise ();
				}

				public override Vector2 GetForce ()
				{
						float distanceToClosest = float.MaxValue;
						Vector2 bestHidingSpot = Vector2.zero;

						var obstacles = GetEntitiesWithTagName (OBSTACLE_TAG_NAME, SCRIPT_NAME, LOGGING_ENABLED);
						var player = GetEntityWithTagName (PLAYER_TAG_NAME, SCRIPT_NAME, LOGGING_ENABLED);

						if (!player)
								return Vector2.zero;

						foreach (var obj in obstacles) {
								Vector2 hidingPos = GetHidingPosition (obj, player.transform.position);
				
								float dist = Vector2.Distance (hidingPos, transform.position);
				
								float distSquared = dist * dist;

								if (distSquared < distanceToClosest) {
										distanceToClosest = dist;					
										bestHidingSpot = hidingPos;	
								}
				
						} 
			
			
						return Arrive (bestHidingSpot, 10f);
				}

				private Vector2 GetHidingPosition (GameObject obj, Vector2 targetsPos)
				{
						float distanceFromBoundary = 10f;

						var collider = obj.GetComponent<CircleCollider2D> ();
			
						if (!collider) {
								Debug.LogError ("Behaviour, Hide: Obstacles must have circle collider 2D");
								return Vector2.zero;
						}
		
						float distAway = (collider.radius * obj.transform.localScale.x) + distanceFromBoundary;
			
						Vector2 objPos = new Vector2 (obj.transform.position.x, obj.transform.position.y);
			
						Vector2 toObj = (objPos - targetsPos).normalized;
			
						return (toObj * distAway) + objPos;
			
				}

		}
}
