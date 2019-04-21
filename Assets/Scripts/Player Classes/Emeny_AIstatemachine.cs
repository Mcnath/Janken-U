using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading;

public class Emeny_AIstatemachine : MonoBehaviour {
	public static Combat_statemachine CSM;
	public Player_base enemy;
	public HandleTurn myAttack;
    public int count;

    // Initialized Enemy's turn progression
    public enum turnState{
		START,
		CHOOSEACTION,
		WAITING,
		ACTION,
		LOSE
	}
	public turnState currentState;

	private Vector3 startPosition;

	// inintialized enemy state
	void Start () {
		currentState = turnState.START;
		enemy.LeftHand_state = true;
		enemy.RightHand_state = true;

        CSM = GameObject.Find("BattleManager").GetComponent<Combat_statemachine> ();
		startPosition = transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		switch (currentState) {
		case(turnState.START):
			//check if the enemy lost all the hands
			if (enemy.LeftHand_state == false && enemy.RightHand_state == false) {
				currentState = turnState.LOSE;
				
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

			int c = CSM.PerformList.Count;
			if (c <= 4) {
				currentState = turnState.ACTION;
			}
			break;
		case(turnState.ACTION): // idle
			//check if the enemy lost all the hands
			if (enemy.LeftHand_state == false && enemy.RightHand_state == false)
			{
				currentState = turnState.LOSE;
			}
			else if (CSM.currentState == Combat_statemachine.turnState.START)
			{
				myAttack = new HandleTurn();
				currentState = turnState.START;
			}
			break;
		case(turnState.LOSE):
			Debug.Log (enemy.name +" is destroyed");
			this.gameObject.SetActive(false);
			break;
		}
			
	}
	public void chooseAction(){
		//record action chosen by the AI
		myAttack.Attacker = enemy.name;
        myAttack.AttackGameObject = this.gameObject;
        myAttack.AttackTarget = CSM.PlayerInBattle[Random.Range(0, CSM.PlayerInBattle.Count)];
        while (myAttack.AttackTarget == myAttack.AttackGameObject)
        {
            myAttack.AttackTarget = CSM.PlayerInBattle[Random.Range(0, CSM.PlayerInBattle.Count)];
        }

        //myAttack.AttackTarget = CSM.PlayerInBattle[0];
        myAttack.LeftAttackType = HandleTurn.randomJanken();
        myAttack.RightAttackType = HandleTurn.randomJanken();
        //myAttack.skill = HandleTurn.randomSkill();
        Debug.Log(enemy.name + " is ready for battle");
        CSM.CollectActions(myAttack);
        count++;


        
		
	}

	public void isSelected(){
		// update player choice of Attack target

			Debug.Log (this.gameObject + " is selected");
			CSM.playerChoice.AttackTarget = this.gameObject; 
	}
}
