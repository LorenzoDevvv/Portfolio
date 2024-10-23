using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Gun : MonoBehaviour
{
    [SerializeField] protected float timeBetweenShots;
    [SerializeField] protected GameObject bulletPrefab;
    [SerializeField] protected Transform gunPoint;
    protected bool canShoot = false;
    public int maxClip = 10;
    public int maxAmmo = 100;
    public int currentAmmo;
    public float reloadTime = 1f;
    protected bool isReloading = false;
    protected ObjectPool objectPool;
    [SerializeField] protected string bulletType;
    //[SerializeField] protected int buffer = 10;
    public virtual event Action<int> OnBulletShot; // Add the 'virtual' keyword here
    public virtual event Action<int> OnAmmoPickup; // Add the 'virtual' keyword here
    public ParticleSystem shootParticle;
    public ParticleSystem smokeParticle;
    public ParticleSystem trailParticle;
    public string weaponName;
    public Sprite weaponImage;

    protected SoundPlayer soundPlayer;

    protected virtual void Awake()
    {
        currentAmmo = maxAmmo;
        objectPool = FindObjectOfType<ObjectPool>();
        soundPlayer = GetComponent<SoundPlayer>();
        //objectPool.AssignBulletType(bulletType, buffer);
    }

    protected void OnEnable()
    {
        
        canShoot = true;
        
    }
    protected void OnDisable()
    {
        canShoot = false;
    }
    
    protected virtual void Shoot(int _numberOfProjectiles = 1)
    {
        if (shootParticle != null && smokeParticle != null && trailParticle != null)
        {
            shootParticle.Emit(5);
            smokeParticle.Emit(5);
            trailParticle.Emit(15);
        }
    

        currentAmmo--;
        maxAmmo--;

        GameObject bullet = objectPool.GetPooledObject(bulletType);
        if (bullet != null)
        {
            bullet.transform.position = gunPoint.position;
            bullet.transform.rotation = transform.rotation;
            bullet.SetActive(true);


        }
        soundPlayer.PlaySound();
        //Instantiate(bulletPrefab, gunPoint.position, transform.rotation);
        canShoot = false;
        StartCoroutine(ShotCooldown());

    

    }
    protected void InvokeBulletShotEvent(int ammoCount)
    {
        OnBulletShot?.Invoke(ammoCount);
    }

    public void InvokeAmmoPickupEvent(int ammoCount)
    {
        OnAmmoPickup?.Invoke(ammoCount);
    }
    protected abstract void GetPlayerInput();

    protected virtual IEnumerator Reload()
    {
        isReloading = true;
        Debug.Log("Reloading...");
        yield return new WaitForSeconds(reloadTime);
        //currentAmmo = maxClip;
        isReloading = false;
    }

    protected virtual IEnumerator ShotCooldown()
    {
        yield return new WaitForSeconds(timeBetweenShots);
        canShoot = true;
    }
}