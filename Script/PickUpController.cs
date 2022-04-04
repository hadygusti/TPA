using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpController : MonoBehaviour
{

    public WeaponManager weaponManager, secondary;
    public Rigidbody rb;
    public BoxCollider coll;
    public Transform player, gunContainer, camera, aimPos, backContainer;
    public ActiveWeapon activeWeapon;
    public ActionStateManager actionState;
    public AimController aim;

    public GameObject actionCanvas;

    public static float pickUpRange = 3, aimPosRange = 0.5f, dropForwardForce = 3, dropUpwardForce = 2;

    public bool equipped;
    public static bool slotFull;

    // Start is called before the first frame update
    void Start()
    {
        //if(!equipped){
            weaponManager.enabled = false;
            rb.isKinematic = false;
            coll.isTrigger = false;
        //} else {
        //    weaponManager.enabled = true;
        //    rb.isKinematic = true;
        //    coll.isTrigger = true;
        //}
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 distanceToPlayer = player.position - transform.position;

        if (shouldPickUp() && Input.GetKeyDown(KeyCode.F)) PickUp();

        if (equipped && Input.GetKeyDown(KeyCode.E)) Drop();

        if (shouldPickUp()) actionCanvas.SetActive(true);
        else actionCanvas.SetActive(false);

        // Debug.Log(weaponManager.enabled);
        // Debug.Log(this.gameObject);
        // Debug.Log(aim);
    }

    private bool shouldPickUp()
    {
        Vector3 distanceToPlayer = player.position - transform.position;
        Vector3 distanceToAim = aimPos.position - transform.position;
        if (equipped) return false;
        if (distanceToPlayer.magnitude > pickUpRange) return false;
        if (distanceToAim.magnitude > aimPosRange) return false;
        return true;
    }

    public void PickUp()
    {

        if (secondary.pickUpController.equipped)
        {
            //secondary.transform.SetParent(backContainer);
            //secondary.transform.localPosition = new Vector3(0, 0, 0.1f);
            //secondary.enabled = false;
            activeWeapon.Holster(secondary);
        }

        equipped = true;
        slotFull = true;

        Debug.Log("picked");
        activeWeapon.Equip(weaponManager);
        //actionState.currentWeapon = this.gameObject;
        Debug.Log(actionState);
        actionState.setWeapon(this.gameObject);

        //Make weapon a child of the container and move it to default position
        transform.SetParent(gunContainer);
        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.Euler(Vector3.zero);
        transform.localScale = Vector3.one;

        //Make RigidBody kinematic and BoxCollider a trigger
        rb.isKinematic = true;
        coll.isTrigger = true;

        //Enable Script
        weaponManager.enabled = true;

        if (QuestController.questIdx == 1 && weaponManager.weaponName == "Pistol") QuestController.status = true;
        if (QuestController.questIdx == 3 && weaponManager.weaponName == "Rifle") QuestController.status = true;
    }

    private void Drop()
    {
        equipped = false;
        slotFull = false;

        transform.SetParent(null);

        //Make RigidBody kinematic and BoxCollider a trigger
        rb.isKinematic = false;
        coll.isTrigger = false;

        //Gun carries momentum of the player
        rb.velocity = player.GetComponent<Rigidbody>().velocity;

        Debug.Log("dropped");
        //AddForce
        rb.AddForce(camera.forward * dropForwardForce, ForceMode.Impulse);
        rb.AddForce(camera.up * dropUpwardForce, ForceMode.Impulse);

        //Add random rotation

        //Disable Script
        if(weaponManager.weaponName == "Rifle"){
            activeWeapon.rifleEquipped = false;
        } else if(weaponManager.weaponName == "Pistol"){
            activeWeapon.pistolEquipped = false;
        }

        //Change Current Weapon

        activeWeapon.rigController.Play("Unarmed");
        activeWeapon.weapon = null;
        weaponManager.enabled = false;
    }
}
