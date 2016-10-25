using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Automation
{
		/// <summary>
		/// Initialises the agents used in the demo scene. Uses a simple switch to initialise different entities based on users input.
		/// </summary>
		public class Director : MonoBehaviour
		{

				private List<GameObject> entities;

				private List<Vector2> wayPointVectors;

				private BackgroundColourChanger colourChanger;

				private const int NumOfFlock = 40;
				private const int NumOfProjAvoid = 60;
				private const int NumOfPlayerAvoid = 180;
				private const int NumOfEncircle = 40;
				private const int NumOfMob = 40;
				private const float boundsWidth = 13f;
				private const float boundsHeight = 5f;
	
				void Start ()
				{
						entities = new List<GameObject> ();
						wayPointVectors = new List<Vector2> ();
						colourChanger = FindObjectOfType<BackgroundColourChanger> ();
				}

				public void UpdateState (State state)
				{
						DestroyEntities ();

						colourChanger.IncrementBackgroundColour ();
			
						switch (state) {
						case State.Flock: 
								InitialiseFlock ();
								break;
						case State.Hide:
								InitialiseHide ();
								break;
						case State.PathFollowing:
								InitialisePathFollowing ();
								break;
						case State.AvoidProjectiles:
								InitAvoidProjectiles ();
								break;
						case State.AvoidPlayer:
								InitAvoidPlayer ();
								break;
						case State.SeekPlayer:
								InitSeekPlayer ();
								break;	
						case State.BlackHoleWander:
								InitBlackHole ();
								break;
						case State.Encircle:
								InitEncircle ();
								break;
						case State.MobDisperse:
								InitMobDisperse ();
								break;
						}
				}



				private void DestroyEntities ()
				{

						foreach (var entity in entities) {
								ObjectPool.instance.PoolObject (entity);
						}

						entities.Clear ();

				}

				private void InitialiseFlock ()
				{

						for (int i = 0; i < NumOfFlock; i++) {
								InitEntityAtPos ("Flock", GetRandomVectorInBounds ());
						}
				}

				private void InitialiseHide ()
				{
						InitEntityAtPosWithScale ("Obstacle", new Vector2 (249, 101), new Vector3 (6, 6, 1));
						InitEntityAtPos ("Player", new Vector2 (20, 20));
						InitEntityAtPos ("Hide", new Vector2 (180, 60));
				}

				private void InitialisePathFollowing ()
				{
						InitWaypointVectors ();

						for (int i = 0; i < wayPointVectors.Count; i++) {
								InitEntityAtPos ("Waypoint", wayPointVectors [i]);
						}

			
						InitEntityAtPos ("PathFollowing", new Vector2 (100, 100));
				}

				private void InitWaypointVectors ()
				{
						wayPointVectors.Clear ();

						wayPointVectors.Add (new Vector2 (75, 154));
						wayPointVectors.Add (new Vector2 (230, 77));
						wayPointVectors.Add (new Vector2 (390, 32));
						wayPointVectors.Add (new Vector2 (464, 113));
						wayPointVectors.Add (new Vector2 (420, 169));
						wayPointVectors.Add (new Vector2 (237, 40));
						wayPointVectors.Add (new Vector2 (125, 83));
				}

				private void InitAvoidProjectiles ()
				{
						InitEntityAtPos ("PlayerWithProjectiles", new Vector2 (250, 150));

						for (int i = 0; i < NumOfProjAvoid; i++) {
								InitEntityAtPos ("AvoidProjectiles", new Vector2 (20 + i, 20 + i));
						}
				}

				private void InitAvoidPlayer ()
				{
						InitEntityAtPosWithScale ("Obstacle", new Vector2 (120, 101), new Vector3 (2, 2, 1));
						InitEntityAtPosWithScale ("Obstacle", new Vector2 (249, 101), new Vector3 (3, 3, 1));
						InitEntityAtPosWithScale ("Obstacle", new Vector2 (381, 101), new Vector3 (4, 4, 1));
						InitEntityAtPos ("Player", new Vector2 (250, 150));

						for (int i = 0; i < NumOfPlayerAvoid; i++) {
								InitEntityAtPos ("AvoidPlayer", new Vector2 (150, (i + 12) + 1.5f));
						}
				}

				private void InitSeekPlayer ()
				{
						InitEntityAtPos ("Player", new Vector2 (250, 150));
						InitEntityAtPos ("SeekPlayer", new Vector2 (50, 50));
				}

				private void InitWander ()
				{
						InitEntityAtPos ("Wander", new Vector2 (50, 50));
				}

				private void InitBlackHole ()
				{
						InitEntityAtPos ("BlackHole", new Vector2 (99, 101));
						InitEntityAtPos ("BlackHole", new Vector2 (249, 101));
						InitEntityAtPos ("BlackHole", new Vector2 (398, 101));
						InitEntityAtPos ("Gravity", new Vector2 (50, 50));
						//InitEntityAtPos ("Gravity", new Vector2 (250, 50));
						//InitEntityAtPos ("Gravity", new Vector2 (398, 50));
						InitEntityAtPos ("PlayerWithGravity", new Vector2 (398, 50));
				}

				private void InitEncircle ()
				{
						InitEntityAtPos ("Player", new Vector2 (180, 100));

						for (int i = 0; i < NumOfEncircle; i++)
								InitEntityAtPos ("Encircle", new Vector2 (200, (i + 30) * 2));

				}

				private void InitMobDisperse ()
				{
						InitEntityAtPos ("Player", new Vector2 (180, 100));
		
						for (int i = 0; i < NumOfMob; i++)
								InitEntityAtPos ("MobDisperse", new Vector2 (200, (i + 30) * 2));
				}

				private void InitEntityAtPosWithScale (string entityName, Vector2 pos, Vector3 scale)
				{
						GameObject ob = InitEntityAtPos (entityName, pos);

						ob.transform.localScale = scale;
				}

				/// <summary>
				/// Inits the entity at position using the object pool.
				/// </summary>
				/// <returns>The entity at position.</returns>
				/// <param name="entityName">Entity name.</param>
				/// <param name="pos">Position.</param>
				private GameObject InitEntityAtPos (string entityName, Vector2 pos)
				{
						var obj = ObjectPool.instance.GetObjectForType (entityName, false);

						entities.Add (obj);

						obj.transform.position = pos;

						obj.SetActive (true);

						return obj;
				}

				private Vector2 GetRandomVectorInBounds ()
				{
						return new Vector2 (Random.Range (0, 510), Random.Range (-35, 235));
				}



		}
}
