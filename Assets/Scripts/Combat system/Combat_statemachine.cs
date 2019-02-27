using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Combat_statemachine : MonoBehaviour {

	//public Player_base player;
	//public Enemy_base AI_1;
	//public Enemy_base AI_2;
	//public Enemy_base AI_3;
	//private int seconds_current;
	//private int seconds_max;

	public enum turnState{
		START,
		PLAYERCHOICE,
		ENEMYCHOICE,
		LOSE,
		WIN
	}
	public turnState currentState;

	public List<HandleTurn> PerformList = new List<HandleTurn> ();
	public List<GameObject> PlayerInBattle = new List<GameObject>();

	public enum janken{ ROCK, PAPER, SCISSORS}


	private Player_base.leftHand_state playerLeftState;
	private Player_base.rightHand_state playerRightState;

	//initialization
	void Start () {
		currentState = turnState.START;
		PlayerInBattle.AddRange (GameObject.FindGameObjectsWithTag("Player"));
		PlayerInBattle.AddRange (GameObject.FindGameObjectsWithTag("AI"));
		playerLeftState = Player_base.leftHand_state.IDLE;
		playerRightState = Player_base.rightHand_state.IDLE;

	}
	
	// Update is called once per frame
	void Update () {
		Debug.Log (currentState);
		switch(currentState){
		case(turnState.START):
			
			break;
		case(turnState.PLAYERCHOICE):
			if(playerLeftState == Player_base.leftHand_state.CHOSEN && playerRightState == Player_base.rightHand_state.CHOSEN){
				currentState = turnState.ENEMYCHOICE;
			}
			else if(playerLeftState == Player_base.leftHand_state.INACTIVE && playerRightState == Player_base.rightHand_state.INACTIVE){
				currentState = turnState.LOSE;
			}
			break;
		case(turnState.ENEMYCHOICE):
			
			break;
		case(turnState.LOSE):
			break;
		case(turnState.WIN):
			break;
		}
	}

	void timerPlayer(){
		
	}

	public void CollectActions(HandleTurn input){
		PerformList.Add (input);
	}
}

