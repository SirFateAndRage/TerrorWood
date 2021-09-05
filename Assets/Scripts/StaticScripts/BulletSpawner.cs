using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletSpawner : MonoBehaviour
{

    //el solteron que se dedica a crear las pools de las balas 
    public static BulletSpawner instance;
    public BulletEntity bullet;
    public ObjectPool<BulletEntity> pool;

    private void Awake()
    {
        if(instance != null)
        {
            Destroy(this);
        }
        else
        {
            instance = this;
        }
        
    }
    public BulletEntity BulletReturn()
    {
        return Instantiate(bullet);
    }

}
