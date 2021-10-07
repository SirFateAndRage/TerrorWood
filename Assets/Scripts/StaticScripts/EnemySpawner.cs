using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TimerCloack;
using checker.spawn;
using checker.vector;
using VectorDesired;


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
    [SerializeField]
    Transform _ground;
    TimerMethod _timer;
    SpawnSeek _SpawnSeek;
    SpawnChecker _spawnChecker;
    DesiredVector _vectors;
    public PlayerEntity _player;
    
    [SerializeField]
    int _decoRange;
    [SerializeField]
    Grid _gridToEnemy;

    public ObjectPool<EnemyNormalEntity> pool;

     public int MaxRadius() => _maxRadius;

    private void Awake()
    {
        if (instance != null)
            Destroy(gameObject);
        else
            instance = this;

    }

    private void Start()
    {
        _timer = new TimerMethod();
        _vectors = new DesiredVector();
        _SpawnSeek = new SpawnSeek(this,_player,_vectors);
        _spawnChecker = new SpawnChecker(_ground);
        
        EnemySpawner.instance.enemy = enemy;
        EnemySpawner.instance.pool = new ObjectPool<EnemyNormalEntity>(instance.EnemyReturn, EnemyNormalEntity.TurnOn, EnemyNormalEntity.TurnOff, _maxEnemy);
    }

   
    
    public EnemyNormalEntity EnemyReturn()
    {
        enemy.TakeReference(_player,_gridToEnemy);
        return Instantiate(enemy);
    }

    //pasar a metodo que se engargue de buscar los spawn
    Vector3 _desired;
    int countenemy;
    public void SpawnInstance()
    {
        _desired = Vector3.zero;
        if (_SpawnSeek.DesiredVector(_minRadius) == Vector3.zero) return;

        _desired = _SpawnSeek.DesiredVector(_minRadius);

        if (_spawnChecker.Checker(_desired,_decoRange, _minRadius) == false) return;

        EnemyInstance(_desired);
    }
    void EnemyInstance(Vector3 desired)
    {
        var b = EnemySpawner.instance.pool.GetObject();

        b.transform.position = desired;
        countenemy++;
        _timer.passTime = 0;
    }

    public void Update()
    {
        
        _SpawnSeek.Onupdate();

        if(countenemy< _maxEnemy)
            if (_timer.Timer(_spawnBeetweenEnemy))
                SpawnInstance();


    }

    
}


       
    

