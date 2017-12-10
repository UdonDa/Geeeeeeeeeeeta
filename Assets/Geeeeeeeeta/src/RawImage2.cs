using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RawImage2 : MonoBehaviour {
	private float alfa = 0;
	private float red, green, blue;


	// Use this for initialization
	void Start () {
		red = GetComponent<RawImage>().color.r;
		green = GetComponent<RawImage>().color.g;
		blue = GetComponent<RawImage>().color.b;
		alfa = 0;
		GetComponent<RawImage>().color = new Color(red, green, blue, alfa);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	public void onFinish(){
		
		alfa = 0.4f;
		GetComponent<RawImage>().color = new Color(red, green, blue, alfa);
	}
}
