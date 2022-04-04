using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponAmmo : MonoBehaviour
{

    public int clipSize;
    public int extraAmmo;
    public int currentAmmo;

    [SerializeField] AudioClip reloadSound;
    AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        currentAmmo = clipSize;
        audioSource = GetComponent<AudioSource>();
    }

    public void Reload(){
        // audioSource.PlayOneShot(reloadSound);
        if(extraAmmo >= clipSize){
            int ammoToReload = clipSize - currentAmmo;
            extraAmmo -= ammoToReload;
            currentAmmo += ammoToReload;
        }else if(extraAmmo > 0){
            if(extraAmmo + currentAmmo > clipSize){
                int lefOverAmmo = extraAmmo + currentAmmo - clipSize;
                extraAmmo = lefOverAmmo;
                currentAmmo = clipSize;
            } else{
                currentAmmo += extraAmmo;
                extraAmmo = 0;
            }
        }
    }
}
