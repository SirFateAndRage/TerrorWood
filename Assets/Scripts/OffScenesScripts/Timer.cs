using System;
using UnityEngine;

namespace TimerCloack
{
    public class TimerMethod
    {

        public float passTime;

        public bool Timer(float f)
        {

            passTime += Time.deltaTime;

             bool _spawnbullet = (passTime >= f) ? true : false;

            return _spawnbullet;
        }
    }
}

