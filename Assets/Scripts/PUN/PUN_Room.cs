using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Pun;
using Photon.Realtime;


public class PUN_Room : MonoBehaviourPunCallbacks, IInRoomCallbacks {

	//Room Info
	public static PUN_Room room;
	private PhotonView PV;

	public bool isGameLoaded;
	public int currentScene;
	public int multiplayerScene;

	//Player info
	Player[] photonPlayers;
	public int playersInRoom;
	public int myNumberInRoom;
	public int playersInGame;

	//Delayed Start
	private bool readyToCount;
	private bool readyToStart;
	public float startingTime;
	private float lessThanMaxPlayers;
	private float atMaxPlayers;
	private float timeToStart;

	private void Awake()
	{
		//set up singleton
		if(PUN_Room.room == null)
		{
			PUN_Room.room = this;
		}
		else
		{
			if(PUN_Room.room == this)
			{
				Destroy(PUN_Room.room.gameObject);
				PUN_Room.room = this;
			}
		}
		DontDestroyOnLoad(this.gameObject);
		PV = GetComponent<PhotonView>();
	}

	public override void OnEnable()
	{
		//subscribe to functions
		base.OnEnable();
		PhotonNetwork.AddCallbackTarget(this);
		SceneManager.sceneLoaded += OnSceneFinishedLoading;
		//PhotonNetwork.CurrentRoom.IsOpen = false;
	}

	public override void OnDisable()
	{
		base.OnDisable();
		PhotonNetwork.RemoveCallbackTarget(this);
		SceneManager.sceneLoaded -= OnSceneFinishedLoading;
	}

	// Use this for initialization
	void Start () {
		//set private variables
		PV = GetComponent<PhotonView>();
		readyToCount = false;
		readyToStart = false;
		lessThanMaxPlayers = startingTime;
		atMaxPlayers = 6;
		timeToStart = startingTime;
	}
	
	// Update is called once per frame
	void Update () {
		//for delay start only, countdown to start
		if (MultiplayerSetting.MS.delayStart)
		{
			if (playersInRoom == 1)
			{
				RestartTimer();
			}
			if (!isGameLoaded)
			{
				if (readyToStart)
				{
					atMaxPlayers -= Time.deltaTime;
					lessThanMaxPlayers = atMaxPlayers;
					timeToStart = atMaxPlayers;
				}
				else
				{
					lessThanMaxPlayers -= Time.deltaTime;
					timeToStart = lessThanMaxPlayers;
				}
				Debug.Log("Display time to start to the players" + timeToStart);
				if (timeToStart <= 0)
				{
					StartGame();
				}
			}
		}
	}

	public override void OnJoinedRoom()
	{
		//set player data when we join the room
		base.OnJoinedRoom();
		Debug.Log("We are now in a room");
		photonPlayers = PhotonNetwork.PlayerList;
		playersInRoom = photonPlayers.Length;
		myNumberInRoom = playersInRoom;
		PhotonNetwork.NickName = myNumberInRoom.ToString();
		//for delay start
		if (MultiplayerSetting.MS.delayStart)
		{
			Debug.Log("Displayer players in room out of max players possible (" + playersInRoom + ":" + MultiplayerSetting.MS.maxPlayers);
			if (playersInRoom > 1)
			{
				readyToCount = true;
			}
			if (playersInRoom == MultiplayerSetting.MS.maxPlayers)
			{
				readyToStart = true;
				if (!PhotonNetwork.IsMasterClient) { return; }
				PhotonNetwork.CurrentRoom.IsOpen = false;
			}
		}
			//for non-delay start
		else
		{
			StartGame();
		}
	}

	void StartGame()
	{
		//load the multiplayer scene for all  players
		isGameLoaded = true;
		if (!PhotonNetwork.IsMasterClient){ return;}
		if (MultiplayerSetting.MS.delayStart)
		{
			PhotonNetwork.CurrentRoom.IsOpen = false;
		}
		PhotonNetwork.LoadLevel(MultiplayerSetting.MS.multiplayerScene);
	}

	void RestartTimer()
	{
		//restart the time for when players leave the room(DelayStart)
		lessThanMaxPlayers = startingTime;
		timeToStart = startingTime;
		atMaxPlayers = 3;
		readyToCount = false;
		readyToStart = false;
	}

	public override void OnPlayerEnteredRoom(Player newPlayer)
	{
		base.OnPlayerEnteredRoom(newPlayer);
		Debug.Log("New player join the room");
		photonPlayers = PhotonNetwork.PlayerList;
		playersInRoom++;
		if (MultiplayerSetting.MS.delayStart)
		{
			Debug.Log("Displayer players in room out of max players possible (" + playersInRoom + ":" + MultiplayerSetting.MS.maxPlayers);
			if (playersInRoom > 1)
			{
				readyToCount = true;
			}
			if(playersInRoom == MultiplayerSetting.MS.maxPlayers)
			{
				readyToStart = true;
				if (!PhotonNetwork.IsMasterClient) { return; }
				PhotonNetwork.CurrentRoom.IsOpen = false;
			}
		}
	}

	void OnSceneFinishedLoading(Scene scene, LoadSceneMode mode)
	{
		//called when multiplayer scene is loaded
		currentScene = scene.buildIndex;
		if(currentScene == MultiplayerSetting.MS.multiplayerScene)
		{
			isGameLoaded = true;
			//for delay start game
			if (MultiplayerSetting.MS.delayStart)
			{
				PV.RPC("RPC_LoadedGameScene", RpcTarget.MasterClient);
			}
				//for non-delay game
			else
			{
				RPC_CreatePlayer();
			}
		}
	}

	[PunRPC]
	private void RPC_LoadedGameScene()
	{
		playersInGame++;
		if(playersInGame == PhotonNetwork.PlayerList.Length)
		{
			PV.RPC("RPC_CreatePlayer", RpcTarget.AllBuffered);
		}
	}

	private void RPC_CreatePlayer()
	{
		//creates player network controller but not player character
		PhotonNetwork.Instantiate(Path.Combine("PhotonPrefab", "PhotonNetworkPlayer"), transform.position, Quaternion.identity, 0);
	}
}
