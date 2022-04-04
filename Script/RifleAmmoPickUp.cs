using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RifleAmmoPickUp : MonoBehaviour
{
    public WeaponAmmo rifleAmmo;
    public Transform aimPos, player;
    [SerializeField] LayerMask ammoLayer;
    public static float pickUpRange = 3, aimPosRange = 0.05f;

    public GameObject actionCanvas;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Vector3 distanceToPlayer = player.position - transform.position;
        if (ShouldPickUp() && Input.GetKeyDown(KeyCode.F)) PickUp();

        if (ShouldPickUp()) actionCanvas.SetActive(true);
        else actionCanvas.SetActive(false);
    }

    public bool ShouldPickUp()
    {
        Vector3 distanceToPlayer = player.position - transform.position;
        Vector3 distanceToAim = aimPos.position - transform.position;
        //Debug.Log("p");
        if (distanceToPlayer.magnitude > pickUpRange) return false;
        Debug.Log("Here");
        if (Physics.CheckSphere(aimPos.position, aimPosRange, ammoLayer) == true) return false;


        Debug.Log("canpickUpAmmo");
        return true;
    }

    public void PickUp()
    {
        actionCanvas.SetActive(false);
        Destroy(this.gameObject);
        rifleAmmo.extraAmmo += 30;
    }
}
