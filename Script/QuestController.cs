using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class QuestController : MonoBehaviour
{

    List<string> questList = new List<string>();

    [SerializeField] public TextMeshProUGUI counterDisplay, statusDisplay, questDisplay;

    public static int questIdx = 0;
    public static bool status;
    public static int bulletCount = 0, targetCount = 0, killCount = 0;
    public int counter;

    public PickUpController pistolPickUp, riflePickUp;

    // Check Boss
    public bool isBossStage;

    // Timer
    //public TimerScript timer;

    // Start is called before the first frame update
    void Start()
    {
        pistolPickUp.enabled = false;
        riflePickUp.enabled = false;
        questList.Add("Talk To Asuna");
        questList.Add("Take Pistol");
        questList.Add("Hit 10 targets");
        questList.Add("Shoot 30 bullets with the rifle");
        questList.Add("Kill all enemies in the village");
        questList.Add("Proceed to the teleporter");
        questList.Add("Kill all the enemies before the timer runs out");
        status = false;
        isBossStage = false;
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(status);
        questDisplay.text = questList[questIdx];

        if (!status) statusDisplay.text = "Not Done";
        else statusDisplay.text = "Done";

        if(questIdx == 1)
        {
            pistolPickUp.enabled = true;
        }
        else if(questIdx == 2)
        {
            if(targetCount >= 10)
            {
                counterDisplay.text = ("10/10");
                status = true;
            }
            else
            {
                counterDisplay.text = (targetCount.ToString() + "/10");
            }
        } 
        else if (questIdx == 3)
        {
            riflePickUp.enabled = true;
            if (bulletCount >= 30)
            {
                counterDisplay.text = ("30/30");
                status = true;
            }
            else
            {
                counterDisplay.text = (bulletCount.ToString() + "/30");
            }
        }
        else if (questIdx == 4)
        {
            if (killCount >= 16)
            {
                counterDisplay.text = ("16/16");
                status = true;
            }
            else
            {
                counterDisplay.text = (killCount.ToString() + "/16");
            }
        }
        else if (questIdx == 5)
        {
            killCount = 0;
        }
        else if (questIdx == 6)
        {
            if(killCount >= 8)
            {
                counterDisplay.text = ("8/8");
                status = true;
                TimerScript.isActive = false;

                //questIdx = 5;
            }
            else
            {
                counterDisplay.text = (killCount.ToString() + "/8");
            }
        }
    }
}
