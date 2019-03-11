using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]

public class HandleTurn {

	public enum janken { ROCK, PAPER, SCISSORS }
	public string Attacker; //attack option
	public GameObject AttackGameObject;// for animation
	public GameObject AttackTarget;//choosen target for attack
	public janken AttackType;//chosen attack type
	//attack is performed


}
