using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Combat_statemachine : MonoBehaviour {

	public enum battleState
	{
		START,
		PLAYERCHOICE,
		ENEMYCHOICE,
		LOSE,
		WIN
	}

	private battleState currentState;


	// Use this for initialization
	void Start () {
		currentState = battleState.START;

	}
	
	// Update is called once per frame
	void Update () {
		Debug.Log (currentState);
		switch(currentState){
		case(battleState.START):
			//setup battle function
			break;
		case(battleState.PLAYERCHOICE):
			break;
		case(battleState.ENEMYCHOICE):
			break;
		case(battleState.LOSE):
			break;
		case(battleState.WIN):
			break;
		}
	}
	void onGUI(){
		if (GUILayout.Button ("NEXT STATE")) {
			{
				currentState = (battleState)(((int)currentState + 1) % 5);
			}
		}
	}
}

