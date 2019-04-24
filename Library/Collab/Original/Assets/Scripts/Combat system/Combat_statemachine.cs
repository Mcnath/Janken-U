using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Combat_statemachine : MonoBehaviour {
	public Animator ani;
	public Animator c1;
	public Animator c2;
	//Initialized player skills
	public GameObject Attack;
    public GameObject Recover;
    public GameObject DoubleAttack;
    public GameObject AttackAll;   
    //playerpanel
    public GameObject leftR;
    public GameObject leftP;
    public GameObject leftS;
    public GameObject rightR;
    public GameObject rightP;
    public GameObject rightS;
	//player
	public Player_statemachine PSM;
    public GameObject e1;
    public GameObject e2;
    public GameObject e3;
	//canvas for switch to battle scene
	public GameObject battleCanvas;
	public GameObject classSelectCanvas;
	public GameObject resultCanvas;
	//variable for objects form resultCanvas
	public GameObject winText;
	public GameObject loseText;
	public GameObject restartButton;
	public GameObject exitButton;

	//variable for timers
	private int seconds_current = 0;
	private int seconds_max = 60;
    private int count = 0;
	//battle turn state
	public enum turnState{
		START,
		PLAYERCHOICE,
		ENEMYCHOICE,
		ACTION
	}
	public turnState currentState;

	//List for storing existing player's information
	public List<HandleTurn> PerformList = new List<HandleTurn> ();// collect all the actions via HandleTurn Class
	public List<GameObject> PlayerInBattle = new List<GameObject>();// collect all the player existed in the field

	//initialize player input
	public enum PlayerGUI{ ACTIVATE, INPUT, DONE}
	public PlayerGUI playerInput;
	public HandleTurn playerChoice;

    //initialization of state
    void Start () {
		ani.GetComponent<Animator>();
		c1.GetComponent<Animator>();
		c2.GetComponent<Animator>();
		currentState = turnState.START;
		playerInput = PlayerGUI.ACTIVATE;
		PlayerInBattle.AddRange (GameObject.FindGameObjectsWithTag("Player"));
		PlayerInBattle.AddRange(GameObject.FindGameObjectsWithTag("AI"));


		//player objects

       
		leftR = GameObject.Find("Left_Rock");
		leftP = GameObject.Find("Left_Paper");
		leftS = GameObject.Find("Left_Scissors");
		rightR = GameObject.Find("Right_Rock");
		rightP = GameObject.Find("Right_Paper");
		rightS = GameObject.Find("Right_Scissors");

		//enemies
		e1 = GameObject.Find("Enemy 1");
        e2 = GameObject.Find("Enemy 2");
        e3 = GameObject.Find("Enemy 3");

		
		// canvas
		battleCanvas = GameObject.Find("BattleCanvas");
		classSelectCanvas = GameObject.Find("ClassSelectionCanvas");
		resultCanvas = GameObject.Find("ResultCanvas");
		battleCanvas.SetActive(false);
		classSelectCanvas.SetActive(true);
		resultCanvas.SetActive(false);


    }

    // Update is called once per frame
    void Update()
    {
        GameResult();
        //how the battle is progressed each turn
        //Debug.Log ("currentState: "+ currentState);
        switch (currentState)
        {
            case (turnState.START):
                PerformList = new List<HandleTurn> ();
                playerChoice = new HandleTurn();
                currentState = turnState.PLAYERCHOICE;
                break;
            case (turnState.PLAYERCHOICE):
                if (playerInput == PlayerGUI.DONE) { currentState = turnState.ENEMYCHOICE; }
                break;
            case (turnState.ENEMYCHOICE):
                switch (playerChoice.skill)
                {
                    case HandleTurn.skills.DoubleAttack:
                        if (PerformList.Count == PlayerInBattle.Count + 1)
                        {
                            currentState = turnState.ACTION;
                        }

                        break;
                    case HandleTurn.skills.Attack:
                        if (PerformList.Count == PlayerInBattle.Count)
                        {
                            currentState = turnState.ACTION;
                        }

                        break;
                    case HandleTurn.skills.AttackAll:
                        if (PerformList.Count == PlayerInBattle.Count + 2)
                        {
                            currentState = turnState.ACTION;
                        }

                        break;
                    
                    case HandleTurn.skills.Recover:
                        //if (PerformList.Count == PlayerInBattle.Count -1)
                        //{
                            currentState = turnState.ACTION;
                        //}
                        break;
                    case HandleTurn.skills.DeniService:
                            currentState = turnState.ACTION;

                        break;
                    case HandleTurn.skills.ExtraTurn:
                        if (PerformList.Count == PlayerInBattle.Count + 1)
                        {
                            currentState = turnState.ACTION;
                        }

                        break;
                    case HandleTurn.skills.MinRisk:
                        if (PerformList.Count == PlayerInBattle.Count)
                        {
                            currentState = turnState.ACTION;
                        }
                        break;

                    case HandleTurn.skills.Sleep:
                        if (PerformList.Count == PlayerInBattle.Count-1)
                        {
                            currentState = turnState.ACTION;
                        }

                        break;
                    case HandleTurn.skills.SplashAttack:
                        if (PerformList.Count == PlayerInBattle.Count + 1)
                        {
                            currentState = turnState.ACTION;
                        }

                        break;
                    case HandleTurn.skills.Wall:
                        if (PerformList.Count == PlayerInBattle.Count)
                        {
                            currentState = turnState.ACTION;
                        }

                        break;
                 
                }

                break;
            case (turnState.ACTION):
                //put in the logic here
                skillused();
				// Replace with transition animation
                currentState = turnState.START;
                break;
        }


        //how the player turn is progressed
        //Debug.Log ("playerInput: "+ playerInput);
        switch (playerInput)
        {
            case (PlayerGUI.ACTIVATE):
                playerInput = PlayerGUI.INPUT;
                break;
            case (PlayerGUI.INPUT):
                if (playerChoice.AttackTarget != null)
                {
                    playerInput = PlayerGUI.DONE;
                }
                break;
            case (PlayerGUI.DONE):
                if (currentState == turnState.PLAYERCHOICE) { playerInput = PlayerGUI.ACTIVATE; }
                break;
        }
    }


	void timerPlayer(){
		//counting down the time and force skip player's turn if no action taken after time limit
	}

	public void CollectActions(HandleTurn input){

        
        PerformList.Add (input); // recorded actions chosen by enemy
	}
	//initialize attack choice here (initialize class first)
	public void classSlectedISTD()
	{
		Debug.Log("ISTD");
		PSM.player.pillar = Player_base.pillars.ISTD;
		battleCanvas.SetActive(true);
		classSelectCanvas.SetActive(false);
		ani.SetBool("istd", true);
		c1.SetBool("istd", true);
		c2.SetBool("istd", true);
		ani.SetBool("lefthand", true);
		ani.SetBool("righthand", true);
	}
	public void classSlectedEPD()
	{
		Debug.Log("EPD");
		PSM.player.pillar = Player_base.pillars.EPD;
		battleCanvas.SetActive(true);
		classSelectCanvas.SetActive(false);
		ani.SetBool("epd", true);
		c1.SetBool("epd", true);
		c2.SetBool("epd", true);
		ani.SetBool("lefthand", true);
		ani.SetBool("righthand", true);
	}
	public void classSlectedESD()
	{
		Debug.Log("ESD");
		PSM.player.pillar = Player_base.pillars.ESD;
		battleCanvas.SetActive(true);
		classSelectCanvas.SetActive(false);
		ani.SetBool("esd", true);
		c1.SetBool("esd", true);
		c2.SetBool("esd", true);
		ani.SetBool("lefthand", true);
		ani.SetBool("righthand", true);
	}
	public void classSlectedASD()
	{
		Debug.Log("ASD");
		PSM.player.pillar = Player_base.pillars.ASD;
		battleCanvas.SetActive(true);
		classSelectCanvas.SetActive(false);
		ani.SetBool("asd", true);
		c1.SetBool("asd", true);
		c2.SetBool("asd", true);
		ani.SetBool("lefthand", true);
		ani.SetBool("righthand", true);
	}
	// Attack options

	public void chooseRockLeft()
	{
		ani.SetBool("leftscissors", false);
		ani.SetBool("leftpaper", false);
		ani.SetBool("leftrock", true);
		Debug.Log("Chosen Rock on the left");
		playerChoice.LeftAttackType = HandleTurn.janken.ROCK;
	}

		public void chooseScissorsLeft()
	{
		ani.SetBool("leftscissors", true);
		ani.SetBool("leftpaper", false);
		ani.SetBool("leftrock", false);
		Debug.Log("Chosen Scissors on the left");
		playerChoice.LeftAttackType = HandleTurn.janken.SCISSORS;
	}

	public void choosePaperLeft()
	{
		ani.SetBool("leftpaper", true);
		ani.SetBool("leftscissors", false);
		ani.SetBool("leftrock", false);
		Debug.Log("Chosen Paper on the left");
		playerChoice.LeftAttackType = HandleTurn.janken.PAPER;
	}

	public void chooseRockRight()
	{
		ani.SetBool("rightrock", true);
		ani.SetBool("rightscissors", false);
		ani.SetBool("rightpaper", false);
		Debug.Log("Chosen Rock on the right");
		playerChoice.RightAttackType = HandleTurn.janken.ROCK;
	}

	public void chooseScissorsRight()
	{
		ani.SetBool("rightscissors", true);
		ani.SetBool("rightpaper", false);
		ani.SetBool("rightrock", false);
		Debug.Log("Chosen Scissors on the right");
		playerChoice.RightAttackType = HandleTurn.janken.SCISSORS;
	}

	public void choosePaperRight()
	{
		ani.SetBool("rightpaper", true);
		ani.SetBool("rightscissors", false);
		ani.SetBool("rightrock", false);
		Debug.Log("Chosen Paper on the right");
		playerChoice.RightAttackType = HandleTurn.janken.PAPER;
	}

	//enemy select

	public void enemySelected(){
        // update player choice of Attack target

		playerChoice.AttackTarget = this.gameObject;
	}

	//skills
	public void skillsSelectedAttack()
	{
		Debug.Log("Attack");
		playerChoice.skill = HandleTurn.skills.Attack;
	}
	public void skillsSelectedClassSkill1()
	{
		switch (PSM.player.pillar)
		{
			case Player_base.pillars.ASD:
				Debug.Log("BuildAWall");
				playerChoice.skill = HandleTurn.skills.Wall;
				break;
			case Player_base.pillars.EPD:
				Debug.Log("DoubleAttack");
				playerChoice.skill = HandleTurn.skills.DoubleAttack;
				break;
			case Player_base.pillars.ESD:
				Debug.Log("MinRisk");
				playerChoice.skill = HandleTurn.skills.MinRisk;
				break;
			case Player_base.pillars.ISTD:
				Debug.Log("DenialofService");
				playerChoice.skill = HandleTurn.skills.DeniService;
				break;
		}
	}
	public void skillsSelectedClassSkill2()
	{
		switch (PSM.player.pillar)
		{
			case Player_base.pillars.ASD:
				Debug.Log("ExtraTurn");
				playerChoice.skill = HandleTurn.skills.ExtraTurn;
				break;
			case Player_base.pillars.EPD:
				Debug.Log("SplashAttack");
				playerChoice.skill = HandleTurn.skills.SplashAttack;
				break;
			case Player_base.pillars.ESD:
				Debug.Log("Sleep");
				playerChoice.skill = HandleTurn.skills.Sleep;
				break;
			case Player_base.pillars.ISTD:
				Debug.Log("AttackAll");
				playerChoice.skill = HandleTurn.skills.AttackAll;
				break;
		}
	}
	public void skillsSelectedRecover()
	{
		Debug.Log("Recover");
		playerChoice.skill = HandleTurn.skills.Recover;
	}

	public void skillused()
    {
        Debug.Log("Battle Start");

        switch (playerChoice.skill)
        {
            case (HandleTurn.skills.AttackAll):
                {
                    battleLogic();


                    break;
                }
            case (HandleTurn.skills.Attack):
                {
                    battleLogic();
                    break;
                }
            //EPD
            case HandleTurn.skills.SplashAttack:
                {
                    battleLogic();
                    break;
                }
            case (HandleTurn.skills.DoubleAttack):
                {
                    //for (int m = 0; m < 2; m++)
                    //{
                        battleLogic();
                    //}
                    break;
                }
            case (HandleTurn.skills.Recover):
                {
                    switch (count)
                    {
                        case (1):
                            {
                                Debug.Log("Recover can only use once");
                                break;
                            }
                        case (0):
                            {


                               
                                   if (PSM.player.RightHand_state == false)
                                        {

                                            try
                                            {
                                                PSM.player.RightHand_state = true;
												ani.SetBool("righthand", true);
												ani.SetTrigger("recover");
												rightP.SetActive(true);
                                                rightR.SetActive(true);
                                                rightS.SetActive(true);
                                                count++;
                                            }
                                          
                                            catch (NullReferenceException)
                                            {

                                            }
                                        }


                                   else if (PSM.player.LeftHand_state == false)
                                        {

                                            try
                                            {
                                                PSM.player.LeftHand_state = true;
												ani.SetBool("lefthand", true);
												ani.SetTrigger("recover");
												leftP.SetActive(true);
                                                leftR.SetActive(true);
                                                leftS.SetActive(true);
                                                count++;
                                            }
                                            
                                            catch (NullReferenceException)
                                            {

                                            }

                                        
                                    }




                                    break;
                                }
                            }

                            break;
                    }
            //ASD
            case HandleTurn.skills.ExtraTurn:
                {
                    break;
                }
            case (HandleTurn.skills.Wall):
                {
                    battleLogic();
                    if (PSM.player.RightHand_state == false)
                    {

                        try
                        {
                            PSM.player.RightHand_state = true;
							ani.SetBool("righthand", true);
							rightP.SetActive(true);
                            rightR.SetActive(true);
                            rightS.SetActive(true);
                           
                        }

                        catch (NullReferenceException)
                        {

                        }
                    }

                    if (PSM.player.LeftHand_state == false)
                    {

                        try
                        {
                            PSM.player.LeftHand_state = true;
							ani.SetBool("lefthand", true);
							leftP.SetActive(true);
                            leftR.SetActive(true);
                            leftS.SetActive(true);
                           
                        }

                        catch (NullReferenceException)
                        {

                        }
                    }
                    break;
                }
            //ESD
            case (HandleTurn.skills.MinRisk):
                {
                    battleLogic();
                    if (PSM.player.RightHand_state == false)
                    {

                        try
                        {
                            PSM.player.RightHand_state = true;
							ani.SetBool("righthand", true);
							rightP.SetActive(true);
                            rightR.SetActive(true);
                            rightS.SetActive(true);

                        }

                        catch (NullReferenceException)
                        {

                        }
                    }

                    if (PSM.player.LeftHand_state == false)
                    {

                        try
                        {
                            PSM.player.LeftHand_state = true;
							ani.SetBool("lefthand", true);
							leftP.SetActive(true);
                            leftR.SetActive(true);
                            leftS.SetActive(true);

                        }

                        catch (NullReferenceException)
                        {

                        }
                    }
                    break;
                }
            
            case HandleTurn.skills.Sleep:
                {
                    battleLogic();
                    if (PSM.player.RightHand_state == false)
                    {

                        try
                        {
                            PSM.player.RightHand_state = true;
							ani.SetBool("righthand", true);
							rightP.SetActive(true);
                            rightR.SetActive(true);
                            rightS.SetActive(true);

                        }

                        catch (NullReferenceException)
                        {

                        }
                    }

                    if (PSM.player.LeftHand_state == false)
                    {

                        try
                        {
                            PSM.player.LeftHand_state = true;
							ani.SetBool("lefthand", true);
							leftP.SetActive(true);
                            leftR.SetActive(true);
                            leftS.SetActive(true);

                        }

                        catch (NullReferenceException)
                        {

                        }
                    }
                    break;
                }
            //ISTD
            case HandleTurn.skills.DeniService:
                { 
                    //if (playerChoice.AttackTarget == e1)
                    //{
                    //    ESM1.myAttack.skill = HandleTurn.skills.Attack;
                    //}
                    //else if(playerChoice.AttackTarget == e2)
                    //{
                    //    ESM2.myAttack.skill = HandleTurn.skills.Attack;
                    //}
                    //else if(playerChoice.AttackTarget == e3)
                    //{
                    //    ESM3.myAttack.skill = HandleTurn.skills.Attack;
                    //}
                    battleLogic();
                    break;
                }
            
        }

    }



	public void battleLogic() {
		Debug.Log("Battle Start");
		for (int i = 0; i < PerformList.Count; i++) {
			for (int j = 1; j < PerformList.Count; j++) {
				if (PerformList[j].AttackTarget == PerformList[i].AttackGameObject) {
					int resultLeft = howToWin(PerformList[j].LeftAttackType, PerformList[i].LeftAttackType);
					int resultRight = howToWin(PerformList[j].RightAttackType, PerformList[i].RightAttackType);
					
					
					// Battle result and sprite's destruction
					if (resultLeft == 1) {
						Debug.Log(PerformList[j].Attacker + " win, " + PerformList[i].Attacker + " lose");
						if (PerformList[i].AttackGameObject == GameObject.FindWithTag("Player") &&
							PerformList[i].AttackGameObject.GetComponent<Player_statemachine>().player.LeftHand_state == true)
						{
							PerformList[i].AttackGameObject.GetComponent<Player_statemachine>().player.LeftHand_state = false;
							try
							{
								ani.SetBool("leftrock", false);
								ani.SetBool("leftpaper", false);
								ani.SetBool("leftscissors", false);
								ani.SetBool("lefthand", false);
								ani.SetTrigger("losing");
								leftP.SetActive(false);
								leftR.SetActive(false);
								leftS.SetActive(false);
							}
							catch (Exception e)
							{
								Debug.Log("already removed");
							}

						}
						else
						{
							if (PerformList[i].Attacker == "e1" && PerformList[i].AttackGameObject.GetComponent<Emeny_AIstatemachine>().enemy.LeftHand_state == true)
							{
								try
								{
									PerformList[i].AttackGameObject.GetComponent<Emeny_AIstatemachine>().enemy.LeftHand_state = false;
									
								}
								catch (Exception e)
								{
									Debug.Log("already removed");
								}
							}
							else if (PerformList[i].Attacker == "e2" && PerformList[i].AttackGameObject.GetComponent<Emeny_AIstatemachine>().enemy.LeftHand_state == true)
							{
								try
								{
									PerformList[i].AttackGameObject.GetComponent<Emeny_AIstatemachine>().enemy.LeftHand_state = false;
									
								}
								catch (Exception e)
								{
									Debug.Log("already removed");
								}
							}
							else if (PerformList[i].Attacker == "e3" && PerformList[i].AttackGameObject.GetComponent<Emeny_AIstatemachine>().enemy.RightHand_state == true)
							{
								try
								{
									PerformList[i].AttackGameObject.GetComponent<Emeny_AIstatemachine>().enemy.LeftHand_state = false;
									
								}
								catch (Exception e)
								{
									Debug.Log("already removed");
								}
							}
							//PerformList[i].AttackGameObject.GetComponent<Emeny_AIstatemachine>().enemy.LeftHand_state = false;
						}
					} else {
						Debug.Log(PerformList[i].Attacker + " win, " + PerformList[j].Attacker + " lose");
						if (PerformList[j].AttackGameObject == GameObject.FindWithTag("Player") && PerformList[j].AttackGameObject.GetComponent<Player_statemachine>().player.LeftHand_state == true)
						{
							PerformList[j].AttackGameObject.GetComponent<Player_statemachine>().player.LeftHand_state = false;
							try
							{
								ani.SetBool("leftrock", false);
								ani.SetBool("leftpaper", false);
								ani.SetBool("leftscissors", false);
								ani.SetBool("lefthand", false);
								ani.SetTrigger("losing");
								leftR.SetActive(false);
								leftP.SetActive(false);
								leftS.SetActive(false);
							}
							catch (Exception e)
							{
								Debug.Log("already removed");
							}
						}
						else
						{
							if (PerformList[j].Attacker == "e1" && PerformList[j].AttackGameObject.GetComponent<Emeny_AIstatemachine>().enemy.LeftHand_state == true)
							{
								try
								{
									PerformList[j].AttackGameObject.GetComponent<Emeny_AIstatemachine>().enemy.LeftHand_state = false;
									
								}
								catch (Exception e)
								{
									Debug.Log("already removed");
								}
							}
							else if (PerformList[j].Attacker == "e2" && PerformList[j].AttackGameObject.GetComponent<Emeny_AIstatemachine>().enemy.LeftHand_state == true)
							{
								try
								{
									PerformList[j].AttackGameObject.GetComponent<Emeny_AIstatemachine>().enemy.LeftHand_state = false;
									
								}
								catch (Exception e)
								{
									Debug.Log("already removed");
								}
							}
							else if (PerformList[j].Attacker == "e3" && PerformList[j].AttackGameObject.GetComponent<Emeny_AIstatemachine>().enemy.LeftHand_state == true)
							{
								try
								{
									PerformList[j].AttackGameObject.GetComponent<Emeny_AIstatemachine>().enemy.LeftHand_state = false;
									
								}
								catch (Exception e)
								{
									Debug.Log("already removed");
								}
							}
							//PerformList[j].AttackGameObject.GetComponent<Emeny_AIstatemachine>().enemy.LeftHand_state = false;
						}
					}
					if (resultRight == 1) {
						Debug.Log(PerformList[j].Attacker + " win, " + PerformList[i].Attacker + " lose");
						if (PerformList[i].AttackGameObject == GameObject.FindWithTag("Player") &&
							PerformList[i].AttackGameObject.GetComponent<Player_statemachine>().player.RightHand_state == true)
						{
							PerformList[i].AttackGameObject.GetComponent<Player_statemachine>().player.RightHand_state = false;
							try
							{
								ani.SetBool("rightrock", false);
								ani.SetBool("rightpaper", false);
								ani.SetBool("rightscissors", false);
								ani.SetBool("righthand", false);
								ani.SetTrigger("losing");
								rightR.SetActive(false);
								rightP.SetActive(false);
								rightS.SetActive(false);
							}
							catch (Exception e)
							{
								Debug.Log("already removed");
							}
						}
						else
						{
							if (PerformList[i].Attacker == "e1" && PerformList[i].AttackGameObject.GetComponent<Emeny_AIstatemachine>().enemy.RightHand_state == true)
							{
								try
								{
									PerformList[i].AttackGameObject.GetComponent<Emeny_AIstatemachine>().enemy.RightHand_state = false;
									
								}
								catch (Exception e)
								{
									Debug.Log("already removed");
								}
							}
							else if (PerformList[i].Attacker == "e2" && PerformList[i].AttackGameObject.GetComponent<Emeny_AIstatemachine>().enemy.RightHand_state == true)
							{
								try
								{
									PerformList[i].AttackGameObject.GetComponent<Emeny_AIstatemachine>().enemy.RightHand_state = false;
									
								}
								catch (Exception e)
								{
									Debug.Log("already removed");
								}
							}
							else if (PerformList[i].Attacker == "e3" && PerformList[i].AttackGameObject.GetComponent<Emeny_AIstatemachine>().enemy.RightHand_state == true)
							{
								try
								{
									PerformList[i].AttackGameObject.GetComponent<Emeny_AIstatemachine>().enemy.RightHand_state = false;
									
								}
								catch (Exception e)
								{
									Debug.Log("already removed");
								}
							}
							//PerformList[i].AttackGameObject.GetComponent<Emeny_AIstatemachine>().enemy.RightHand_state = false;
						}
					} else {
						Debug.Log(PerformList[i].Attacker + " win, " + PerformList[j].Attacker + " lose");
						if (PerformList[j].AttackGameObject == GameObject.FindWithTag("Player") &&
							PerformList[j].AttackGameObject.GetComponent<Player_statemachine>().player.RightHand_state == true)
						{
							PerformList[j].AttackGameObject.GetComponent<Player_statemachine>().player.RightHand_state = false;
							try
							{
								ani.SetBool("rightrock", false);
								ani.SetBool("rightpaper", false);
								ani.SetBool("rightscissors", false);
								ani.SetBool("righthand", false);
								ani.SetTrigger("losing");
								rightP.SetActive(false);
								rightR.SetActive(false);
								rightS.SetActive(false);
							}
							catch (Exception e)
							{
								Debug.Log("already removed");
							}
						}
						else
						{
							if (PerformList[j].Attacker == "e1" && PerformList[j].AttackGameObject.GetComponent<Emeny_AIstatemachine>().enemy.RightHand_state == true)
							{
								try
								{
									PerformList[j].AttackGameObject.GetComponent<Emeny_AIstatemachine>().enemy.RightHand_state = false;
									
								}
								catch (Exception e)
								{
									Debug.Log("already removed");
								}
							}
							else if (PerformList[j].Attacker == "e2" && PerformList[j].AttackGameObject.GetComponent<Emeny_AIstatemachine>().enemy.RightHand_state == true)
							{
								try
								{
									PerformList[j].AttackGameObject.GetComponent<Emeny_AIstatemachine>().enemy.RightHand_state = false;
									
								}
								catch (Exception e)
								{
									Debug.Log("already removed");
								}
							}
							else if (PerformList[j].Attacker == "e3" && PerformList[j].AttackGameObject.GetComponent<Emeny_AIstatemachine>().enemy.RightHand_state == true)
							{
								try
								{
									PerformList[j].AttackGameObject.GetComponent<Emeny_AIstatemachine>().enemy.RightHand_state = false;
									
								}
								catch (Exception e)
								{
									Debug.Log("already removed");
								}
							}
							//PerformList[j].AttackGameObject.GetComponent<Emeny_AIstatemachine>().enemy.RightHand_state = false;
						}
					}	
				}

				//if (PlayerInBattle[i].GetComponent<Player_statemachine>().currentState == Player_statemachine.turnState.LOSE)
				//{
				//	PlayerInBattle[i] = null;
				//}
				//else if (PlayerInBattle[i].GetComponent<Emeny_AIstatemachine>().currentState == Emeny_AIstatemachine.turnState.LOSE)
				//{
				//	PlayerInBattle[i] = null;
				//}
			}
		}
		Debug.Log("Battle End");
		//remove character from the Performlist
		/*
		for (int i = 0; i < PlayerInBattle.Count; i++) {
			if (PlayerInBattle[i].GetComponent<Player_statemachine>().currentState == Player_statemachine.turnState.LOSE) {
				PlayerInBattle[i] = null;
			}
			else if (PlayerInBattle[i].GetComponent<Emeny_AIstatemachine>().currentState == Emeny_AIstatemachine.turnState.LOSE)
			{
				PlayerInBattle[i] = null;
			}
		}
		*/
	}

	public static int howToWin(HandleTurn.janken source, HandleTurn.janken target){
		if (source == HandleTurn.janken.PAPER && target == HandleTurn.janken.ROCK) {
			return 1;
		}
		else if (source == HandleTurn.janken.ROCK && target == HandleTurn.janken.SCISSORS) {
			return 1;
		}
		else if (source == HandleTurn.janken.SCISSORS && target == HandleTurn.janken.PAPER) {
			return 1;
		}
		else if(source == target)
		{
			return 1;
		}
		return 0;
	}

	public void OnRestartButtonClicked(){
		Debug.Log("Restarting the battle");
		SceneManager.LoadScene("Battle_test");
	}

	public void OnExitButtonClicked()
	{
		Debug.Log("Going back to main menu");
		SceneManager.LoadScene("Main Menu");
	}

	public void GameResult()
	{
		//Debug.Log("Checking the result");
		if(PSM.currentState == Player_statemachine.turnState.LOSE)
		{
			battleCanvas.SetActive(false);
			classSelectCanvas.SetActive(false);
			resultCanvas.SetActive(true);
			winText.SetActive(false);
			loseText.SetActive(true);
			restartButton.SetActive(true);
			exitButton.SetActive(true);
		}
		else if(e1.GetComponent<Emeny_AIstatemachine>().currentState == Emeny_AIstatemachine.turnState.LOSE && e2.GetComponent<Emeny_AIstatemachine>().currentState == Emeny_AIstatemachine.turnState.LOSE && e3.GetComponent<Emeny_AIstatemachine>().currentState == Emeny_AIstatemachine.turnState.LOSE)
		{
			battleCanvas.SetActive(false);
			classSelectCanvas.SetActive(false);
			resultCanvas.SetActive(true);
			winText.SetActive(true);
			loseText.SetActive(false);
			restartButton.SetActive(true);
			exitButton.SetActive(true);
		}
	}

}