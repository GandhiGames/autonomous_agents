using UnityEngine;
using System.Collections;

namespace Automation
{
		/// <summary>
		/// Changes the background colour when UpdateBackgroundCOlour is called.
		/// Used for the demo scene.
		/// Attach to a camera.
		/// </summary>
		[RequireComponent (typeof(Camera))]
		public class BackgroundColourChanger : MonoBehaviour
		{

				public Color[] BackgroundColours;
				private int currentColour;

				// Use this for initialization
				void Start ()
				{
						currentColour = 0;
						UpdateBackgroundColour (currentColour);
				}

				private void UpdateBackgroundColour (int index)
				{
						GetComponent<Camera>().backgroundColor = BackgroundColours [index];
				}
	
				public void IncrementBackgroundColour ()
				{
						currentColour = (currentColour + 1) % BackgroundColours.Length;

						UpdateBackgroundColour (currentColour);
				}
		}
}
