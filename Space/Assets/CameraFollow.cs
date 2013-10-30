using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour {
	
	
	public Vector3 offset = new Vector3(0,20f,0);
	
	public Transform following;
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if(following != null)
		{
			transform.position = following.transform.position + offset;
			transform.LookAt(following.transform.position);
		}
	}
}
