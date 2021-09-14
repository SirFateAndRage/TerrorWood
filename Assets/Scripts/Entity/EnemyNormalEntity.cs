using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyNormalEntity : CombatEntity,IenviromentData
{
    float _fhealt;
    protected override void Start()
    {
        base.Start();
        _fhealt = maxhealt;
        DataReturn(this.transform);
        
    }
    protected virtual void Reset()
    {
        _fhealt = maxhealt;
    }
    protected virtual void TakeReference()
    {

    }
    public static void TurnOn(EnemyNormalEntity b)
    {
        b.TakeReference();
        b.Reset();
        b.gameObject.SetActive(true);
    }
    public static void TurnOff(EnemyNormalEntity b)
    {
        b.gameObject.SetActive(false);
    }

    public void DataReturn()
    {
        
    }

    public void DataReturn(Transform t)
    {
        GameManager.instance.AddEnviromentData(t.transform);
    }
}
