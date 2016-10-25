using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Automation
{

		/// <summary>
		/// Gets force from the Behaviour system and applies it to the objects position.
		/// Attach to an entity that has behaviour scripts attached.
		/// </summary>
		[RequireComponent (typeof(BehaviourManager))]
		public class Entity : MonoBehaviour
		{
				public float MaxVelocity = 2f;
				public float Mass = 0.5f;

				// How far the agent can see - objects within this radius will be taken into account when apply behaviours.
				public float SightRadius = 20;	

				// Friction the agent experiences travelling.
				public float Friction = 1.01f;

				// Smoothing sums an a number of the agents movement updates. Use this if the agents movement is twitchy.
				public bool smoothing = true;

				[HideInInspector]
				public Vector2
						heading;

				[HideInInspector]
				public Vector2
						velocity;	

	

				private BehaviourManager behaviourManager;

				private Smoother smoother;

				/// <summary>
				/// Gets behaviour manager and smoother (if smoothing is enabled) and sets heading to an arbitrary position. 
				/// </summary>
				void Start ()
				{
						behaviourManager = GetComponent<BehaviourManager> ();

						velocity = Vector2.zero;
			
						float rotation = Random.Range (0f, 2f) * (Mathf.PI * 2);
						heading = new Vector2 ((float)Mathf.Sin (rotation), (float)-Mathf.Cos (rotation));

						if (smoothing) {

								// If smoother attached then use that.
								smoother = GetComponent<Smoother> ();

								// Else add smoother componenet.
								if (!smoother) {
										smoother = gameObject.AddComponent<Smoother> ();
								}
						}

				}

				private Vector2 PerformSmootingIfEnabled (Vector2 vel)
				{
						if (smoothing) {
								vel = smoother.UpdateHeading (velocity);
						}

						return vel;
				}

				/// <summary>
				/// Gets force from behaviour manager. Updates entities velocity based on force.
				/// Smooths agents velocity (if enabled) and finally applies velocity to agents position.
				/// </summary>
				void Update ()
				{
						Vector2 force = behaviourManager.GetForce (); 

						UpdateVelocity (force);

						velocity = PerformSmootingIfEnabled (velocity);
			
						velocity /= Friction;

						transform.position += (Vector3)velocity;

				}
		


				private void UpdateVelocity (Vector2 force)
				{
			
						Vector2 acceleration = force / Mass;
			
						velocity += acceleration * Time.deltaTime;
						velocity = Truncate (velocity, MaxVelocity);
			
						if (velocity.sqrMagnitude > 0.00000001) {
								heading = (velocity * Time.deltaTime).normalized;
						}
			
						if (velocity != Vector2.zero) {
								float angle = Mathf.Atan2 (heading.y, heading.x) * Mathf.Rad2Deg;
								transform.rotation = Quaternion.AngleAxis (angle, Vector3.forward);
						}
			
				}

				private Vector2 Truncate (Vector2 original, float max)
				{
						if (original.magnitude > max) {
								original.Normalize ();
				
								original *= max;
						}
			
						return original;
				}


		}
}
