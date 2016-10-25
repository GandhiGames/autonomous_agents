using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Automation
{
		/// <summary>
		/// Object pool used to enable and disable objects on request.
		/// Useful for improving performance and memory usage by reusing objects rather
		/// than instantiating and destroying.
		/// </summary>
		public class ObjectPool : MonoBehaviour
		{
				public GameObject[] objectPrefabs;

				public List<GameObject>[] pooledObjects;

				// The number of objects to pre-load.
				public int[] amountToBuffer;

				// Default number of objects to pre-load.
				public int defaultBufferAmount = 3;

				protected GameObject containerObject;

				private static ObjectPool _instance;
				public static ObjectPool instance { 
						get {
								if (!_instance) {
										_instance = FindObjectOfType<ObjectPool> ();
								}

								return _instance;
						}
				}

				/// <summary>
				/// Creates a new list for each object prefab and instantiates the buffer amount.
				/// </summary>
				void Start ()
				{
						containerObject = new GameObject ("ObjectPool");

						pooledObjects = new List<GameObject>[objectPrefabs.Length];

						int i = 0;
						foreach (GameObject objectPrefab in objectPrefabs) {
								pooledObjects [i] = new List<GameObject> (); 

								int bufferAmount;

								if (i < amountToBuffer.Length)
										bufferAmount = amountToBuffer [i];
								else
										bufferAmount = defaultBufferAmount;

								for (int n = 0; n < bufferAmount; n++) {
										var newObj = Instantiate (objectPrefab) as GameObject;
										newObj.name = objectPrefab.name;
										newObj.SetActive (false);
										PoolObject (newObj);
								}

								i++;
						}
								
				}
				/// <summary>
				/// Gets a new object for the name type provided.  If no object type exists or if onlypooled is true and there is no objects of that type in the pool
				/// then null will be returned.
				/// </summary>
				/// <returns>
				/// The object for type.
				/// </returns>
				/// <param name='objectType'>
				/// Object type.
				/// </param>
				/// <param name='onlyPooled'>
				/// If true, it will only return an object if there is one currently pooled.
				/// </param>

				
				/// <summary>
				/// Returns a pooled object of objectType if the objects type is in the pool. 
				/// </summary>
				/// <returns>The object for type.</returns>
				/// <param name="objectType">Name of prefab.</param>
				/// <param name="onlyPooled">If set to <c>true</c> an object will only be returned if there is an object of that type in pool.</param>
				public GameObject GetObjectForType (string objectType, bool onlyPooled)
				{
						for (int i = 0; i < objectPrefabs.Length; i++) {
								GameObject prefab = objectPrefabs [i];
								if (prefab.name == objectType) {

										if (pooledObjects [i].Count > 0) {
												GameObject pooledObject = pooledObjects [i] [0];
												
												if (pooledObject) {
														pooledObjects [i].RemoveAt (0);
														pooledObject.transform.parent = null;
												} else {
														Debug.LogError (objectType + ": should not destroy objects used by the object pool");
												}

												return pooledObject;

										} else if (!onlyPooled) {
												Debug.Log ("Created new: " + objectType);
												GameObject newObj = Instantiate (prefab) as GameObject;
												newObj.name = prefab.name;
												return newObj;
										}

										break;

								}
						}

						Debug.Log (objectType + " not found");

						// No object of specified type in pool or buffer amount reached and isPooled = false.
						return null;
				}

				/// <summary>
				/// Pools the object if object is in the prefabs list.
				/// Will not pool objects that are not in prefab list.
				/// Disables objects before adding to pool.
				/// </summary>
				/// <param name="obj">Object to pool.</param>
				public void PoolObject (GameObject obj)
				{
						for (int i = 0; i < objectPrefabs.Length; i++) {
								if (objectPrefabs [i].name == obj.name) {
										obj.SetActive (false);
										obj.transform.SetParent (containerObject.transform);
										pooledObjects [i].Add (obj);
										return;
								}
						}
				}



		}


}
