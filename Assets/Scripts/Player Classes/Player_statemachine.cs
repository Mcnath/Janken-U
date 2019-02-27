using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_statemachine : MonoBehaviour {
	private Combat_statemachine CSM;
	public Player_base player;
	public enum turnState{
		START,
		CHOOSEACTION,
		WAITING,
		ACTION,
		LOSE
	}
	public enum leftHand_state{ IDLE, CHOSEN, INACTIVE}
	public enum rightHand_state{ IDLE, CHOSEN, INACTIVE}

	public turnState currentState;
	public leftHand_state pLFS;
	public rightHand_state pRHS;

	// Use this for initialization
	void Start () {
		currentState = turnState.START;
		pLFS = leftHand_state.IDLE;
		pRHS = rightHand_state.IDLE;
		CSM = GameObject.Find ("BattleManager").GetComponent<Combat_statemachine> ();
	}

	// Update is called once per frame
	void Update () {
		switch (currentState) {
		case(turnState.START):

			break;
		case(turnState.CHOOSEACTION):

			break;
		case(turnState.WAITING):

			break;
		case(turnState.ACTION):

			break;
		case(turnState.LOSE):

			break;
		}	
	}
}
