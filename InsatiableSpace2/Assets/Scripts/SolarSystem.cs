﻿using UnityEngine;
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
	
		float radiusSet = 0;
		
		Planet sun = Instantiate(sunPlanet, new Vector3(0, 0, 0), Quaternion.identity) as Planet;
		sun.transform.localScale = new Vector3(15f,15f,15f);
		float currentRadius = sun.transform.localScale.x;
		sun.setMoon();
		
		radiusSet += 30f;
		for(int i = 0 ; i < numberOfPlanets; i++)
		{
			
			float randomVal = Random.value * 1000;
			
			Planet newPlanet;
			
			float planetScale = (((Random.value * Random.value)  * (.3f + ((float)i / (float)numberOfPlanets) * ((float)i / (float)numberOfPlanets) * 3f)) + .25f);
				
			float totalShipSet = 4f + (int)(planetScale * 12f);
			
			if (randomVal  < 250) {      // Terra
				newPlanet = Instantiate(terraPlanet, new Vector3(0, 0, 0), Quaternion.identity) as Planet;
				newPlanet.planetType = "Terra";
				newPlanet.foodGiven = 7f;
				newPlanet.totalShips = (int)(totalShipSet * 1.4f);
			}
			else if (randomVal  < 500) { // Gas
				newPlanet = Instantiate(gasPlanet, new Vector3(0, 0, 0), Quaternion.identity) as Planet;
				newPlanet.planetType = "Gas";
				newPlanet.foodGiven = 3f;
				newPlanet.totalShips = (int)(totalShipSet / 1.4f);
				newPlanet.growthRate = 1.6f;
			}
			
			else if (randomVal  < 750) { // Rock
				newPlanet = Instantiate(rockPlanet, new Vector3(0, 0, 0), Quaternion.identity) as Planet;
				newPlanet.planetType = "Rock";
				newPlanet.foodGiven = 1f;
				newPlanet.totalShips = (int)(totalShipSet / 2f);
				newPlanet.growthRate  = 1.3f;
			}
			
			else {                               // Water
				newPlanet = Instantiate(waterPlanet, new Vector3(0, 0, 0), Quaternion.identity) as Planet;
				newPlanet.planetType = "Water";
				newPlanet.foodGiven = 5f;
				newPlanet.totalShips = (int)(totalShipSet);
			}
			
			newPlanet.orbiting = sun;
			newPlanet.orbitSpeed = Random.value * planetSpeed;
			newPlanet.orbitRadius = radiusSet;
			radiusSet = newPlanet.orbitRadius + (planetSpacing * Random.value + 10);
			
			newPlanet.transform.localScale = new Vector3(10f,10f,10f) * (planetScale + .25f);
			
			currentRadius += newPlanet.transform.localScale.x;
			
			newPlanet.planetRandomizer = Random.value * 1000f;
			newPlanet.randomShipTime = .4f;
			
			if(Random.value < moonChance)//It'd be cool if the moons could rotate on any axis, but that requires more code.
			{
				int numMoons = (int)(Random.value * 3); 
				for(int j = 0; j < numMoons; j++)
				{
					Planet newMoon = Instantiate(rockPlanet, new Vector3(0, 0, 0), Quaternion.identity) as Planet;
					newMoon.orbiting = newPlanet;
					newMoon.transform.localScale = newPlanet.transform.localScale * (Random.value * .1f + .1f); //Make the new moon smaller than its planet
					newMoon.orbitRadius = newPlanet.transform.localScale.x * (Random.value + 1f); //Make sure it doesn't intersect the planet and then add a random distance to it.
					newMoon.orbitSpeed *= 10f;
					newMoon.randomShipTime = newPlanet.randomShipTime;
					newMoon.setMoon();
				}
			}
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}