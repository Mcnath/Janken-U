using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class PaperButton : MonoBehaviour {

	public void paperClick(){
		Debug.Log ("Choose Scissors");
		HandleTurn attack = new HandleTurn();
		attack.AttackType = HandleTurn.janken.PAPER;
	}
}
