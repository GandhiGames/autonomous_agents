using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Automation
{
		/// <summary>
		/// Steers to avoid potential collisions with walls. Three rays are projected in front of the character
		/// </summary>
		public class WallAvoidance : AIBehaviour
		{
				// You would normally want the front facing ray to be longer than left/right.
				public float FrontFacingRayLength = 12f;
				public float LeftRightFacingRayLength = 10f;

				public float ObstacleRayAngle = 25f;
		
				// Use this for initialization
				void Start ()
				{
						Initialise ();
				}

				public override Vector2 GetForce ()
				{

						var frontRay = CreateFrontFacingRay ();

						var frontHit = Physics2D.Raycast (frontRay.origin, frontRay.direction, 
			                             FrontFacingRayLength, 1 << LayerMask.NameToLayer (WALL_LAYER_NAME));

						if (frontHit.collider != null && !frontHit.collider.OverlapPoint (frontRay.origin)) {
								var overShoot = (frontRay.origin + (frontRay.direction * LeftRightFacingRayLength)) - frontHit.point;
				
								var reflect = (Vector2)Vector3.Reflect (frontRay.direction, frontHit.normal);
				
								if (LOGGING_ENABLED)
										Debug.DrawRay (transform.position, reflect);
				
								return reflect * overShoot.magnitude;
						}
			
			    
			                             
						var rayList = CreateLeftRightRays ();

						foreach (Ray2D ray in rayList) {
								var hit = Physics2D.Raycast (ray.origin, ray.direction, 
				                                     LeftRightFacingRayLength, 1 << LayerMask.NameToLayer (WALL_LAYER_NAME));


								if (hit.collider != null && !hit.collider.OverlapPoint (ray.origin)) {

										var overShoot = (ray.origin + (ray.direction * LeftRightFacingRayLength)) - hit.point;

										var reflect = (Vector2)Vector3.Reflect (ray.direction, hit.normal);

										if (LOGGING_ENABLED)
												Debug.DrawRay (transform.position, reflect);

										return reflect * overShoot.magnitude;
										
								}

						}
			
						return Vector2.zero;
				}

				private Ray2D CreateFrontFacingRay ()
				{
						var frontRay = new Ray2D (transform.position, entity.heading);
			
						if (LOGGING_ENABLED)
								Debug.DrawRay (frontRay.origin, frontRay.direction, Color.green);

						return frontRay;
				}
		
				private List<Ray2D> CreateLeftRightRays ()
				{
						var rayList = new List <Ray2D> ();
			
						//right
						Vector2 rightDir = Quaternion.AngleAxis (-ObstacleRayAngle, Vector3.forward) * entity.heading;
						Ray2D rightRay = new Ray2D (transform.position, rightDir);
						rayList.Add (rightRay);

						if (LOGGING_ENABLED)
								Debug.DrawRay (rightRay.origin, rightRay.direction, Color.yellow);

						rightDir = Quaternion.AngleAxis (-ObstacleRayAngle * 2f, Vector3.forward) * entity.heading;
						rightRay = new Ray2D (transform.position, rightDir);
						rayList.Add (rightRay);

						if (LOGGING_ENABLED)
								Debug.DrawRay (rightRay.origin, rightRay.direction, Color.yellow);


						//left
						Vector2 leftDir = Quaternion.AngleAxis (ObstacleRayAngle, Vector3.forward) * entity.heading;
						Ray2D leftRay = new Ray2D (transform.position, leftDir);
						rayList.Add (leftRay);

						if (LOGGING_ENABLED)
								Debug.DrawRay (leftRay.origin, leftRay.direction, Color.blue);

						leftDir = Quaternion.AngleAxis (ObstacleRayAngle * 2f, Vector3.forward) * entity.heading;
						leftRay = new Ray2D (transform.position, leftDir);
						rayList.Add (leftRay);

						if (LOGGING_ENABLED)
								Debug.DrawRay (leftRay.origin, leftRay.direction, Color.blue);


						return rayList;
				}
		}
}
