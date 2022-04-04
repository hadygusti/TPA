using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class WeaponCanvas : MonoBehaviour
{
    public TextMeshProUGUI rifleAmmoText, pistolAmmoText;
    public WeaponAmmo rifleAmmo, pistolAmmo;

    public ActiveWeapon activeWeapon;

    public Image rifleImage, pistolImage;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(activeWeapon.rifleEquipped)
        {
            
        } 
        else
        {
            
        }


        if(!activeWeapon.rifleEquipped && !activeWeapon.rifleStored)
        {
            rifleAmmoText.enabled = false;
            rifleImage.enabled = false;
        }
        else if (activeWeapon.rifleStored)
        {
            rifleAmmoText.enabled = false;
        }
        else
        {
            rifleAmmoText.enabled = true;
            rifleImage.enabled = true;
            rifleAmmoText.text = rifleAmmo.currentAmmo.ToString() + "/" + rifleAmmo.extraAmmo.ToString();
        }

        if (!activeWeapon.pistolEquipped && !activeWeapon.pistolStored)
        {
            pistolAmmoText.enabled = false;
            pistolImage.enabled = false;
        }
        else if (activeWeapon.pistolStored)
        {
            pistolAmmoText.enabled = false;
        }
        else
        {
            pistolAmmoText.enabled = true;
            pistolImage.enabled = true;
            pistolAmmoText.text = pistolAmmo.currentAmmo.ToString() + "/" + pistolAmmo.extraAmmo.ToString();
        }
    }
}
