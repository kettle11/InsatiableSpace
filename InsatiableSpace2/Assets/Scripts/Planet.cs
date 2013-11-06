using UnityEngine;
using System.Collections;

[RequireComponent (typeof (LineRenderer))]

public class Planet : MonoBehaviour {

	GameObject clone; //A clone planet used for rendering a preview of the future.
	
	[HideInInspector]
	public bool cloned = false;
	
	public GameObject planetPreview;
	
	public bool test1 = false;
	public bool test2 = false;
	public float currentShips = 2;
	public float totalShips = 12;
	[HideInInspector]
	public float timeRunning;
	[HideInInspector]
	public bool isDecaying = false;	
	[HideInInspector]
	public float randomShipTime;
	public bool istouching = false;
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
		
		foodAmount = foodGiven * Random.value * 10f;
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
	float foodAmount;
	
	float angle;
	
	public Vector3 getPositionAtTimeAhead(float timeAhead)
	{
		if(orbiting == null)
		{
			return transform.position;
		}
		
		return orbiting.getPositionAtTimeAhead(timeAhead) + orbitRadius * new Vector3(Mathf.Cos(angle + timeAhead * orbitSpeed), 0, Mathf.Sin(angle + timeAhead * orbitSpeed));
	}
	
	public float growthRate = 1f;
	// Update is called once per frame
	void Update () {
		if(SolarSystem.timeRunning)
		{
			foodAmount += (currentShips * currentShips * Time.deltaTime * foodGiven) / 8f; //Generate food!
			growthRate = currentShips*.5f;
			if(currentShips > 0 && !isMoon)
			{
				if(currentShips > totalShips)
				{
					totalShips -= ((randomShipTime * Time.deltaTime ) * growthRate) / 8f;
				}
				else
				{
					currentShips += (randomShipTime * Time.deltaTime * (currentShips / 4f) * (currentShips / totalShips)) * growthRate;
				}
				
				if (totalShips <= 2 && test1) {
					Terminal.addMessage("A planet is close to destruction!", 0);
					test1 = false;
					//print("A planet is close to destruction");
				}
				if (totalShips <= 1 && test2) {
					Terminal.addMessage("A planet is nearing its end!", 0);
					test1 = false;
					//print("A planet is close to destruction");
				}
				if (totalShips < 0) {
					Terminal.addMessage("A planet has been destroyed!", 0);
					//print ("A planet has been destroyed");
					Destroy(this.gameObject);
					Destroy(clone.gameObject);
				}
			}
			
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
		
		RenderAhead();
		
		
		
		
	}

	public GUISkin skin;
	public void touch(bool a){
		istouching = a;
		if(!a)
		{
			playerHere = false;
		}
		Player.currentPlayer.trigger = false;
	}
	
	
	public bool playerHere = false;
	
	public void landHere()
	{
		
		if(!visited || currentShips == 0)
		{
			Terminal.addMessage("You forage the planet and find " + (int)foodAmount + " food.", 1);
			if(!visited)
			{
				visited = true;
				Player.currentPlayer.trigger = true;
				Player.currentPlayer.triggerEvent(planetType);
			}
		}
		else
		{
			Terminal.addMessage("Your colonists provide you with " + (int)foodAmount + " food.", 1);
		}
		
		playerHere = true;
		
		
		
		Player.currentPlayer.foodAmount += foodAmount;
		Player.currentPlayer.foodgain += foodAmount;
		
		foodAmount = 0;
		Player.currentPlayer.gaingain = true;
		Player.currentPlayer.prevtime = Time.time;
	}
	
	void OnGUI () {
		
		if(isMoon)
		{
			return;
		}
		
		GUI.skin = skin;
		//Vector2 size = GUI.skin.GetStyle("ProgressBarText").CalcSize(GUIContent(label));
		Vector3 point = 
			Camera.main.WorldToScreenPoint(transform.position + new Vector3(transform.localScale.x,0,0));
		Vector3 point2 = 
			point - new Vector3(0, -60,0);
			//Camera.main.WorldToScreenPoint(transform.position + new Vector3(0,0,transform.localScale.z)) - new Vector3(0,-10,0);
		
		if(Camera.main.pixelRect.Contains(point))
		{
		
			if (istouching){
				if(playerHere)
				{
					if (currentShips >= 1 && GUI.Button(new Rect(point2.x , -point2.y + Screen.height - 100, 200, 40), "Take Ship"))
					{
						if(currentShips > 0){
							currentShips -= 1;
							Player.currentPlayer.addShip();
							Terminal.addMessage("Took a ship from the planet.", 1);
						}
						Event.current.Use();
					
					}
					if (Player.currentPlayer.shipCount() >= 2 && GUI.Button(new Rect(point2.x , -point2.y + Screen.height - 60, 200, 40), "Deposit Ship"))
					{
						if(Player.currentPlayer.shipCount() > 0){
							Player.currentPlayer.removeShip();
							currentShips += 1;
							Terminal.addMessage("Deposited a ship onto the planet.", 1);
						}
						Event.current.Use();
	
					}
				}
				else
				{
					if(GUI.Button(new Rect(point2.x, -point2.y + Screen.height - 80, 200, 40), "Land on planet"))
					{
						landHere();
					}
				}
			}
			
			if(visited)
			{
				GUI.Label (new Rect (point.x, -point.y + Screen.height -80 , 200, 40), "Food: ");
				GUI.Label (new Rect (point.x, -point.y + Screen.height -40, 200, 40), " "+(int)foodAmount);
				GUI.Label (new Rect (point.x, -point.y + Screen.height, 200, 40), " " + currentShips.ToString("F1") + "/" + totalShips.ToString("F1") + " ");
			}
		}
		
	}
	
	public float foodgain;
	public static float shipgain;
	public bool gaingain = false;
	
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
