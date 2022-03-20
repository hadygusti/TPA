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

        if(isGrounded && velocity.y < 0){
            velocity.y -= 2f;
        }

        // Input.GetButton("Vertical"); 

        float inputHorizontal = Input.GetAxis("Horizontal");

        anim.SetFloat("straight", Input.GetAxis("Vertical"));
        anim.SetFloat("side", inputHorizontal);

        // if(inputHorizontal == 0){
            float camPos = cam.transform.rotation.eulerAngles.y;
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0 , camPos, 0), turnSpeed * Time.deltaTime);
        // } else {
            // float camPos = cam.transform.rotation.eulerAngles.x;
            // transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0, camPos, 0), turnSpeed * Time.deltaTime);
        // }

        print(cam.transform.rotation.eulerAngles.y);
        print(cam.transform.rotation.eulerAngles.x);


        // if(Input.GetAxis(""))

        velocity.y += gravity * Time.deltaTime;
        Debug.Log(velocity.y);



        controller.Move(velocity * Time.deltaTime);
    }
}
