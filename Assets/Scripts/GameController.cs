using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour {
	
	private List<Canvas> canvaslist = new List<Canvas> (); //make list of canvases
	public Player player = new Player ("Kake");	//make new player with name "Kake"

	// Use this for initialization
	void Start () {

		//add all canvases to the canvaslist
		canvaslist.Add(GameObject.Find("Canvas_Start").GetComponent<Canvas>());
		canvaslist.Add (GameObject.Find ("Canvas_LobbyLeft").GetComponent<Canvas> ());
		canvaslist.Add (GameObject.Find ("Canvas_Toilet").GetComponent<Canvas> ());
		canvaslist.Add (GameObject.Find ("Canvas_Bedroom").GetComponent<Canvas> ());
		canvaslist.Add (GameObject.Find ("Canvas_Livingroom").GetComponent<Canvas> ());
		canvaslist.Add (GameObject.Find ("Canvas_Kitchen").GetComponent<Canvas> ());
		canvaslist.Add (GameObject.Find ("Canvas_Closet").GetComponent<Canvas> ());
		canvaslist.Add (GameObject.Find ("Canvas_LobbyRight").GetComponent<Canvas> ());
		canvaslist.Add (GameObject.Find ("Canvas_HouseDoor").GetComponent<Canvas> ());
		canvaslist.Add (GameObject.Find ("Canvas_Outside1").GetComponent<Canvas> ());
		canvaslist.Add (GameObject.Find ("Canvas_Outside1.1").GetComponent<Canvas> ());
		canvaslist.Add (GameObject.Find ("Canvas_Outside1.2").GetComponent<Canvas> ());
		canvaslist.Add (GameObject.Find ("Canvas_Outside1.3").GetComponent<Canvas> ());
		canvaslist.Add (GameObject.Find ("Canvas_Playground").GetComponent<Canvas> ());
		canvaslist.Add (GameObject.Find ("Canvas_Highway").GetComponent<Canvas> ());
		canvaslist.Add (GameObject.Find ("Canvas_Trainstation").GetComponent<Canvas> ());
		canvaslist.Add (GameObject.Find ("Canvas_DarkAlley").GetComponent<Canvas> ());
		canvaslist.Add (GameObject.Find ("Canvas_Store").GetComponent<Canvas> ());







		//make all canvases hidden and set rendermode right
		foreach (Canvas canvas in canvaslist) {
			canvas.enabled = false;
			canvas.renderMode = RenderMode.ScreenSpaceCamera;
			canvas.GetComponentInChildren<CanvasScaler>().uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
		}

		//set playerlocation
		player.SetLocation (canvaslist [0]);
	}
	void Update(){
		GameObject.Find ("Text_Clock").GetComponent<Text> ().text = player.getClock();
		Canvas temp = GameObject.Find ("Canvas_UI").GetComponent<Canvas> ();
		if (canvaslist [0].enabled == true) {
			temp.enabled = false;
		} else {
			temp.enabled = true;
		}
	}

}
