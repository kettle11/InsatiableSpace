using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AIShip : MonoBehaviour {
	
	static List<AIShip> ships = new List<AIShip>();
	// Use this for initialization
	void Start () {
		ships.Add(this);
		angle = Random.value * Mathf.PI * 2f;
		dir = Mathf.Sign(.5f - Random.value);
		radius = radius + Random.value * 6f;
	}
	
	
	public Transform following;
	
	public float speed = 50f;
	public float acceleration = 30f;
	Vector3 velocity;
	
	float angle;
	public float radius = 10f;
	float dir = 1f;
	Vector3 destination;
	
    public float calmRadius = 10f; //If I'm this close to the ship, it's probably ok and I can calm down.
	
	// Update is called once per frame
	void Update () {
	 	
		destination = following.position + new Vector3(Mathf.Cos(angle), 0f, Mathf.Sin(angle)) * radius;
		angle += dir * 1f * Time.deltaTime;
		Vector3 dif = destination - transform.position;
		Vector3 distanceToShip = following.position - transform.position;
		
		if(following != null && distanceToShip.magnitude > calmRadius)
		{
			
			velocity += dif.normalized * acceleration * Time.deltaTime;
		}
		else
		{
			velocity -= velocity.normalized * acceleration * .08f * Time.deltaTime;
		}
		
		if(velocity.magnitude > speed)
		{
			velocity = velocity.normalized * speed;
		}
		
		if(velocity.magnitude > .2f)
		{
			transform.rotation = Quaternion.AngleAxis(Mathf.Rad2Deg * Mathf.Atan2(-velocity.z, velocity.x) - 90, Vector3.up) *  Quaternion.AngleAxis(270, Vector3.right);
		}
		transform.position += velocity * Time.deltaTime;
	}
}
