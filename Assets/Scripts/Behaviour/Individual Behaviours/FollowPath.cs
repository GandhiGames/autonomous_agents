using UnityEngine;
using System.Collections;

namespace Automation
{
		/// <summary>
		/// Follows a set path using waypoint manager. Path can be defined using list of vectors or as game objects.
		/// Returns a force that seeks to the current waypoint.
		/// </summary>
		[RequireComponent (typeof(WaypointManager))]
		public class FollowPath : AIBehaviour
		{
				// If not looped will end at last wapoint.
				public bool isLooped = true;

				// When entity is within this distance to waypoint it will be registered as visited.
				public float WaypointProximity = 5f;

				// Script name - used in GetEntitiesInSight for debugging.
				private static readonly string SCRIPT_NAME = typeof(FollowPath).Name;

				private WaypointManager waypointManager;

				void OnEnable ()
				{
						Initialise ();

						InitialiseWaypointManager ();
				}

				private void InitialiseWaypointManager ()
				{
						waypointManager = GetComponent<WaypointManager> ();

						//Find all game objects with tag name equal to WAYPOINT_TAG_NAME
						var waypoints = GetEntitiesWithTagName (WAYPOINT_TAG_NAME, SCRIPT_NAME, false);

						waypointManager.InitialiseWaypoints (waypoints);

						waypointManager.waypointProximity = WaypointProximity;
						waypointManager.isLooped = isLooped;
				}



				//Loops through all waypoints and seeks to current
				public override Vector2 GetForce ()
				{
						Vector2 force = Vector2.zero;

						if (waypointManager.InitialisedCorrectly ()) {

								if (waypointManager.isComplete)
										return force;

								var wayPointPos = waypointManager.GetCurrentWaypoint (transform.position);

								if (wayPointPos != Vector2.zero) {
										force = (wayPointPos - (Vector2)transform.position).normalized * entity.MaxVelocity;
								}
						}

						return force;

				}
		


	
		
		}
}
