using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class Terminal : MonoBehaviour {
	
	// Use this for initialization
	void Start () {
		addMessage("Welcome!", 0);
	}
	
	static List<string> messages = new List<String>();
	static List<float> lifespan = new List<float>();
	public GUISkin skin;
	
	public int rectHeight = 100;
	public int rectWidth = 800;
	
	public int messageMax = 8;
	
	
	public static void addMessage(string message, int priority)
	{
		messages.Add(message);
		lifespan.Add(15f);
	}
	
	void Update(){
		for(int i = 0; i < messages.Count; i++)
		{
			lifespan[i] -= Time.deltaTime;
			if(lifespan[i] < 0)
			{
				messages.RemoveAt(i);
				lifespan.RemoveAt(i);
			}
		}
	}
	
	void OnGUI () {
	
	
		GUI.contentColor = Color.white;
		GUI.skin = skin;
		
		for(int i = messages.Count - 1; i >= 0; i--)
		{
			if(i < 3)
			{
				GUI.contentColor = Color.Lerp(Color.white, Color.clear, ((float)(i - messageMax)) / messageMax);
			}
			
			if(lifespan[i] < 1f)
			{
				GUI.contentColor = Color.Lerp(Color.clear, Color.white, lifespan[i]);
			}
			
			GUI.Label (new Rect (20, -40 + Screen.height + -rectHeight * i, rectWidth * 10, rectHeight), messages[i]);
		}
		
	}
		
	
}
