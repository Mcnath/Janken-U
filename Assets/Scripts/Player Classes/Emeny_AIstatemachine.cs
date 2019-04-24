using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading;

public class Emeny_AIstatemachine : MonoBehaviour
{
	public static Combat_statemachine CSM;
	public Player_base enemy;
	public HandleTurn myAttack;
	public Animator ani;

	// Initialized Enemy's turn progression
	public enum turnState
	{
		START,
		CHOOSEACTION,
		WAITING,
		ACTION,
		LOSE
	}
	public turnState currentState;

	private Vector3 startPosition;

	// inintialized enemy state
	void Start()
	{
		ani.GetComponent<Animator>();

		currentState = turnState.START;
		enemy.LeftHand_state = true;
		enemy.RightHand_state = true;
		ani.SetBool("lefthand", true);
		ani.SetBool("righthand", true);
		CSM = GameObject.Find("BattleManager").GetComponent<Combat_statemachine>();
		startPosition = transform.position;
	}

	// Update is called once per frame
	void Update()
	{
		switch (currentState)
		{
			case (turnState.START):
				//check if the enemy lost all the hands
				if (enemy.LeftHand_state == false && enemy.RightHand_state == false)
				{
					currentState = turnState.LOSE;
					;

				}
				else
				{
					currentState = turnState.CHOOSEACTION;
				}
				break;
			case (turnState.CHOOSEACTION):
				if (CSM.PSM.currentState == Player_statemachine.turnState.WAITING)
				{
					chooseAction();
					currentState = turnState.WAITING;
				}
				break;
			case (turnState.WAITING):
				// change to ACTION once all player have chosen a move
				int c = CSM.PerformList.Count;
				if (c <= 4)
				{
					currentState = turnState.ACTION;
				}
				break;
			case (turnState.ACTION): // idle
									 //check if the enemy lost all the hands
				if (enemy.name == "e1")
				{
					ani.SetBool("lr", false);
					ani.SetBool("lp", false);
					ani.SetBool("ls", false);
					ani.SetBool("rr", false);
					ani.SetBool("rp", false);
					ani.SetBool("rs", false);
					if (enemy.LeftHand_state == true && enemy.RightHand_state == false)
					{
						ani.SetBool("righthand", false);
						ani.SetTrigger("losingright");
						ani.SetBool("1hand", true);
					}
					if (enemy.LeftHand_state == false && enemy.RightHand_state == true)
					{
						ani.SetBool("lefthand", false);
						ani.SetTrigger("losingleft");
						ani.SetBool("1hand", true);
					}
				}
				else if (enemy.name == "e2")
				{
					ani.SetBool("lr", false);
					ani.SetBool("lp", false);
					ani.SetBool("ls", false);
					ani.SetBool("rr", false);
					ani.SetBool("rp", false);
					ani.SetBool("rs", false);
					if (enemy.LeftHand_state == true && enemy.RightHand_state == false)
					{
						ani.SetBool("righthand", false);
						ani.SetTrigger("losingright");
						ani.SetBool("1hand", true);
					}
					if (enemy.LeftHand_state == false && enemy.RightHand_state == true)
					{
						ani.SetBool("lefthand", false);
						ani.SetTrigger("losingleft");
						ani.SetBool("1hand", true);
					}
				}
				else if (enemy.name == "e3")
				{
					ani.SetBool("lr", false);
					ani.SetBool("lp", false);
					ani.SetBool("ls", false);
					ani.SetBool("rr", false);
					ani.SetBool("rp", false);
					ani.SetBool("rs", false);
					if (enemy.LeftHand_state == true && enemy.RightHand_state == false)
					{
						ani.SetBool("righthand", false);
						ani.SetTrigger("losingright");
						ani.SetBool("1hand", true);
					}
					if (enemy.LeftHand_state == false && enemy.RightHand_state == true)
					{
						ani.SetBool("lefthand", false);
						ani.SetTrigger("losingleft");
						ani.SetBool("1hand", true);
					}
				}
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
			case (turnState.LOSE):
				if (enemy.name == "e3")
				{
					ani.SetBool("lefthand", false);
					ani.SetBool("righthand", false);
					ani.SetTrigger("death");
				}
				else if (enemy.name == "e1")
				{
					ani.SetBool("lefthand", false);
					ani.SetBool("righthand", false);
					ani.SetTrigger("death");
				}
				else if (enemy.name == "e2")
				{
					ani.SetBool("lefthand", false);
					ani.SetBool("righthand", false);
					ani.SetTrigger("death");
				}

				Debug.Log(enemy.name + " is destroyed");
				//this.gameObject.SetActive(false);
				break;
		}

	}
	public void chooseAction()
	{
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
		if (enemy.name == "e3")
		{

			if (myAttack.LeftAttackType == HandleTurn.janken.ROCK)
			{
				ani.SetBool("lr", true);
			}
			else if (myAttack.LeftAttackType == HandleTurn.janken.SCISSORS)
			{
				ani.SetBool("ls", true);
			}
			else if (myAttack.LeftAttackType == HandleTurn.janken.PAPER)
			{
				ani.SetBool("lp", true);
			}
			if (myAttack.RightAttackType == HandleTurn.janken.ROCK)
			{
				ani.SetBool("rr", true);
			}
			else if (myAttack.RightAttackType == HandleTurn.janken.SCISSORS)
			{
				ani.SetBool("rs", true);
			}
			else if (myAttack.RightAttackType == HandleTurn.janken.PAPER)
			{
				ani.SetBool("rp", true);
			}
		}
		else if (enemy.name == "e1")
		{

			if (myAttack.LeftAttackType == HandleTurn.janken.ROCK)
			{
				ani.SetBool("lr", true);
			}
			else if (myAttack.LeftAttackType == HandleTurn.janken.SCISSORS)
			{
				ani.SetBool("ls", true);
			}
			else if (myAttack.LeftAttackType == HandleTurn.janken.PAPER)
			{
				ani.SetBool("lp", true);
			}
			if (myAttack.RightAttackType == HandleTurn.janken.ROCK)
			{
				ani.SetBool("rr", true);
			}
			else if (myAttack.RightAttackType == HandleTurn.janken.SCISSORS)
			{
				ani.SetBool("rs", true);
			}
			else if (myAttack.RightAttackType == HandleTurn.janken.PAPER)
			{
				ani.SetBool("rp", true);
			}
		}
		else if (enemy.name == "e2")
		{

			if (myAttack.LeftAttackType == HandleTurn.janken.ROCK)
			{
				ani.SetBool("lr", true);
			}
			else if (myAttack.LeftAttackType == HandleTurn.janken.SCISSORS)
			{
				ani.SetBool("ls", true);
			}
			else if (myAttack.LeftAttackType == HandleTurn.janken.PAPER)
			{
				ani.SetBool("lp", true);
			}
			if (myAttack.RightAttackType == HandleTurn.janken.ROCK)
			{
				ani.SetBool("rr", true);
			}
			else if (myAttack.RightAttackType == HandleTurn.janken.SCISSORS)
			{
				ani.SetBool("rs", true);
			}
			else if (myAttack.RightAttackType == HandleTurn.janken.PAPER)
			{
				ani.SetBool("rp", true);
			}
		}
		Debug.Log(enemy.name + " is ready for battle");
		CSM.CollectActions(myAttack);


	}

	public void isSelected()
	{
		// update player choice of Attack target

		Debug.Log(this.gameObject + " is selected");
		CSM.playerChoice.AttackTarget = this.gameObject;
	}
}