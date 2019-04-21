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
    public int count=0;

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
                    //Extraturn;

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
			if (player.LeftHand_state == false && player.RightHand_state == false)//check if the player lost all the hands
				{
				currentState = turnState.LOSE;
			}
			else if (CSM.currentState == Combat_statemachine.turnState.START) {
				currentState = turnState.START;
			}
			break;
		case(turnState.LOSE):
			Debug.Log ("You Lose");
				//Application.Quit();
			break;
		}
	}
	public void readyToBattle(){
		//Globals.executeAction.WaitOne(1000);
		Debug.Log (this.gameObject.name + " is ready for battle");
		CSM.playerChoice.Attacker = this.gameObject.name;
		CSM.playerChoice.AttackGameObject = this.gameObject;
        switch (CSM.playerChoice.skill)
        {
            case HandleTurn.skills.DoubleAttack:
                CSM.PerformList.Add(CSM.playerChoice);
                CSM.PerformList.Add(CSM.playerChoice);
                break;
            case HandleTurn.skills.AttackAll:
                CSM.playerChoice.AttackTarget = GameObject.Find("Enemy 1");
                CSM.PerformList.Add(CSM.playerChoice);
                CSM.playerChoice.AttackTarget = GameObject.Find("Enemy 2");
                CSM.PerformList.Add(CSM.playerChoice);
                CSM.playerChoice.AttackTarget = GameObject.Find("Enemy 3");
                CSM.PerformList.Add(CSM.playerChoice);
                break;
            case HandleTurn.skills.SplashAttack:
                if(CSM.playerChoice.AttackTarget == GameObject.Find("Enemy 1"))
                {
                    CSM.PerformList.Add(CSM.playerChoice);
                    CSM.playerChoice.AttackTarget = GameObject.Find("Enemy 2");
                    CSM.PerformList.Add(CSM.playerChoice);
                }
                else if (CSM.playerChoice.AttackTarget == GameObject.Find("Enemy 2"))
                {
                    CSM.PerformList.Add(CSM.playerChoice);
                    CSM.playerChoice.AttackTarget = GameObject.Find("Enemy 3");
                    CSM.PerformList.Add(CSM.playerChoice);
                }
                else if (CSM.playerChoice.AttackTarget == GameObject.Find("Enemy 3"))
                {
                    CSM.PerformList.Add(CSM.playerChoice);
                    CSM.playerChoice.AttackTarget = GameObject.Find("Enemy 1");
                    CSM.PerformList.Add(CSM.playerChoice);
                }
                break;

            //CSM.PerformList.Add(CSM.playerChoice);
            case HandleTurn.skills.Wall:
                CSM.PerformList.Add(CSM.playerChoice);
                break;
            case HandleTurn.skills.MinRisk:
                CSM.PerformList.Add(CSM.playerChoice);
                break;
            case HandleTurn.skills.Recover:
                //CSM.PerformList.Add(CSM.playerChoice);
                break;
            case HandleTurn.skills.ExtraTurn:
                CSM.PerformList.Add(CSM.playerChoice);
                break;
            case HandleTurn.skills.Sleep:
                break;
            case HandleTurn.skills.DeniService:
                CSM.PerformList.Add(CSM.playerChoice);
                break;
            case HandleTurn.skills.Attack:
                CSM.PerformList.Add(CSM.playerChoice);
                break;

        }
    }

}
