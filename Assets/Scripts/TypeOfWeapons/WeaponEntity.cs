using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TimerCloack;

public abstract class WeaponEntity : MonoBehaviour, IObserver, IObservable
{
    // este script es el padre de todas las armas, todas las armas tienen, metodo de balas,cantidad de balas,frecuencia del disparo, recarga.
    [SerializeField]
    Transform[] spawn;
    public PlayerEntity Player;
    [SerializeField]
    BulletEntity Bullet;
    [SerializeField]
    int MaxBullet;

    IObservable _playerToCopy;

    [SerializeField]
    float _timeBetweenBullet;
    [SerializeField]
    float recharge;
    [SerializeField] 
    float waitForRecharge;

    int _countBullet;
    bool _charge = false;
    TimerMethod _timer;

    bool _testing = true;


    public int BulletCapacity()
    {
        return MaxBullet;
    }
    protected virtual void Start()
   {
        _timer = new TimerMethod();
        BulletSpawner.instance.bullet = Bullet;
        BulletSpawner.instance.pool = new ObjectPool<BulletEntity>(BulletSpawner.instance.BulletReturn, BulletEntity.TurnOn, BulletEntity.TurnOff, MaxBullet);
        _playerToCopy = Player;
        _playerToCopy.Subscribe(this);
    }

    public void Notify(Utils.ActionObservers Action)
    {
        if (Action == Utils.ActionObservers.shooting)   
            Shoot();
       /* if(Action == Utils.ActionObservers.notshooting)
        {
            if (_countBullet == 0) return;
            if (_timer.Timer(waitForRecharge))
            {
                StartCoroutine(Recharge());
            }
        }*/

    }

    protected virtual void Shoot()
    {
        if (_charge) return;

            if (_countBullet != MaxBullet)
            {
                   if (_testing)
                        Testing();
                  if (_timer.Timer(_timeBetweenBullet))
                       Testing();  
            }
            else StartCoroutine(Recharge());


    }

    protected virtual void Testing()
    {
        _testing = false;
        _timer.passTime = 0;
        _countBullet++;
        
       
        for (int i = 0; i < spawn.Length; i++)
        {
            NotifyToObservers(Utils.ActionObservers.bulletQuantity);
            var b = BulletSpawner.instance.pool.GetObject();
            b.transform.position = spawn[i].transform.position;
            b.transform.forward = spawn[i].transform.forward;
        }
        
        
    }
    protected IEnumerator Recharge()
    {
        _charge = true;
        yield return new WaitForSeconds(recharge);
        NotifyToObservers(Utils.ActionObservers.ResetBulletQuantity);
        _testing = true;
        _charge = !_testing;
        _countBullet = 0;
        

    }
   


    List<IObserver> _allObservers = new List<IObserver>();
    public void Subscribe(IObserver obs)
    {
        if (!_allObservers.Contains(obs))
            _allObservers.Add(obs);
    }

    public void Unsubscribe(IObserver obs)
    {
        if (_allObservers.Contains(obs))
            _allObservers.Remove(obs);
    }

    public void NotifyToObservers(Utils.ActionObservers actionObservers)
    {
        for (int i = 0; i < _allObservers.Count; i++)
        {
            _allObservers[i].Notify(actionObservers);
        }
    }

  

}

   

