using UnityEngine;
using System.Collections;
using System.Collections.Generic;


namespace Automation
{
		/// <summary>
		/// Shoots projectiles towards mouse position.
		/// </summary>
		public class BulletManager : MonoBehaviour
		{

				private static readonly float PROJECTILE_SPEED = 150f;

				// Time between projectiles.
				private static readonly float TIME_TO_NEXT_PROJECTILE = 0.1f;

				// Max projectiles in scene.
				private static readonly float MAX_PROJECTILES = 30;

				// Keep track of projectiles to remove them from scene when max projectiles reached or parent object disabled.
				private List<GameObject> bullets = new List<GameObject> ();

				private float time = 0f;

				// Update is called once per frame
				void Update ()
				{
						time += Time.deltaTime;

						if (time >= TIME_TO_NEXT_PROJECTILE) {
								CreateBullet ();
								time = 0f;
						}
				}

				private void CreateBullet ()
				{
						var projectile = ObjectPool.instance.GetObjectForType ("Bullet", false);

						projectile.SetActive (true);

						Vector2 cursorInWorldPos = Camera.main.ScreenToWorldPoint (Input.mousePosition);
			
						Vector2 direction = cursorInWorldPos - new Vector2 (transform.position.x, transform.position.y);

						direction.Normalize ();

						projectile.transform.position = transform.position;

						projectile.GetComponent<Rigidbody2D> ().velocity = direction * PROJECTILE_SPEED;

						bullets.Add (projectile);

						LimitProjectileNum ();
			
				}

				private void LimitProjectileNum ()
				{
						if (bullets.Count > MAX_PROJECTILES) {
								ObjectPool.instance.PoolObject (bullets [0]);
								bullets.RemoveAt (0);
						}
				}

				void OnDisable ()
				{
						foreach (var bullet in bullets) {
								if (bullet != null) {
										ObjectPool.instance.PoolObject (bullet);
								}
						}

						bullets.Clear ();
				}
		}
}
