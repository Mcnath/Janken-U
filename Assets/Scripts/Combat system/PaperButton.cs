using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class PaperButton : MonoBehaviour {
	private Combat_statemachine CSM;
	//click paper button
	public void paperClick(){
		Debug.Log ("Choose Paper");
		HandleTurn attack = new HandleTurn();
		CSM.playerChoice.AttackType = HandleTurn.janken.PAPER;
	}
}
