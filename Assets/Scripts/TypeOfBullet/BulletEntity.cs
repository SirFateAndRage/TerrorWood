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

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other);
        ICombat _iCombat = other.gameObject.GetComponent<ICombat>();
        Debug.Log(_iCombat);
        if (_iCombat == null) return;

        _iCombat.TakeDamage(PistolDamage());
        _weapon.pool.ReturnObject(this);
    }

    private int _chancheToCrit;
    private int _falsePercentage;
    private int dmg;
    public float PistolDamage()
    {
        _chancheToCrit = Random.Range(_falsePercentage + 0, 100);
        if (_chancheToCrit <= 65)
        {
            _falsePercentage += 10;
            return dmg = Random.Range(10, 35);
        }
        else
        {
            _falsePercentage = 0;
            return dmg = Random.Range(35, 50);
        }
    }

}
