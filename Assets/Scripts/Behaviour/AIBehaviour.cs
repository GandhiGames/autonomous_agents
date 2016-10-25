using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Automation
{
		/// <summary>
		/// Base class for each steering behaviour. 
		/// Also has a list of tag and layer names used throughout the project.
		/// </summary>
		[RequireComponent (typeof(Entity))]
		public abstract class AIBehaviour : MonoBehaviour
		{

				// Script name used for debug purposes.
				private static readonly string SCRIPT_NAME = typeof(AIBehaviour).Name;

				// Tag Names.
				protected static readonly string ENEMY_TAG_NAME = "Enemy";
				protected static readonly string OBSTACLE_TAG_NAME = "Obstacle";
				protected static readonly string PLAYER_TAG_NAME = "Player";
				protected static readonly string PROJECTILE_TAG_NAME = "Bullet";
				protected static readonly string WAYPOINT_TAG_NAME = "Waypoint";
				protected static readonly string BLACKHOLE_TAG_NAME = "BlackHole";

				// Layer Names.
				public static readonly string WALL_LAYER_NAME = "Wall";

				// Default weight for each behaviour - edit individual weights using inspector.
				public float Weight = 10f;

				[HideInInspector]
				protected Entity
						entity;

				public abstract Vector2 GetForce ();

				// If enabled will output debug logs when entities with required tags are not found in scene.
				protected static readonly bool LOGGING_ENABLED = true;

				void Start ()
				{
						Initialise ();
				}

				protected void Initialise ()
				{
						entity = GetComponent<Entity> ();

						if (!entity && LOGGING_ENABLED) {
								Debug.LogError (SCRIPT_NAME + ": Entity Script on object not found");
						}
				}

				/// <summary>
				/// Gets the entity in with tag name if in sight range.
				/// </summary>
				/// <returns>The entity if within sight radius.</returns>
				/// <param name="sightRadius">Sight radius.</param>
				/// <param name="tagName">Tag name of object.</param>
				/// <param name="scriptName">Script name of calling script. Used for debugging.</param>
				/// <param name="loggingEnabled">If set to <c>true</c> debug will be logged if entity not found.</param>
				protected GameObject GetEntityInSight (float sightRadius, 
		                                               string tagName, string scriptName, bool loggingEnabled)
				{
						var entity = GetEntityWithTagName (tagName, scriptName, loggingEnabled);

						if (entity) {
								float to = (entity.transform.position - transform.position).sqrMagnitude;

								if (to < (sightRadius * sightRadius)) {
										return entity;
								}
						}

						return null;

				}

				protected List<GameObject> GetEntitiesInSight (float sightRadius, 
		                                              string tagName, string scriptName, bool loggingEnabled)
				{
						var entities = GetEntitiesWithTagName (tagName, scriptName, loggingEnabled);

			
						var retVals = new List<GameObject> ();
			
						foreach (var obj in entities) {
								if (obj.gameObject.GetInstanceID () != this.gameObject.GetInstanceID ()) {
										float to = (obj.transform.position - transform.position).sqrMagnitude;
					
										if (to < (sightRadius * sightRadius)) {
												retVals.Add (obj);
										}
								}
						}
			
						return retVals;
				}

				/// <summary>
				/// Gets entities with tag name that are in scene. Does not take into account sight range.
				/// </summary>
				/// <returns>The entities with specified tag.</returns>
				/// <param name="tagName">Tag name of object.</param>
				/// <param name="scriptName">Script name for debugging.</param>
				/// <param name="loggingEnabled">If set to <c>true</c> debug will be logged if no object found.</param>
				protected GameObject[] GetEntitiesWithTagName (string tagName, string scriptName, bool loggingEnabled)
				{
						var entities = GameObject.FindGameObjectsWithTag (tagName);

						if (loggingEnabled && (entities == null || entities.Length == 0)) {
								Debug.Log (scriptName + ": No GameObject with tag " + tagName + " found");
						}

						return entities;
				}


				protected GameObject GetEntityWithTagName (string tagName, string scriptName, bool loggingEnabled)
				{
						var entity = GameObject.FindGameObjectWithTag (tagName);
			
						if (!entity && loggingEnabled) {
								Debug.Log (scriptName + ": No GameObject with tag " + tagName + " found");
						}
			
						return entity;
				}

				/// <summary>
				/// Arrive at the specified targetPos. Decelerates on arrival. Used by a number of
				/// different behaviours.
				/// </summary>
				/// <param name="targetPos">Target position.</param>
				/// <param name="deceleration">Deceleration rate.</param>
				protected Vector2 Arrive (Vector2 targetPos, float deceleration)
				{
						var toTarget = targetPos - (Vector2)transform.position;
			
						var dist = toTarget.magnitude;
			
						if (dist > 0) {
				
								var speed = dist / deceleration;
				
								speed = Mathf.Min (speed, entity.MaxVelocity);
				
								var desiredVel = toTarget * speed / dist;
				
								return (desiredVel - entity.velocity);
						}
			
						return Vector2.zero; 
		
				}
		}
}
