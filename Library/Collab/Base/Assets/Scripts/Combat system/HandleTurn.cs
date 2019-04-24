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
    public enum skills:int { Attack=0, AttackAll=1, Recover=2, DoubleAttack=3,Wall=4,SplashAttack=5,ExtraTurn=6,Sleep=7,MinRisk=8,DeniService=9};
    public skills skill = skills.Attack;
    //public enum GameObject { Attack, AttackAll, Recover, DoubleAttack}
    //public GameObject skill = GameObject.Attack;
    //attack is performed
    public static janken randomJanken(){
		return (janken)Random.Range(0, 2);
	}
    ///**************for skill testing only
    public static skills randomSkill()
    {
        return (skills)Random.Range(0, 9);
    }
}
