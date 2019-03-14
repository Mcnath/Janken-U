using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackAll : MonoBehaviour {
	private Combat_statemachine CSM;
	// Use this for initialization
	void Start () {
		
	}

	// Update is called once per frame
	void Update () {
		if (CSM.playerChoice.LeftAttackType == HandleTurn.janken.ROCK) {
			for (int i = 1; i < 3; i++) {
				if (CSM.PerformList [i].RightAttackType == HandleTurn.janken.SCISSORS) {
					//win
				} else if (CSM.PerformList [i].RightAttackType == HandleTurn.janken.PAPER) {
					//lose
				} else { 
					//draw
				}	
			}
		}
		if (CSM.playerChoice.LeftAttackType == HandleTurn.janken.PAPER) {
			for (int i = 1; i < 3; i++) {
				if (CSM.PerformList [i].RightAttackType == HandleTurn.janken.ROCK) {
					//win
				} else if (CSM.PerformList [i].RightAttackType == HandleTurn.janken.SCISSORS) {
					//lose
				} else { 
					//draw
				}	
			}
		}
		if (CSM.playerChoice.LeftAttackType == HandleTurn.janken.SCISSORS) {
			for (int i = 1; i < 3; i++) {
				if (CSM.PerformList [i].RightAttackType == HandleTurn.janken.PAPER) {
					//win
				} else if (CSM.PerformList [i].RightAttackType == HandleTurn.janken.ROCK) {
					//lose
				} else { 
					//draw
				}	
			}
		}
	}
}
