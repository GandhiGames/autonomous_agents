using UnityEngine;
using System.Collections;

namespace Automation
{
		/// <summary>
		/// Any object with the black hole tag will pull the character towards it. 
		/// </summary>
		public class GravitationPull : AIBehaviour
		{
				public float StrengthOfPull = 2;

				//Script name - used in GetEntitiesInSight for debugging
				private static readonly string SCRIPT_NAME = typeof(GravitationPull).Name;

				private GameObject[] blackHoles;

				// Use this for initialization
				void Start ()
				{
						Initialise ();

						if (LOGGING_ENABLED) {
								CheckBlackHolesPresentInScene ();
						}
				}

				private void CheckBlackHolesPresentInScene ()
				{
						GetEntitiesWithTagName (BLACKHOLE_TAG_NAME, SCRIPT_NAME, LOGGING_ENABLED);
				}

				public override Vector2 GetForce ()
				{

						Vector2 force = Vector2.zero;

						if (blackHoles == null) {
								blackHoles = GetEntitiesWithTagName (BLACKHOLE_TAG_NAME, SCRIPT_NAME, LOGGING_ENABLED);
						}

						foreach (var blackHole in blackHoles) {

								var radius = GetBlackHoleRadius (blackHole);

								float to = (blackHole.transform.position - transform.position).sqrMagnitude;
				
								if (to < (radius * radius)) {
										var dPos = blackHole.transform.position - transform.position;
										var length = dPos.magnitude;
					
					
										force += ScaleTo (dPos, Mathf.Lerp (StrengthOfPull, 0, length / 200f));
								}

						}

						return force;
				}

				private float GetBlackHoleRadius (GameObject obj)
				{
						var collider = obj.GetComponent<CircleCollider2D> ();

						if (!collider) {
								Debug.LogError (SCRIPT_NAME + ": object " + obj.name + " does not have a circlecollider2d, which is used to" +
										"define the radius of the black hole");
								return 0;
						}

						return collider.radius;
				}

				private Vector2 ScaleTo (Vector2 vector, float length)
				{
						return vector * (length / vector.magnitude);
				}
		


		}
}
