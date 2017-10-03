using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Player {
	
	private string name;
	private Canvas location;	//var for location
	private List<InteractableObject> inventory = new List<InteractableObject>();	//list to use as inventory
	private double clothes;

	public void SetLocation (Canvas loc) {
		if (location != null) { //if location is set
			location.enabled = false; //set location off
		}
		this.location = loc;	//set new location
		location.enabled = true;	//set new location on
	}

	public void Inventory (string action, InteractableObject item = null) {
		if (action == "Add") {
			inventory.Add (item);
		} else if (action == "Remove" && inventory.Contains (item)) {	//if item is in the list remove it
			inventory.Remove (item);
		} else if (action == "print") {
			string text = "";
			foreach (InteractableObject _item in inventory) {
				text += (_item.ToString() + ", ");
			}
			Debug.Log (text);
		}
	}
	public void dressUp(string name){
		if (this.clothes==1) {
			Debug.Log ("You are fully dressed");
		} else if (name.Equals ("Socks")) {
			this.clothes += 0.25;
		} else if (name.Equals ("T-Shirt")) {
			this.clothes += 0.25;
		} else if (name.Equals ("Jeans")) {
			this.clothes += 0.25;
		} else if (name.Equals ("Hoodie")) {
			this.clothes += 0.25;
		} else {
			Debug.Log ("Clothing not found");
		}
		Debug.Log (this.clothes);
	}

	public Player (string name) {
		this.name = name;
		this.clothes = 0;
	}


	void Start () {
		

	}
}

