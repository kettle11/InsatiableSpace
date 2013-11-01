using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {
	// Use this for initialization
	void Start () {
		
	}
	public bool trigger = false;
	Vector3 velocity;
	Vector3 destination;
	public float speed = 5f;
	public float acceleration = .1f;
	public float foodAmount = 0;
	public float shipsAmount = 0;
	public Texture aTexture;
	void OnTriggerEnter(Collider other) {
		string sub = other.gameObject.name.Substring(0, 6);
		//Debug.Log(sub);
		if(sub.Equals("Planet")){
			Planet mosthelpfulthingintheworld = other.GetComponent <Planet>();
			trigger = true;
			string testme = mosthelpfulthingintheworld.planetType;
			float tequila = Random.value * 600f;
			if (testme.Equals("Terra")){
				Makeevent(1,tequila);
			}
			if (testme.Equals("Gas")){
				Makeevent(2,tequila);
			}
			if (testme.Equals("Rock")){
				Makeevent(3,tequila);
			}
			if (testme.Equals("Water")){
				Makeevent(4,tequila);
			}
		}		
	}
	public void Makeevent(int type, float rng){
		if(type == 1){
			if(rng<= 100 )
				aTexture = Resources.Load("TERRA/terra_bad") as Texture;
		    else if(rng<= 200 && rng > 100)
				aTexture = Resources.Load("TERRA/terra_neu") as Texture;
		    else if(rng<= 300 && rng > 200)
				aTexture = Resources.Load("TERRA/terra_good") as Texture;
			else if(rng<= 400 && rng > 300)
				aTexture = Resources.Load("TERRA/terra_trade_bad") as Texture;
			else if(rng<= 500 && rng > 400)
				aTexture = Resources.Load("TERRA/terra_trade_neu") as Texture;
			else if(rng > 500)
				aTexture = Resources.Load("TERRA/terra_trade_good") as Texture;
		}
		if(type == 2){
			if(rng<= 100 )
				aTexture = Resources.Load("GAS/gas_bad") as Texture;
		    else if(rng<= 200 && rng > 100)
				aTexture = Resources.Load("GAS/gas_neu") as Texture;
		    else if(rng<= 300 && rng > 200)
				aTexture = Resources.Load("GAS/gas_good") as Texture;
			else if(rng<= 400 && rng > 300)
				aTexture = Resources.Load("GAS/gas_ship_bad") as Texture;
			else if(rng<= 500 && rng > 400)
				aTexture = Resources.Load("GAS/gas_ship_neu") as Texture;
			else if(rng > 500)
				aTexture = Resources.Load("GAS/gas_ship_good") as Texture;
		}
		if(type == 3){
			if(rng<= 66 )
				aTexture = Resources.Load("ROCK/rock_asylum_bad") as Texture;
		    else if(rng<= 132 && rng > 66)
				aTexture = Resources.Load("ROCK/rock_asylum_good") as Texture;
		    else if(rng<= 198 && rng > 132)
				aTexture = Resources.Load("ROCK/rock_asylum_neu") as Texture;
			else if(rng<= 264 && rng > 198)
				aTexture = Resources.Load("ROCK/rock_good") as Texture;
			else if(rng<= 330 && rng > 264)
				aTexture = Resources.Load("ROCK/rock_bad") as Texture;
			else if(rng<= 396 && rng > 330)
				aTexture = Resources.Load("ROCK/rock_neu") as Texture;
			else if(rng<= 462 && rng > 396)
				aTexture = Resources.Load("ROCK/rock_starshipbase_good") as Texture;
			else if(rng<= 528 && rng > 462)
				aTexture = Resources.Load("ROCK/rock_starshipbase_bad") as Texture;
			else if(rng > 528)
				aTexture = Resources.Load("ROCK/rock_starshipbase_neu") as Texture;
		}
		if(type == 4){
			if(rng<= 100 )
				aTexture = Resources.Load("WATER/water_bad") as Texture;
		    else if(rng<= 200 && rng > 100)
				aTexture = Resources.Load("WATER/water_neu") as Texture;
		    else if(rng<= 300 && rng > 200)
				aTexture = Resources.Load("WATER/water_good") as Texture;
			else if(rng<= 400 && rng > 300)
				aTexture = Resources.Load("WATER/water_alien_neu") as Texture;
			else if(rng<= 500 && rng > 400)
				aTexture = Resources.Load("WATER/water_alien_bad") as Texture;
			else if(rng > 500)
				aTexture = Resources.Load("WATER/water_alien_good") as Texture;
		}
	}
	void OnGUI () {
		// Make a background box
		GUI.Label (new Rect (10, 10, 100, 20), "Food: "+  foodAmount);
		GUI.Label (new Rect (10, 25, 100, 20), "Ships: "+  shipsAmount);
		if (trigger)
			//6 hours to find the screen height and width, only to find out we need them in parentheses...
        	GUI.DrawTexture(new Rect(0, 0, (Screen.width), (Screen.height)), aTexture);
		
	}
		//if(started)
       		//Destroy(other.gameObject);
    
	 void OnTriggerExit( Collider other)
    {
		string sub = other.gameObject.name.Substring(0, 6);
		//Debug.Log(sub);
		if(sub.Equals("Planet")){
			trigger = false;
		}
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
