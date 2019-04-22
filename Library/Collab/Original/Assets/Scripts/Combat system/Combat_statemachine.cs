using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System;
using UnityEngine;
using UnityEngine.UI;
public class Combat_statemachine : MonoBehaviour {
    //Initialized player skills
    public GameObject Attack;
    public GameObject Recover;
    public GameObject DoubleAttack;
    public GameObject AttackAll;
    //Initialized player jankens
    public GameObject p1rock;
    public GameObject p1paper;
    public GameObject p1scissors;
    public GameObject p1rock2;
    public GameObject p1paper2;
    public GameObject p1scissors2;
    public GameObject p1left;
    public GameObject p1right;
    //e1janken
    public GameObject e1rock;
    public GameObject e1paper;
    public GameObject e1scissors;
    public GameObject e1rock2;
    public GameObject e1paper2;
    public GameObject e1scissors2;
    public GameObject e1left;
    public GameObject e1right;
    //e2janken
    public GameObject e2rock;
    public GameObject e2paper;
    public GameObject e2scissors;
    public GameObject e2rock2;
    public GameObject e2paper2;
    public GameObject e2scissors2;
    public GameObject e2left;
    public GameObject e2right;
    //e3janken
    public GameObject e3rock;
    public GameObject e3paper;
    public GameObject e3scissors;
    public GameObject e3rock2;
    public GameObject e3paper2;
    public GameObject e3scissors2;
    public GameObject e3left;
    public GameObject e3right;
    //playerpanel
    public GameObject leftR;
    public GameObject leftP;
    public GameObject leftS;
    public GameObject rightR;
    public GameObject rightP;
    public GameObject rightS;
    //players
    public GameObject e1;
    public GameObject e2;
    public GameObject e3;
    //Initialized players(may not need)
    public Player_statemachine PSM;
	public Emeny_AIstatemachine ESM1, ESM2, ESM3;
	private HandleTurn HT;
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
    //public List<GameObject> PlayerToManage = new List<GameObject>
    //  //initialize attack choice here (initialize class first)
    public void classSlectedISTD()
    {
        Debug.Log("ISTD");
        PSM.player.pillar = Player_base.pillars.ISTD;
    }
    public void classSlectedEPD()
    {
        Debug.Log("EPD");
        PSM.player.pillar = Player_base.pillars.EPD;
    }
    public void classSlectedESD()
    {
        Debug.Log("ESD");
        PSM.player.pillar = Player_base.pillars.ESD;
    }
    public void classSlectedASD()
    {
        Debug.Log("ASD");
        PSM.player.pillar = Player_base.pillars.ASD;
    }
    
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


    //initialization of state
    void Start () {
		currentState = turnState.START;
		playerInput = PlayerGUI.ACTIVATE;
		PlayerInBattle.AddRange (GameObject.FindGameObjectsWithTag("Player"));
		PlayerInBattle.AddRange (GameObject.FindGameObjectsWithTag("AI"));
        //e1 = GameObject.Find("Enemy 1");
        //e2 = GameObject.Find("Enemy 2");
        //e3 = GameObject.Find("Enemy 3"); 


		//player objects

        p1rock = GameObject.Find("playerleftR");
        p1paper = GameObject.Find("playerleftP");
        p1scissors = GameObject.Find("playerleftS");
        p1rock.SetActive(true);
        p1paper.SetActive(false);
        p1scissors.SetActive(false);
        p1rock2 = GameObject.Find("playerrightR");
        p1paper2 = GameObject.Find("playerrightP");
        p1scissors2 = GameObject.Find("playerrightS");
        p1rock2.SetActive(true);
        p1paper2.SetActive(false);
        p1scissors2.SetActive(false);
        p1left = GameObject.Find("playerleft");
        p1right = GameObject.Find("playerright");
        p1left.SetActive(true);
        p1right.SetActive(true);
        leftR = GameObject.Find("Left_Rock");
        leftP = GameObject.Find("Left_Paper");
        leftS = GameObject.Find("Left_Scissors");
        rightR = GameObject.Find("Right_Rock");
        rightP = GameObject.Find("Right_Paper");
        rightS = GameObject.Find("Right_Scissors");
		//enemy 1 objects
        e1paper = GameObject.Find("e1leftp");
        e1rock = GameObject.Find("e1leftr");
        e1scissors = GameObject.Find("e1lefts");
        e1paper2 = GameObject.Find("e1rightp");
        e1rock2 = GameObject.Find("e1rightr");
        e1scissors2 = GameObject.Find("e1rights");
		e1left = GameObject.Find("e1left");
		e1right = GameObject.Find("e1right");
		e1left.SetActive(true);
		e1right.SetActive(true);
		e1rock.SetActive(true);
		e1rock2.SetActive(true);
		e1paper.SetActive(false);
		e1paper2.SetActive(false);
		e1scissors.SetActive(false);
        e1scissors2.SetActive(false);

		//enemy 2 objects
        e2paper = GameObject.Find("e2leftp");
        e2rock = GameObject.Find("e2leftr");
        e2scissors = GameObject.Find("e2lefts");
        e2paper2 = GameObject.Find("e2rightp");
        e2rock2 = GameObject.Find("e2rightr");
        e2scissors2 = GameObject.Find("e2rights");
		e2left = GameObject.Find("e2left");
		e2right = GameObject.Find("e2right");
		e2left.SetActive(true);
		e2right.SetActive(true);
		e2rock.SetActive(true);
		e2rock2.SetActive(true);
		e2paper.SetActive(false);
		e2paper2.SetActive(false);
		e2scissors.SetActive(false);
		e2scissors2.SetActive(false);
		//enemy 3 objects
		e3paper = GameObject.Find("e3leftp");
        e3rock = GameObject.Find("e3leftr");
        e3scissors = GameObject.Find("e3lefts");
        e3paper2 = GameObject.Find("e3rightp");
        e3rock2 = GameObject.Find("e3rightr");
        e3scissors2 = GameObject.Find("e3rights");
        e3left = GameObject.Find("e3left");
        e3right = GameObject.Find("e3right");
        e3left.SetActive(true);
        e3right.SetActive(true);
        e3rock.SetActive(true);
        e3rock2.SetActive(true);
        e3paper.SetActive(false);
        e3paper2.SetActive(false);
        e3scissors.SetActive(false);
        e3scissors2.SetActive(false);



    }

