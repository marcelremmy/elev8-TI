using UnityEngine;
using System.Collections;

public class CamController : MonoBehaviour {
	public GameObject player;
	public float maxDistance = 20f;
	public float turnSpeed = 4.0f;
	private Vector3 offset;
	// Use this for initialization
	void Start () {
		//offset = transform.position;
		offset = new Vector3(player.transform.position.x, player.transform.position.y + 6.0f, player.transform.position.z + 4.0f);
	}
	
	// Update is called once per frame
	void LateUpdate () {
		transform.LookAt(player.transform.position);
		offset = Quaternion.AngleAxis (Input.GetAxis("Mouse X") * turnSpeed, Vector3.up) * offset;
		transform.position = player.transform.position + offset;
		Vector3 camPos = transform.position;
		if (transform.position.x > maxDistance)
			transform.position = (new Vector3(maxDistance,camPos.y,camPos.z));
		if (transform.position.x < -maxDistance)
			transform.position = (new Vector3(-maxDistance,camPos.y,camPos.z));
		if (transform.position.z > maxDistance)
			transform.position = (new Vector3(camPos.x,camPos.y,maxDistance));
		if (transform.position.z < -maxDistance)
			transform.position = (new Vector3(camPos.x,camPos.y,-maxDistance));

	}
}
