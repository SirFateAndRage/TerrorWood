using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatEntity : BaseEntity
{
    
    protected override void Awake() => base.Awake();
    protected override void Start()
    {
        base.Start();
    }

    protected override void FixedUpdate() => base.FixedUpdate();

    protected override void Update()
    {
        base.Update();
        
    }

    protected virtual void HealBarr( float life)
    {

    }

    protected virtual void TakeDamage( float dmg)
    {

    }
    protected virtual void Die()
    {

    }
   
}
