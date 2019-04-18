using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]

public class HandleTurn {

	public enum janken:int { ROCK = 0 , PAPER = 1, SCISSORS = 2, NONE = 3 };
	public string Attacker; //attack option
	public GameObject AttackGameObject;// for animation
	public GameObject AttackTarget;//choosen target for attack
    public GameObject SkillToUSe;
	public janken LeftAttackType = janken.NONE;//chosen attack type on Left Hand
	public janken RightAttackType = janken.NONE;//chosen attack type on the right hand
    public enum skills { Attack, AttackAll, Recover, DoubleAttack,Wall,SplashAttack,ExtraTurn,Sleep,MinRisk,DeniService};
    public skills skill = skills.Attack;
    //public enum GameObject { Attack, AttackAll, Recover, DoubleAttack}
    //public GameObject skill = GameObject.Attack;
    //attack is performed
    public static janken randomJanken(){
		return (janken)Random.Range(0, 2);
	}
    ///**************for skill testing only
}
