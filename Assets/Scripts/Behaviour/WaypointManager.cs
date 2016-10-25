using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Automation
{
		/// <summary>
		/// Accepts an array of Game Objects (uses there position) or an array of Vector2s.
		/// GetCurrentWaypoint checks if current waypoint has been reached (and increments the current waypoint to the next in the list or the first 
		/// if the final waypoint has been reached and isLooped = true.
		/// </summary>
		public class WaypointManager : MonoBehaviour
		{

				// If not looped will end at last wapoint.
				[HideInInspector]
				public bool
						isLooped = true;

				// When entity is within this distance to waypoint it will be registered as visited.
				[HideInInspector]
				public float
						waypointProximity = 5f;

				// List of waypoint positions - initialised in InitiailiseWayPoints method.
				[HideInInspector]
				public List<Vector2>
						waypoints;

				[HideInInspector]
				public bool
						isComplete = false;

				private int currentWaypoint;

				private bool isInitialisedCorrectly = true;

				void OnEnable ()
				{
						waypoints = new List<Vector2> ();
						currentWaypoint = 0;
				}

				public void InitialiseWaypoints (GameObject[] waypoints)
				{
						ClearWaypoints ();

						foreach (var waypoint in waypoints) {
								this.waypoints.Add (waypoint.transform.position);
						}
			
				}

				public void InitialiseWaypoints (Vector2[] waypoints)
				{
						ClearWaypoints ();
						this.waypoints.AddRange (waypoints);
				}

			
				public void CheckHasReachedCurrentWaypointAndIncrement (Vector2 entityPos)
				{
						if (HasReachedCurrentWaypoint (GetWaypoint (currentWaypoint), entityPos))
								IncrementCurrentWaypoint ();
				}


				private bool HasReachedCurrentWaypoint (Vector2 waypointPos, Vector2 entityPos)
				{
						return (Vector2.Distance (waypointPos, entityPos) < waypointProximity);
				}

				public Vector2 GetCurrentWaypoint (Vector2 entityPos)
				{
						CheckHasReachedCurrentWaypointAndIncrement (entityPos);
						return GetWaypoint (currentWaypoint);
				}

				private Vector2 GetWaypoint (int waypointNum)
				{
						return waypoints [waypointNum];
				}

				private void IncrementCurrentWaypoint ()
				{
						if (currentWaypoint == waypoints.Count - 1) {
								if (isLooped) //seek to first waypoint
										currentWaypoint = 0;
								else
										isComplete = true;
						} else {
								currentWaypoint++;
						}
				}

				public void ClearWaypoints ()
				{
						waypoints.Clear ();
				}

				public bool InitialisedCorrectly ()
				{
						return isInitialisedCorrectly && waypoints.Count > 0;
				}
		}
}
