﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Emeny_AIstatemachine : MonoBehaviour {
	private Combat_statemachine CSM;
	public Player_base enemy;

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
	public leftHand_state eLFS;
	public rightHand_state eRHS;

	private Vector3 startPosition;

	// Use this for initialization
	void Start () {
		currentState = turnState.START;
		eLFS = leftHand_state.IDLE;
		eRHS = rightHand_state.IDLE;
		CSM = GameObject.Find ("BattleManager").GetComponent<Combat_statemachine> ();
		startPosition = transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		switch (currentState) {
		case(turnState.START):

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
		myAttack.Attack = enemy.name;
		myAttack.AttackGameObject = this.gameObject;
		myAttack.AttackTarget = CSM.PlayerInBattle[Random.Range(0, CSM.PlayerInBattle.Count)];
		CSM.CollectActions (myAttack);
	}
}