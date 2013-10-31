using UnityEngine;
using System.Collections;

public class GUITest : MonoBehaviour {
	public Texture aTexture;
	void OnGUI () {
		// Make a background box
		if (!aTexture) {
            Debug.LogError("Assign a Texture in the inspector.");
            return;
        }
        GUI.DrawTexture(new Rect(0, 0, 0, 1000), aTexture, ScaleMode.ScaleToFit, true, 10.0F);
		
	}
}