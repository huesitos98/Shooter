using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;
using System.Collections;
public class WeaponControler : MonoBehaviour
{
    [Header("Configuracion")]
    [SerializeField] private WeaponsData weaponData;
    [SerializeField] private LayerMask hittableLayers = ~0;
    [SerializeField] private LineRenderer lineRenderer;
    [SerializeField] private int CurrentAmmo;
    private float nextFireTime;
    [SerializeField] private Transform firePoint;
    [Header("Efectos")]
    [SerializeField] private ParticleSystem muzzleFlash;
    [SerializeField] private AudioSource audiosource;
    [SerializeField] private GameObject bloodHitEffectPrefab;
    [Header("Recarga")]
    public Transform magVisual;
    public float tiempoRecarga = 1.8f;
    public float fuerzaCaidaMag = 2f;

    public Transform FirePoint => firePoint;
    public bool TieneMunicion => CurrentAmmo > 0;
    public bool EstaRecargando { get; private set; }

    private void Start()
    {
        CurrentAmmo = weaponData.magazineSize;
    }

    public bool TryFire(Vector3? direccionOverride = null)
    {
        if (EstaRecargando)
        {
            return false;
        }
        if (Time.time < nextFireTime)
        {
            return false;
        }
        if (CurrentAmmo <= 0)
        {
            return false;
        }
        nextFireTime = Time.time + (1f / weaponData.fireRate);
        CurrentAmmo--;

        muzzleFlash.Play();
        audiosource.PlayOneShot(weaponData.fireSound);

        DispararRayo(direccionOverride);
        return true;

    }


    private void DispararRayo(Vector3? direccionOverride = null)
    {
        Vector3 direccion = direccionOverride ?? firePoint.forward;
        RaycastHit hit;
        bool golpeo = Physics.Raycast(firePoint.position, direccion, out hit, weaponData.range, hittableLayers);
        Vector3 puntoFinal;
        if (golpeo)
        {
            puntoFinal = hit.point;
            IDamageable damageable = hit.collider.GetComponent<IDamageable>();

            if (damageable != null)
            {
                damageable.TakeDamage(weaponData.damage, hit.point, hit.normal, gameObject);
                if (bloodHitEffectPrefab != null)
                {
                    Quaternion rotacionImpacto = Quaternion.LookRotation(hit.normal);
                    Instantiate(bloodHitEffectPrefab, hit.point, rotacionImpacto);
                }
            }

            Debug.Log("El disparo golpeo a : " + hit.collider.name);
        }
        else
        {
            puntoFinal = firePoint.position + direccion * weaponData.range;

            Debug.Log("El disparo no golpeo nada");
        }
        MostrarLinea(firePoint.position, puntoFinal);
    }
    private void MostrarLinea(Vector3 origen, Vector3 destino)
    {
        lineRenderer.SetPosition(0, origen);
        lineRenderer.SetPosition(1, destino);
        lineRenderer.enabled = true;
        Invoke(nameof(OcultarLinea), 0.25f);

    }
    private void OcultarLinea()
    {
        lineRenderer.enabled = false;
    }

    public void IniciarRecarga()
    {
        if (EstaRecargando || CurrentAmmo == weaponData.magazineSize)
        {
            return;
        }
        StartCoroutine(RecargarCorutina());            
    }
    private IEnumerator RecargarCorutina()
    {
        EstaRecargando = true;
        audiosource.PlayOneShot(weaponData.reloadSound);
        SoltarMagAlPiso();
        yield return new WaitForSeconds(tiempoRecarga);

        CurrentAmmo = weaponData.magazineSize;
        if(magVisual != null)
        {
            magVisual.gameObject.SetActive(true);
        }
        EstaRecargando = false;
    }
    private void SoltarMagAlPiso()
    {
        if(magVisual == null)
        {
            return;
        }
        GameObject magCaida = Instantiate(magVisual.gameObject, magVisual.position, magVisual.rotation);
        magCaida.transform.SetParent(null);

        Rigidbody rb = magCaida.GetComponent<Rigidbody>();
        if(rb == null)
        {
            rb = magCaida.AddComponent<Rigidbody>();
        }
        Vector3 direccionAleatoria = new Vector3(Random.Range(-0.3f, 0.3f), 0.3f, Random.Range(-0.3f, 0.3f));
        rb.AddForce(direccionAleatoria * fuerzaCaidaMag, ForceMode.Impulse);
        rb.AddTorque(Random.insideUnitSphere * fuerzaCaidaMag, ForceMode.Impulse);

        Destroy(magCaida, 5f);
        magVisual.gameObject.SetActive(false);
    }

}    