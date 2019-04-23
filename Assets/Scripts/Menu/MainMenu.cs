using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
	public static MainMenu menu;
	public GameObject singleButton;
	public GameObject multiButton;
	public GameObject exitButton;
	private AssetBundle myLoadedAssetBundle;

	// Start is called before the first frame update
	void Start()
    {

	}

    // Update is called once per frame
    void Update()
    {
        
    }

	public void OnSinglePlayerButtonClicked()
	{
		Debug.Log("Single Player mode selected, proceding to battle");
		SceneManager.LoadScene("Battle_test");
	}

	public void OnMultiplayerButtonClicked()
	{
		Debug.Log("Multiplayer mode selected, proceding to Lobby");
		SceneManager.LoadScene("Multiplayer-Lobby");
	}

	public void OnExitButtonClicked()
	{
		Debug.Log("Exiting the game");
		Application.Quit();
	}
}
