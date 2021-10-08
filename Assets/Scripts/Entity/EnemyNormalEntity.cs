using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VectorDesired;

public class EnemyNormalEntity : CombatEntity
{

    // DETALLES DE LO QUE QUIERO QUE HAGA EL ENEMIGO BASE O COMPORTAMIENTO STANDARD PARA TODOS LOS ENEMIGOS MODIFICABLE
    // SPAWNEA, APLICA FLOCCKING MIENTRAS SIEMPRE VA HACIA EL PLAYER + BUSCAR COMO PODRIA EVITAR ATRAVESAR LOS EDIFICIOS
    // CERCA DEL PLAYER ATACA
    // AL MORIRSE FIJARSE QUE HACER PARA RESETEARLO
    // ENTONCES STATE MACHINE DEBERIA TENER 3 ESTADOS
    // 1ª estado Spawn --> està hecho
    // 2º estado ir hacia el player --> aplicar flocking, quiero que siempre puedan ir en manada y no se pisen entre si.
    //3º atacar base cuando estan a rango( despues se buscarà que cada enemigo tenga un ataque especifico)
    //4 estado recibir daño
    //5 morirse
    [SerializeField]
    CombatEntity _target;
    BaseMovement _baseMovement;
    DesiredVector _desired;
    PathFindingMovement _movementPath;
    Separation _separation;
    DataManager instance;
    

    //prueba con pathFinding
    [SerializeField]
    Grid _referencegrid;
    PathFinding _pathfinding;

    [SerializeField]
    int _maxSpeed;
    [SerializeField]
    float _maxForce;
    [SerializeField]
    int _playerInRange;
    [SerializeField]
    float _distancezero;
    [SerializeField]
    float _distSeparation;

    public int currentPathindex;
    float actualLife;


    protected override void Awake()
    {
        base.Awake();
        
       

    }

    protected override void Start()
    {
        base.Start();
        _desired = new DesiredVector();
        _baseMovement = new BaseMovement(this.transform, _maxSpeed);
         DataReturn(this.transform);
        _pathfinding = new PathFinding(this.transform, _target.transform, _referencegrid);
        _movementPath = new PathFindingMovement(_desired, _maxSpeed, _maxForce, _pathfinding, this.transform, _target.transform);
        _separation = new Separation(_desired, this.transform, _distSeparation, _maxSpeed, _maxForce);


    }
    protected virtual void Reset()
    {
        actualLife = maxhealt;

    }
    public  void TakeReference(CombatEntity c,Grid g) 
    {
        _target = c;
        _referencegrid = g;
   
    } 
    protected override void Update()
    {
       
        base.Update();
        _baseMovement.OnUpdate();
        _movementPath.OnUpdate();
    }

    protected override void FixedUpdate()
    {
        base.FixedUpdate();
        _baseMovement.AddForce(_movementPath.MoveWithFinding(_baseMovement.Velocity)  + _separation.SepationEn(_baseMovement.Velocity)* 1);
    }
    public static void TurnOn(EnemyNormalEntity b)
    {
       
        b.Reset();
        b.gameObject.SetActive(true);
    }
    public static void TurnOff(EnemyNormalEntity b)
    {
        b.RemoveData(b.transform);
        b.gameObject.SetActive(false);
    }

    public override void TakeDamage(float dmg)
    {
        actualLife -= dmg;
        Debug.Log(actualLife);
        if (actualLife <= 0)
        {
            TurnOff(this);
            EnemySpawner.instance.countenemy--;

        }
           
    }



}
