using UnityEngine;
using UnityEngine.InputSystem;
public class PlayerInputShoot : MonoBehaviour
{
    public WeaponControler WeaponControler
        ;
  
    void Update()
    {
       if(Mouse.current.leftButton.isPressed)
        {
            WeaponControler.TryFire();
        }

    }
}
