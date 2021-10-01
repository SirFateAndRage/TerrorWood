using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseEntity : MonoBehaviour,IEnviromentData
{
    // Entidad Basica, todo lo que tiene en comun.
    // se va a dividir en:
    // EnvEntity -> todas aquellos objetos que se pueda interactuar.
    // PlayerEntity -> el jugador
    // Enemy Entity ->enemigos

    #region Variable
    protected Animator anim;
    protected CapsuleCollider capCol;
    [SerializeField] protected CapsuleCollider entityCol;
    protected bool alive;
    [SerializeField] float _maxHealt;
    [SerializeField] float _healt;

    // Damage material variable
    public bool feedDamage;
    float timerFeed = 0;
    protected Material defaultMat;
    [SerializeField] protected Material damageMat;
    [SerializeField] protected Material deathMat;
    [SerializeField] protected Renderer render;
    #endregion
    #region References to variable
    public bool IsAlive { get => alive;}
    public float maxhealt { get => _maxHealt; }
    public float healt { get => _healt;}
    public Rigidbody rb { get; protected set; }
    #endregion
    protected virtual void Awake()
    {
        capCol = GetComponent<CapsuleCollider>();
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();

       
    }

    protected virtual void Start()
    {
        alive = true;
        _healt = _maxHealt;
    }
    protected virtual void Update()
    {
        BasicFeed();
    }
    protected virtual void FixedUpdate()
    {

    }

    protected virtual void BasicFeed()
    {
        if (feedDamage)
        {
            render.material = damageMat;
            timerFeed += Time.deltaTime;
            if(timerFeed > 0.2)
            {
                feedDamage = false;
                timerFeed = 0;
                render.material = defaultMat;
            }
        }
    }

    public void DataReturn(Transform t)
    {
        DataManager.instance.AddEnviromentData(t);
    }

    public void RemoveData(Transform t)
    {
        DataManager.instance.RemoveEnviromentData(t);
    }
}
