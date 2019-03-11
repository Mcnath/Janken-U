using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySelect : MonoBehaviour {

	// enemy selection
	public GameObject EnemyPreFab;

	public void SelectEnemy(){
		GameObject.Find ("BattleManager").GetComponents<Combat_statemachine>(); // save input of enemy prefab
	}
}
