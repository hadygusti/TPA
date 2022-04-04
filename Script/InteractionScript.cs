using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionScript : MonoBehaviour
{

    public static float pickUpRange = 5, aimPosRange = 1f;
    public Transform player, aimPos;

    public GameObject canvas;

    public QuestController quest;
    public SceneController scene;

    // Start is called before the first frame update
    void Start()
    {
        //scene.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (shouldInteract())
        {
            canvas.SetActive(true);
        }
        else
        {
            canvas.SetActive(false);
        }

        if(shouldInteract() && Input.GetKeyDown(KeyCode.F))
        {
            if (QuestController.questIdx == 0) QuestController.status = true;
            if (QuestController.status)
            {
                QuestController.questIdx++;
                QuestController.status = false;
                quest.counterDisplay.text = "";
            } else
            {

            }
            //scene.gameObject.SetActive(true);
            scene.playScene();
            //scene.gameObject.SetActive(false);
        }
    }

    private bool shouldInteract()
    {
        Vector3 distanceToPlayer = player.position - transform.position;
        Vector3 distanceToAim = aimPos.position - transform.position;
        if (distanceToPlayer.magnitude > pickUpRange) return false;
        if (distanceToAim.magnitude > aimPosRange) return false;
        return true;
    } 
}
