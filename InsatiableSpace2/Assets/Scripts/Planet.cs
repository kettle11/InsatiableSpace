using UnityEngine;
using System.Collections;

[RequireComponent (typeof (LineRenderer))]

public class Planet : MonoBehaviour {
	
	bool started = false;

	GameObject clone; //A clone planet used for rendering a preview of the future.
	
	[HideInInspector]
	public bool cloned = false;
	
	public GameObject planetPreview;
	
	// Use this for initialization
	void Start () {
		angle = Random.value * Mathf.PI * 2f;
		started = true;

		//Sets up clone 
		//To-fix: Frequently (always?) clones a clone of each clone.
		if(!cloned)
		{
			clone = Instantiate(planetPreview, new Vector3(0, 0, 0), Quaternion.identity) as GameObject;
			//clone.cloned = true;
			clone.transform.localScale = transform.localScale;
			clone.renderer.enabled = false;
		}
		
		this.enabled = true;
	}
	public void setvisit() {
			visited = true;
		
	}
	 
	
	public Planet orbiting; 
	public float orbitSpeed = .2f;
	public float orbitRadius = 3f;
	public bool visited = false;
	public float planetRandomizer;
	public string planetType;
	public float foodGiven;
	
	float angle;
	
	float foodAvailable = 15f;
	float peopleHere = 0f;
	
	public Vector3 getPositionAtTimeAhead(float timeAhead)
	{
		if(orbiting == null)
		{
			return transform.position;
		}
		
		return orbiting.getPositionAtTimeAhead(timeAhead) + orbitRadius * new Vector3(Mathf.Cos(angle + timeAhead * orbitSpeed), 0, Mathf.Sin(angle + timeAhead * orbitSpeed));
	}
	// Update is called once per frame
	void Update () {
		if(SolarSystem.timeRunning)
		{
			if(orbiting != null)
			{
				angle += orbitSpeed * Time.deltaTime;
				transform.position = getPositionAtTimeAhead(0f);
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
		else
		{
			RenderAhead();
			//Vector2 point = Camera.main.WorldToScreenPoint(worldPosition);
		}
		
	}
	
	public static int lineResolution = 30;
	
	//Render a preview of what's to come.
	void RenderAhead () {
		
		LineRenderer lineRenderer = GetComponent<LineRenderer>();
		lineRenderer.SetVertexCount(lineResolution+1);

		if(!SolarSystem.timeRunning)
		{
			if(clone == null)
			{
 				print("noo");
			}
			
			clone.renderer.enabled = true;
			clone.renderer.transform.position = getPositionAtTimeAhead(SolarSystem.timeAhead);
			for(int i = 0; i <= lineResolution; i++)
			{
				lineRenderer.SetPosition(i, getPositionAtTimeAhead(((float)i / (float)lineResolution) * SolarSystem.timeAhead));
			}
			lineRenderer.enabled = true;
		}
		else
		{
			lineRenderer.enabled = false;
			clone.renderer.enabled = false;
		}
	}
}
