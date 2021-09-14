using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TimerCloack;
using checker.spawn;
using checker.vector;


public class EnemySpawner : MonoBehaviour
{
    public static EnemySpawner instance;
    public EnemyNormalEntity enemy;
    [SerializeField]
    int _minRadius;
    [SerializeField]
    int _maxRadius;
    [SerializeField]
    int _maxEnemy;
    [SerializeField]
    float _spawnBeetweenEnemy;
    TimerMethod _timer;
    SpawnSeek _SpawnSeek;
    SpawnChecker _spawnChecker;
    public PlayerEntity _player;
    
    [SerializeField]
    int _decoRange;


    public ObjectPool<EnemyNormalEntity> pool;

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(this);
        }
        else
        {
            instance = this;
        }

    }

    private void Start()
    {
        _timer = new TimerMethod();
        _SpawnSeek = new SpawnSeek(this,_player);
        _spawnChecker = new SpawnChecker();
        EnemySpawner.instance.enemy = enemy;
        EnemySpawner.instance.pool = new ObjectPool<EnemyNormalEntity>(instance.EnemyReturn, EnemyNormalEntity.TurnOn, EnemyNormalEntity.TurnOff, _maxEnemy);

    }

    public int MaxRadius() => _maxRadius;
    public int DataRange() => _decoRange;
    
    
   
    public EnemyNormalEntity EnemyReturn()
    {
        return Instantiate(enemy);
    }

    //pasar a metodo que se engargue de buscar los spawn
    Vector3 _desired;
    public void SpawnInstance()
    {
        _desired = Vector3.zero;
        if (_SpawnSeek.DesiredVector(_minRadius) == Vector3.zero) return;

        _desired = _SpawnSeek.DesiredVector(_minRadius);

        if (_spawnChecker.Checker(_desired,_decoRange, _minRadius) == false) return;

        
             var b = EnemySpawner.instance.pool.GetObject();
             b.transform.position = _desired;
             _timer.passTime = 0;
        

    }

    public void Update()
    {
        /*if (Input.GetKeyDown(KeyCode.Space))
        {
            SpawnSeek();
        }*/
        _SpawnSeek.Onupdate();

        if (_timer.Timer(_spawnBeetweenEnemy))
            SpawnInstance();

            

    }
}


       
    

