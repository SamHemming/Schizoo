using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class DialogController : MonoBehaviour {

	private List<string> sentences;	//list of strings called sentences
	private Text header;	//header of dialogbox
	private Text dialogText;	//text of dialogbox
	private Animator animator;	//animation of dialogbox
	private Dialog dialog;	//dialog object
	private int sentenceCounter;	//sentense counter :D
	private Button button1;	//reference for buttons
	private string button1Command;	//var for buttons command
	private string button1Argument;	//var for buttons argument
	private string button1Argument2;	//var for buttons second argument :D
	private Button button2;
	private string button2Command;
	private string button2Argument;
	private string button2Argument2;
	private Button button3;
	private string button3Command;
	private string button3Argument;
	private string button3Argument2;
	private string[,] commandArray;	//2d array for commands
	private string command;
	private string argument;
	private string argument2;

	//----------------------------------------------------------------------------------------------------
	// Use this for initialization
	void Start () {
		sentences = new List<string>();	//initiated sentences

		header = GameObject.Find ("DialogHeader").GetComponent<Text> ();	//get headers text component
		dialogText = GameObject.Find ("DialogText").GetComponent<Text> ();	//get dialog text component
		animator = GameObject.Find ("DialogueBox").GetComponent<Animator> ();	//get dialog opening animation

		button1 = GameObject.Find ("Button_Option1").GetComponent<Button> ();	//get buttons and set there onClick action
		button1.onClick.AddListener (()=>DialogButtonPressed("Button1"));

		button2 = GameObject.Find ("Button_Option2").GetComponent<Button> ();
		button2.onClick.AddListener (()=>DialogButtonPressed("Button2"));

		button3 = GameObject.Find ("Button_Option3").GetComponent<Button> ();
		button3.onClick.AddListener (()=>DialogButtonPressed("Button3"));
	}

	//----------------------------------------------------------------------------------------------------------

	private void DialogButtonPressed(string option) {	//set what dialog buttons do ones pressed
		command = null;
		argument = null;
		argument2 = null;
		if (option == "Button1") {	//if pressed button was button1
			command = button1Command;	//take command from button1
			if (button1Argument != null) {	//if button has argument
				argument = button1Argument;	//store the argument
				if (button1Argument2 != null) {
					argument2 = button1Argument2;
				}
			}
		}
		else if (option == "Button2") {
			command = button2Command;
			if (button2Argument != null) {
				argument = button2Argument;
				if (button2Argument2 != null) {
					argument2 = button2Argument2;
				}
			}
		}
		else if (option == "Button3") {
			command = button3Command;
			if (button3Argument != null) {
				argument = button3Argument;
				if (button3Argument2 != null) {
					argument2 = button3Argument2;
				}
			}
		} else {
			Debug.Log ("Invalid button pressed.");
		}

		switch (command) {	//check what the command is

		case "next":
			sentenceCounter++;	//iterate sentenceCounter
			DisplayNextSentence ();	//display said sentence
			break;
		
		case "end":
			EndDialog ();	//end dialog
			break;
		
		case "to":
			if (Int32.TryParse (argument, out sentenceCounter)) {	//try to convert given argument into Int32
				DisplayNextSentence ();	//if successfull display said sentence
			}	else {	//else error message
				Debug.Log("TryParse Failed to convert: " + argument + " to Int32.");
			}
			break;
		
		case "back":
			sentenceCounter--;	//iterate sentenceCounter back by one
			DisplayNextSentence ();	//display said sentence
			break;

		case "takeItem":
			InteractableObject item = new GameObject ().AddComponent<InteractableObject> ();	//make new gameobject with interactableObject script
			item.name = argument;	//name the item
			FindObjectOfType<GameController> ().player.Inventory ("add", item);	//add the item to players inventory
			EndDialog();
			break;
		
		default:	//if no case match the command give error message
			Debug.Log ("Command not listed.");
			break;
		}

		switch (argument2) {
		case "lock":
			dialog.GetComponentInChildren<Button> ().interactable = false;
			break;
		default:
			Debug.Log ("argument2 default case argument2: " + argument2);
			break;
		}
	}

	//------------------------------------------------------------------------------------------------------

	public void StartDialog(Dialog dialogTemp) {	//start dialog of argument dialog
		//Debug.Log ("Dialog started with " + dialog.name);
		animator.SetBool("IsOpen", true);	//run dialog opening animation
		dialog = dialogTemp;
		commandArray = InterpretCommands (dialog.commands);	//interpret raw commands
		header.text = dialog.header;	//set the dialogheader
		sentences = dialog.sentences;	//get reference to dialogs sentences
		DisplayNextSentence ();	//display first sentence
	}

	//----------------------------------------------------------------------------------------------------

	private string[,] InterpretCommands(string[] commands) {	//split commands into separate parts
		string[,] slicedCommands = new string[commands.Length,6];	//new 2d array to hold the command parts in
		string separateCommand = "";
		int row = 0;
		foreach (string command in commands) {	//each commands gets own row
			int column = 0;
			foreach(char letter in command.ToCharArray()){	//each command type gets own column
				if(letter.Equals(';')){	//if ";" is get split the word
					slicedCommands[row,column] = separateCommand;	//write command into its own slot
					column++;	//iterate column
					separateCommand = "";	//clear separate command
				}else {	//if the letter is not spacer (;) add it to the word
					separateCommand += letter;	//add letter to separate command
				}
			}
			row++;	//iterate row
		}
		return slicedCommands;	//return the 2d array
	}

	//-------------------------------------------------------------------------------------------------------------

	public void DisplayNextSentence() {	//write next sentence to the dialog
		if (sentenceCounter > sentences.Count || sentenceCounter < 0) {	//if sentence counter is not valid number
			Debug.Log("SentenceCounter out of range: " + sentenceCounter);
			EndDialog ();
		} else {
			string sentence = sentences[sentenceCounter];	//take sentence pointed by counter
			StopAllCoroutines();	//stop previous coroutine
			StartCoroutine(TypeSentence(sentence));	//start coroutine that types the dialog
			//dialog.text = sentence;
			//Debug.Log (sentence);
			UpdateButtons ();	//update the buttons
		}
	}

	//------------------------------------------------------------------------------------------------------------

	IEnumerator TypeSentence (string sentence) {	//add one letter to the dialog every frame
		dialogText.text = null;	//clear the dialog text
		foreach (char letter in sentence.ToCharArray()) {	//for each letter in sentence
			dialogText.text += letter;	//add the letter to dialog
			yield return null;	//mandatory yield
		}
	}

	//---------------------------------------------------------------------------------------------------------

	private void EndDialog() {
		animator.SetBool ("IsOpen", false);	//run dialog closing animation
		sentenceCounter = 0;
		sentences = null;	//clear sentences
		commandArray = null;
		Debug.Log ("End of dialog");
	}

	//-----------------------------------------------------------------------------------------------------

	public void UpdateButtons(){	//update button texts
		//Debug.Log ("UpdateButton start");
		button1Command = null;	//clear the command variables
		button2Command = null;
		button3Command = null;
		for(int row=0; row < commandArray.GetLength(0); row++){	//for each row in commandArray
			//Debug.Log ("For loop: row, SentCount: " + sentenceCounter.ToString());
			if (commandArray [row, 0].Equals (sentenceCounter.ToString ())) {	//column 0 sets the sentence in which the button should be displayed
				if (commandArray [row, 1].Equals ("1")) {	//column 1 sets which button to update
					button1.GetComponentInChildren<Text> ().text = commandArray [row, 2];	//column 2 sets what text to display on the button
					button1Command = commandArray[row, 3];	//store command
					button1.interactable = true;	//set button to be interactable
					button1Argument = null;	//make sure the argument is not set
					button1Argument2 = null;
					button1Argument = commandArray [row, 4];	//if there is add it as argument
					button1Argument2 = commandArray [row, 5];	//-||-
				}
				else if (commandArray [row, 1].Equals ("2")) {
					button2.GetComponentInChildren<Text> ().text = commandArray [row, 2];
					button2Command = commandArray[row, 3];
					button2.interactable = true;
					button2Argument = null;
					button2Argument2 = null;
					button2Argument = commandArray [row, 4];
					button2Argument2 = commandArray [row, 5];
				}
				else if (commandArray [row, 1].Equals ("3")) {
					button3.GetComponentInChildren<Text> ().text = commandArray [row, 2];
					button3Command = commandArray[row, 3];
					button3.interactable = true;
					button3Argument = null;
					button3Argument2 = null;
					button3Argument = commandArray [row, 4];
					button3Argument2 = commandArray [row, 5];
				}
				else {
					Debug.Log ("UpdateButton: button not found. value for button not valid(1, 2, 3) != " + commandArray [row, 1]);
				}
			}
		}
		if (button1Command == null) {	//if buttons command is not set after looping trough all commands
			button1.GetComponentInChildren<Text> ().text = "";	//clear the button text
			button1.interactable = false;	//set it not interactable
		}
		if (button2Command == null) {
			button2.GetComponentInChildren<Text> ().text = "";
			button2.interactable = false;
		}
		if (button3Command == null) {
			button3.GetComponentInChildren<Text> ().text = "";
			button3.interactable = false;
		}
	}
}
