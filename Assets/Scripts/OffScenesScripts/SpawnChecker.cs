using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace checker.vector
{
    public class SpawnChecker
    {

        bool _checked;

        public SpawnChecker()
        {

        }
       
         public bool Checker(Vector3 v, float data, float minRadius)
         {
             //Vector3 distance = Vector3.zero;
             foreach (var datacolect in GameManager.instance.allenviromentData)
             {
                 if (datacolect.gameObject.layer == 13)
                 {
                     var _child = datacolect.GetChild(0);

                     _child.position = v;

                     if (_child.localPosition.x > 0.8f || _child.localPosition.x < -0.8f) data = minRadius;

                    if (_child.localPosition.z > 0.8f || _child.localPosition.z < -0.8f) data = minRadius;
                    else data = 100f;

                 }
                 else
                 {
                     data = minRadius;
                     

                 }

                 var distance = v - datacolect.position;
                

                if (distance.magnitude > data) _checked = true;
                else return _checked = false;


             }

             return _checked;

         }

    }
}



    