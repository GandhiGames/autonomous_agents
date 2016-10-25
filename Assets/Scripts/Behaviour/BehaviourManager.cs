using UnityEngine;
using System.Collections;

namespace Automation
{
		/// <summary>
		/// Loads all attached scripts that inherit from AIBehaviour.
		/// Iterates over each behaviour and combines their force multipled by their respective weights.
		/// </summary>
		public class BehaviourManager : MonoBehaviour
		{
				private AIBehaviour[] behaviours;

				// Use this for initialization
				void Start ()
				{
						GetBehaviours ();
				}

				private void GetBehaviours ()
				{
						behaviours = gameObject.GetComponents<AIBehaviour> ();
			
						if (behaviours == null || behaviours.Length == 0) {
								Debug.LogError ("Entity: No Behaviour Scripts Attached to Object " + gameObject.name);
						}
			
				}


				public Vector2 GetForce ()
				{
						Vector2 force = Vector2.zero;

						foreach (var behaviour in behaviours) {

								if (behaviour.enabled) {
										force += behaviour.GetForce () * behaviour.Weight;
								}
						}

						return force;

				}



		}
}
