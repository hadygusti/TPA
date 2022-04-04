using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    [Header("Fire Rate")]

    [SerializeField] float fireRate;
    float fireRateTimer;
    [SerializeField] bool semiAuto;


    [Header("Bullet Properties")]
    [SerializeField] GameObject bullet;
    [SerializeField] Transform barrelPos;
    [SerializeField] float bulletVelocity;
    [SerializeField] int bulletPerShot;
    [SerializeField] AimController aim;
    public PickUpController pickUpController;
    public string weaponName;


    [SerializeField] AudioClip gunShot;
    AudioSource audioSource;
    WeaponAmmo ammo;

    [SerializeField] ActionStateManager actions;
    WeaponRecoil recoil;


    Light muzzleFlashLight;
    ParticleSystem muzzleFlashParticle;
    float lightIntensity;
    [SerializeField] float lightReturnSpeed = 2;

    void Start()
    {
        this.enabled = false;
    }

    // Start is called before the first frame update
    void Awake()
    {
        recoil = GetComponent<WeaponRecoil>();
        audioSource = GetComponent<AudioSource>();
        // aim = GetComponentInParent<AimController>();
        // actions = GetComponentInParent<ActionStateManager>();
        ammo = GetComponent<WeaponAmmo>();
        muzzleFlashLight = GetComponentInChildren<Light>();
        lightIntensity = muzzleFlashLight.intensity;
        muzzleFlashLight.intensity = 0;
        muzzleFlashParticle = GetComponentInChildren<ParticleSystem>();
        fireRateTimer = fireRate;
        pickUpController = GetComponent<PickUpController>();
    }


    // Update is called once per frame
    void Update()
    {
        if (ShouldFire())
        {
            Fire();
            if(QuestController.questIdx == 3 && weaponName == "Rifle")
            {
                QuestController.bulletCount++;
            }
        }
        muzzleFlashLight.intensity = Mathf.Lerp(muzzleFlashLight.intensity, 0, lightReturnSpeed * Time.deltaTime);
    }

    bool ShouldFire(){
        fireRateTimer += Time.deltaTime;
        if(fireRateTimer < fireRate) return false;
        // Debug.Log("firerate");
        if(ammo.currentAmmo == 0) return false; 
        // Debug.Log("ammo");
        if(actions.currentState == actions.Reload) return false;
        // Debug.Log("reload state");
        if(!this.enabled) return false;
        // Debug.Log("Here");
        if(semiAuto && Input.GetKeyDown(KeyCode.Mouse0)) return true;
        if(!semiAuto && Input.GetKey(KeyCode.Mouse0)) return true;

        return false;
    }

    void Fire(){
        fireRateTimer = 0;
        barrelPos.LookAt(aim.aimPos);
        audioSource.PlayOneShot(gunShot);
        recoil.TriggerRecoil();
        TriggerMuzzleFlash();
        ammo.currentAmmo--;
        aim.yAxis.Value += Random.Range(-1, -2);
        for(int i = 0; i < bulletPerShot; i++){
            GameObject currentBullet = Instantiate(bullet, barrelPos.position, barrelPos.rotation);
            Rigidbody rb = currentBullet.GetComponent<Rigidbody>();
            rb.AddForce(barrelPos.forward * bulletVelocity, ForceMode.Impulse);
        }
    }

    void TriggerMuzzleFlash(){
        muzzleFlashParticle.Play(true);
        muzzleFlashLight.intensity = lightIntensity;
    }
}
