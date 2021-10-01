using UnityEngine;

public class BatPowerUp : IPowerUp // como es un powerup arma deberá heredar de weaponEWntity
{

    Transform _transform; // posicion de donde tiene que estar el arma rigeada al cuerpo
    public BatPowerUp(Transform t)
    {

    }
    public void PowerUp()
    {
       //animacion
    }
}
