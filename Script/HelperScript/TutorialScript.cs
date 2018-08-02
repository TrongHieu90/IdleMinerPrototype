using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialScript : MonoBehaviour {

    private string _text;

	// Use this for initialization
	void Start () {
        _text = "Press 'Up Arrow' and 'Down Arrow' or 'W' and 'S' to move the camera up and down";
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnGUI()
    {
        GUI.Label(new Rect(150, 10, 100, 100), _text);
    }
}

