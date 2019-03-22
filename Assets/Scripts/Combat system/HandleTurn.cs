using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]

public class HandleTurn {

	public string[] janken =  { "ROCK", "PAPER", "SCISSORS" };
	public string Attacker; //attack option
	public GameObject AttackGameObject;// for animation
	public GameObject AttackTarget;//choosen target for attack
	public string LeftAttackType;//chosen attack type on Left Hand
	public string RightAttackType;//chosen attack type on Left Hand
	//attack is performed


}
