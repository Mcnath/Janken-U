using System.Collections;
using System.Collections.Generic;
using System.Threading;
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
	public enum leftHand_state{ ACTIVE, INACTIVE}
	public enum rightHand_state{ ACTIVE, INACTIVE}
	public Queue<string> leftIncoming = new Queue<string> ();
	public Queue<string> rightIncoming = new Queue<string> ();
	public turnState currentState;
	public leftHand_state pLHS;
	public rightHand_state pRHS;

	// Use this for initialization
	void Start () {
		currentState = turnState.START;
		pLHS = leftHand_state.ACTIVE;
		pRHS = rightHand_state.ACTIVE;
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
			//chooseAction ();
			currentState = turnState.WAITING;
			break;
		case(turnState.WAITING):
			// change to ACTION once all player have chosen a move\
			readyToBattle();
			for (int i = 0; i < 2 ; i++) {
				if (CSM.PerformList [i].AttackTarget == this) {
					incomingAttack(i);
				}
			}
			currentState = turnState.ACTION;
			break;
		case(turnState.ACTION): // idle
			if (CSM.currentState == Combat_statemachine.turnState.START) {
				currentState = turnState.START;
			}
			break;
		case(turnState.LOSE):
			
			break;
		}
		//state of player's left hand
		switch(pLHS){
		case(leftHand_state.ACTIVE): // alive

			break;
		case(leftHand_state.INACTIVE):

		break;
		}
		//state of player's right hand
		switch(pRHS){
		case(rightHand_state.ACTIVE):

			break;
		case(rightHand_state.INACTIVE):

			break;
		}
	}
	public void readyToBattle(){
		//Globals.executeAction.WaitOne(1000);
		CSM.PerformList.Add (CSM.playerChoice);
	}

	void incomingAttack(int index){
		Debug.Log("Taking: "+leftIncoming.Peek()+"and "+rightIncoming.Peek());
		leftIncoming.Enqueue(CSM.PerformList[index].LeftAttackType);
		rightIncoming.Enqueue(CSM.PerformList[index].RightAttackType);
	}
}
