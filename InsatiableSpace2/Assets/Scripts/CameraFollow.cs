using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour {
	
	
	public Vector3 offset = new Vector3(0,20f,0);
	Vector3 display;
	
	public Transform following;
	
	public float scrollSpeed = 20f;
	
	// Use this for initialization
	void Start () {
		display = offset;
	}
	
	// Update is called once per frame
	void Update () {
		if(following != null)
		{
			transform.position = following.transform.position + display;
			transform.LookAt(following.transform.position);
		}
		if(Input.GetKeyDown(KeyCode.UpArrow)){
			camera.fieldOfView -= 5;
		}
		if(Input.GetKeyDown(KeyCode.DownArrow)){
			camera.fieldOfView += 5;
		}
		
		display += -Input.GetAxis("Mouse ScrollWheel") * offset.normalized * scrollSpeed;
		if(display.magnitude < offset.magnitude)
		{
			display = offset;
		}
		
	}
}
