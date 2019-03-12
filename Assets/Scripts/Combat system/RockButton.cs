using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockButton : MonoBehaviour {
	private Combat_statemachine CSM;
	public void chooseRock(){
		Debug.Log ("Chosen Rock");
		CSM.playerChoice.AttackType = HandleTurn.janken.ROCK;
	}
}
