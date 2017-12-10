using System.Collections;
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
			
		}
	}
	private Rigidbody rb;
	public void onKicked(){
		kicked = true;
		Debug.Log ("cubeKick");
		rb = transform.GetComponent<Rigidbody> ();
		rb.AddForce (new Vector3(0,5f,3f) * 50);
		rb.AddTorque((Vector3.up+Vector3.forward) * Mathf.PI);
	}
}
