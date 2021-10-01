using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TimerCloack;

// aplicar strategy o decorator
public abstract class WeaponEntity : MonoBehaviour,IObservable<int>
{

    //Strategy para los powerups

    // este script es el padre de todas las armas, todas las armas tienen, metodo de balas,cantidad de balas,frecuencia del disparo, recarga.
    
    [SerializeField]
    Transform[] spawn;
    public PlayerEntity Player;
    [SerializeField]
     public BulletEntity _Bullet;
    [SerializeField]
    int MaxBullet;

   public  ObjectPool<BulletEntity> pool;
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
    public bool _isShooting = false;

    public WeaponEntity Weapon
    {
        get => this;
    }
    public int Bullet
    {
        get => MaxBullet;
    }

   
    protected virtual void Start()
   {
        _timer = new TimerMethod();
       
         pool = new ObjectPool<BulletEntity> (BulletReturn,BulletEntity.TurnOn, BulletEntity.TurnOff, MaxBullet);

       _countBullet = MaxBullet;
    }

    private void Update()
    {
        if (_isShooting)
            Shoot();
    }

    protected virtual void Shoot()
    {
        if (_charge) return;

            if (_countBullet != 0)
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
        _countBullet--;
        
       
        for (int i = 0; i < spawn.Length; i++)
        {
            NotifyToObservers(_countBullet);
            var b = pool.GetObject();
            b.transform.position = spawn[i].transform.position;
            b.transform.forward = spawn[i].transform.forward;
        }
        
        
    }
    protected IEnumerator Recharge()
    {
        _charge = true;
        yield return new WaitForSeconds(recharge);
        _testing = true;
        _charge = !_testing;
        _countBullet = MaxBullet;
        NotifyToObservers(_countBullet);


    }

    public BulletEntity BulletReturn()
    {
        _Bullet.InitialBullet(this);
        return Instantiate(_Bullet);
    }

 

    List<IObserver<int>> _allObserver = new List<IObserver<int>>();

    public void Subscribe(IObserver<int> obs)
    {
        if (!_allObserver.Contains(obs))
            _allObserver.Add(obs);
    }

    public void Unsubscribe(IObserver<int> obs)
    {
        if (_allObserver.Contains(obs))
            _allObserver.Remove(obs);
    }

    public void NotifyToObservers(int action)
    {
        for (int i = 0; i < _allObserver.Count; i++)
        {
            _allObserver[i].Notify(action);
        }
    }
}

   

