﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cube : MonoBehaviour {

	private bool kicked = false;

	[SerializeField]
	private GameObject qrcodePlane;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (!kicked) {
			transform.position = qrcodePlane.transform.TransformPoint (new Vector3 (0, 0.01f, 0));
			transform.rotation = qrcodePlane.transform.rotation * Quaternion.LookRotation (Vector3.forward, Vector3.up);
		} else {
			transform.GetComponent<Rigidbody> ().AddForce (transform.forward * 4);
		}
	}
	public void onKicked(){
		kicked = true;
	}
}
