using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Automation
{
		/// <summary>
		/// Sometimes objects following different behaviours find themselves in situations where conflicting forces
		/// are applied, this can cause the object to jitter. Smoothing attempts to solve this problem by averaging
		/// the objects heading over a number of time steps (set by SampleSize).
		/// </summary>
		public class Smoother : MonoBehaviour
		{
				// Number of headings to average.
				public int SampleSize = 5;

				private List<Vector2> history;

				private int nextUpdate;

				void OnEnable ()
				{
						history = new List<Vector2> ();
		
						for (int i = 0; i < SampleSize; i++) {
								history.Add (Vector2.zero);
						}

						nextUpdate = 0;
				}

				public Vector2 UpdateHeading (Vector2 heading)
				{
						history.RemoveAt (nextUpdate);

						history.Insert (nextUpdate++, heading);

						if (nextUpdate == SampleSize)
								nextUpdate = 0;

						Vector2 sum = Vector2.zero;

						for (int i = 0; i < SampleSize; i++) {
								sum += history [i];
						}

						sum /= SampleSize;

						return sum;
				}

		}
}
