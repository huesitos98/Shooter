using UnityEngine;
using UnityEngine.InputSystem;
public class PlayerInputShoot : MonoBehaviour
{
    public WeaponControler WeaponControler;
    public Animator animator;
    void Update()
    {
       if(Mouse.current.leftButton.isPressed)
        {
            WeaponControler.TryFire();
        }
       if(Keyboard.current.rKey.isPressed)
        {
            WeaponControler.IniciarRecarga();
            animator.SetTrigger("Reload");
        }
    }
}
