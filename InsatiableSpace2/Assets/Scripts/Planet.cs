using UnityEngine;
using System.Collections;

[RequireComponent (typeof (LineRenderer))]

public class Planet : MonoBehaviour {

	GameObject clone; //A clone planet used for rendering a preview of the future.
	
	[HideInInspector]
	public bool cloned = false;
	
	public GameObject planetPreview;
	
	
	public float currentShips = 2;
	public float totalShips = 12;
	[HideInInspector]
	public float timeRunning;
	[HideInInspector]
	public bool isDecaying = false;	
	[HideInInspector]
	public float randomShipTime;
	
	// Use this for initialization
	void Start () {
		angle = Random.value * Mathf.PI * 2f;

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
	
	
	bool isMoon = false;
	
	public void setMoon() {
		rigidbody.detectCollisions = false;
		isMoon = true;
		setvisit();
	}
	
	public void setvisit() {
			visited = true;
		
	}
	 
	
	public Planet orbiting; 
	public float orbitSpeed = .2f;
	public float orbitRadius = 3f;
	public bool visited = false;
	[HideInInspector]
	public float planetRandomizer;
	public string planetType;
	public float foodGiven;
	
	float angle;
	
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
				timeRunning += Time.deltaTime;
				if (timeRunning > randomShipTime) {
					if (currentShips > 0) {
						if (isDecaying == false) {
							currentShips += 1;
							if (currentShips >= totalShips) {
								isDecaying = true;
							}
						}
						else {
							totalShips -= 1;
							if (totalShips == 2) {
								print("A planet is close to destruction");
							}
							if (totalShips == 0) {
								print ("A planet has been destroyed");
								Destroy(this.gameObject);
								Destroy(clone.gameObject);
							}
						}
					}
					timeRunning = 0;
				}
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
		
		RenderAhead();
		
		
		
		
	}

	public GUISkin skin;
	void OnGUI () {
		
		if(isMoon)
		{
			return;
		}
		
		GUI.skin = skin;
		//Vector2 size = GUI.skin.GetStyle("ProgressBarText").CalcSize(GUIContent(label));
		Vector3 point = Camera.main.WorldToScreenPoint(transform.position);

		
		
		if (GUI.Button(new Rect(point.x, -point.y + Screen.height - 40, 100, 20), "Take Ship"))
		{
			currentShips -= 1;
		}
		if (GUI.Button(new Rect(point.x, -point.y + Screen.height - 20, 100, 20), "Add Ship"))
		{
			currentShips += 1;
		}
		
		GUI.Label (new Rect (point.x, -point.y + Screen.height, 100, 20), " " + currentShips + "/" + totalShips + " ");
		
	}
	
	public static int lineResolution = 30;
	
	//Render a preview of what's to come.
	void RenderAhead () {
		
		LineRenderer lineRenderer = GetComponent<LineRenderer>();
		lineRenderer.SetVertexCount(lineResolution+1);
		
		if(!SolarSystem.timeRunning && !isMoon)
		{
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
