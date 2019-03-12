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
		LOSE
	}
	public enum leftHand_state{ IDLE, CHOSEN, INACTIVE}
	public enum rightHand_state{ IDLE, CHOSEN, INACTIVE}

	public turnState currentState;
	public leftHand_state pLHS;
	public rightHand_state pRHS;

	// Use this for initialization
	void Start () {
		currentState = turnState.START;
		pLHS = leftHand_state.IDLE;
		pRHS = rightHand_state.IDLE;
		CSM = GameObject.Find ("BattleManager").GetComponent<Combat_statemachine> ();
	}

	// Update is called once per frame
	void Update () {
		switch (currentState) {
		case(turnState.START):
			if (pLHS == leftHand_state.INACTIVE && pRHS == rightHand_state.INACTIVE) {
				currentState = turnState.LOSE;
			}
			currentState = turnState.CHOOSEACTION;
			break;
		case(turnState.CHOOSEACTION):
			chooseAction ();
			currentState = turnState.WAITING;
			break;
		case(turnState.WAITING): // idle
			currentState = turnState.START;
			break;
		case(turnState.LOSE):
			
			break;
		}
		//state of player's left hand
		switch(pLHS){
		case(leftHand_state.IDLE): // idle

			break;
		case(leftHand_state.CHOSEN):

			break;
		case(leftHand_state.INACTIVE):

		break;
		}
		//state of player's right hand
		switch(pRHS){
		case(rightHand_state.IDLE): // idle

			break;
		case(rightHand_state.CHOSEN):

			break;
		case(rightHand_state.INACTIVE):

			break;
		}
	}

	void chooseAction(){
		// record player action(will be updated)
		HandleTurn myAttack = new HandleTurn ();
		myAttack.Attacker = player.name;
		myAttack.AttackGameObject = this.gameObject;
		myAttack.AttackTarget = CSM.PlayerInBattle[Random.Range(0, CSM.PlayerInBattle.Count)];
		CSM.CollectActions (myAttack);
	}
}
