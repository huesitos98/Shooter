using UnityEngine;

public class EnemyDethHandler : MonoBehaviour
{
    public Animator animatorEnemy;
    public Health healtEnemy;
    public float tiempoAntesDeDestruir = 4f;

    private void OnEnable()
    {
        healtEnemy.OnDeath += ManejarMuerte;
    }

    private void OnDisable()
    {
        healtEnemy.OnDeath -= ManejarMuerte;
    }





    private void ManejarMuerte(GameObject atacante)
    {
        animatorEnemy.SetTrigger("Die");
        Destroy(gameObject, tiempoAntesDeDestruir);
    }

}
