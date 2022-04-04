using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BossController : MonoBehaviour
{

    // Animator
    private Animator anim;

    // Player
    public Transform player;

    // LayerMask
    [SerializeField] LayerMask whatIsPlayer;

    // States
    public float attackRange = 20, timeBetweenAttacks = 5, attackTime = 0;
    public bool playerInAttackRange, alreadyAttacked;
    public int attackCount = 0;

    // WeaponController
    public BossWeaponController weaponController;

    // Health Manager
    public BossHealthManager bossHealth;

    //Path
    List<Node> path = new List<Node>();
    public PathFinding pathFounder;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);

        if (playerInAttackRange == true)
        {
            if(attackCount == 100)
            {
                Chase();
            }
            else
            {
                Attack();
            }
            //Debug.Log("Attack");
        }else
        {
            Chase();
            //Debug.Log("Chase");
        }

        if (bossHealth.health <= 0)
        {
            anim.SetBool("Die", true);
        }
    }

    private void Chase()
    {
        anim.SetBool("Running", true);
        anim.SetBool("Shooting", false);

        GetPath(player);
        transform.LookAt(player);
        Vector3 walkTo = new Vector3(path[0].worldPosition.x, transform.position.y, path[0].worldPosition.z);
        transform.position = Vector3.Lerp(transform.position, walkTo, Time.deltaTime);
    }

    public void GetPath(Transform target)
    {
        path = pathFounder.FindPath(transform.position, target.position);
    }

    private void Attack()
    {
        anim.SetBool("Running", false);
        anim.SetBool("Shooting", true);

        transform.LookAt(player);
        if(alreadyAttacked == false)
        {
            Burst();

            attackTime += Time.deltaTime;
            if(attackTime >= 5)
            {
                alreadyAttacked = true;
                Invoke(nameof(ResetAttack), timeBetweenAttacks);
            }
        }
    }

    void Burst()
    {
        //if(attackTime % 0.5 == 0)
        //{
            weaponController.Fire();
        //}
    }

    public void ResetAttack()
    {
        alreadyAttacked = false;
        attackCount = 0;
        attackTime = 0;
    }
}
