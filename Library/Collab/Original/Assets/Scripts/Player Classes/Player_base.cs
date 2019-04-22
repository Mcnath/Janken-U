using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Player_base {
	// superclass for any player object
	public string name;
	private int killCount;// collectively collect all the kills done by player 
	public bool LeftHand_state;
	public bool RightHand_state;
    //testing skills only
    public enum pillars { ISTD, ESD, ASD, EPD };
    public pillars pillar = pillars.ESD;

}
