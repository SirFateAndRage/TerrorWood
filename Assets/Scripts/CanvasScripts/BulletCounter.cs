using UnityEngine;
using UnityEngine.UI;
using System;

public class BulletCounter : MonoBehaviour,IObserver<int>// remover observer de bala
{
    [SerializeField] Text bulletNumber;


    IObservable<int> _bulleQuantityToCopy;
    [SerializeField]
    WeaponEntity _weapon;
    int _bullet;

   

    private void Start()
    {
        _weapon = FindObjectOfType<WeaponEntity>();
        _bullet = _weapon.Bullet;
        _bulleQuantityToCopy = _weapon;
        _bulleQuantityToCopy.Subscribe(this);
        bulletNumber.text = Convert.ToString(_bullet);
    }
    public void Notify(int action)
    {
        bulletNumber.text = Convert.ToString(action);
    }




}
