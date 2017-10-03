using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InteractableObject : MonoBehaviour {

	private GameController gc;
	public string name;
	public string mainObjective;

	// Use this for initialization
	void Start () {
		this.GetComponent<Button> ().onClick.AddListener (Interact);	//run interact() on click
		gc = GameObject.Find ("GameController").GetComponent<GameController>();	//get reference of GameController
	}

	private void Interact() {
		switch(tag) {
		case "Door":	//if this.object is door
			Debug.Log ("Case Door");
			Debug.Log (mainObjective);
			if (mainObjective != null) {	//if it has mainObjective
				Canvas tempCanvas = GameObject.Find (mainObjective).GetComponent<Canvas> ();	//get canvas named mainObjective
				gc.player.SetLocation (tempCanvas);	//move player to the new canvas
			}
			break;
		case "Item":	//if this.object is item
			Debug.Log ("Case Item");
			Debug.Log (mainObjective);
			if (mainObjective == "PickUp") {	//if item is ment to be picked up
				gc.player.Inventory("Add", this);	//add item to inventory
				Debug.Log(name + " was picked up");
				gc.player.Inventory ("print");	//print the inventory
				this.transform.SetParent(null);	//Moving the button away from the canvas
				//Destroy (this.gameObject);	//destroy this item
			} else if(mainObjective=="Use"){
				gc.player.dressUp (name);
				this.transform.SetParent (null);
			}
			break;
		case "Container":
			Debug.Log ("Case Container");

			break;
		default:
			Debug.Log ("Default Case");
			break;
		}
	}
}
