using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class PUN_Lobby : MonoBehaviourPunCallbacks
{

	public static PUN_Lobby lobby;
	public GameObject connectButton;
	public GameObject cancelButton;
	RoomInfo[] rooms;
	//public string userId;


	private void Awake()
	{
		lobby = this; // create singleton, lives with the menu scene
		//userId = "" + Random.Range(-1000000, 1000000);
	}

		// Use this for initialization
	void Start()
	{
		//PhotonNetwork.AuthValues = new AuthenticationValues(userId);
		//Debug.Log("userId:" + userId);
		PhotonNetwork.ConnectUsingSettings();//Connect to master server	
	}		

	public override void OnConnectedToMaster()
	{
		Debug.Log("Connected to master server");
		PhotonNetwork.AutomaticallySyncScene = true;
		connectButton.SetActive(true);
	}

	public void OnConnectedButtonClicked()
	{
		Debug.Log("connectButton clicked");
		connectButton.SetActive(false);
		cancelButton.SetActive(true);
		PhotonNetwork.JoinRandomRoom();//try to join random room
	} 

	public void OnCancelButtonClicked()
	{
		Debug.Log("cancelButton clicked");
		connectButton.SetActive(true);
		cancelButton.SetActive(false);
		PhotonNetwork.LeaveRoom();//leave the room
	}

	public override void OnJoinRandomFailed(short returnCode, string message)
	{

		Debug.Log("Failed to join a random room, no open games available");
		// #Critical: we failed to join a random room, maybe none exists or they are all full. No worries, we create a new room.
		CreateRoom();
	}

	public override void OnJoinedRoom()
	{
		base.OnJoinedRoom();
	}

	void CreateRoom()
	{
		int RandomRoomName = Random.Range(0, 10000);
		RoomOptions roomOps = new RoomOptions() { IsVisible = true, IsOpen = true, MaxPlayers = MultiplayerSetting.MS.maxPlayers};
		PhotonNetwork.CreateRoom("Room:" + RandomRoomName, roomOps);
	}

	public override void OnCreateRoomFailed(short returnCode, string message)
	{
		Debug.Log("Failed to create a new room, there must be another room with same name");
		CreateRoom(); // create room with different name
	}




}
