using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SoldierController : MonoBehaviour
{
    [SerializeField] public Transform player;
    public LayerMask whatIsGround, whatIsPlayer;

    //Attacking
    public float timeBetweenAttacks;
    bool alreadyAttacked;

    //States
    public float sightRange, attackRange;
    public bool playerInSightRange, playerInAttackRange;

    //WeaponController
    SoldierWeaponController weaponController;

    //Animator
    private Animator anim;

    //Path
    List<Node> path = new List<Node>();
    public PathFinding pathFounder;

    //Patrol Target
    public Transform target1, target2, patrolTo;
    float distanceMag;

    // Start is called before the first frame update
    void Start()
    {
        weaponController = GetComponent<SoldierWeaponController>();    
        anim = GetComponent<Animator>();
        patrolTo = target1;
    }

    // Update is called once per frame
    void Update()
    {
        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);

        if(playerInAttackRange){
            Attack();
        } else if (playerInSightRange){
            Chase();
        } else {
            Patroling();
        }
    }


    private void Patroling(){
        anim.SetBool("Running", true);
        anim.SetBool("Shooting", false);

        //if (patrolTo != target1) patrolTo = target1;
        //else patrolTo = target2;

        GetPath(patrolTo);
        //Vector3 walkTo = new Vector3(patrolTo.position.x, transform.position.y, patrolTo.position.z);
        //transform.position = Vector3.Lerp(transform.position, walkTo, 0.8f*Time.deltaTime);
        Vector3 walkTo = new Vector3(path[0].worldPosition.x, transform.position.y, path[0].worldPosition.z);
        transform.position = Vector3.Lerp(transform.position, walkTo, 2f*Time.deltaTime);

        Vector3 lookAtPos = new Vector3(path[0].worldPosition.x, transform.position.y, path[0].worldPosition.z);
        transform.LookAt(lookAtPos);

        Vector3 distance = transform.position - patrolTo.position;
        distanceMag = distance.magnitude;

        if (distanceMag < 1f)
        {
            ChangePatrolPoint();
        }
    }

    private void ChangePatrolPoint()
    {
        if (patrolTo == target1) patrolTo = target2;
        else patrolTo = target1;
        //Debug.Log(patrolTo);
    }

    private void Chase(){
        // soldier.SetDestination(player.position);
        anim.SetBool("Running", true);
        anim.SetBool("Shooting", false);
        transform.LookAt(player);
        GetPath(player);
        Vector3 walkTo = new Vector3(path[0].worldPosition.x, transform.position.y, path[0].worldPosition.z);
        transform.position = Vector3.Lerp(transform.position, walkTo, Time.deltaTime);        
    }

    private void Attack(){
        // soldier.SetDestination(transform.position);

        anim.SetBool("Running", false);
        anim.SetBool("Shooting", true);

        // Transform target = new Transform();
        // target.position = new Vector3(player.position.x, 2f, player.position.z);
        transform.LookAt(player);
        if(!alreadyAttacked){

            //Attack Here
            weaponController.Fire();

            alreadyAttacked = true;
            Invoke(nameof(ResetAttack), timeBetweenAttacks);
        }
    }

    private void ResetAttack(){
        alreadyAttacked = false;
    }

    public void GetPath(Transform target)
    {
        path = pathFounder.FindPath(transform.position, target.position);
    }
}
