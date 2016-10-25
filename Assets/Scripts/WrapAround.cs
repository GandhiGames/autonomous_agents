using UnityEngine;
using System.Collections;

namespace Automation
{
		/// <summary>
		/// Wrap around used for demo scene.
		/// </summary>
		public class WrapAround : MonoBehaviour
		{
				Renderer[] renderers;
				bool isWrappingX = false;
				bool isWrappingY = false;


				void Start ()
				{
						renderers = GetComponentsInChildren<Renderer> ();
				}	
	

				void Update ()
				{
						var isVisible = CheckRenderers ();
			
						if (isVisible) {
								isWrappingX = false;
								isWrappingY = false;
								return;
						}
			
						if (isWrappingX && isWrappingY) {
								return;
						}
			
						var cam = Camera.main;
						var viewportPosition = cam.WorldToViewportPoint (transform.position);
						var newPosition = transform.position;
			
						if (!isWrappingX && (viewportPosition.x > 1 || viewportPosition.x < 0)) {
								newPosition.x = -newPosition.x;
				
								isWrappingX = true;
						}
			
						if (!isWrappingY && (viewportPosition.y > 1 || viewportPosition.y < 0)) {
								newPosition.y = -newPosition.y;
				
								isWrappingY = true;
						}
			
						transform.position = newPosition;
				}

				private bool CheckRenderers ()
				{
						foreach (var renderer in renderers) {

								if (renderer.isVisible) {
										return true;
								}
						}
		
						return false;
				}
		}
}