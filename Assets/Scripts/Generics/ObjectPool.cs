using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ObjectPool<T>
{
    //creo un tipo de dato que se utiliza como var poara guardar metodos que devuelvan algo
    public delegate T FactoryMethod();
//stock de lo que guardo
    List<T> _currentStock = new List<T>();
    //metodo que nos devuelve el obj
    FactoryMethod _factoryMethod;
    Action<T> _turnOnCallBack;
    Action<T> _turnOffCallBack;
    

    public ObjectPool(FactoryMethod factory,Action<T> turnOnCallBack, Action<T> turnOffCallBack,int initialStock = 0)
    {
        _factoryMethod = factory;
        _turnOffCallBack = turnOffCallBack;
        _turnOnCallBack = turnOnCallBack;
       


        for (int i = 0; i < initialStock; i++)
        {
            var o = _factoryMethod();
            _turnOffCallBack(o);
            _currentStock.Add(o);
           
        }

    }



    public T GetObject()
    {
        var result = default(T);

        if(_currentStock.Count > 0)
        {
            result = _currentStock[0];
            _currentStock.RemoveAt(0);
        }
        else
            result = _factoryMethod();


        _turnOnCallBack(result);

        return result;
        
    }

    public void ReturnObject(T o)
    {
        _turnOffCallBack(o);
        _currentStock.Add(o);
       
    }
}
