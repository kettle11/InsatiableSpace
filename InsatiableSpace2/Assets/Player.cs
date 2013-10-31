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
	public bool trigger = false;
	public float foodAmount = 0;
	public Texture aTexture;
	void OnTriggerEnter(Collider other) {
		string sub = other.gameObject.name.Substring(0, 6);
		//Debug.Log(sub);
		if(sub.Equals("Planet")){
			trigger = true;
			Planet mosthelpfulthingintheworld = other.GetComponent <Planet>();
			string testme = mosthelpfulthingintheworld.planetType;
			
			float tequila = Random.value * 1000f;
			Debug.Log(tequila);
			if(tequila<= 333 )
				aTexture = Resources.Load("GAS/gas_bad") as Texture;
			
			if(tequila<= 666 && tequila > 333 )
				aTexture = Resources.Load("GAS/gas_generic")as Texture;
			
			if(tequila > 666)
				aTexture = Resources.Load("GAS/gas_good")as Texture;
			
		}
			
	}
		//if(started)
       		//Destroy(other.gameObject);
    
	 void OnTriggerExit( Collider other)
    {
		string sub = other.gameObject.name.Substring(0, 6);
		//Debug.Log(sub);
		if(sub.Equals("Planet")){
			Debug.Log(other.gameObject.name);
			trigger = false;
			
		}
    }
	
	
	void OnGUI () {
		// Make a background box
		
		if (trigger)
			//6 hours to find the screen height and width, only to find out we need them in parentheses...
        	GUI.DrawTexture(new Rect(0, 0, (Screen.width), (Screen.height)), aTexture);
		
		
	
		
		
	}
	 
	
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
