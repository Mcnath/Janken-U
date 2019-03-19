using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading;

public class Emeny_AIstatemachine : MonoBehaviour {
	private Combat_statemachine CSM;
	public Player_base enemy;

	// Initialized Enemy's turn progression
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
	public leftHand_state eLHS;
	public rightHand_state eRHS;

	private Vector3 startPosition;

	// inintialized enemy state
	void Start () {
		currentState = turnState.START;
		eLHS = leftHand_state.ACTIVE;
		eRHS = rightHand_state.ACTIVE;
		CSM = GameObject.Find("BattleManager").GetComponent<Combat_statemachine> ();
		startPosition = transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		switch (currentState) {
		case(turnState.START):
			if (eLHS == leftHand_state.INACTIVE && eRHS == rightHand_state.INACTIVE) {
				currentState = turnState.LOSE;
			}
			currentState = turnState.CHOOSEACTION;
			break;
		case(turnState.CHOOSEACTION):
			//chooseAction ();
			currentState = turnState.WAITING;
			break;
		case(turnState.WAITING):
			// change to ACTION once all player have chosen a move
			chooseAction();
			for (int i = 0; i < 2; i++) {
				if (CSM.PerformList [i].AttackTarget == this) {
					incomingAttack (i);
				}
			}
			currentState = turnState.ACTION;
			break;
		case(turnState.ACTION): // idle
			
			currentState = turnState.START;
			break;
		case(turnState.LOSE):

			break;
		}
	}

	public void chooseAction(){
		//record action chosen by the AI
		HandleTurn myAttack = new HandleTurn ();
		myAttack.Attacker = enemy.name;
		myAttack.AttackGameObject = this.gameObject;
		myAttack.AttackTarget = CSM.PlayerInBattle[Random.Range(0, CSM.PlayerInBattle.Count)];
		myAttack.LeftAttackType = myAttack.janken[Random.Range(0,2)];
		myAttack.RightAttackType = myAttack.janken[Random.Range(0,2)];
		//Globals.executeAction.WaitOne(1000);
		CSM.CollectActions (myAttack);
	}

	public void isSelected(){
		// update player choice of Attack target
		CSM.playerChoice.AttackTarget = this.gameObject;
	}
	void incomingAttack(int index){
		leftIncoming.Enqueue(CSM.PerformList[index].LeftAttackType);
		rightIncoming.Enqueue(CSM.PerformList[index].RightAttackType);
	}
}
