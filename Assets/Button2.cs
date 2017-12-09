using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button2 : MonoBehaviour {
	public GameObject GameManager;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	public void onButtonClick(){
		GameManager.GetComponent<GameManager> ().onKicked ();
	}
}
