using UnityEngine;
using UnityEngine.AI;

public class ObjetivoAleatorio : MonoBehaviour
{
    public float radioMovimiento = 10f;
    public float tiempoEspera = 3f;

    private NavMeshAgent agente;
    private float cronometro;

    void Start()
    {
        agente = GetComponent<NavMeshAgent>();
        cronometro = tiempoEspera;
    }

    void Update()
    {
        cronometro += Time.deltaTime;

        if (cronometro >= tiempoEspera)
        {
            Vector3 nuevaPosicion = ObtenerPuntoAleatorio(transform.position, radioMovimiento);
            agente.SetDestination(nuevaPosicion);
            cronometro = 0f;
        }
    }

    Vector3 ObtenerPuntoAleatorio(Vector3 centro, float radio)
    {
        Vector3 direccionAleatoria = Random.insideUnitSphere * radio;
        direccionAleatoria += centro;

        NavMeshHit hit;
        // Busca una posición válida en el NavMesh
        if (NavMesh.SamplePosition(direccionAleatoria, out hit, radio, NavMesh.AllAreas))
        {
            return hit.position;
        }

        return centro;
    }
}