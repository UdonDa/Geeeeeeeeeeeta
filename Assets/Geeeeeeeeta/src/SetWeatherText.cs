using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


namespace UnityEngine.XR.iOS {
	public class SetWeatherText : MonoBehaviour {

		RequestWetherAPI requestWetherAPI = new RequestWetherAPI();
		private Text targetText; 

		void Start () {	
			
		}
		
		void Update () {
			
			var isFinishedForText = requestWetherAPI.getIsFinishedForText ();
			if (isFinishedForText) {
				var weatherText = requestWetherAPI.getTomorrowWeather();
				var userPlace = requestWetherAPI.getUserPlace ();
				setWetherTexts (weatherText, userPlace);
				//Debug.Log ("マンげ！！！！！！！！！！！！！" + weatherText + " : " + userPlace);
				requestWetherAPI.setIsFinishedForText ();
			}
		}

		private void setWetherTexts(string text, string userPlace){
			
			this.targetText = this.GetComponent<Text>();
			this.targetText.text = "明日の\n" + userPlace +"の天気は\n" + text;
		}
	}
}