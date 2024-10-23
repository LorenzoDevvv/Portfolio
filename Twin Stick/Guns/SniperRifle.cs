using UnityEngine;
using UnityEngine.InputSystem;

public class SniperRifle : Gun
{
    private PlayerInput playerInput;

    protected override void Awake()
    {
        base.Awake();
        playerInput = GetComponent<PlayerInput>();
    }

    protected override void GetPlayerInput()
    {
        if (Input.GetButton("Shoot") && canShoot)
        {
            Shoot();
            InvokeBulletShotEvent(currentAmmo);
        }

        // Other input handling specific to the sniper rifle
    }

    // Other methods specific to the sniper rifle

    private void Update()
    {
        if (!PauseMenu.isPaused)
        {
            // Handle common gun functionality
            if (maxAmmo == 0)
                return;

            if (isReloading)
                return;
            GetPlayerInput();

            if (currentAmmo <= 0)
            {
                Reload();
            }
        }
    }

}