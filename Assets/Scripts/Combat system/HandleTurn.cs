using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]

public class HandleTurn {

	public enum janken { ROCK, PAPER, SCISSORS }
	//randomizing the enum
	public janken RandomChoice<janken>(){
		System.Array A = System.Enum.GetValues(typeof(janken));
		janken V = (janken)A.GetValue(UnityEngine.Random.Range(0,A.Length));
		return V;
	}
	public string Attacker; //attack option
	public GameObject AttackGameObject;// for animation
	public GameObject AttackTarget;//choosen target for attack
	public janken LeftAttackType;//chosen attack type on Left Hand
	public janken RightAttackType;//chosen attack type on Left Hand
	//attack is performed


}
