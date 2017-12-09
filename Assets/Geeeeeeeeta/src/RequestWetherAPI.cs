using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using MiniJSON;
using System.Text.RegularExpressions;
using SimpleJSON;
using UnityEngine.UI;

namespace UnityEngine.XR.iOS {
	public class RequestWetherAPI : MonoBehaviour {
		public static string weather;
		public static string weatherIcon;
		public static string userPlace;
		public static bool isFinishedForImage = false;
		public static bool isFinishedForText = false;

		void Start () {
			StartCoroutine(getWeather());
		}
		
		void Update () {}

		public IEnumerator getWeather () {
			var lat = 35.699947;
			var log = 139.77326;

			var URL = "http://api.openweathermap.org/data/2.5/weather?lat=" + lat+ "&lon="+ log+"&appid=277df9ba1b0de6b002d78f85a9bf890a";

			WWW www = new WWW (URL);
			yield return www;
			Debug.Log (www.text);

			//var json = Regex.Unescape(www.text);
			var N = JSON.Parse(www.text); // Nは、JSONObject型

			Debug.Log (N);
			weather = N [1][0]["main"];
			weatherIcon = N [1] [0] ["icon"];
			weatherIcon = "http://openweathermap.org/img/w/" + weatherIcon+".png";
			Debug.Log ("天気: " +weather + "アイコン" + weatherIcon);
			userPlace = N [10];
			Debug.Log ("userPlace : " + userPlace);

			if (www.error == null) {
				Debug.Log("Success");
			} else{
				Debug.Log("Error");  
				weather = "Clouds";
				weatherIcon = "http://openweathermap.org/img/w/03n.png";
			}
		}   

		public IEnumerator getIcon(string iconURL){
							
			WWW www = new WWW(iconURL);
			yield return www;

			RawImage rawImage = GetComponent<RawImage>();

			Debug.Log ("アイコン設置！！！！！！！！！！");
			rawImage.texture = www.textureNonReadable;
			rawImage.SetNativeSize();
				
		}

		public bool getIsFinishedForText() {
			return isFinishedForText;
		}

		public void setIsFinishedForText() {
			isFinishedForText = true;
		}

		public bool getIsFinishedForImage() {
			return isFinishedForImage;
		}

		public void setIsFinishedForImage() {
			isFinishedForImage = true;
		}

		public string getIconURL() {
			return weatherIcon;
		}

		public string getUserPlace() {
			return userPlace;
		}

		public string getTomorrowWeather() {
			return weather;
		}
		
	}
}
