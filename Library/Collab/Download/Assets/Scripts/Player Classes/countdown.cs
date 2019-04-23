using System.Collections;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

public class countdown : MonoBehaviour
{

    public float timeLeft = 60.0f;
    public Text counter; // used for showing countdown from 60
    public GameObject counter1;

    void Update()
    {
        timeLeft -= Time.deltaTime;
        counter1 = GameObject.Find("counter");
        counter.text = (timeLeft).ToString("0");
       // if (timeLeft < 0)
       // {
            //Do something useful or Load a new game scene depending on your use-case
       // }
    }
}
