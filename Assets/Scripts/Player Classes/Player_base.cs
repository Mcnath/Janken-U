using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Player_base {

	public string name;
	public enum leftHand_state{ IDLE, CHOSEN, INACTIVE}
	public enum rightHand_state{ IDLE, CHOSEN, INACTIVE}
	private int killCount;


}
