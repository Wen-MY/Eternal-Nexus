using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSwitcher : MonoBehaviour
{
    // Start is called before the first frame update
    public List<GameObject> weapons = new List<GameObject>();
    private int currentWeaponIndex = 0;
    private bool isSwitching = false;
    private AmmoManager ammoManager;
    void Start()
    {
        GameObject gameplay = GameObject.Find("Gameplay");
        ammoManager = gameplay.GetComponent<AmmoManager>();
        // Set the first weapon as the initially equipped weapon
        weapons[currentWeaponIndex].SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        takeInput();
    }

    private void takeInput()
    {
        float scrollInput = Input.GetAxis("Mouse ScrollWheel");
        if (scrollInput > 0 && !isSwitching) // Scroll up
        {
            isSwitching = true;
            SwitchToNextWeapon();
            Invoke("resetSwitching", 2f);
        }
        else if (scrollInput < 0 && !isSwitching) // Scroll down
        {
            isSwitching = true;
            SwitchToPreviousWeapon();
            Invoke("resetSwitching", 2f);
        }
    }
    private void resetSwitching()
    {
        isSwitching = false;

    }
    private void SwitchToNextWeapon()
    {
        // Deactivate the current weapon
        weapons[currentWeaponIndex].SetActive(false);

        // Increment the weapon index, and wrap around if needed
        currentWeaponIndex = (currentWeaponIndex + 1) % weapons.Count;

        // Activate the next weapon
        weapons[currentWeaponIndex].SetActive(true);
        ammoManager.UpdateAmmo(weapons[currentWeaponIndex].GetComponentInChildren<FiringSystem>().bulletsInMagazine);
    }

    private void SwitchToPreviousWeapon()
    {
        // Deactivate the current weapon
        weapons[currentWeaponIndex].SetActive(false);

        // Decrement the weapon index, and wrap around if needed
        currentWeaponIndex = (currentWeaponIndex - 1 + weapons.Count) % weapons.Count;

        // Activate the previous weapon
        weapons[currentWeaponIndex].SetActive(true);
        ammoManager.UpdateAmmo(weapons[currentWeaponIndex].GetComponentInChildren<FiringSystem>().bulletsInMagazine);
    }
}
