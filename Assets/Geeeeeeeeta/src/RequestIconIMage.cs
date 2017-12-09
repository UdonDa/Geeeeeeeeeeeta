using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;


namespace UnityEngine.XR.iOS {
	public class RequestIconIMage : MonoBehaviour {

		RequestWetherAPI requestWetherAPI = new RequestWetherAPI();

		private float alfa = 0;
		private float red, green, blue;

		void Start () {	
			red = GetComponent<RawImage>().color.r;
			green = GetComponent<RawImage>().color.g;
			blue = GetComponent<RawImage>().color.b;
		}

		void Update () {
			
			GetComponent<RawImage>().color = new Color(red, green, blue, alfa);

			var isFinishedForImage = requestWetherAPI.getIsFinishedForImage ();
			if (isFinishedForImage) {
				var iconURL = requestWetherAPI.getIconURL ();
				StartCoroutine (setWetherIcon(iconURL));
				requestWetherAPI.setIsFinishedForImage ();
				alfa = 100;
			}
		}


		private IEnumerator setWetherIcon(string url){

			WWW www = new WWW(url);
			yield return www;
			RawImage rawImage = GetComponent<RawImage>();
			rawImage.texture = www.textureNonReadable;
			//rawImage.s
			//rawImage.SetNativeSize();
		}
	}
}