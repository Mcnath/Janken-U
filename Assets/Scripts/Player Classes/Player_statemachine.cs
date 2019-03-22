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
	public List<string> leftIncoming = new List<string> ();
	public List<string> rightIncoming = new List<string> ();
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
			//check if the player lost all the hands
			if (pLHS == leftHand_state.INACTIVE && pRHS == rightHand_state.INACTIVE) {
				currentState = turnState.LOSE;
			} 
			currentState = turnState.CHOOSEACTION;
			break;
		case(turnState.CHOOSEACTION):
			if (CSM.playerChoice.LeftAttackType != null 
				&& CSM.playerChoice.LeftAttackType.Length != 0 
				&& CSM.playerChoice.RightAttackType != null
				&& CSM.playerChoice.RightAttackType.Length != 0 
				&& CSM.playerChoice.AttackTarget != null) {
				readyToBattle();
				currentState = turnState.WAITING;
			}
			else {
			//Debug.Log ("Complete your actions!!");
			}
			break;
		case(turnState.WAITING):
			// change to ACTION once all player have chosen a move\
			int attackCount = 0;
			for (int i = 1; i < CSM.PlayerInBattle.Count ; i++) {
				if (CSM.PerformList [i].AttackTarget == this) {
					incomingAttack(attackCount);
					attackCount++;
				}
			}
			currentState = turnState.ACTION;
			break;
		case(turnState.ACTION): // idle
			if (CSM.currentState == Combat_statemachine.turnState.START) {
				Debug.Log("choosing");
				currentState = turnState.START;
			}
			break;
		case(turnState.LOSE):
			Debug.Log ("You Lose");
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
		Debug.Log (this.gameObject.name + " is ready for battle");
		CSM.playerChoice.Attacker = this.gameObject.name;
		CSM.playerChoice.AttackGameObject = this.gameObject;
		CSM.PerformList.Add (CSM.playerChoice);
	}

	void incomingAttack(int index){
		leftIncoming.Add(CSM.PerformList[index].LeftAttackType);
		rightIncoming.Add(CSM.PerformList[index].RightAttackType);
		Debug.Log("Taking: "+leftIncoming[index]+"and "+rightIncoming[index]);
	}
}
