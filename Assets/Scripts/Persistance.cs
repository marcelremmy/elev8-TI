using UnityEngine;
using System.Collections;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class Persistance : MonoBehaviour
{
		public static Persistance control;
		public int score;
		public int highscore;

		void Awake ()
		{
				if (control == null) {
						DontDestroyOnLoad (gameObject);
						control = this;
				} else if (control != this) {
						Destroy (gameObject);
				}
		}

	void OnGUI() {
		GUI.Label (new Rect (10, 10, 100, 30), "Score: " + score);
		GUI.Label (new Rect (10, 10, 140, 30), "Highscore: " + highscore);
		}

		public void Save ()
		{
				BinaryFormatter bf = new BinaryFormatter ();
				FileStream file = File.Create (Application.persistentDataPath + "/playerinfo.dat");

				PlayerData data = new PlayerData ();
				data.score = score;
				data.highscore = highscore;

				bf.Serialize (file, data);
				file.Close ();
		}

		public void Load ()
		{
				if (File.Exists (Application.persistentDataPath + "/playerinfo.dat")) {
						BinaryFormatter bf = new BinaryFormatter ();
						FileStream file = File.Open (Application.persistentDataPath + "/playerinfo.dat", FileMode.Open);
						PlayerData data = (PlayerData) bf.Deserialize (file);
						file.Close();
			score = data.score;
			highscore = data.highscore;
				}
		}
}

[Serializable]
class PlayerData
{
		public int score;
		public int highscore;
}