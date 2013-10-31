using UnityEngine;
using System.Collections;

public class Planet : MonoBehaviour {
	
	bool started = false;
	// Use this for initialization
	void Start () {
		angle = Random.value * Mathf.PI * 2f;
		started = true;
	}
	
	 
	
	public Transform orbiting; 
	public float orbitSpeed = .2f;
	public float orbitRadius = 3f;
	
	public float planetRandomizer;
	public string planetType;
	public float foodGiven;
	
	float angle;
	
	// Update is called once per frame
	void Update () {
		if(orbiting != null)
		{
			angle += orbitSpeed * Time.deltaTime;
			transform.position = orbiting.position + orbitRadius * new Vector3(Mathf.Cos(angle), 0, Mathf.Sin(angle));
			if(angle > 2f * Mathf.PI)
			{
				angle -= 2f * Mathf.PI;
			}
			if(angle < 0)
			{
				angle += 2f * Mathf.PI;
			}
		}
	}
}
