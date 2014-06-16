using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{
		public float speed;
		public int minPickups = 8;
		public GUIText countText;
		public GUIText winText;
		public GUIText timeText;
		private int pickCount;
		private float startTime;

		// sound files
		public AudioSource au_pickup;
		public AudioSource au_win;

		void Start ()
		{
				/** init sounds **/
				au_pickup = (AudioSource)gameObject.AddComponent ("AudioSource");
				AudioClip au_pickup_clip;
				au_pickup_clip = (AudioClip)Resources.Load ("SFX/countdown");
				au_pickup.clip = au_pickup_clip;
				au_win = (AudioSource)gameObject.AddComponent ("AudioSource");
				AudioClip au_win_clip;
				au_win_clip = (AudioClip)Resources.Load ("SFX/applause-4");
				au_win.clip = au_win_clip;

				/** init the game **/
				pickCount = 0;
				countText.text = "";
				showCount ();
				winText.text = "";
				startTime = Time.time;

		}

		void FixedUpdate ()
		{
				float moveHorizontal = Input.GetAxis ("Horizontal");
				float moveVertical = Input.GetAxis ("Vertical");
				Vector3 movement = new Vector3 (moveHorizontal, 0.0f, moveVertical);	
				rigidbody.AddForce (movement * speed * Time.deltaTime);
				
				if (pickCount < minPickups) {
						timeText.text = (Time.time - startTime).ToString ("00.00");
				}
				
		}

		void OnTriggerEnter (Collider other)
		{
				//Destroy(other.gameObject);
				if (other.gameObject.tag == "pickable") {
						other.gameObject.SetActive (false);
						pickCount++;
						showCount ();
						au_pickup.Play ();
				}
		}

		void showCount ()
		{
				countText.text = "Score: " + pickCount.ToString ();
				if (pickCount >= minPickups) {
						winText.text = "You have made it!";
						au_win.Play ();
				}
		}
}