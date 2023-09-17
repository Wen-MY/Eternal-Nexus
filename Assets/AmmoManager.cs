using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class AmmoManager : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI ammoText;
    public void UpdateAmmo(int count)
    {
        ammoText.text = "Ammo: " + count;
    }
}
