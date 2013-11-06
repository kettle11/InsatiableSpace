using UnityEngine;

using System.Collections;
using System.Collections.Generic;

public class Player : MonoBehaviour {
	// Use this for initialization
	public bool trigger = false;
	Vector3 velocity;
	Vector3 destination;
	public float speed = 5f;
	public float acceleration = .1f;
	public float foodAmount = 0;
	public static float shipsAmount = 0;
	public Texture aTexture;
	public float counter = 0;
	public Texture titleTexture;
	public bool titleBool = true;
	public Texture storyTexture;
	public bool storyBool = false;
	public Texture controlTexture;
	public bool controlBool = false;
	public float prevtime = 0f;
	public Texture victoryTexture;
	public bool victoryBool = false;
	public Texture defeatTexture;
	public bool defeatBool = false;
	public float prevship = 0f;
	
	List<AIShip> ships= new List<AIShip>();
	
	public static Player currentPlayer;
	
	public int startShips = 2;
	
	Texture infoTexture;
		
	void Start () {
		infoTexture = Resources.Load("Top Readout") as Texture;
		
		currentPlayer = this;
        for (int i = 0; i < startShips; i++)
        {
            addShip();
        }
		/*for(int i = 0; i < shipsAmount; i++)
		{
			addAiShip();
		}*/
		//foodAmount = 1000;
	}
	
	public int shipCount()
	{
		return ships.Count;
	}
	
	public void changeShips(int changing)
	{
		for(int i = 0; i < Mathf.Abs(changing); i++)
		{
			if(changing > 0)
			{
				addShip();
			}
			else
			{
				removeShip();
			}
		}
	}
	
	public void addShip()
	{
		AIShip newShip = Instantiate(aiShip, transform.position, Quaternion.identity) as AIShip;
		newShip.following = this.transform;
		newShip.radius = 10f + Random.value * 10f;
		newShip.calmRadius = newShip.calmRadius + Random.value * 10f;
		ships.Add(newShip);
	}
	
	public void removeShip()
	{
		if(ships.Count > 0)
		{
			Destroy(ships[ships.Count-1].gameObject);
			ships.RemoveAt(ships.Count-1);
		}
	}
	
	public AIShip aiShip;
	
	void OnTriggerEnter(Collider other) {
		
		print(other.GetType());
		other.GetComponent<Planet>().touch(true);
		// Freeze when touching any planet, we also need to set the destination to the current location when this happens
		Planet otherPlanet = other.GetComponent<Planet>();
		
		if(other && otherPlanet.cloned == false)
		{
			SolarSystem.timeRunning = false;
			SolarSystem.timeAhead = 0f;
			otherPlanet.touch(true);
		}
		
		if(other && otherPlanet.cloned == false && otherPlanet.visited == false){
			audio.Play();
			Planet mosthelpfulthingintheworld = other.GetComponent <Planet>();
			//trigger = true;
			//other.GetComponent<Planet>().setvisit();
			//string testme = mosthelpfulthingintheworld.planetType;
			//float tequila = Random.value * 600f;
			//triggerEvent(testme);
		}		
	}
	
