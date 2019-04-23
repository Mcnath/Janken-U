using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class PUN_Player : MonoBehaviour
{
	private PhotonView PV;
	public GameObject myAvatar;

    // Start is called before the first frame update
    void Start()
    {
		PV = GetComponent<PhotonView>();
		int spawnPicker = Random.Range(0, GameSetup.GS.spawnPoints.Length);
		if (PV.IsMine)
		{
			PhotonNetwork.Instantiate(Path.Combine("PhotonPrefab","PlayerAvatar"),GameSetup.GS.spawnPoints[spawnPicker].position, GameSetup.GS.spawnPoints[spawnPicker].rotation, 0);
		}
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
