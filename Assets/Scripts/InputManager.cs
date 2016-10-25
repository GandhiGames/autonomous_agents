using UnityEngine;
using System.Collections;

namespace Automation
{
		public enum State
		{
				Flock,
				Hide,
				PathFollowing,
				AvoidProjectiles,
				AvoidPlayer,
				SeekPlayer,
				BlackHoleWander,
				Encircle,
				MobDisperse,
				Reset
}
		;

		/// <summary>
		/// Handles input for the demo scene.
		/// </summary>
		[RequireComponent (typeof(Director) )]
		public class InputManager : MonoBehaviour
		{
				private State state;
				private State previousState;

				private Director entityManager;

				void Start ()
				{
						state = State.Flock;
						previousState = State.Hide;

						entityManager = GetComponent<Director> ();
				}

				// Update is called once per frame
				void Update ()
				{
	
						if (Input.GetKeyUp (KeyCode.Alpha1) || Input.GetKeyUp (KeyCode.Keypad1)) {
								state = State.Flock;
						} else if (Input.GetKeyUp (KeyCode.Alpha2) || Input.GetKeyUp (KeyCode.Keypad2)) {
								state = State.Hide;
						} else if (Input.GetKeyUp (KeyCode.Alpha3) || Input.GetKeyUp (KeyCode.Keypad3)) {
								state = State.PathFollowing;
						} else if (Input.GetKeyUp (KeyCode.Alpha4) || Input.GetKeyUp (KeyCode.Keypad4)) {
								state = State.AvoidProjectiles;
						} else if (Input.GetKeyUp (KeyCode.Alpha5) || Input.GetKeyUp (KeyCode.Keypad5)) {
								state = State.AvoidPlayer;
						} else if (Input.GetKeyUp (KeyCode.Alpha6) || Input.GetKeyUp (KeyCode.Keypad6)) {
								state = State.SeekPlayer;
						} else if (Input.GetKeyUp (KeyCode.Alpha7) || Input.GetKeyUp (KeyCode.Keypad7)) {
								state = State.BlackHoleWander;
						} else if (Input.GetKeyUp (KeyCode.Alpha8) || Input.GetKeyUp (KeyCode.Keypad8)) {
								state = State.Encircle;
						} else if (Input.GetKeyUp (KeyCode.Alpha9) || Input.GetKeyUp (KeyCode.Keypad9)) {
								state = State.MobDisperse;
						} else if (Input.GetKeyUp (KeyCode.R)) {
								previousState = State.Reset;
						}

						if (HasStateChanged ()) {
								entityManager.UpdateState (state);
						}

						previousState = state;
			
				}

				private bool HasStateChanged ()
				{
						return previousState != state;
				}
	
		}
}
