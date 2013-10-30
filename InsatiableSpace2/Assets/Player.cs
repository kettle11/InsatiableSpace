using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {
	// Use this for initialization
	void Start () {
		
	}
	
	Vector3 velocity;
	Vector3 destination;
	
	public float speed = 5f;
	public float acceleration = .1f;
	
	public float foodAmount = 0;
	
	
	//It'd be nice if there was some tweening on movement, so the ship would slow down gradually as approaching its goal
	// Update is called once per frame
	void Update () {
		if(Input.GetMouseButtonDown(0))
		{
			 Ray ray = Camera.main.ScreenPointToRay(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0));
			 Plane p = new Plane(new Vector3(0,1,0), Vector3.zero);
			 float distance;
			 p.Raycast(ray, out distance);
		 	 
			 Vector3 pointHit = ray.GetPoint(distance);
			 destination = pointHit;
		}
		
		if((transform.position - destination).magnitude > .1f)
		{
			velocity = (destination - transform.position).normalized * speed;
		}
		else
		{
			velocity = Vector3.zero;
		}
		
		transform.position += velocity * Time.deltaTime;
	}
}
