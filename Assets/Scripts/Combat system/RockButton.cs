using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class RockButton : MonoBehaviour {
	private Combat_statemachine CSM;
	// click rock button
	public void rockClick(){
		Debug.Log ("Choose Rock");
		HandleTurn attack = new HandleTurn();
		CSM.playerChoice.AttackType = HandleTurn.janken.ROCK;
	}
}
