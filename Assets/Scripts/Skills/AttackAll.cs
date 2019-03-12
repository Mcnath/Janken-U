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
		if (CSM.playerChoice.AttackType == HandleTurn.janken.ROCK) {
			for (int i = 1; i < 3; i++) {
				if (CSM.PerformList [i].AttackType == HandleTurn.janken.SCISSORS) {
					//win
				} else if (CSM.PerformList [i].AttackType == HandleTurn.janken.PAPER) {
					//lose
				} else { 
					//draw
				}	
			}
		}
		if (CSM.playerChoice.AttackType == HandleTurn.janken.PAPER) {
			for (int i = 1; i < 3; i++) {
				if (CSM.PerformList [i].AttackType == HandleTurn.janken.ROCK) {
					//win
				} else if (CSM.PerformList [i].AttackType == HandleTurn.janken.SCISSORS) {
					//lose
				} else { 
					//draw
				}	
			}
		}
		if (CSM.playerChoice.AttackType == HandleTurn.janken.SCISSORS) {
			for (int i = 1; i < 3; i++) {
				if (CSM.PerformList [i].AttackType == HandleTurn.janken.PAPER) {
					//win
				} else if (CSM.PerformList [i].AttackType == HandleTurn.janken.ROCK) {
					//lose
				} else { 
					//draw
				}	
			}
		}
	}
}
