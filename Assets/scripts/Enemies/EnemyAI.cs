using UnityEngine;

public class EnemyAI : AIAgentBase
{
    public WeaponControler weaponController;
    public float alturaApuntando = 1.4f;

    protected override void EjecutarAtaque()
    {
        if(objetivoActual == null)
        {
            return;
        }
        if(weaponController.EstaRecargando)
        {
            return;
        }
        if (!weaponController.TieneMunicion)
        {
            animator?.SetTrigger("Reload");
            weaponController.IniciarRecarga();
            return;
        }

        Vector3 puntoApuntado = objetivoActual.position + Vector3.up * alturaApuntando;
        Vector3 direccionAlObjetivo = (puntoApuntado - weaponController.FirePoint.position).normalized;

        bool disparo = weaponController.TryFire(direccionAlObjetivo);

        if(disparo)
        {
            animator?.SetTrigger("Shoot");
        }
   
    }


}
