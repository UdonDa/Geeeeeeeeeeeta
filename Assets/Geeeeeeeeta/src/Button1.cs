using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UnityEngine.XR.iOS {
public class Button1 : MonoBehaviour {
		
		RequestWetherAPI requestWetherAPI = new RequestWetherAPI ();

		// Use this for initialization
		void Start () {
		}
		
		// Update is called once per frame
		void Update () {
			
		}

		public void onClick() {
			Debug.Log ("押されたよーーーーーーーーーーーーーーーーん");
			requestWetherAPI.setIsFinishedForImage ();// この命令で、雲の画像を設置できる！
			requestWetherAPI.setIsFinishedForText(); //この命令で、天気予報テキスト表示！
		}
	}
}
	