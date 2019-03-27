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
	public turnState currentState;


	// Use this for initialization
	void Start () {
		currentState = turnState.START;
		player.LeftHand_state = true;
		player.RightHand_state = true;
		CSM = GameObject.Find ("BattleManager").GetComponent<Combat_statemachine> ();
	}

	// Update is called once per frame
	void Update () {
		switch (currentState) {
		case(turnState.START):
			//check if the player lost all the hands
			if (player.LeftHand_state == false && player.RightHand_state == false) {
				currentState = turnState.LOSE;
			} else {
				currentState = turnState.CHOOSEACTION;
			}
			break;
		case(turnState.CHOOSEACTION):
			if (CSM.playerChoice.AttackTarget != null && CSM.playerChoice.LeftAttackType != HandleTurn.janken.NONE 
				&& CSM.playerChoice.RightAttackType != HandleTurn.janken.NONE) {
				readyToBattle();
				currentState = turnState.WAITING;
			}
			else {
			//Debug.Log ("Complete your actions!!");
			}
			break;
		case(turnState.WAITING):
			// change to ACTION once all player have chosen a move
			if (CSM.PerformList.Count <= 4) {
				currentState = turnState.ACTION;
			}
			break;
		case(turnState.ACTION): // idle
			if (CSM.currentState == Combat_statemachine.turnState.START) {
				currentState = turnState.START;
			}
			break;
		case(turnState.LOSE):
			Debug.Log ("You Lose");
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

}
