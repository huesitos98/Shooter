using UnityEngine;

public class ZoneDetection : MonoBehaviour
{
    public enum TipoZona {Detection , Ataque}

    public TipoZona tipo = TipoZona.Detection;
    public string tagObjetivo = "Player";
    public AIAgentBase agente;

    public void OnTriggerEnter(Collider other)
    {
        if(!other.CompareTag(tagObjetivo))
        {
            return;
        }
        if(tipo == TipoZona.Detection)
        {
            agente.OnObjetivoDetectado(other.transform);
        }
        if(tipo == TipoZona.Ataque)
        {
            agente.OnEntroRangoDeAtaque();
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (!other.CompareTag(tagObjetivo))
        {
            return;
        }
        if (tipo == TipoZona.Detection)
        {
            agente.OnObjetivoPerdido();
        }
        if (tipo == TipoZona.Ataque)
        {
            agente.OnSalioRangoDeAtaque();
        }
    }






}
