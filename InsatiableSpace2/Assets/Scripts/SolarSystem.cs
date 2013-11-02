using UnityEngine;
using System.Collections;

public class SolarSystem : MonoBehaviour {
	
	public int numberOfPlanets = 10;
	public float radius = 10;
	
	public float moonChance = .3f;
	public float planetSpeed = .1f;
	
	public float planetSpacing = 20f;
	public Planet terraPlanet;
	public Planet waterPlanet;
	public Planet gasPlanet; 
	public Planet rockPlanet;
	
	public Planet sunPlanet;
	
	public static bool timeRunning = true; //Controls the flow of time for all time and space... 
	public static float timeAhead = 0f; //Time to render ahead
	
	//Would be useful the night before a gamedev project is due..
	
	// Way more public variables could be exposed.
	void Start () {
	
		
		Planet sun = Instantiate(sunPlanet, new Vector3(0, 0, 0), Quaternion.identity) as Planet;
		sun.transform.localScale = new Vector3(15f,15f,15f);
		float currentRadius = sun.transform.localScale.x;
		for(int i = 0 ; i < numberOfPlanets; i++)
		{
			
			float randomVal = Random.value * 1000;
			
			Planet newPlanet;
				
			if (randomVal  < 250) {      // Terra
				newPlanet = Instantiate(terraPlanet, new Vector3(0, 0, 0), Quaternion.identity) as Planet;
				newPlanet.planetType = "Terra";
				newPlanet.foodGiven = 10f;
			}
			else if (randomVal  < 500) { // Gas
				newPlanet = Instantiate(gasPlanet, new Vector3(0, 0, 0), Quaternion.identity) as Planet;
				newPlanet.planetType = "Gas";
				newPlanet.foodGiven = 5f;
			}
			
			else if (randomVal  < 750) { // Rock
				newPlanet = Instantiate(rockPlanet, new Vector3(0, 0, 0), Quaternion.identity) as Planet;
				newPlanet.planetType = "Rock";
				newPlanet.foodGiven = 2f;
			}
			
			else {                               // Water
				newPlanet = Instantiate(waterPlanet, new Vector3(0, 0, 0), Quaternion.identity) as Planet;
				newPlanet.planetType = "Water";
				newPlanet.foodGiven = 5f;
			}
			
			newPlanet.orbiting = sun.transform;
			newPlanet.orbitSpeed = Random.value * planetSpeed;
			newPlanet.orbitRadius = planetSpacing;
			planetSpacing = newPlanet.orbitRadius + (15f * Random.value + 10);
			newPlanet.transform.localScale = new Vector3(10f,10f,10f) * Random.value;
			currentRadius += newPlanet.transform.localScale.x;
			
			newPlanet.planetRandomizer = Random.value * 1000f;
			
			
			if(Random.value < moonChance)//It'd be cool if the moons could rotate on any axis, but that requires more code.
			{
				int numMoons = (int)(Random.value * 3); 
				for(int j = 0; j < numMoons; j++)
				{
					Planet newMoon = Instantiate(rockPlanet, new Vector3(0, 0, 0), Quaternion.identity) as Planet;
					newMoon.orbiting = newPlanet.transform;
					newMoon.transform.localScale = newPlanet.transform.localScale * .1f; //Make the new moon smaller than its planet
					newMoon.orbitRadius = newPlanet.transform.localScale.x * (Random.value + 1f); //Make sure it doesn't intersect the planet and then add a random distance to it.
					newMoon.orbitSpeed *= 10f;
				}
			}
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}