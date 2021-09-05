using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public  class BulletEntity : MonoBehaviour
{
    //todo lo comun entre las balas
    [SerializeField]
    float _speed;
    [SerializeField]
    float maxDistance;
    float _distance;
    
    WeaponEntity _weapon;

    protected virtual void Update()
    {
        transform.position += transform.forward * _speed * Time.deltaTime;
        _distance += _speed * Time.deltaTime;

        if(_distance >= maxDistance)
        {
           BulletSpawner.instance.pool.ReturnObject(this);
        }
 
    }
    private void Reset()
    {
        _distance = 0;
    }

   private void TakeReference()
    {
       
    }
    public static void TurnOn(BulletEntity b)
    {
        b.TakeReference();
        b.Reset();
        b.gameObject.SetActive(true);
    }
    public static void TurnOff(BulletEntity b)
    {
        b.gameObject.SetActive(false);
    }
   
}
