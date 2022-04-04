using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveWeapon : MonoBehaviour
{
    WeaponManager equippedWeapon;

    public Animator rigController;
    public bool rifleEquipped, rifleStored = false;
    public bool pistolEquipped, pistolStored = false;

    public Transform backContainer;

    public WeaponManager weapon, storedPistol = null, storedRifle = null;

    public PickUpController pistolPickUp, riflePickUp;

    // Start is called before the first frame update
    void Start()
    {
        equippedWeapon = GetComponentInChildren<WeaponManager>();
        if(equippedWeapon){
            Equip(equippedWeapon);
        }
    }

    // Update is called once per frame
    void Update()
    {
        // if(rifleEquipped){
        //     rigController.Play("equip" + weapon.weaponName);
        // } else if (pistolEquipped){
        //     rigController.Play("equipPistol");
        // } else {
        //     rigController.Play("Unarmed");
        // }

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            if (rifleEquipped)
            {
                Holster(weapon);
            } 
            else
            {
                if (rifleStored)
                {
                    if (pistolEquipped)
                    {
                        Holster(weapon);
                    }
                    Equip(storedRifle);
                    riflePickUp.PickUp();
                }
            }
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            if (pistolEquipped)
            {
                Holster(weapon);
            }
            else
            {
                if (pistolStored)
                {
                    if (rifleEquipped)
                    {
                        Holster(weapon);
                    }
                    Equip(storedPistol);
                    pistolPickUp.PickUp();
                }
            }
        }
    }

    public void Equip(WeaponManager newWeapon){
        if (weapon)
        {
            Holster(weapon);
        }

        weapon = newWeapon;

        

        rigController.Play("equip" + weapon.weaponName);

        if(weapon.weaponName == "Rifle"){
            pistolPickUp.equipped = false;
            riflePickUp.equipped = true;
            rifleEquipped = true;
            rifleStored = false;
            storedRifle = null;
        } else if (weapon.weaponName == "Pistol"){
            riflePickUp.equipped = false;
            pistolPickUp.equipped = true;
            pistolEquipped = true;
            pistolStored = false;
            storedPistol = null;
        }
    }

    public void Holster(WeaponManager weapon){
        rigController.Play("holster" + weapon.weaponName);
        if (weapon.weaponName == "Rifle"){
            // Debug.Log("Holster " + weapon.weaponName);
            riflePickUp.equipped = false;
            rifleEquipped = false;
            rifleStored = true;
            storedRifle = weapon;
        } else if (weapon.weaponName == "Pistol"){
            pistolPickUp.equipped = false;
            pistolEquipped = false;
            pistolStored = true;
            storedPistol = weapon;
        }
        Debug.Log(weapon.name);

        weapon.transform.SetParent(backContainer);
        weapon.transform.localPosition = new Vector3(0, 0, 0.1f);
        weapon.transform.localRotation = Quaternion.Euler(new Vector3(0, 90f, 0));
        weapon.enabled = false;
        weapon = null;
    }
}
