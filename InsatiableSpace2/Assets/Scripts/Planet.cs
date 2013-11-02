using UnityEngine;
using System.Collections;

[RequireComponent (typeof (LineRenderer))]

public class Planet : MonoBehaviour {
	
	public Material previewMaterial;
	
	bool started = false;

	Planet clone; //A clone planet used for rendering a preview of the future.
	
	[HideInInspector]
	public bool cloned = false;
	
	// Use this for initialization
	void Start () {
		angle = Random.value * Mathf.PI * 2f;
		started = true;
		
		
		//Sets up clone
		if(!cloned)
		{
			this.enabled = false;
			clone = Instantiate(this, new Vector3(0, 0, 0), Quaternion.identity) as Planet;
			clone.cloned = true;
			clone.renderer.material = previewMaterial;
			clone.renderer.enabled = false;
			clone.enabled = false;
		}
		this.enabled = true;
	}
	
	 
	
	public Transform orbiting; 
	public float orbitSpeed = .2f;
	public float orbitRadius = 3f;
	
	public float planetRandomizer;
	public string planetType;
	public float foodGiven;
	
	float angle;
	
	Vector3 getPositionAtAngle(float angle)
	{
		return orbiting.position + orbitRadius * new Vector3(Mathf.Cos(angle), 0, Mathf.Sin(angle));
	}
	// Update is called once per frame
	void Update () {
		if(SolarSystem.timeRunning)
		{
			if(orbiting != null)
			{
				angle += orbitSpeed * Time.deltaTime;
				transform.position = getPositionAtAngle(angle);
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
		RenderAhead();
		
	}
	
	public static int lineResolution = 30;
	
	//Render a preview of what's to come.
	void RenderAhead () {
		LineRenderer lineRenderer = GetComponent<LineRenderer>();
		lineRenderer.SetVertexCount(lineResolution+1);
		
		if(!SolarSystem.timeRunning)
		{
			clone.renderer.enabled = true;
			clone.renderer.transform.position = getPositionAtAngle(angle + SolarSystem.timeAhead * orbitSpeed);
			for(int i = 0; i <= lineResolution; i++)
			{
				lineRenderer.SetPosition(i, getPositionAtAngle(angle + SolarSystem.timeAhead * ((float)i / (float)lineResolution) * orbitSpeed));
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
