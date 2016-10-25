using UnityEngine;
using System.Collections;

namespace Automation
{

		/// <summary>
		/// Provides a seperation force from any entity within sight with tag equal to PLAYER_TAG_NAME.
		/// </summary>
		public class AvoidPlayer : AIBehaviour
		{

				// Script name - used in GetEntitiesInSight for debugging.
				private static readonly string SCRIPT_NAME = typeof(AvoidPlayer).Name;

				// Increases the strength of the force - lower numbers equals a stronger force.
				private static readonly float MAG_OFFSET = 0.1f;

				void Start ()
				{
						Initialise ();
				}

				/// <summary>
				/// Iterates through any objects in sight that have a tag of PLAYER_TAG_NAME and returns a seperation force.
				/// </summary>
				/// <returns>The seperation force.</returns>
				public override Vector2 GetForce ()
				{
						Vector2 steeringForce = Vector2.zero;
			
						var entities = GetEntitiesInSight (entity.SightRadius, PLAYER_TAG_NAME, SCRIPT_NAME, LOGGING_ENABLED);
			
						foreach (var obj in entities) {
								Vector2 toAgent = transform.position - obj.transform.position;
								steeringForce += toAgent.normalized / (toAgent.magnitude * MAG_OFFSET);
						}
			
						return steeringForce;
				}
		}
}
