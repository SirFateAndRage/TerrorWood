using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class WeaponEntity : MonoBehaviour, IObserver
{
    // este script es el padre de todas las armas, todas las armas tienen, metodo de balas,cantidad de balas,frecuencia del disparo, recarga.
    [SerializeField] Transform[] spawn;
    public PlayerEntity Player;
    [SerializeField]
    BulletEntity Bullet;
    [SerializeField]
    int MaxBullet;
    IObservable _playerToCopy;

    float _timer;
    bool _spawnbullet;
    public float timeBetweenBullet;
    public float recharge;
    public float waitForRecharge;
    int _countBullet;
    bool _charge = false;


    protected virtual void Start()
   {
        BulletSpawner.instance.bullet = Bullet;
        BulletSpawner.instance.pool = new ObjectPool<BulletEntity>(BulletSpawner.instance.BulletReturn, BulletEntity.TurnOn, BulletEntity.TurnOff, MaxBullet);
        _playerToCopy = Player;
        _playerToCopy.Subscribe(this);
    }

    public void Notify(string Action)
    {
        if (Action == "shooting")   
            Shoot();
        if(Action == "notshooting")
        {
            if (_countBullet == 0) return;
            if (Timer(waitForRecharge))
            {
                StartCoroutine(Recharge());
            }
        }
            
        
    }

    protected virtual void Shoot()
    {
        if (_charge) return;

            if (_countBullet != MaxBullet)
            {
                if (Timer(timeBetweenBullet))
                       Testing();  
            }
            else StartCoroutine(Recharge());


    }

    protected virtual void Testing()
    {
        _testing = false;
        _countBullet++;
        for (int i = 0; i < spawn.Length; i++)
        {
            var b = BulletSpawner.instance.pool.GetObject();
            b.transform.position = spawn[i].transform.position;
            b.transform.forward = spawn[i].transform.forward;
        }
        
        _timer = 0;
    }
    protected IEnumerator Recharge()
    {
        _charge = true;

        yield return new WaitForSeconds(recharge);
        _testing = true;
        _charge = false;
        _countBullet = 0;
        _timer = 0;

    }
    bool _testing = true;
    protected virtual bool Timer(float f)
    {
        if (_testing)
             Testing();
           
        
        _timer += Time.deltaTime;

        _spawnbullet = (_timer >= f) ? true : false;
        return _spawnbullet;
    }



   
   
}

   

