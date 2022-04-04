using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportEnterTrigger : MonoBehaviour
{

    public GameObject questCanvas, actionCanvas;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(TimerScript.isActive == false)
        {
            QuestController.questIdx = 6;
            TimerScript.isActive = true;

            questCanvas.SetActive(false);
            actionCanvas.SetActive(false);
        }
    }
}
