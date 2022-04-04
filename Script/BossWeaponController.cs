using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossWeaponController : MonoBehaviour
{

    [SerializeField] GameObject player;

    public BossController controller;

    [SerializeField] GameObject bullet;
    [SerializeField] Transform muzzle;
    [SerializeField] float bulletVelocity;
    [SerializeField] int bulletPerShot;

    [SerializeField] float fireRate;
    float fireRateTimer;

    [SerializeField] AudioSource audioSource;
    [SerializeField] AudioClip gunShot;

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<BossController>();
        fireRateTimer = fireRate;
    }

    // Update is called once per frame
    void Update()
    {
        if (ShouldFire())
        {
            //Fire();
        }
    }

    private bool ShouldFire()
    {
        fireRateTimer += Time.deltaTime;
        if (controller.playerInAttackRange)
        {
            if (fireRateTimer < fireRate) return false;
        }
        return true;
    }

    public void Fire()
    {
        fireRateTimer = 0;
        muzzle.LookAt(player.transform);
        audioSource.PlayOneShot(gunShot);

        for(int i = 0; i < bulletPerShot; i++)
        {
            GameObject currentBullet = Instantiate(bullet, muzzle.position, muzzle.rotation);
            Rigidbody rb = currentBullet.GetComponent<Rigidbody>();
            rb.AddForce(muzzle.forward * bulletVelocity, ForceMode.Impulse);
        }
    }
}
