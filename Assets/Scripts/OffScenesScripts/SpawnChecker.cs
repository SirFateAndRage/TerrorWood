using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace checker.vector
{
    public class SpawnChecker
    {

        bool _checked;
        Transform _plane;

        public SpawnChecker(Transform t)
        {
            _plane = t;
        }
       
         public bool Checker(Vector3 v, float data, float minRadius)
         {
            var _planeChild = _plane.GetChild(0);
            _planeChild.position = v;
            if (Mathf.Abs(_planeChild.localPosition.x)<= 0.5f && Mathf.Abs(_planeChild.localPosition.z) <= 0.5f)
            {
                foreach (var datacolect in DataManager.instance.allenviromentData)
                {
                    if (datacolect.gameObject.layer == 13)
                    {
                        var _child = datacolect.GetChild(0);

                        _child.position = v;

                        if (Mathf.Abs(_child.localPosition.x) > 0.8f || Mathf.Abs(_child.localPosition.z) > 0.8f) data = minRadius;
                        else data = 100f;

                        _child.position = _child.parent.position;

                    }
                    else
                    {
                        data = minRadius;

                    }

                    var distance = v - datacolect.position;


                    if (distance.magnitude > data) _checked = true;
                    else return _checked = false;

                }

            }
            else
            {
                _checked = false;
            }


            return _checked;

         }

    }
}



    