using UnityEngine;
using System.Collections;

namespace Automation
{
		/// <summary>
		/// Simple player controller used for the demo scene.
		/// </summary>
		public class PlayerController : MonoBehaviour
		{

				[HideInInspector]
				public Vector2
						heading = Vector2.zero;

				[HideInInspector]
				public Vector2
						velocity = Vector2.zero;

				public float MaxSpeed = 5f;
				public float Friction = 1.05f;
				public float MoveSpeed = 2f;

				private static readonly int WIDTH = 510;
				private static readonly int HEIGHT = 195;

	 
				// Update is called once per frame
				void Update ()
				{
						float moveDirX = Input.GetAxisRaw ("Horizontal") * MoveSpeed;
						float moveDirY = Input.GetAxisRaw ("Vertical") * MoveSpeed;

						Vector2 force = new Vector2 (moveDirX, moveDirY);

						UpdateVelocity (force);

						velocity /= Friction;

						transform.position = new Vector3 (
											Mathf.Clamp (transform.position.x + velocity.x, 0, WIDTH), 
											Mathf.Clamp (transform.position.y + velocity.y, 0, HEIGHT),
											0f);

				}
	

				private void UpdateVelocity (Vector2 force)
				{
						Vector2 acceleration = force / 0.1f;
		
						velocity += acceleration * Time.deltaTime;

						velocity = Truncate (velocity, MaxSpeed);
		
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