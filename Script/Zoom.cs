using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class Zoom : MonoBehaviour
{
    bool Zoomed;
    [SerializeField] CinemachineVirtualCamera vCam;
    public float pov;

    // Start is called before the first frame update
    void Start()
    {
        pov = vCam.m_Lens.FieldOfView;
    }

    // Update is called once per frame
    void Update()
    {
        Zoomed = Input.GetMouseButton(1);
        if(Zoomed){
            vCam.m_Lens.FieldOfView = pov - 7;
        } else {
            vCam.m_Lens.FieldOfView = pov;
        }
    }
}
