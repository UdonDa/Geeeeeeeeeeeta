// Copyright 2016 Google Inc. All rights reserved.
//
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
//     http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

using System;
using System.Collections;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

using Firebase;
using Firebase.Analytics;
using Firebase.Unity.Editor;
using Firebase.Database;

// Handler for UI buttons on the scene.  Also performs some
// necessary setup (initializing the firebase app, etc) on
// startup.

namespace UnityEngine.XR.iOS {
	public class UIHandler : MonoBehaviour {

	  public GUISkin fb_GUISkin;
	  private Vector2 controlsScrollViewVector = Vector2.zero;
	  private Vector2 scrollViewVector = Vector2.zero;
	  bool UIEnabled = true;
	  private string logText = "";
	  const int kMaxLogSize = 16382;
	  DependencyStatus dependencyStatus = DependencyStatus.UnavailableOther;	


		//var ref = FirebaseDatabase.DefaultInstance.GetReference("GameSessionComments");
	  


	  // When the app starts, check to make sure that we have
	  // the required dependencies to use Firebase, and if not,
	  // add them if possible.
	  public virtual void Start() {
			FirebaseApp.DefaultInstance.SetEditorDatabaseUrl("https://geeeeta-n00b.firebaseio.com/");

			DatabaseReference reference = FirebaseDatabase.DefaultInstance.RootReference;
			FirebaseDatabase.DefaultInstance.GetReference("message").ValueChanged += HandleValueChanged;




	    FirebaseApp.CheckAndFixDependenciesAsync().ContinueWith(task => {
	      dependencyStatus = task.Result;
	      if (dependencyStatus == DependencyStatus.Available) {
	        InitializeFirebase();
	      } else {
	        Debug.LogError("Could not resolve all Firebase dependencies: " + dependencyStatus);
	      }
	    });
	  }

	  // Exit if escape (or back, on mobile) is pressed.
	  public virtual void Update() {
	    if (Input.GetKeyDown(KeyCode.Escape)) {
	      Application.Quit();
	    }

			//FirebaseDatabase.DefaultInstance.GetReference("message").ValueChanged += HandleValueChanged;
			FirebaseDatabase.DefaultInstance.GetReference("geeeeta-n00b").ValueChanged += HandleValueChanged;
			//Debug.Log ("Firabase : 変更点 : " + FirebaseDatabase.DefaultInstance.GetReference ("message").ValueChanged );
	  }

	  // Handle initialization of the necessary firebase modules:
	  void InitializeFirebase() {
	    DebugLog("Enabling data collection.");
	    FirebaseAnalytics.SetAnalyticsCollectionEnabled(true);

	    DebugLog("Set user properties.");
	    // Set the user's sign up method.
	    FirebaseAnalytics.SetUserProperty(
	      FirebaseAnalytics.UserPropertySignUpMethod,
	      "Google");
	    // Set the user ID.
	    FirebaseAnalytics.SetUserId("uber_user_510");

		// Realtime DB

	  }

	  // End our analytics session when the program exits.
	  void OnDestroy() { }

	  public void AnalyticsLogin() {
	    // Log an event with no parameters.
	    DebugLog("Logging a login event.");
	    FirebaseAnalytics.LogEvent(FirebaseAnalytics.EventLogin);
	  }

	  public void AnalyticsProgress() {
	    // Log an event with a float.
	    DebugLog("Logging a progress event.");
	    FirebaseAnalytics.LogEvent("progress", "percent", 0.4f);
	  }

	  public void AnalyticsScore() {
	    // Log an event with an int parameter.
	    DebugLog("Logging a post-score event.");
	    FirebaseAnalytics.LogEvent(
	      FirebaseAnalytics.EventPostScore,
	      FirebaseAnalytics.ParameterScore,
	      42);
	  }

	  public void AnalyticsGroupJoin() {
	    // Log an event with a string parameter.
	    DebugLog("Logging a group join event.");
	    FirebaseAnalytics.LogEvent(FirebaseAnalytics.EventJoinGroup, FirebaseAnalytics.ParameterGroupId,
	      "spoon_welders");
	  }

	  public void AnalyticsLevelUp() {
	    // Log an event with multiple parameters.
	    DebugLog("Logging a level up event.");
	    FirebaseAnalytics.LogEvent(
	      FirebaseAnalytics.EventLevelUp,
	      new Parameter(FirebaseAnalytics.ParameterLevel, 5),
	      new Parameter(FirebaseAnalytics.ParameterCharacter, "mrspoon"),
	      new Parameter("hit_accuracy", 3.14f));
	  }

	  // Output text to the debug log text field, as well as the console.
	  public void DebugLog(string s) {
	    print(s);
	    logText += s + "\n";

	    while (logText.Length > kMaxLogSize) {
	      int index = logText.IndexOf("\n");
	      logText = logText.Substring(index + 1);
	    }

	    scrollViewVector.y = int.MaxValue;
	  }

	  void DisableUI() {
	    UIEnabled = false;
	  }

	  void EnableUI() {
	    UIEnabled = true;
	  }

	  // Render the log output in a scroll view.
	  void GUIDisplayLog() {
	    scrollViewVector = GUILayout.BeginScrollView (scrollViewVector);
	    GUILayout.Label(logText);
	    GUILayout.EndScrollView();
	  }

	  // Render the buttons and other controls.
	  void GUIDisplayControls(){
	    if (UIEnabled) {
	      controlsScrollViewVector =
	          GUILayout.BeginScrollView(controlsScrollViewVector);
	      GUILayout.BeginVertical();

	      if (GUILayout.Button("Log Login")) {
	        AnalyticsLogin();
	      }
	      if (GUILayout.Button("Log Progress")) {
	        AnalyticsProgress();
	      }
	      if (GUILayout.Button("Log Score")) {
	        AnalyticsScore();
	      }
	      if (GUILayout.Button("Log Group Join")) {
	        AnalyticsGroupJoin();
	      }
	      if (GUILayout.Button("Log Level Up")) {
	        AnalyticsLevelUp();
	      }
	      GUILayout.EndVertical();
	      GUILayout.EndScrollView();
	    }
	  }
	
		//DB そうさ
		private void writePushed(string message) {
		
			Debug.Log ("firebase : writePushed() : " + message);
			UserPushed userPushed = new UserPushed (message);
			string json = JsonUtility.ToJson (userPushed);
			Debug.Log ("jsonの型 : " + json.GetType ());
			FirebaseApp.DefaultInstance.SetEditorDatabaseUrl("https://geeeeta-n00b.firebaseio.com/");
			DatabaseReference reference = FirebaseDatabase.DefaultInstance.RootReference;
			reference.SetRawJsonValueAsync (json);
		}

		public void sendMessagePushed() {
			
			Debug.Log ("firebase : onClick()");
			var message = "pushed";
			writePushed (message);
		}


		void HandleValueChanged(object sender, ValueChangedEventArgs args) {
			if (args.DatabaseError != null) {
				Debug.LogError(args.DatabaseError.Message);
				return;
			}
			// Do something with the data in args.Snapshot
			//Debug.Log(args.Snapshot.Child("message").Value);
		}
	}
}