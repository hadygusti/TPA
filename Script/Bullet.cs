using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    [SerializeField] float timeToDestroy;
    float timer;
    public GameObject impact;
    Camera cam;


    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if(timer >= timeToDestroy){
            Destroy(this.gameObject);
        }
    }

    private void OnCollisionEnter(Collision collider){
        Destroy(this.gameObject);

        // Debug.Log(collider.gameObject.name);
        //if (QuestController.questIdx == 4) QuestController.bulletCount++;

        RaycastHit hit;

        if (Physics.Raycast(cam.transform.position, cam.transform.forward, out hit, Mathf.Infinity))
        {
            GameObject imp = Instantiate(impact, hit.point + new Vector3(0f, 0f, -.02f), Quaternion.LookRotation(-hit.normal));
        }

        if (collider.gameObject.tag == "Player"){
            PlayerHealthManager playerHealth = collider.transform.GetComponent<PlayerHealthManager>();
            playerHealth.getShot();
            // Debug.Log("aduh sakit");

        } else if (collider.gameObject.tag == "Soldier"){
            SoldierHealthManager soldierHealth = collider.transform.GetComponent<SoldierHealthManager>();
            soldierHealth.getShot(10);
            // Debug.Log("kena");
        } else if (collider.gameObject.tag == "Target")
        {
            if (QuestController.questIdx == 2) QuestController.targetCount++;
            //Debug.Log("Target Hitted");
        } else if (collider.gameObject.tag == "Boss")
        {
            BossHealthManager bossHealth = collider.transform.GetComponent<BossHealthManager>();
            Debug.Log(bossHealth);
            bossHealth.GetShot(50);
        }

        //print(collider.gameObject.tag);
    }
}
