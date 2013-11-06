using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class Terminal : MonoBehaviour {
	
	// Use this for initialization
	void Start () {
		messages.Add("Hello");
		messages.Add("Test!");
	}
	
	static List<string> messages = new List<String>();
	
	public GUISkin skin;
	
	public int rectHeight = 30;
	public int rectWidth = 200;
	
		// Update is called once per frame
	void OnGUI () {
	
		
		GUI.skin = skin;
		
		for(int i = 0; i < messages.Count; i++)
		{
			GUI.Label (new Rect (20, -40 + Screen.height + -rectHeight * i, rectWidth, rectHeight), messages[i]);
		}
		
	}
		
	
}
