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
   [SerializeField]
    WeaponEntity _weapon;
    protected virtual void Update()
    {
        transform.position += transform.forward * _speed * Time.deltaTime;
        _distance += _speed * Time.deltaTime;

        if(_distance >= maxDistance)
        {
          _weapon.pool.ReturnObject(this);
        }
 
    }
    private void Reset() => _distance = 0;
    public virtual void InitialBullet(WeaponEntity w)
    {
        _weapon = w;
    }
    

  
    public static void TurnOn(BulletEntity b)
    {
        //Debug.Log("3");
        b.Reset();
        b.gameObject.SetActive(true);
    }
    public static void TurnOff(BulletEntity b)
    {
      //  Debug.Log("2");
        b.gameObject.SetActive(false);
    }
   
}
