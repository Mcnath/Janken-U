using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Combat_statemachine : MonoBehaviour {

	public Player_base player;
	public Enemy_base AI_1;
	public Enemy_base AI_2;
	public Enemy_base AI_3;

	public enum battleState{
		START,
		PLAYERCHOICE,
		ENEMYCHOICE,
		LOSE,
		WIN
	}

	public enum janken{ ROCK, PAPER, SCISSORS}

	private battleState currentState;
	private Player_base.leftHand_state playerLeftState;
	private Player_base.rightHand_state playerRightState;

	// Use this for initialization
	void Start () {
		currentState = battleState.START;
		playerLeftState = Player_base.leftHand_state.IDLE;
		playerRightState = Player_base.rightHand_state.IDLE;

	}
	
	// Update is called once per frame
	void Update () {
		Debug.Log (currentState);
		switch(currentState){
		case(battleState.START):
			//currentState = battleState.PLAYERCHOICE;
		case(battleState.PLAYERCHOICE):
			if(playerLeftState == Player_base.leftHand_state.CHOSEN && playerRightState == Player_base.rightHand_state.CHOSEN){
				currentState = battleState.ENEMYCHOICE;
			}
			else if(playerLeftState == Player_base.leftHand_state.INACTIVE && playerRightState == Player_base.rightHand_state.INACTIVE){
				currentState = battleState.LOSE;
			}
			break;
		case(battleState.ENEMYCHOICE):
			
			break;
		case(battleState.LOSE):
			break;
		case(battleState.WIN):
			break;
		}
	}

	void choose(){
		
	}
}

