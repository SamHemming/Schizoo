using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InteractableObject : MonoBehaviour {

	private GameController gc;
	public string mainObjective;
	public string sideObjective;
	public bool isLocked;
	private GameObject popUpBox;
	private CanvasRenderer popUpBoxRenderer;
	private CanvasRenderer popUpBoxTextRenderer;
	public string description;
	public string type;

	// Use this for initialization
	void Start () {
		if (this.GetComponent<Button>()) {	//if has button component
			this.GetComponent<Button> ().onClick.AddListener (Interact);	//run interact() on click
		}
		gc = GameObject.Find ("GameController").GetComponent<GameController>();	//get reference of GameController
		popUpBox = GameObject.Find ("PopUpBox");	//get reference to popupbox
		popUpBoxRenderer = popUpBox.GetComponent<CanvasRenderer> ();	//get reference to popupbox renderer
		popUpBoxTextRenderer = popUpBox.GetComponentInChildren<Text> ().GetComponent<CanvasRenderer> ();	//get reference to popupbox text renderer
	}

	//-------------------------------------------------------------------------------------

	public InteractableObject(string name){
		this.name = name;
	}

	//-------------------------------------------------------------------------------------

	private void Interact() {
		switch(tag) {

		case "Door":	//if this.object is door
			Debug.Log ("Case Door");
			Debug.Log (mainObjective);
			if (isLocked) {	//if the door is locked
				if (gc.player.InventoryContain(this.name)) {
					isLocked = false;
					//gc.player.Inventory ("Remove" , GameObject.Find(this.name).GetComponent<InteractableObject>());
				}
				else {
					StopAllCoroutines ();	//make sure all loops are closed
					StartCoroutine (PopUp ("The door is locked."));	//render popup with given text
				}
			}
			if (mainObjective != null && !isLocked) {	//if it has mainObjective and is NOT locked
				Canvas newCanvas = GameObject.Find (mainObjective).GetComponent<Canvas> ();	//get canvas named mainObjective
				gc.player.SetLocation (newCanvas);	//move player to the new canvas
			}
			break;

		case "Item":	//if this.object is item
			//Debug.Log ("Case Item");
			//Debug.Log (mainObjective);
			if (mainObjective == "PickUp") {	//if item is ment to be picked up
				gc.player.Inventory("Add", this);	//add item to inventory
				//Debug.Log(name + " was picked up");
				StopAllCoroutines();
				StartCoroutine(PopUp(name + " was added to inventory."));
				//gc.player.Inventory ("print");	//print the inventory
				this.transform.SetParent (null);	//Moving the button away from the canvas
			} else if(mainObjective == "Use") {
				gc.player.DressUp (name);
				StopAllCoroutines ();
				StartCoroutine(PopUp("You put " + name + " on."));
				this.transform.SetParent (null);
			}
			break;

//		case "Container":
//			Debug.Log ("Case Container");
//			if (mainObjective == "Dialog") {
//				
//			}
//			break;

		default:
			Debug.Log ("Default Case");
			break;
		}
	}

	//--------------------------------------------------------------------------------------

	IEnumerator PopUp(string message) {	//iterate once per frame
		//Debug.Log (popUpBox.name);
		popUpBox.GetComponentInChildren<Text>().text = message;	//set popup message
		popUpBox.SetActive (true);	//set popupbox active
		popUpBoxRenderer.SetAlpha (1);	//set the alpha channel to full/1
		popUpBoxTextRenderer.SetAlpha (1);	//set alpha for text too
		yield return new WaitForSeconds(3);	//wait for 3 sec so that player has time to read the message before it starts to fade

		for (float i = 1f; i > 0.00f; i-=0.01f) {	//iterate from 1 to 0 in 0.01 steps
			popUpBoxRenderer.SetAlpha(i);	//set alpha channel	1% lower every frame
			popUpBoxTextRenderer.SetAlpha (i);
			//Debug.Log ("Alpha of the popup: " + popUpBoxRenderer.GetAlpha());
			if (i <= 0.01) {	//when the popup hase faded
				popUpBox.SetActive (false);	//set it inactive
			}
			yield return null;	//mandetory yield for the coroutine
		}
	}
}
