using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    public static DataManager instance;

    public List<Transform> allenviromentData = new List<Transform>();

    private void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(gameObject);
    }

    public void AddEnviromentData(Transform t)
    {
        if (allenviromentData.Contains(t)) return;
        else allenviromentData.Add(t);
    }
    public void RemoveEnviromentData(Transform t)
    {
        if (allenviromentData.Contains(t))
            allenviromentData.Remove(t);
    }
}
