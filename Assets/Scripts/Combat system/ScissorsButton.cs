using System.Collections;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using UnityEngine;

public class ScissorsButton : MonoBehaviour {
	private Combat_statemachine CSM;
	//click scissors button
	public void scissorsClick(){
		Debug.Log ("Choose Scissors");
		CSM.playerChoice.AttackType = HandleTurn.janken.SCISSORS;
	}
}
