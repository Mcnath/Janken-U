using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Combat_statemachine : MonoBehaviour {

	public Player_base player;
	public Enemy_base AI_1;
	public Enemy_base AI_2;
	public Enemy_base AI_3;
	private int seconds_current;
	private int seconds_max;

	public enum turnState{
		START,
		PLAYERCHOICE,
		ENEMYCHOICE,
		LOSE,
		WIN
	}

	public enum janken{ ROCK, PAPER, SCISSORS}

	private turnState currentState;
	private Player_base.leftHand_state playerLeftState;
	private Player_base.rightHand_state playerRightState;

	// Use this for initialization
	void Start () {
		currentState = turnState.START;
		playerLeftState = Player_base.leftHand_state.IDLE;
		playerRightState = Player_base.rightHand_state.IDLE;

	}
	
	// Update is called once per frame
	void Update () {
		Debug.Log (currentState);
		switch(currentState){
		case(turnState.START):
			currentState == turnState.PLAYERCHOICE;
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
}

