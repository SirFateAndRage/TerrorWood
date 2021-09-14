using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class BulletCounter : MonoBehaviour,IObserver
{

    [SerializeField] Text bulletNumber;
    

    public WeaponEntity Weapon;

    IObservable _weaponToCopy;
    float number;
    public void Notify(Utils.ActionObservers actionObservers)
    {
        if(actionObservers == Utils.ActionObservers.bulletQuantity)
        {
            BulletQuantity();
        }
        if(actionObservers == Utils.ActionObservers.ResetBulletQuantity)
        {
            Reset();
        }

    }


    void BulletQuantity()
    {
        number--;
        bulletNumber.text = Convert.ToString(number);

    }

    void Reset()
    {
        number = Weapon.BulletCapacity();
        bulletNumber.text = Convert.ToString(number);
    }
   
    void Start()
    {
        _weaponToCopy = Weapon;
        _weaponToCopy.Subscribe(this);
        number = Weapon.BulletCapacity();
        bulletNumber.text = Convert.ToString(number);
        

    }

   
}
