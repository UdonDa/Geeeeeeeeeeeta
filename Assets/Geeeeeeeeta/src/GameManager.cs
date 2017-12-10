using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

	public GameObject Cube;
	public GameObject Button2;

	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {

	}

	public void onKicked(){
		Cube.GetComponent<Cube> ().onKicked ();
		Debug.Log ("gamemanagerKick");
	}

}
