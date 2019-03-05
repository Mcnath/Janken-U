using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class RockButton : MonoBehaviour {

	public void rockClick(){
		Debug.Log ("Choose Scissors");
		HandleTurn attack = new HandleTurn();
		attack.AttackType = HandleTurn.janken.ROCK;
	}
}
