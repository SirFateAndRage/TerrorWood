using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VectorDesired
{
    public class DesiredVector
    {

        //pos fin - incial
        public Vector3 Desired(Vector3 finalPos,Vector3 inicialpos)
        {
            var desired = finalPos - inicialpos;
            return desired;
        }

        //rotacion Objeto
        public Vector3 Steering(Vector3 desired, Vector3 velocity,float maxForce)
        {
            Vector3 steering = desired - velocity;
            steering = Vector3.ClampMagnitude(steering, maxForce);
            return steering;
        }

       
    }

}

