using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSceneController : MonoBehaviour
{
    [SerializeField] MainCharacterMovement playerMove;
    [SerializeField] WeaponManager rifle, pistol;

    public void Freeze()
    {
        playerMove.enabled = false;
        rifle.enabled = false;
        pistol.enabled = false;
    }

    public void Release()
    {
        playerMove.enabled = true;
        rifle.enabled = true;
        pistol.enabled = true;
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
