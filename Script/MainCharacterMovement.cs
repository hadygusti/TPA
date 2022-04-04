using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCharacterMovement : MonoBehaviour
{

    private Animator anim;

    [SerializeField] Camera cam;
    [SerializeField] float turnSpeed;

    [SerializeField] LayerMask ground;
    private bool isGrounded;
    private float gravity = -9.8f;
    [SerializeField] private float groundDistance;
    private CharacterController controller;
    private Vector3 velocity;

    private float ySpeed;
    public float jumpSpeed;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        isGrounded = Physics.CheckSphere(transform.position, groundDistance, ground);
        // isGrounded = controller.isGrounded;


        // if(isGrounded && velocity.y < 0){
        //     velocity.y = -0.8f;
        // }

        if(isGrounded && ySpeed < 0){
            // ySpeed = -0.5f;
        }

        float inputHorizontal = Input.GetAxis("Horizontal");
        float inputVertical = Input.GetAxis("Vertical");

        anim.SetFloat("straight", inputVertical);
        anim.SetFloat("side", inputHorizontal);

        float camPos = cam.transform.rotation.eulerAngles.y;
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0 , camPos, 0), turnSpeed * Time.deltaTime);

        bool running = Input.GetKey(KeyCode.LeftShift);

        if(running){
            anim.SetBool("Running", true);
        } else {
            anim.SetBool("Running", false);
        }

        ySpeed += gravity * Time.deltaTime;
        
        if(isGrounded && ySpeed < 0){
            
            ySpeed = -0.8f;

            if(Input.GetButtonDown("Jump")){
                ySpeed = jumpSpeed;
            }
            
        }

        velocity.y = ySpeed;

        controller.Move(velocity * Time.deltaTime);
    }
}
