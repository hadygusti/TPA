using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PistolAmmoPickUpController : MonoBehaviour
{

    public WeaponAmmo pistolAmmo;
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
        if (ShouldPickUp() && Input.GetKeyDown(KeyCode.F))
        {
            actionCanvas.SetActive(false);
            PickUp();
        }

        if (ShouldPickUp()) actionCanvas.SetActive(true);
        else actionCanvas.SetActive(false);
    }

    public bool ShouldPickUp()
    {
        Vector3 distanceToPlayer = player.position - transform.position;
        Vector3 distanceToAim = aimPos.position - transform.position;

        if (distanceToPlayer.magnitude > pickUpRange) return false;
        if (Physics.CheckSphere(aimPos.position, aimPosRange, ammoLayer) == false) return false;

        Debug.Log("canpickUpAmmo");
        return true;
    }

    public void PickUp()
    {
        Debug.Log("pluru masuk");
        actionCanvas.SetActive(false);
        pistolAmmo.extraAmmo += 7;
        Destroy(this.gameObject);
    }
}
