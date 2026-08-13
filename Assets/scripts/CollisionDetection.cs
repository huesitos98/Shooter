using UnityEngine;

public class CollisionDetection : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("El objeto " + other.name + "entro en el collider del objeto" + gameObject.name);

    }

    private void OnTriggerExit(Collider other)
    {
        Debug.Log("El objeto " + other.name + "salio en el collider del objeto" + gameObject.name);

    }


    private void OnTriggerStay(Collider other)
    {
        Debug.Log("El objeto " + other.name + "permanece en el collider del objeto" + gameObject.name);

    }








}



