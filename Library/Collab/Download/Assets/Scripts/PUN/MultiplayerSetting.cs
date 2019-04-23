using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultiplayerSetting : MonoBehaviour {

	public static MultiplayerSetting MS;
	public bool delayStart;
	public int maxPlayers;
	public int menuScene;
	public int multiplayerScene;

	private void Awake(){
		if (MultiplayerSetting.MS == null)
		{
			MultiplayerSetting.MS = this;
		}
		else
		{
			if (MultiplayerSetting.MS == this)
			{
				Destroy(this.gameObject);
			}
		}
		DontDestroyOnLoad(this.gameObject);
	}
}
