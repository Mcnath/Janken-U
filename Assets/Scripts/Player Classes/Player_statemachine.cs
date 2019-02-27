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
			currentState = turnState.CHOOSEACTION;
			break;
		case(turnState.CHOOSEACTION):
			chooseAction ();
			currentState = turnState.WAITING;
			break;
		case(turnState.WAITING): // idle

			break;
		case(turnState.ACTION):

			break;
		case(turnState.LOSE):

			break;
		}
	}

	void chooseAction(){
		HandleTurn myAttack = new HandleTurn ();
		myAttack.Attacker = player.name;
		myAttack.AttackGameObject = this.gameObject;
		myAttack.AttackTarget = CSM.PlayerInBattle[Random.Range(0, CSM.PlayerInBattle.Count)];
		CSM.CollectActions (myAttack);
	}
}