    // Update is called once per frame
    void Update()
    {
        //how the battle is progressed each turn
        //Debug.Log ("currentState: "+ currentState);
        switch (currentState)
        {
            case (turnState.START):
                //PerformList = new List<HandleTurn> ();
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
                //battleLogic();
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
//=======
		//e3left = GameObject.Find("e3left");
		//e3right = GameObject.Find("e3right");
		//e3left.SetActive(true);
		//e3right.SetActive(true);
		//e3rock.SetActive(true);
		//e3rock2.SetActive(true);
		//e3paper.SetActive(false);
		//e3paper2.SetActive(false);
		//e3scissors.SetActive(false);
		//e3scissors2.SetActive(false);
	//}
	
	//// Update is called once per frame
	//void Update () {
	//	//how the battle is progressed each turn
	//	//Debug.Log ("currentState: "+ currentState);
	//	switch(currentState){
	//	case(turnState.START):
	//		//PerformList = new List<HandleTurn> ();
	//		playerChoice = new HandleTurn ();
	//		currentState = turnState.PLAYERCHOICE;
	//		break;
	//	case(turnState.PLAYERCHOICE):
	//		if (playerInput == PlayerGUI.DONE) {currentState = turnState.ENEMYCHOICE;}
	//		break;
	//	case(turnState.ENEMYCHOICE):
	//		if (PerformList.Count == PlayerInBattle.Count) {currentState = turnState.ACTION;}
	//		break;
	//	case(turnState.ACTION):
	//		//put in the logic here
	//		battleLogic ();
	//		// Replace with transition animation
	//		currentState = turnState.START;
	//		break;
	//	}

	//	//how the player turn is progressed
	//	//Debug.Log ("playerInput: "+ playerInput);
	//	switch (playerInput){
	//	case(PlayerGUI.ACTIVATE):
	//		playerInput = PlayerGUI.INPUT;
	//		break;
	//	case(PlayerGUI.INPUT):
	//		if (playerChoice.AttackTarget != null) {
	//			playerInput = PlayerGUI.DONE;
	//		}
	//		break;
	//	case(PlayerGUI.DONE):
	//		if(currentState == turnState.PLAYERCHOICE){playerInput = PlayerGUI.ACTIVATE;}
	//		break;
	//	}	
	//}
//>>>>>>> 393b6b260cee2ac98cb173e1c3a6de69a7e33ce9

	void timerPlayer(){
		//counting down the time and force skip player's turn if no action taken after time limit
	}

	public void CollectActions(HandleTurn input){

        
        PerformList.Add (input); // recorded actions chosen by enemy
	}

	public void chooseRockLeft(){
        p1rock.SetActive(true);
        p1paper.SetActive(false);
        p1scissors.SetActive(false);
        Debug.Log ("Chosen Rock on the left");
		playerChoice.LeftAttackType = HandleTurn.janken.ROCK;
	}

	public void chooseScissorsLeft(){
        p1rock.SetActive(false);
        p1paper.SetActive(false);
        p1scissors.SetActive(true);
        Debug.Log ("Chosen Scissors on the left");
		playerChoice.LeftAttackType = HandleTurn.janken.SCISSORS;
	}

	public void choosePaperLeft(){
        p1rock.SetActive(false);
        p1paper.SetActive(true);
        p1scissors.SetActive(false);
        Debug.Log ("Chosen Paper on the left");
		playerChoice.LeftAttackType = HandleTurn.janken.PAPER;
	}

	public void chooseRockRight(){
        p1rock2.SetActive(true);
        p1paper2.SetActive(false);
        p1scissors2.SetActive(false);
        Debug.Log ("Chosen Rock on the right");
		playerChoice.RightAttackType = HandleTurn.janken.ROCK;
	}

	public void chooseScissorsRight(){
        p1rock2.SetActive(false);
        p1paper2.SetActive(false);
        p1scissors2.SetActive(true);
        Debug.Log ("Chosen Scissors on the right");
		playerChoice.RightAttackType = HandleTurn.janken.SCISSORS;
	}

	public void choosePaperRight(){
        p1rock2.SetActive(false);
        p1paper2.SetActive(true);
        p1scissors2.SetActive(false);
        Debug.Log ("Chosen Paper on the right");
		playerChoice.RightAttackType = HandleTurn.janken.PAPER;
	}

	public void enemySelected(){
        // update player choice of Attack target

		playerChoice.AttackTarget = this.gameObject;
	}




    public void skillused()
    {
        Debug.Log("Battle Start");

        switch (playerChoice.skill)
        {
            case (HandleTurn.skills.AttackAll):
                {
                    //playerChoice.AttackTarget = e1;
                    ////PerformList[i].AttackTarget = e1;
                    //battleLogic();
                    ////PerformList[i].AttackTarget = e2;
                    //playerChoice.AttackTarget = e2;
                    //battleLogic();
                    ////PerformList[i].AttackTarget = e3;
                    //playerChoice.AttackTarget = e3;
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
                                                p1right.SetActive(true);
                                                p1rock2.SetActive(true);
                                                p1paper2.SetActive(true);
                                                p1scissors2.SetActive(true);
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
                                                p1left.SetActive(true);
                                                p1rock.SetActive(true);
                                                p1paper.SetActive(true);
                                                p1scissors.SetActive(true);

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
                            p1right.SetActive(true);
                            p1rock2.SetActive(true);
                            p1paper2.SetActive(true);
                            p1scissors2.SetActive(true);
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
                            p1left.SetActive(true);
                            p1rock.SetActive(true);
                            p1paper.SetActive(true);
                            p1scissors.SetActive(true);

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
                            p1right.SetActive(true);
                            p1rock2.SetActive(true);
                            p1paper2.SetActive(true);
                            p1scissors2.SetActive(true);
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
                            p1left.SetActive(true);
                            p1rock.SetActive(true);
                            p1paper.SetActive(true);
                            p1scissors.SetActive(true);

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
                            p1right.SetActive(true);
                            p1rock2.SetActive(true);
                            p1paper2.SetActive(true);
                            p1scissors2.SetActive(true);
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
                            p1left.SetActive(true);
                            p1rock.SetActive(true);
                            p1paper.SetActive(true);
                            p1scissors.SetActive(true);

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


//    public void battleLogic()
//    {
//        Debug.Log("Battle Start");
//        for (int i = 0; i < PerformList.Count; i++)
//        {
//            for (int j = 1; j < PerformList.Count; j++)
//            {
//                if (PerformList[j].AttackTarget == PerformList[i].AttackGameObject)
//                {
//                    int resultLeft = howToWin(PerformList[j].LeftAttackType, PerformList[i].LeftAttackType);
//                    int resultRight = howToWin(PerformList[j].RightAttackType, PerformList[i].RightAttackType);
//                    // enemy sprite's change
//                    if (PerformList[i].Attacker == "e1")
//                    {
//                        if (PerformList[i].LeftAttackType == HandleTurn.janken.ROCK)
//                        {
//                            e1rock.SetActive(true);
//                            e1scissors.SetActive(false);
//                            e1paper.SetActive(false);
//                        }
//                        else if (PerformList[i].LeftAttackType == HandleTurn.janken.PAPER)
//                        {
//                            e1rock.SetActive(false);
//                            e1scissors.SetActive(false);
//                            e1paper.SetActive(true);
//                        }
//                        else if (PerformList[i].LeftAttackType == HandleTurn.janken.SCISSORS)
//                        {
//                            e1rock.SetActive(false);
//                            e1scissors.SetActive(true);
//                            e1paper.SetActive(false);
//                        }
//                        else if (PerformList[i].RightAttackType == HandleTurn.janken.ROCK)
//                        {
//                            e1rock2.SetActive(true);
//                            e1scissors2.SetActive(false);
//                            e1paper2.SetActive(false);
//                        }
//                        else if (PerformList[i].RightAttackType == HandleTurn.janken.PAPER)
//                        {
//                            e1rock2.SetActive(false);
//                            e1scissors2.SetActive(false);
//                            e1paper2.SetActive(true);
//                        }
//                        else if (PerformList[i].RightAttackType == HandleTurn.janken.SCISSORS)
//                        {
//                            e1rock2.SetActive(false);
//                            e1scissors2.SetActive(true);
//                            e1paper2.SetActive(false);
//                        }
//                    }
//                    else if (PerformList[i].Attacker == "e2")
//                    {
//                        if (PerformList[i].LeftAttackType == HandleTurn.janken.ROCK)
//                        {
//                            e2rock.SetActive(true);
//                            e2scissors.SetActive(false);
//                            e2paper.SetActive(false);
//                        }
//                        else if (PerformList[i].LeftAttackType == HandleTurn.janken.PAPER)
//                        {
//                            e2rock.SetActive(false);
//                            e2scissors.SetActive(false);
//                            e2paper.SetActive(true);
//                        }
//                        else if (PerformList[i].LeftAttackType == HandleTurn.janken.SCISSORS)
//                        {
//                            e2rock.SetActive(false);
//                            e2scissors.SetActive(true);
//                            e2paper.SetActive(false);
//                        }
//                        else if (PerformList[i].RightAttackType == HandleTurn.janken.ROCK)
//                        {
//                            e2rock2.SetActive(true);
//                            e2scissors2.SetActive(false);
//                            e2paper2.SetActive(false);
//                        }
//                        else if (PerformList[i].RightAttackType == HandleTurn.janken.PAPER)
//                        {
//                            e2rock2.SetActive(false);
//                            e2scissors2.SetActive(false);
//                            e2paper2.SetActive(true);
//                        }
//                        else if (PerformList[i].RightAttackType == HandleTurn.janken.SCISSORS)
//                        {
//                            e2rock2.SetActive(false);
//                            e2scissors2.SetActive(true);
//                            e2paper2.SetActive(false);
//                        }
//                    }
//                    else if (PerformList[i].Attacker == "e3")
//                    {
//                        if (PerformList[i].LeftAttackType == HandleTurn.janken.ROCK)
//                        {
//                            e3rock.SetActive(true);
//                            e3scissors.SetActive(false);
//                            e3paper.SetActive(false);
//                        }
//                        else if (PerformList[i].LeftAttackType == HandleTurn.janken.PAPER)
//                        {
//                            e3rock.SetActive(false);
//                            e3scissors.SetActive(false);
//                            e3paper.SetActive(true);
//                        }
//                        else if (PerformList[i].LeftAttackType == HandleTurn.janken.SCISSORS)
//                        {
//                            e3rock.SetActive(false);
//                            e3scissors.SetActive(true);
//                            e3paper.SetActive(false);
//                        }
//                        else if (PerformList[i].RightAttackType == HandleTurn.janken.ROCK)
//                        {
//                            e3rock2.SetActive(true);
//                            e3scissors2.SetActive(false);
//                            e3paper2.SetActive(false);
//                        }
//                        else if (PerformList[i].RightAttackType == HandleTurn.janken.PAPER)
//                        {
//                            e3rock2.SetActive(false);
//                            e3scissors2.SetActive(false);
//                            e3paper2.SetActive(true);
//                        }
//                        else if (PerformList[i].RightAttackType == HandleTurn.janken.SCISSORS)
//                        {
//                            e3rock2.SetActive(false);
//                            e3scissors2.SetActive(true);
//                            e3paper2.SetActive(false);
//                        }
//                    }
//                    if (PerformList[j].Attacker == "e1")
//                    {
//                        if (PerformList[j].LeftAttackType == HandleTurn.janken.ROCK)
//                        {
//                            e1rock.SetActive(true);
//                            e1scissors.SetActive(false);
//                            e1paper.SetActive(false);
//                        }
//                        else if (PerformList[j].LeftAttackType == HandleTurn.janken.PAPER)
//                        {
//                            e1rock.SetActive(false);
//                            e1scissors.SetActive(false);
//                            e1paper.SetActive(true);
//                        }
//                        else if (PerformList[j].LeftAttackType == HandleTurn.janken.SCISSORS)
//                        {
//                            e1rock.SetActive(false);
//                            e1scissors.SetActive(true);
//                            e1paper.SetActive(false);
//                        }
//                        else if (PerformList[j].RightAttackType == HandleTurn.janken.ROCK)
//                        {
//                            e1rock2.SetActive(true);
//                            e1scissors2.SetActive(false);
//                            e1paper2.SetActive(false);
//                        }
//                        else if (PerformList[j].RightAttackType == HandleTurn.janken.PAPER)
//                        {
//                            e1rock2.SetActive(false);
//                            e1scissors2.SetActive(false);
//                            e1paper2.SetActive(true);
//                        }
//                        else if (PerformList[j].RightAttackType == HandleTurn.janken.SCISSORS)
//                        {
//                            e1rock2.SetActive(false);
//                            e1scissors2.SetActive(true);
//                            e1paper2.SetActive(false);
//                        }
//                    }
//                    else if (PerformList[j].Attacker == "e2")
//                    {
//                        if (PerformList[j].LeftAttackType == HandleTurn.janken.ROCK)
//                        {
//                            e2rock.SetActive(true);
//                            e2scissors.SetActive(false);
//                            e2paper.SetActive(false);
//                        }
//                        else if (PerformList[j].LeftAttackType == HandleTurn.janken.PAPER)
//                        {
//                            e2rock.SetActive(false);
//                            e2scissors.SetActive(false);
//                            e2paper.SetActive(true);
//                        }
//                        else if (PerformList[j].LeftAttackType == HandleTurn.janken.SCISSORS)
//                        {
//                            e2rock.SetActive(false);
//                            e2scissors.SetActive(true);
//                            e2paper.SetActive(false);
//                        }
//                        else if (PerformList[j].RightAttackType == HandleTurn.janken.ROCK)
//                        {
//                            e2rock2.SetActive(true);
//                            e2scissors2.SetActive(false);
//                            e2paper2.SetActive(false);
//                        }
//                        else if (PerformList[j].RightAttackType == HandleTurn.janken.PAPER)
//                        {
//                            e2rock2.SetActive(false);
//                            e2scissors2.SetActive(false);
//                            e2paper2.SetActive(true);
//                        }
//                        else if (PerformList[j].RightAttackType == HandleTurn.janken.SCISSORS)
//                        {
//                            e2rock2.SetActive(false);
//                            e2scissors2.SetActive(true);
//                            e2paper2.SetActive(false);
//                        }
//                    }
//                    else if (PerformList[j].Attacker == "e3")
//                    {
//                        if (PerformList[j].LeftAttackType == HandleTurn.janken.ROCK)
//                        {
//                            e3rock.SetActive(true);
//                            e3scissors.SetActive(false);
//                            e3paper.SetActive(false);
//                        }
//                        else if (PerformList[j].LeftAttackType == HandleTurn.janken.PAPER)
//                        {
//                            e3rock.SetActive(false);
//                            e3scissors.SetActive(false);
//                            e3paper.SetActive(true);
//                        }
//                        else if (PerformList[j].LeftAttackType == HandleTurn.janken.SCISSORS)
//                        {
//                            e3rock.SetActive(false);
//                            e3scissors.SetActive(true);
//                            e3paper.SetActive(false);
//                        }
//                        else if (PerformList[j].RightAttackType == HandleTurn.janken.ROCK)
//                        {
//                            e3rock2.SetActive(true);
//                            e3scissors2.SetActive(false);
//                            e3paper2.SetActive(false);
//                        }
//                        else if (PerformList[j].RightAttackType == HandleTurn.janken.PAPER)
//                        {
//                            e3rock2.SetActive(false);
//                            e3scissors2.SetActive(false);
//                            e3paper2.SetActive(true);
//                        }
//                        else if (PerformList[j].RightAttackType == HandleTurn.janken.SCISSORS)
//                        {
//                            e3rock2.SetActive(false);
//                            e3scissors2.SetActive(true);
//                            e3paper2.SetActive(false);
//                        }
//                    }
//                    // Battle result and sprite's destruction
//                    if (resultLeft == 1)
//                    {
//                        Debug.Log(PerformList[j].Attacker + " win, " + PerformList[i].Attacker + " lose");
//                        if (PerformList[i].AttackGameObject == GameObject.FindWithTag("Player") &&
//                            PerformList[i].AttackGameObject.GetComponent<Player_statemachine>().player.LeftHand_state == true)
//                        {
//                            PerformList[i].AttackGameObject.GetComponent<Player_statemachine>().player.LeftHand_state = false;
//                            try
//                            {
//                                p1left.SetActive(false);
//                                p1rock2.SetActive(false);
//                                p1paper2.SetActive(false);
//                                p1scissors2.SetActive(false);
//                                leftP.SetActive(false);
//                                leftR.SetActive(false);
//                                leftS.SetActive(false);
//                            }
//                            catch (Exception e)
//                            {
//                                Debug.Log("already removed");
//                            }

//                        }
//                        else
//                        {
//                            if (PerformList[i].Attacker == "e1" && PerformList[i].AttackGameObject.GetComponent<Emeny_AIstatemachine>().enemy.LeftHand_state == true)
//                            {
//                                try
//                                {
//                                    GameObject.Find("e1left").SetActive(false);
//                                    GameObject.Find("e1leftr").SetActive(false);
//                                    GameObject.Find("e1leftp").SetActive(false);
//                                    GameObject.Find("e1lefts").SetActive(false);
//                                    PerformList[i].AttackGameObject.GetComponent<Emeny_AIstatemachine>().enemy.LeftHand_state = false;
//                                }
//                                catch (Exception e)
//                                {
//                                    Debug.Log("already removed");
//                                }
//                            }
//                            else if (PerformList[i].Attacker == "e2" && PerformList[i].AttackGameObject.GetComponent<Emeny_AIstatemachine>().enemy.LeftHand_state == true)
//                            {
//                                try
//                                {
//                                    GameObject.Find("e2left").SetActive(false);
//                                    GameObject.Find("e2leftr").SetActive(false);
//                                    GameObject.Find("e2leftp").SetActive(false);
//                                    GameObject.Find("e2lefts").SetActive(false);
//                                    PerformList[i].AttackGameObject.GetComponent<Emeny_AIstatemachine>().enemy.LeftHand_state = false;
//                                }
//                                catch (Exception e)
//                                {
//                                    Debug.Log("already removed");
//                                }
//                            }
//                            else if (PerformList[i].Attacker == "e3" && PerformList[i].AttackGameObject.GetComponent<Emeny_AIstatemachine>().enemy.RightHand_state == true)
//                            {
//                                try
//                                {
//                                    GameObject.Find("e3left").SetActive(false);
//                                    GameObject.Find("e3leftr").SetActive(false);
//                                    GameObject.Find("e3leftp").SetActive(false);
//                                    GameObject.Find("e3lefts").SetActive(false);
//                                    PerformList[i].AttackGameObject.GetComponent<Emeny_AIstatemachine>().enemy.LeftHand_state = false;
//                                }
//                                catch (Exception e)
//                                {
//                                    Debug.Log("already removed");
//                                }
//                            }
//                            //PerformList[i].AttackGameObject.GetComponent<Emeny_AIstatemachine>().enemy.LeftHand_state = false;
//                        }
//                    }
//                    else
//                    {
//                        Debug.Log(PerformList[i].Attacker + " win, " + PerformList[j].Attacker + " lose");
//                        if (PerformList[j].AttackGameObject == GameObject.FindWithTag("Player") && PerformList[j].AttackGameObject.GetComponent<Player_statemachine>().player.LeftHand_state == true)
//                        {
//                            PerformList[j].AttackGameObject.GetComponent<Player_statemachine>().player.LeftHand_state = false;
//                            try
//                            {
//                                p1left.SetActive(false);
//                                p1rock.SetActive(false);
//                                p1paper.SetActive(false);
//                                p1scissors.SetActive(false);
//                                leftR.SetActive(false);
//                                leftP.SetActive(false);
//                                leftS.SetActive(false);
//                            }
//                            catch (Exception e)
//                            {
//                                Debug.Log("already removed");
//                            }
//                        }
//                        else
//                        {
//                            if (PerformList[j].Attacker == "e1" && PerformList[j].AttackGameObject.GetComponent<Emeny_AIstatemachine>().enemy.LeftHand_state == true)
//                            {
//                                try
//                                {
//                                    GameObject.Find("e1left").SetActive(false);
//                                    GameObject.Find("e1leftr").SetActive(false);
//                                    GameObject.Find("e1lefts").SetActive(false);
//                                    GameObject.Find("e1leftp").SetActive(false);
//                                    PerformList[j].AttackGameObject.GetComponent<Emeny_AIstatemachine>().enemy.LeftHand_state = false;
//                                }
//                                catch (Exception e)
//                                {
//                                    Debug.Log("already removed");
//                                }
//                            }
//                            else if (PerformList[j].Attacker == "e2" && PerformList[j].AttackGameObject.GetComponent<Emeny_AIstatemachine>().enemy.LeftHand_state == true)
//                            {
//                                try
//                                {
//                                    GameObject.Find("e2left").SetActive(false);
//                                    GameObject.Find("e2leftr").SetActive(false);
//                                    GameObject.Find("e2lefts").SetActive(false);
//                                    GameObject.Find("e2leftp").SetActive(false);
//                                    PerformList[j].AttackGameObject.GetComponent<Emeny_AIstatemachine>().enemy.LeftHand_state = false;
//                                }
//                                catch (Exception e)
//                                {
//                                    Debug.Log("already removed");
//                                }
//                            }
//                            else if (PerformList[j].Attacker == "e3" && PerformList[j].AttackGameObject.GetComponent<Emeny_AIstatemachine>().enemy.LeftHand_state == true)
//                            {
//                                try
//                                {
//                                    GameObject.Find("e3left").SetActive(false);
//                                    GameObject.Find("e3leftr").SetActive(false);
//                                    GameObject.Find("e3lefts").SetActive(false);
//                                    GameObject.Find("e3leftp").SetActive(false);
//                                    PerformList[j].AttackGameObject.GetComponent<Emeny_AIstatemachine>().enemy.LeftHand_state = false;
//                                }
//                                catch (Exception e)
//                                {
//                                    Debug.Log("already removed");
//                                }
//                            }
//                            //PerformList[j].AttackGameObject.GetComponent<Emeny_AIstatemachine>().enemy.LeftHand_state = false;
//                        }
//                    }
//                    if (resultRight == 1)
//                    {
//                        Debug.Log(PerformList[j].Attacker + " win, " + PerformList[i].Attacker + " lose");
//                        if (PerformList[i].AttackGameObject == GameObject.FindWithTag("Player") &&
//                            PerformList[i].AttackGameObject.GetComponent<Player_statemachine>().player.RightHand_state == true)
//                        {
//                            PerformList[i].AttackGameObject.GetComponent<Player_statemachine>().player.RightHand_state = false;
//                            try
//                            {
//                                p1right.SetActive(false);
//                                p1rock.SetActive(false);
//                                p1paper.SetActive(false);
//                                p1scissors.SetActive(false);
//                                rightR.SetActive(false);
//                                rightP.SetActive(false);
//                                rightS.SetActive(false);
//                            }
//                            catch (Exception e)
//                            {
//                                Debug.Log("already removed");
//                            }
//                        }
//                        else
//                        {
//                            if (PerformList[i].Attacker == "e1" && PerformList[i].AttackGameObject.GetComponent<Emeny_AIstatemachine>().enemy.RightHand_state == true)
//                            {
//                                try
//                                {
//                                    GameObject.Find("e1right").SetActive(false);
//                                    GameObject.Find("e1rightr").SetActive(false);
//                                    GameObject.Find("e1rights").SetActive(false);
//                                    GameObject.Find("e1rightp").SetActive(false);
//                                    PerformList[i].AttackGameObject.GetComponent<Emeny_AIstatemachine>().enemy.RightHand_state = false;
//                                }
//                                catch (Exception e)
//                                {
//                                    Debug.Log("already removed");
//                                }
//                            }
//                            else if (PerformList[i].Attacker == "e2" && PerformList[i].AttackGameObject.GetComponent<Emeny_AIstatemachine>().enemy.RightHand_state == true)
//                            {
//                                try
//                                {
//                                    GameObject.Find("e2right").SetActive(false);
//                                    GameObject.Find("e2rightr").SetActive(false);
//                                    GameObject.Find("e2rights").SetActive(false);
//                                    GameObject.Find("e2rightp").SetActive(false);
//                                    PerformList[i].AttackGameObject.GetComponent<Emeny_AIstatemachine>().enemy.RightHand_state = false;
//                                }
//                                catch (Exception e)
//                                {
//                                    Debug.Log("already removed");
//                                }
//                            }
//                            else if (PerformList[i].Attacker == "e3" && PerformList[i].AttackGameObject.GetComponent<Emeny_AIstatemachine>().enemy.RightHand_state == true)
//                            {
//                                try
//                                {
//                                    GameObject.Find("e3right").SetActive(false);
//                                    GameObject.Find("e3rightr").SetActive(false);
//                                    GameObject.Find("e3rights").SetActive(false);
//                                    GameObject.Find("e3rightp").SetActive(false);
//                                    PerformList[i].AttackGameObject.GetComponent<Emeny_AIstatemachine>().enemy.RightHand_state = false;
//                                }
//                                catch (Exception e)
//                                {
//                                    Debug.Log("already removed");
//                                }
//                            }
//                            //PerformList[i].AttackGameObject.GetComponent<Emeny_AIstatemachine>().enemy.RightHand_state = false;
//                        }
//                    }
//                    else
//                    {
//                        Debug.Log(PerformList[i].Attacker + " win, " + PerformList[j].Attacker + " lose");
//                        if (PerformList[j].AttackGameObject == GameObject.FindWithTag("Player") &&
//                            PerformList[j].AttackGameObject.GetComponent<Player_statemachine>().player.RightHand_state == true)
//                        {
//                            PerformList[j].AttackGameObject.GetComponent<Player_statemachine>().player.RightHand_state = false;
//                            try
//                            {
//                                p1right.SetActive(false);
//                                p1rock2.SetActive(false);
//                                p1paper2.SetActive(false);
//                                p1scissors2.SetActive(false);
//                                rightP.SetActive(false);
//                                rightR.SetActive(false);
//                                rightS.SetActive(false);
//                            }
//                            catch (Exception e)
//                            {
//                                Debug.Log("already removed");
//                            }
//                        }
//                        else
//                        {
//                            if (PerformList[j].Attacker == "e1" && PerformList[j].AttackGameObject.GetComponent<Emeny_AIstatemachine>().enemy.RightHand_state == true)
//                            {
//                                try
//                                {
//                                    GameObject.Find("e1right").SetActive(false);
//                                    GameObject.Find("e1rightr").SetActive(false);
//                                    GameObject.Find("e1rightp").SetActive(false);
//                                    GameObject.Find("e1rights").SetActive(false);
//                                    PerformList[j].AttackGameObject.GetComponent<Emeny_AIstatemachine>().enemy.RightHand_state = false;
//                                }
//                                catch (Exception e)
//                                {
//                                    Debug.Log("already removed");
//                                }
//                            }
//                            else if (PerformList[j].Attacker == "e2" && PerformList[j].AttackGameObject.GetComponent<Emeny_AIstatemachine>().enemy.RightHand_state == true)
//                            {
//                                try
//                                {
//                                    GameObject.Find("e2right").SetActive(false);
//                                    GameObject.Find("e2rightr").SetActive(false);
//                                    GameObject.Find("e2rightp").SetActive(false);
//                                    GameObject.Find("e2rights").SetActive(false);
//                                    PerformList[j].AttackGameObject.GetComponent<Emeny_AIstatemachine>().enemy.RightHand_state = false;
//                                }
//                                catch (Exception e)
//                                {
//                                    Debug.Log("already removed");
//                                }
//                            }
//                            else if (PerformList[j].Attacker == "e3" && PerformList[j].AttackGameObject.GetComponent<Emeny_AIstatemachine>().enemy.RightHand_state == true)
//                            {
//                                try
//                                {
//                                    GameObject.Find("e3right").SetActive(false);
//                                    GameObject.Find("e3rightr").SetActive(false);
//                                    GameObject.Find("e3rightp").SetActive(false);
//                                    GameObject.Find("e3rights").SetActive(false);
//                                    PerformList[j].AttackGameObject.GetComponent<Emeny_AIstatemachine>().enemy.RightHand_state = false;
//                                }
//                                catch (Exception e)
//                                {
//                                    Debug.Log("already removed");
//                                }
//                            }
//                            //PerformList[j].AttackGameObject.GetComponent<Emeny_AIstatemachine>().enemy.RightHand_state = false;
//                        }
//                    }
//                }
//            }
//        }
//        //for(int i = 0; i < PerformList.Count; i++)
//        //{
//        //    if (PerformList[i].AttackGameObject != GameObject.FindWithTag("Player"))
//        //    {
//        //        if(PerformList[i].AttackGameObject.GetComponent<Emeny_AIstatemachine>().enemy.RightHand_state == false&& PerformList[i].AttackGameObject.GetComponent<Emeny_AIstatemachine>().enemy.RightHand_state == false)
//        //        {
//        //            PerformList.Remove(PerformList[i].AttackGameObject.GetComponent<Emeny_AIstatemachine>().myAttack);
//        //        }

//        //    }
//        //    else
//        //    {
//        //        if (PerformList[i].AttackGameObject.GetComponent<Player_statemachine>().player.RightHand_state == false && PerformList[i].AttackGameObject.GetComponent<Emeny_AIstatemachine>().enemy.RightHand_state == false)
//        //        {
//        //            PerformList.Remove(playerChoice);
//        //        }

//        //    }

//        //}

//        Debug.Log("Battle End");
//    }

//    public static int howToWin(HandleTurn.janken source, HandleTurn.janken target)
//    {
//        if (source == HandleTurn.janken.PAPER && target == HandleTurn.janken.ROCK)
//        {
//            return 1;
//        }
//        else if (source == HandleTurn.janken.ROCK && target == HandleTurn.janken.SCISSORS)
//        {
//            return 1;
//        }
//        else if (source == HandleTurn.janken.SCISSORS && target == HandleTurn.janken.PAPER)
//        {
//            return 1;
//        }
//        return 0;
//    }
//}
//=======
	public void battleLogic() {
		Debug.Log("Battle Start");
		for (int i = 0; i < PerformList.Count; i++) {
			for (int j = 1; j < PerformList.Count; j++) {
				if (PerformList[j].AttackTarget == PerformList[i].AttackGameObject) {
					int resultLeft = howToWin(PerformList[j].LeftAttackType, PerformList[i].LeftAttackType);
					int resultRight = howToWin(PerformList[j].RightAttackType, PerformList[i].RightAttackType);
					// enemy sprite's change
					if (PerformList[i].Attacker == "e1")
					{
						if (PerformList[i].LeftAttackType == HandleTurn.janken.ROCK)
						{
							e1rock.SetActive(true);
							e1scissors.SetActive(false);
							e1paper.SetActive(false);
						}
						else if (PerformList[i].LeftAttackType == HandleTurn.janken.PAPER)
						{
							e1rock.SetActive(false);
							e1scissors.SetActive(false);
							e1paper.SetActive(true);
						}
						else if (PerformList[i].LeftAttackType == HandleTurn.janken.SCISSORS)
						{
							e1rock.SetActive(false);
							e1scissors.SetActive(true);
							e1paper.SetActive(false);
						}
						else if (PerformList[i].RightAttackType == HandleTurn.janken.ROCK)
						{
							e1rock2.SetActive(true);
							e1scissors2.SetActive(false);
							e1paper2.SetActive(false);
						}
						else if (PerformList[i].RightAttackType == HandleTurn.janken.PAPER)
						{
							e1rock2.SetActive(false);
							e1scissors2.SetActive(false);
							e1paper2.SetActive(true);
						}
						else if (PerformList[i].RightAttackType == HandleTurn.janken.SCISSORS)
						{
							e1rock2.SetActive(false);
							e1scissors2.SetActive(true);
							e1paper2.SetActive(false);
						}
					}
					else if (PerformList[i].Attacker == "e2")
					{
						if (PerformList[i].LeftAttackType == HandleTurn.janken.ROCK)
						{
							e2rock.SetActive(true);
							e2scissors.SetActive(false);
							e2paper.SetActive(false);
						}
						else if (PerformList[i].LeftAttackType == HandleTurn.janken.PAPER)
						{
							e2rock.SetActive(false);
							e2scissors.SetActive(false);
							e2paper.SetActive(true);
						}
						else if (PerformList[i].LeftAttackType == HandleTurn.janken.SCISSORS)
						{
							e2rock.SetActive(false);
							e2scissors.SetActive(true);
							e2paper.SetActive(false);
						}
						else if (PerformList[i].RightAttackType == HandleTurn.janken.ROCK)
						{
							e2rock2.SetActive(true);
							e2scissors2.SetActive(false);
							e2paper2.SetActive(false);
						}
						else if (PerformList[i].RightAttackType == HandleTurn.janken.PAPER)
						{
							e2rock2.SetActive(false);
							e2scissors2.SetActive(false);
							e2paper2.SetActive(true);
						}
						else if (PerformList[i].RightAttackType == HandleTurn.janken.SCISSORS)
						{
							e2rock2.SetActive(false);
							e2scissors2.SetActive(true);
							e2paper2.SetActive(false);
						}
					}
					else if (PerformList[i].Attacker == "e3")
					{
						if (PerformList[i].LeftAttackType == HandleTurn.janken.ROCK)
						{
							e3rock.SetActive(true);
							e3scissors.SetActive(false);
							e3paper.SetActive(false);
						}
						else if (PerformList[i].LeftAttackType == HandleTurn.janken.PAPER)
						{
							e3rock.SetActive(false);
							e3scissors.SetActive(false);
							e3paper.SetActive(true);
						}
						else if (PerformList[i].LeftAttackType == HandleTurn.janken.SCISSORS)
						{
							e3rock.SetActive(false);
							e3scissors.SetActive(true);
							e3paper.SetActive(false);
						}
						else if (PerformList[i].RightAttackType == HandleTurn.janken.ROCK)
						{
							e3rock2.SetActive(true);
							e3scissors2.SetActive(false);
							e3paper2.SetActive(false);
						}
						else if (PerformList[i].RightAttackType == HandleTurn.janken.PAPER)
						{
							e3rock2.SetActive(false);
							e3scissors2.SetActive(false);
							e3paper2.SetActive(true);
						}
						else if (PerformList[i].RightAttackType == HandleTurn.janken.SCISSORS)
						{
							e3rock2.SetActive(false);
							e3scissors2.SetActive(true);
							e3paper2.SetActive(false);
						}
					}
					if (PerformList[j].Attacker == "e1")
					{
						if (PerformList[j].LeftAttackType == HandleTurn.janken.ROCK)
						{
							e1rock.SetActive(true);
							e1scissors.SetActive(false);
							e1paper.SetActive(false);
						}
						else if (PerformList[j].LeftAttackType == HandleTurn.janken.PAPER)
						{
							e1rock.SetActive(false);
							e1scissors.SetActive(false);
							e1paper.SetActive(true);
						}
						else if (PerformList[j].LeftAttackType == HandleTurn.janken.SCISSORS)
						{
							e1rock.SetActive(false);
							e1scissors.SetActive(true);
							e1paper.SetActive(false);
						}
						else if (PerformList[j].RightAttackType == HandleTurn.janken.ROCK)
						{
							e1rock2.SetActive(true);
							e1scissors2.SetActive(false);
							e1paper2.SetActive(false);
						}
						else if (PerformList[j].RightAttackType == HandleTurn.janken.PAPER)
						{
							e1rock2.SetActive(false);
							e1scissors2.SetActive(false);
							e1paper2.SetActive(true);
						}
						else if (PerformList[j].RightAttackType == HandleTurn.janken.SCISSORS)
						{
							e1rock2.SetActive(false);
							e1scissors2.SetActive(true);
							e1paper2.SetActive(false);
						}
					}
					else if (PerformList[j].Attacker == "e2")
					{
						if (PerformList[j].LeftAttackType == HandleTurn.janken.ROCK)
						{
							e2rock.SetActive(true);
							e2scissors.SetActive(false);
							e2paper.SetActive(false);
						}
						else if (PerformList[j].LeftAttackType == HandleTurn.janken.PAPER)
						{
							e2rock.SetActive(false);
							e2scissors.SetActive(false);
							e2paper.SetActive(true);
						}
						else if (PerformList[j].LeftAttackType == HandleTurn.janken.SCISSORS)
						{
							e2rock.SetActive(false);
							e2scissors.SetActive(true);
							e2paper.SetActive(false);
						}
						else if (PerformList[j].RightAttackType == HandleTurn.janken.ROCK)
						{
							e2rock2.SetActive(true);
							e2scissors2.SetActive(false);
							e2paper2.SetActive(false);
						}
						else if (PerformList[j].RightAttackType == HandleTurn.janken.PAPER)
						{
							e2rock2.SetActive(false);
							e2scissors2.SetActive(false);
							e2paper2.SetActive(true);
						}
						else if (PerformList[j].RightAttackType == HandleTurn.janken.SCISSORS)
						{
							e2rock2.SetActive(false);
							e2scissors2.SetActive(true);
							e2paper2.SetActive(false);
						}
					}
					else if (PerformList[j].Attacker == "e3")
					{
						if (PerformList[j].LeftAttackType == HandleTurn.janken.ROCK)
						{
							e3rock.SetActive(true);
							e3scissors.SetActive(false);
							e3paper.SetActive(false);
						}
						else if (PerformList[j].LeftAttackType == HandleTurn.janken.PAPER)
						{
							e3rock.SetActive(false);
							e3scissors.SetActive(false);
							e3paper.SetActive(true);
						}
						else if (PerformList[j].LeftAttackType == HandleTurn.janken.SCISSORS)
						{
							e3rock.SetActive(false);
							e3scissors.SetActive(true);
							e3paper.SetActive(false);
						}
						else if (PerformList[j].RightAttackType == HandleTurn.janken.ROCK)
						{
							e3rock2.SetActive(true);
							e3scissors2.SetActive(false);
							e3paper2.SetActive(false);
						}
						else if (PerformList[j].RightAttackType == HandleTurn.janken.PAPER)
						{
							e3rock2.SetActive(false);
							e3scissors2.SetActive(false);
							e3paper2.SetActive(true);
						}
						else if (PerformList[j].RightAttackType == HandleTurn.janken.SCISSORS)
						{
							e3rock2.SetActive(false);
							e3scissors2.SetActive(true);
							e3paper2.SetActive(false);
						}
					}
					// Battle result and sprite's destruction
					if (resultLeft == 1) {
						Debug.Log(PerformList[j].Attacker + " win, " + PerformList[i].Attacker + " lose");
						if (PerformList[i].AttackGameObject == GameObject.FindWithTag("Player") &&
							PerformList[i].AttackGameObject.GetComponent<Player_statemachine>().player.LeftHand_state == true)
						{
							PerformList[i].AttackGameObject.GetComponent<Player_statemachine>().player.LeftHand_state = false;
							try
							{
								p1left.SetActive(false);
								p1rock.SetActive(false);
								p1paper.SetActive(false);
								p1scissors.SetActive(false);
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
									GameObject.Find("e1left").SetActive(false);
									GameObject.Find("e1leftr").SetActive(false);
									GameObject.Find("e1leftp").SetActive(false);
									GameObject.Find("e1lefts").SetActive(false);
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
									GameObject.Find("e2left").SetActive(false);
									GameObject.Find("e2leftr").SetActive(false);
									GameObject.Find("e2leftp").SetActive(false);
									GameObject.Find("e2lefts").SetActive(false);
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
									GameObject.Find("e3left").SetActive(false);
									GameObject.Find("e3leftr").SetActive(false);
									GameObject.Find("e3leftp").SetActive(false);
									GameObject.Find("e3lefts").SetActive(false);
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
								p1left.SetActive(false);
								p1rock.SetActive(false);
								p1paper.SetActive(false);
								p1scissors.SetActive(false);
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
									GameObject.Find("e1left").SetActive(false);
									GameObject.Find("e1leftr").SetActive(false);
									GameObject.Find("e1lefts").SetActive(false);
									GameObject.Find("e1leftp").SetActive(false);
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
									GameObject.Find("e2left").SetActive(false);
									GameObject.Find("e2leftr").SetActive(false);
									GameObject.Find("e2lefts").SetActive(false);
									GameObject.Find("e2leftp").SetActive(false);
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
									GameObject.Find("e3left").SetActive(false);
									GameObject.Find("e3leftr").SetActive(false);
									GameObject.Find("e3lefts").SetActive(false);
									GameObject.Find("e3leftp").SetActive(false);
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
								p1right.SetActive(false);
								p1rock2.SetActive(false);
								p1paper2.SetActive(false);
								p1scissors2.SetActive(false);
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
									GameObject.Find("e1right").SetActive(false);
									GameObject.Find("e1rightr").SetActive(false);
									GameObject.Find("e1rights").SetActive(false);
									GameObject.Find("e1rightp").SetActive(false);
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
									GameObject.Find("e2right").SetActive(false);
									GameObject.Find("e2rightr").SetActive(false);
									GameObject.Find("e2rights").SetActive(false);
									GameObject.Find("e2rightp").SetActive(false);
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
									GameObject.Find("e3right").SetActive(false);
									GameObject.Find("e3rightr").SetActive(false);
									GameObject.Find("e3rights").SetActive(false);
									GameObject.Find("e3rightp").SetActive(false);
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
								p1right.SetActive(false);
								p1rock2.SetActive(false);
								p1paper2.SetActive(false);
								p1scissors2.SetActive(false);
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
									GameObject.Find("e1right").SetActive(false);
									GameObject.Find("e1rightr").SetActive(false);
									GameObject.Find("e1rightp").SetActive(false);
									GameObject.Find("e1rights").SetActive(false);
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
									GameObject.Find("e2right").SetActive(false);
									GameObject.Find("e2rightr").SetActive(false);
									GameObject.Find("e2rightp").SetActive(false);
									GameObject.Find("e2rights").SetActive(false);
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
									GameObject.Find("e3right").SetActive(false);
									GameObject.Find("e3rightr").SetActive(false);
									GameObject.Find("e3rightp").SetActive(false);
									GameObject.Find("e3rights").SetActive(false);
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

	}

