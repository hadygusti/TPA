using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class teleportController : MonoBehaviour
{
    static bool isActive;
    public GameObject player, actionCanvas;
    public Transform playerPos, tpPos;

    float playerRange = 20f;


    // Start is called before the first frame update
    void Start()
    {
        isActive = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (QuestController.questIdx == 6 && QuestController.status == true)
        {
            QuestController.questIdx = 5;
            isActive = true;
        }

        if (shouldInteract())   
        {
            Debug.Log("TEPE");
            actionCanvas.SetActive(true);

            if (Input.GetKeyDown(KeyCode.F)){
                playerPos.position = tpPos.position;
            }
        } 
        else
        {
            actionCanvas.SetActive(false);
        }
    }

    public bool shouldInteract()
    {
        Vector3 distanceToPlayer = playerPos.position - transform.position;
        if (isActive == false) return false;
        if (distanceToPlayer.magnitude > playerRange) return false;
        return true;
    }
}
