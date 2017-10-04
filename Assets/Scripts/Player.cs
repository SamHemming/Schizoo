using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Player {
	
	private string name;
	private Canvas location;	//var for location
	private List<InteractableObject> inventory = new List<InteractableObject>();	//list to use as inventory
	private double clothes;
	private string clock;
	private int hours;
	private int minutes;

	public void SetLocation (Canvas loc) {
		if (location != null) { //if location is set
			location.enabled = false; //set location off
		}
		this.location = loc;	//set new location
		location.enabled = true;	//set new location on
		this.setClock(5);
	}



	public void Inventory (string action, InteractableObject item = null) {
		if (action == "Add") {
			inventory.Add (item);
		}
		else if (action == "Remove" && inventory.Contains (item)) {	//if item is in the list remove it
			inventory.Remove (item);
		}
		else if (action == "print") {
			string text = "";
			foreach (InteractableObject _item in inventory) {
				text += (_item.ToString() + ", ");
			}
			Debug.Log (text);
		}
		this.setClock (1);
	}



	public void dressUp(string name){
		if (this.clothes == 1) {
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
		this.setClock (03);
	}
	public void setClock(int addMinutes){
		if (this.minutes + addMinutes >= 60) {
			this.hours++;
			int over = this.minutes + addMinutes - 60;
			this.minutes = over;
		} else {
			this.minutes += addMinutes;
		}
	}
	public string getClock(){
		if (this.hours < 10) {
			this.clock = "0" + this.hours;
		} else {
			this.clock =""+ this.hours;
		}
		this.clock += ":";
		if (this.minutes < 10) {
			this.clock += "0" + this.minutes;
		} else {
			this.clock += this.minutes;
		}
		return this.clock;
	}



	public Player (string name) {
		this.name = name;
		this.clothes = 0;
		this.hours = 09;
		this.minutes = 00;
	}


}

