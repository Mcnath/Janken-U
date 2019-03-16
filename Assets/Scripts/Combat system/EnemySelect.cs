using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySelect : MonoBehaviour {

	// enemy selection
	private Combat_statemachine CSM;

	public void enemySelected(){
		// update player choice of Attack target
		CSM.playerChoice.AttackTarget = this.gameObject;
	}
}
