using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Freshmores {

	private string CharacterPillarName;
	private string PillarDescription;

	//stat
	private int priority;
	private int endurance;
	private int range;

	public string chara_pillarName {
		get{ return CharacterPillarName; }
		set{ CharacterPillarName = value; }
	}

	public string chara_describePillar {
		get{ return PillarDescription; }
		set{ PillarDescription = value; }
	}

	public int chara_priority {
		get{ return priority; }
		set{ priority = 0; }
	}

	public int chara_endurance {
		get{ return endurance; }
		set{ endurance = 0; }
	}

	public int chara_range {
		get{ return range; }
		set{ range = 0; }
	}


}
