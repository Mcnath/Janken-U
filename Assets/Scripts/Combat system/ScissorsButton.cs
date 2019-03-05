using System.Collections;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using UnityEngine;

public class ScissorsButton : MonoBehaviour {

	public void scissorsClick(){
		Debug.Log ("Choose Scissors");
		HandleTurn attack = new HandleTurn();
		attack.AttackType = HandleTurn.janken.SCISSORS;
	}
}
