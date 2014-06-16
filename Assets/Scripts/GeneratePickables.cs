using UnityEngine;

//using System;
//using System.Text;
//using System.Linq;
using System.Collections;
using System.Collections.Generic;

public class GeneratePickables : MonoBehaviour
{

		public static int fieldSize = 16;
		public static GeneratePickables instance;
		public GameObject pickable;
		public GameObject blockable;
		public int pickCount = 1;
		public int blockCount = 4;
		public static int factor = 2;
		public static int offset = -(fieldSize - 1) / 2;
		public float speed;
		public int minPickups = 8;
		public GUIText countText;
		public GUIText winText;
		public GUIText timeText;
		//private int pickCount;
		private float startTime;
		private int xMax = 0;
		private int yMax = 0;
		private int xMin = 0;
		private int yMin = 0;
		private int[][] arr = new int[fieldSize][];
		private List <GameObject> pickableItems = new List<GameObject> ();

		void Awake ()
		{
				if (!instance) {
						instance = this;
						for (int i = 0; i<fieldSize; i++) {
								arr [i] = new int[fieldSize];
						}

						//TODO test for pickCount+blockCount < fieldsize*fieldsize
						generateItems (1, pickCount); //pickable
						generateItems (2, blockCount); //blockable
						
				}
		}

		void Start ()
		{
				/** init the game **/
				//pickCount = 0;
				countText.text = "";
				//showCount ();
				winText.text = "";
				startTime = Time.time;
		}
	
		// Update is called once per frame
		void Update ()
		{
	
		}
	
		public static float range = 1000;
		
		void FixedUpdate () 
		{
			Collider[] cols  = Physics.OverlapSphere(transform.position, range); 
			List<Rigidbody> rbs = new List<Rigidbody>();
			
			foreach(Collider c in cols)
			{
				Rigidbody rb = c.attachedRigidbody;
				if(rb != null && rb != rigidbody && !rbs.Contains(rb))
				{
					rbs.Add(rb);
					Vector3 offset = transform.position - c.transform.position;
					rb.AddForce( offset / offset.sqrMagnitude * rigidbody.mass);
				}
			}
		}

		void generateItems (int type, int maxCount)
		{
				var picks = 0;
				do {
			
						var x = (int)Random.Range (0, fieldSize - 1);
						var y = (int)Random.Range (0, fieldSize - 1);
			
						if (x < xMin)
								xMin = x;
						if (x > xMax)
								xMax = x;
			
						if (y < yMin)
								yMin = y;
						if (y > yMax)
								yMax = y;

						// only place an object if the chosen position is not already filled
						if (arr [x] [y] != null && !(arr [x] [y] > 0)) {
								// initialiaze pickable (1)
								arr [x] [y] = type; // pick items = 1, block items  = 2
								Vector3 position = new Vector3 ((x + offset) * factor, 0.5f, (y + offset) * factor);

								switch (type) {
								case 2:
										GameObject bl = Instantiate (blockable) as GameObject;  /** BLOCK **/
						
										var blSize = 0.0f;
										var blTexture = "clay";
										switch ((int)Random.Range (0, 5)) {
										case 1:
					case 4:
												blSize = 1.25f;
												position = new Vector3 ((x + offset) * factor, 3.5f, (y + offset) * factor);
												break;
										case 2:
					case 5:
												blSize = 1.95f;
						position = new Vector3 ((x + offset) * factor, 5.5f, (y + offset) * factor);
												blTexture = "coal_block";
												break;
										case 3:
												blSize = 2.35f;
						position = new Vector3 ((x + offset) * factor, 10.5f, (y + offset) * factor);
												blTexture = "hardened_clay";
												break;
										case 0:
										default:
												blSize = 0.75f;
												position = new Vector3 ((x + offset) * factor, 2.5f, (y + offset) * factor);
												blTexture = "dirt";
												break;
										}

										bl.transform.position = position;
					bl.transform.Rotate (new Vector3 ((int)Random.Range (0, 359), (int)Random.Range (0, 359), (int)Random.Range (0, 359)));
						// TODO make different sizes and weights for blockables
						

										bl.transform.localScale = (new Vector3 (blSize, blSize, blSize));
										bl.renderer.material.mainTexture = (Texture) Resources.Load (blTexture);
										bl.rigidbody.mass = blSize; 
						//Debug.Log ("new item " + picks, bl);
										break;
								case 1:
										GameObject cube = Instantiate (pickable) as GameObject; /** Pick me up **/
										cube.renderer.material.mainTexture = (Texture) Resources.Load ("emerald_block");
										cube.transform.position = position;
					// TODO put one or two "multipoint" items among the simple pickups
					//Debug.Log ("new block " + picks, cube);
										break;
								default:
										break;
								}

								picks++;
						}
				} while (picks != maxCount);
				Debug.Log ("x: " + xMin + " " + xMax + " : y: " + yMin + " " + yMax);
		}
}