	public void triggerEvent(string testme)
	{
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
	
	public float foodgain;
	public float shipgain;
	public bool gaingain = false;
	public void Makeevent(int type, float rng){
		// apparently not every planet is given a type....
		// state 0 - 8 nothing -food +food -ship +ship -both +both (-ships +food) (+ships -food)
		float state = 0;
		foodgain = 0f;
		shipgain = 0f;
		aTexture = Resources.Load("TERRA/terra_neu") as Texture;
		if(type == 1){
			if(rng<= 100 ){
				aTexture = Resources.Load("TERRA/terra_bad") as Texture;
				state = 7;
			}
		    else if(rng<= 200 && rng > 100){
				aTexture = Resources.Load("TERRA/terra_neu") as Texture;
				state = 0;
			}
		    else if(rng<= 300 && rng > 200){
				aTexture = Resources.Load("TERRA/terra_good") as Texture;
				state = 2;
			}
			else if(rng<= 400 && rng > 300){
				aTexture = Resources.Load("TERRA/terra_trade_bad") as Texture;
				state = 3;
			}
			else if(rng<= 500 && rng > 400){
				aTexture = Resources.Load("TERRA/terra_trade_neu") as Texture;
				state = 0;
			}
			else if(rng > 500){
				aTexture = Resources.Load("TERRA/terra_trade_good") as Texture;
				state = 7;
			}
		}
		if(type == 2){
			if(rng<= 100 ){
				aTexture = Resources.Load("GAS/gas_bad") as Texture;
				state = 5;
			}
		    else if(rng<= 200 && rng > 100){
				aTexture = Resources.Load("GAS/gas_neu") as Texture;
				state = 0;
			}
		    else if(rng<= 300 && rng > 200){
				aTexture = Resources.Load("GAS/gas_good") as Texture;
				state = 6;
			}
			else if(rng<= 400 && rng > 300){
				aTexture = Resources.Load("GAS/gas_ship_bad") as Texture;
				state = 1;
			}
			else if(rng<= 500 && rng > 400){
				aTexture = Resources.Load("GAS/gas_ship_neu") as Texture;
				state = 0;
			}
			else if(rng > 500){
				aTexture = Resources.Load("GAS/gas_ship_good") as Texture;
				state = 4;
			}
		}
		if(type == 3){
			if(rng<= 66 ){
				aTexture = Resources.Load("ROCK/rock_asylum_bad") as Texture;
				state = 5;
			}
		    else if(rng<= 132 && rng > 66){
				aTexture = Resources.Load("ROCK/rock_asylum_good") as Texture;
				state = 4;
			}
		    else if(rng<= 198 && rng > 132){
				aTexture = Resources.Load("ROCK/rock_asylum_neu") as Texture;
				state = 0;
			}
			else if(rng<= 264 && rng > 198){
				aTexture = Resources.Load("ROCK/rock_good") as Texture;
				state = 7;
			}
			else if(rng<= 330 && rng > 264){
				aTexture = Resources.Load("ROCK/rock_bad") as Texture;
				state = 8;
			}
			else if(rng<= 396 && rng > 330){
				aTexture = Resources.Load("ROCK/rock_neu") as Texture;
				state = 0;
			}
			else if(rng<= 462 && rng > 396){
				aTexture = Resources.Load("ROCK/rock_starshipbase_good") as Texture;
				state = 7;
			}
			else if(rng<= 528 && rng > 462){
				aTexture = Resources.Load("ROCK/rock_starshipbase_bad") as Texture;
				state = 8;
			}
			else if(rng > 528){
				aTexture = Resources.Load("ROCK/rock_starshipbase_neu") as Texture;
				state = 0;
			}
		}
		if(type == 4){
			if(rng<= 100 ){
				aTexture = Resources.Load("WATER/water_bad") as Texture;
				state = 5;
			}
		    else if(rng<= 200 && rng > 100){
				aTexture = Resources.Load("WATER/water_neu") as Texture;
				state = 0;
			}
		    else if(rng<= 300 && rng > 200){
				aTexture = Resources.Load("WATER/water_good") as Texture;
				state = 2;
			}
			else if(rng<= 400 && rng > 300){
				aTexture = Resources.Load("WATER/water_alien_neu") as Texture;
				state = 0;
			}
			else if(rng<= 500 && rng > 400){
				aTexture = Resources.Load("WATER/water_alien_bad") as Texture;
				state = 3;
			}
			else if(rng > 500){
				aTexture = Resources.Load("WATER/water_alien_good") as Texture;
				state = 2;
			}
		}
		// state 0 - 8 nothing -food +food -ship +ship -both +both (-ships +food) (+ships -food)
		if (state == 1){
			float randomNumber = (Random.value * 200)+70;
			foodgain= randomNumber;
			foodgain *= -1;
		}
		if (state == 2){
			float randomNumber = (Random.value * 200)+200;
			foodgain= randomNumber;
			if (type == 1 || type == 4){
				foodgain += 100;
				
			}else
				foodgain -= 20;
			
		}
		if (state == 3){
			float randomNumber = (Random.value * 100);
			if (randomNumber < 80)
				shipgain = -1f;
			else
				shipgain = -2f;
			
		}
		if (state == 4){
			float randomNumber = (Random.value * 100);
			if (randomNumber < 70)
				shipgain = 1f;
			else
				shipgain = 2f;
			
		}
		if (state == 5){
			float randomNumber = (Random.value * 200);
			foodgain= -1f * randomNumber;
			float randomNumber2 = (Random.value * 100);
			if (randomNumber2 < 70)
				shipgain = -1f;
			else
				shipgain = -2f;
			
		}
		if (state == 6){
			float randomNumber = (Random.value * 200);
			foodgain= 1f * randomNumber;
			float randomNumber2 = (Random.value * 100);
			if (randomNumber2 < 50)
				shipgain = 1f;
			else
				shipgain = 2f;
		}
		if (state == 7){
			float randomNumber = (Random.value * 200);
			foodgain= 1f * randomNumber;
			float randomNumber2 = (Random.value * 100);
			if (randomNumber2 < 70)
				shipgain = -1f;
			else
				shipgain = -2f;
		}
		if (state == 8){
			float randomNumber = (Random.value * 200);
			foodgain= -1f * randomNumber;
			float randomNumber2 = (Random.value * 100);
			if (randomNumber2 < 50)
				shipgain = 1f;
			else
				shipgain = 2f;
		}
		
		gaingain = true;
		//prevship = shipsAmount;
	//	shipsAmount += Mathf.Round(shipgain);
		
		if(shipgain != 0)
		{
			if(shipgain < 0)
			{
				if(shipgain == -1)
				{
					Terminal.addMessage("You lost a ship!", 1);
				}
				else
				{
					Terminal.addMessage("You lost "+Mathf.Abs(shipgain)+ " ships!", 1);
				}
			}
			if(shipgain > 0)
			{
				if(shipgain == 1)
				{
					Terminal.addMessage("You gained a ship!", 1);
				}
				else
				{
					Terminal.addMessage("You gained "+shipgain+ " ships!", 1);
				}
			}
		}
		
		if(foodgain != 0)
		{
			if(foodgain < 0)
			{
				Terminal.addMessage("You lost "+Mathf.Abs((int)foodgain)+ " food!", 1);
			}
			if(foodgain > 0)
			{
				
				Terminal.addMessage("You gained "+(int)foodgain+ " food!", 1);
			}
		}
		
		changeShips((int)shipgain);
		foodAmount += Mathf.Round(foodgain);
		
		prevtime = Time.time;
	}
	
	void OnGUI () {
		if((Time.time - prevtime) > 2f){
			gaingain = false;
			shipgain = 0;
			foodgain = 0;
		}
		GUI.DrawTexture(new Rect(10, 10, infoTexture.width / 5f, infoTexture.height / 5f), infoTexture);
		// Make a background box
		
		int x = 120;
		if(gaingain == false){
			GUI.Label (new Rect (x, 30, 100, 20), ""+  (int)foodAmount);
			GUI.Label (new Rect (x, 70, 100, 20), ""+  (int)shipCount());
		}
		if (gaingain){
			if(foodgain < 0){
				GUI.Label (new Rect (x, 30, 100, 20), ""+  (int)foodAmount + " " + Mathf.Round(foodgain));
			}
			if(foodgain >= 0){
				GUI.Label (new Rect (x, 30, 100, 20), ""+  (int)foodAmount + " +" + Mathf.Round(foodgain));
			}
			if(shipgain < 0){
				GUI.Label (new Rect (x, 70, 100, 20), ""+  (int)shipCount() + " " + shipgain);
			}
			if(shipgain >= 0){
				GUI.Label (new Rect (x, 70, 100, 20), ""+  (int)shipCount() + " +" + shipgain);
			}
		}
		if (trigger)
			//6 hours to find the screen height and width, only to find out we need them in parentheses...
        	GUI.DrawTexture(new Rect(0, 0, (Screen.width), (Screen.height)), aTexture);
		if (titleBool) {
			titleTexture = Resources.Load("title") as Texture;
			GUI.DrawTexture(new Rect(0, 0, (Screen.width), (Screen.height)), titleTexture);
		}
		if (storyBool) {
			storyTexture = Resources.Load("story") as Texture;
			GUI.DrawTexture(new Rect(0, 0, (Screen.width), (Screen.height)), storyTexture);
		}
		if (controlBool) {
			controlTexture = Resources.Load("controls") as Texture;
			GUI.DrawTexture(new Rect(0, 0, (Screen.width), (Screen.height)), controlTexture);
		}
		if (victoryBool) {
			victoryTexture = Resources.Load("victory") as Texture;
			GUI.DrawTexture(new Rect(0, 0, (Screen.width), (Screen.height)), victoryTexture);
		}
		if (defeatBool) {
			defeatTexture = Resources.Load("defeat") as Texture;
			GUI.DrawTexture(new Rect(0, 0, (Screen.width), (Screen.height)), defeatTexture);
		}
		if (trigger)
			if(GUI.Button(new Rect((Screen.width-200), (Screen.height-100), 100, 60), "OK"))
            	trigger = false;
		
		if(!SolarSystem.timeRunning )
		{
			GUI.Label ( new Rect (x + 50, 30, 100, 20), " - "+ (int)calculateFoodCost(SolarSystem.timeAhead));
		}
		
	}
	
	
	float calculateFoodCost(float time)
	{
		return (shipsAmount*3 + 5) * time;
	}
		//if(started)
       		//Destroy(other.gameObject);
    
	 void OnTriggerExit( Collider other)
    {
		other.GetComponent<Planet>().touch(false);
		if(other){
			trigger = false;
			
		}
    }
	
	
	
	 
	public float calctime;
	float calculateTime()
	{
		return(transform.position - destination).magnitude / realSpeed; //Should be seconds until destination.
	}
	
	public void setDestination(Vector3 setting)
	{
		calctime = Time.time;
		destination = setting;
		SolarSystem.timeAhead = calculateTime();
	}
	
	public void reachDestination()
	{
		SolarSystem.timeRunning = false;
		SolarSystem.timeAhead = 0f;
		destinationSet = false;
	}
	
	//It'd be nice if there was some tweening on movement, so the ship would slow down gradually as approaching its goal
	// Update is called once per frame
	
	Quaternion rotation;
	
	float realSpeed;
	
	bool destinationSet = false;
	
	void Update () {
		LineRenderer lineRenderer = GetComponent<LineRenderer>();
		lineRenderer.enabled = !SolarSystem.timeRunning;
		
		if(ships.Count > 4)
		{
			realSpeed = speed * (1f + ships.Count / 10f) ; //Gives a speedboost with more ships.
		}
		else
		{
			realSpeed = speed;
		}
		if(lineRenderer.enabled)
		{
			lineRenderer.SetVertexCount(2);
			lineRenderer.SetPosition(0, transform.position);
			lineRenderer.SetPosition(1, destination);
			
			SolarSystem.timeAhead = calculateTime();
		}
		
		if(Input.GetMouseButton(0) && GUIUtility.hotControl == 0)
		{
			if(SolarSystem.timeRunning)
			{
				SolarSystem.timeRunning = false;
			}
			 Ray ray = Camera.main.ScreenPointToRay(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0));
			 Plane p = new Plane(new Vector3(0,1,0), Vector3.zero);
			 float distance;
			 p.Raycast(ray, out distance);
		 	 
			 Vector3 pointHit = ray.GetPoint(distance);
			 destinationSet = true;
			 setDestination(pointHit);
		}
		//Victory and loss conditions
		if (foodAmount < 0 || ships.Count == 0) {
			defeatBool = true;	
		}
		else if(foodAmount > 5000 || ships.Count > 30) {
			victoryBool = true;
		}
		
		if(Input.GetKeyDown(KeyCode.Space))
		{
			SolarSystem.timeRunning = !SolarSystem.timeRunning;
			if (titleBool) {
				titleBool = false;
				storyBool = true;
			}
			else if (storyBool) {
				storyBool = false;
				controlBool = true;
			}
			else if (controlBool) {
				controlBool = false;
			}
			if (victoryBool || defeatBool && SolarSystem.timeRunning) {
				Application.Quit();
			}			
		}
		
		if(SolarSystem.timeRunning)
		{
			if(destinationSet)
			{
				if((transform.position - destination).magnitude > .2f)
				{
					velocity = (destination - transform.position).normalized * realSpeed;
					rotation = Quaternion.AngleAxis(Mathf.Rad2Deg * Mathf.Atan2(-velocity.z, velocity.x) - 90, Vector3.up) ;
				}
				else
				{
					velocity = Vector3.zero;
					transform.position = destination;
					reachDestination();
				}
			}
			
			if(velocity.magnitude > realSpeed)
			{
				velocity = velocity.normalized * realSpeed;
			}
		
			
			transform.rotation = rotation;
			transform.position += velocity * Time.deltaTime;
			counter += Time.deltaTime;
			
			
			foodAmount -= (shipsAmount*3 + 5) * Time.deltaTime; //Subtracts food
			
			if(counter >  2f){
				counter = 0;
				foodgain -= shipsAmount*5 + 5;
				gaingain = true;
				prevtime = Time.time;
			}
		}
	}
}
