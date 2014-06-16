using UnityEngine;
using System.Collections;

public class RandomImage : MonoBehaviour {

	public string url = "http://my-textures.com/view/files/view_file.php?id=2458&download=true";
	// Use this for initialization
	void Start () {
		var webImage = new Texture2D(4, 4, TextureFormat.DXT1, false);
		var www = new WWW(url);
		www.LoadImageIntoTexture(webImage);
		//Texture2D tex = (Texture2D) Resources.Load(name, typeof(Texture2D));
		this.renderer.material.mainTexture = webImage;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
