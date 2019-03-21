using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]

public class HandleTurn {

	public string[] janken =  { "ROCK", "PAPER", "SCISSORS" };
	public string Attacker; //attack option
	public GameObject AttackGameObject;// for animation
	[System.NonSerialized] public GameObject AttackTarget;//choosen target for attack
	[System.NonSerialized] public string LeftAttackType;//chosen attack type on Left Hand
	[System.NonSerialized] public string RightAttackType;//chosen attack type on Left Hand
	//attack is performed


}
