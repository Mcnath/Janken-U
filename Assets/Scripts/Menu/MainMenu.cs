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
	public GameObject tutorialButton;
	public GameObject tutorialCanvas;
	public GameObject menuCanvas;
	public GameObject tutorial1;
	public GameObject tutorial2;
	public GameObject tutorial3;
	public GameObject tutorial4;
	public GameObject tutorial5;
	public GameObject tutorial6;
	private AssetBundle myLoadedAssetBundle;

	// Start is called before the first frame update
	void Start()
    {
		menuCanvas = GameObject.Find("MenuSelection");
		menuCanvas.SetActive(true);
		tutorialCanvas = GameObject.Find("Tutorial");
		tutorialCanvas.SetActive(false);
		tutorial1.SetActive(false);
		tutorial2.SetActive(false);
		tutorial3.SetActive(false);
		tutorial4.SetActive(false);
		tutorial5.SetActive(false);
		tutorial6.SetActive(false);
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
		SceneManager.LoadScene("Multiplayer_Lobby");
	}

	public void OnTutorialButtonClicked()
	{
		Debug.Log("Displaying Tutorial");
		tutorialCanvas.SetActive(true);
		menuCanvas.SetActive(false);
		tutorial1.SetActive(true);
	}

	public void tutorialPage1()
	{
		tutorial1.SetActive(false);
		tutorial2.SetActive(true);
		tutorial3.SetActive(false);
		tutorial4.SetActive(false);
		tutorial5.SetActive(false);
		tutorial6.SetActive(false);
	}

	public void tutorialPage2()
	{
		tutorial1.SetActive(false);
		tutorial2.SetActive(false);
		tutorial3.SetActive(true);
		tutorial4.SetActive(false);
		tutorial5.SetActive(false);
		tutorial6.SetActive(false);
	}
	public void tutorialPage3()
	{
		tutorial1.SetActive(false);
		tutorial2.SetActive(false);
		tutorial3.SetActive(false);
		tutorial4.SetActive(true);
		tutorial5.SetActive(false);
		tutorial6.SetActive(false);
	}
	public void tutorialPage4()
	{
		tutorial1.SetActive(false);
		tutorial2.SetActive(false);
		tutorial3.SetActive(false);
		tutorial4.SetActive(false);
		tutorial5.SetActive(true);
		tutorial6.SetActive(false);
	}
	public void tutorialPage5()
	{
		tutorial1.SetActive(false);
		tutorial2.SetActive(false);
		tutorial3.SetActive(false);
		tutorial4.SetActive(false);
		tutorial5.SetActive(false);
		tutorial6.SetActive(true);

	}
	public void tutorialPage6()
	{
		tutorial1.SetActive(false);
		tutorial2.SetActive(false);
		tutorial3.SetActive(false);
		tutorial4.SetActive(false);
		tutorial5.SetActive(false);
		tutorial6.SetActive(false);
		tutorialCanvas.SetActive(false);
		menuCanvas.SetActive(true);
	}

	public void OnExitButtonClicked()
	{
		Debug.Log("Exiting the game");
		Application.Quit();
	}
}
