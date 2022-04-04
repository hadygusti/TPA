using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoldierWeaponController : MonoBehaviour
{
    [SerializeField] GameObject player;

    public SoldierController controller;

    [SerializeField] GameObject bullet;
    [SerializeField] Transform barrelPos;
    [SerializeField] float bulletVelocity;
    [SerializeField] int bulletPerShot;

    [SerializeField] float fireRate;
    float fireRateTimer;

    [SerializeField] AudioClip gunShot;
    [SerializeField] AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<SoldierController>();
        fireRateTimer = fireRate;
        // audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if(ShouldFire()){
            Fire();
        }
        Debug.Log(controller.playerInAttackRange);
    }

    bool ShouldFire(){
        fireRateTimer += Time.deltaTime;
        if(controller.playerInAttackRange){
            if(fireRateTimer < fireRate) return false;
        }
        return true;
    }

    public void Fire(){
        fireRateTimer = 0;
        barrelPos.LookAt(player.transform);
        audioSource.PlayOneShot(gunShot);
        // Debug.Log("Tmbak");
        for(int i = 0; i < bulletPerShot; i++){
            GameObject currentBullet = Instantiate(bullet, barrelPos.position, barrelPos.rotation);
            Rigidbody rb = currentBullet.GetComponent<Rigidbody>();
            rb.AddForce(barrelPos.forward * bulletVelocity, ForceMode.Impulse);
        }
    }
}
