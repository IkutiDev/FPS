using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSwitcher : MonoBehaviour
{
    [SerializeField] private int currentWeapon=0;
    private int maximumWeaponCapacity;
    Dictionary<KeyCode, System.Action> keyCodeDic = new Dictionary<KeyCode, System.Action>();
    void Start()
    {
        GetMaximumWeaponCapacity();
        SetWeaponActive();
        SetupDic();
    }

    private void GetMaximumWeaponCapacity()
    {
        maximumWeaponCapacity = transform.childCount;
    }

    private void SetupDic()
    {
        const int alphaStart = 48;
        const int alphaEnd = 57;

        int paramValue = 0;
        for (int i = alphaStart; i <= alphaEnd; i++)
        {
            KeyCode tempKeyCode = (KeyCode)i;

            //Use temp variable to prevent it from being capture
            int temParam = paramValue;
            keyCodeDic.Add(tempKeyCode, () => MethodCall(temParam));
            paramValue++;
        }
    }
    void MethodCall(int keyNum)
    {
        if (maximumWeaponCapacity >= keyNum && keyNum!=0)
        {
            currentWeapon = keyNum - 1;
        }
    }



    // Update is called once per frame
    void Update()
    {
        int previousWeapon = currentWeapon;

        ProcessKeyInput();
        ProcessScrollWheel();
        if (previousWeapon != currentWeapon)
        {
            SetWeaponActive();
        }
    }

    private void ProcessScrollWheel()
    {
        if (Input.GetAxis("Mouse ScrollWheel") < 0)
        {
            if (currentWeapon >= transform.childCount - 1)
            {
                currentWeapon = 0;
            }
            else
            {
                currentWeapon++;
            }
        }
        else if (Input.GetAxis("Mouse ScrollWheel") > 0)
        {
            if (currentWeapon <= 0)
            {
                currentWeapon = transform.childCount-1;
            }
            else
            {
                currentWeapon--;
            }
        }
    }

    private void ProcessKeyInput()
    {
        foreach (KeyValuePair<KeyCode, System.Action> entry in keyCodeDic)
        {
            //Check if the keycode is pressed
            if (Input.GetKeyDown(entry.Key))
            {
                //Check if the key pressed exist in the dictionary key
                if (keyCodeDic.ContainsKey(entry.Key))
                {
                    //Debug.Log("Pressed" + entry.Key);

                    //Call the function stored in the Dictionary's value
                    keyCodeDic[entry.Key].Invoke();
                }
            }
        }
    }

    private void SetWeaponActive()
    {
        int weaponIndex = 0;
        foreach (Transform weapon in transform)
        {
            if (weaponIndex == currentWeapon)
            {
                weapon.gameObject.SetActive(true);
            }
            else
            {
                weapon.gameObject.SetActive(false);
            }

            weaponIndex++;
        }
    }
}
