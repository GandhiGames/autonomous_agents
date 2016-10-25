using UnityEngine;
using System.Collections;

namespace Automation
{
		/// <summary>
		/// Removes object from scene when hit by an object with tag defined by ProjectileTag.
		/// Attach to any objects that you want to be destroyed when hit by projectile.
		/// </summary>
		public class BulletDamage : MonoBehaviour
		{

				public string ProjectileTag = "Bullet";
	

				void OnTriggerEnter2D (Collider2D other)
				{
						if (other.gameObject.tag == ProjectileTag) {
								ObjectPool.instance.PoolObject (this.gameObject);
						}
				}
		}
}
