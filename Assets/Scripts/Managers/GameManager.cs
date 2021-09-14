using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

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
}
