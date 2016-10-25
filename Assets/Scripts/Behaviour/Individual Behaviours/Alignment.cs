using UnityEngine;
using System.Collections;

namespace Automation
{
		/// <summary>
		/// Group Behaviour.
		/// 
		/// Aligns an entities heading with other entities within it's sight radius.
		/// </summary>
		public class Alignment : AIBehaviour
		{

				// Script name - used in GetEntitiesInSight for debugging.
				private static readonly string SCRIPT_NAME = typeof(Alignment).Name;

				void Start ()
				{
						Initialise ();
				}


				/// <summary>
				/// Iterates through entities in sight range, sums the entities headings and returns the average.
				/// </summary>
				/// <returns>The alignment force.</returns>
				public override Vector2 GetForce ()
				{

						Vector2 averageHeading = Vector2.zero;

						var entities = GetEntitiesInSight (entity.SightRadius, ENEMY_TAG_NAME, SCRIPT_NAME, LOGGING_ENABLED);
			
						foreach (var obj in entities) {
								averageHeading += GetObjHeading (obj);
						}

			
						if (entities.Count > 0) {
								averageHeading /= entities.Count;
								averageHeading -= entity.heading;
						}

						return averageHeading;
				}

				/// <summary>
				/// Gets the object Entity script attached to the object and returns the entities heading.
				/// </summary>
				/// <returns>The objects heading or vector2.zero if Entity script is not found.</returns>
				/// <param name="obj">Object.</param>
				private Vector2 GetObjHeading (GameObject obj)
				{
						var entityScript = obj.GetComponent<Entity> ();
			
						if (!entityScript) {
								Debug.Log (SCRIPT_NAME + ": No Script with name entity found");
								return Vector2.zero;
						}

						return entityScript.heading;
				}

		}
}
