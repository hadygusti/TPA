using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossEnterTrigger : MonoBehaviour
{

    public GameObject bossHealthBar, boss;
    public QuestController questController;


    // Start is called before the first frame update
    void Start()
    {
        bossHealthBar.SetActive(false);
        boss.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        bossHealthBar.SetActive(true);
        boss.SetActive(true);

        questController.PlayBattleMusic();
    }
}
