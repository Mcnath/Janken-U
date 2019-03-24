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
	public List<string> leftIncoming = new List<string> ();
	public List<string> rightIncoming = new List<string> ();
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
			//check if the enemy lost all the hands
			if (eLHS == leftHand_state.INACTIVE && eRHS == rightHand_state.INACTIVE) {
				currentState = turnState.LOSE;
				this.gameObject.SetActive (false);
			} else {
				currentState = turnState.CHOOSEACTION;
			}
			break;
		case(turnState.CHOOSEACTION):
			if (CSM.PSM.currentState == Player_statemachine.turnState.WAITING) {
				chooseAction ();
				currentState = turnState.WAITING;
			}
			break;
		case(turnState.WAITING):
			// change to ACTION once all player have chosen a move
			int attackCount = 0;
			for (int i = 1; i < CSM.PerformList.Count; i++) {
				if (CSM.PerformList [i].AttackTarget == this.gameObject) {
					incomingAttack (attackCount);
					attackCount++;
				}
			}
			currentState = turnState.ACTION;
			break;
		case(turnState.ACTION): // idle
			
			currentState = turnState.START;
			break;
		case(turnState.LOSE):
			Debug.Log (enemy.name +" is destroyed");
			break;
		}
			
	}

	public void chooseAction(){
		//record action chosen by the AI
		HandleTurn myAttack = new HandleTurn ();
		myAttack.Attacker = enemy.name;
		myAttack.AttackGameObject = gameObject;
		myAttack.AttackTarget = CSM.PlayerInBattle [Random.Range (0, CSM.PlayerInBattle.Count)];
		while (myAttack.AttackTarget == myAttack.AttackGameObject) {
			myAttack.AttackTarget = CSM.PlayerInBattle [Random.Range (0, CSM.PlayerInBattle.Count)];
		}
		//myAttack.AttackTarget = CSM.PlayerInBattle[0];
		myAttack.LeftAttackType = myAttack.janken[Random.Range(0,2)];
		myAttack.RightAttackType = myAttack.janken[Random.Range(0,2)];
		Debug.Log(enemy.name + " is ready for battle");
		CSM.CollectActions (myAttack);
	}

	public void isSelected(){
		// update player choice of Attack target

			Debug.Log (this.gameObject + " is selected");
			CSM.playerChoice.AttackTarget = this.gameObject; 
	}
	void incomingAttack(int index){
		leftIncoming.Add(CSM.PerformList[index].LeftAttackType);
		rightIncoming.Add(CSM.PerformList[index].RightAttackType);
	}
}
