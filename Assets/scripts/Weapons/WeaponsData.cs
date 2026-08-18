using System.IO.Enumeration;
using UnityEngine;
[CreateAssetMenu(fileName = "Nueva Arma" , menuName = "ShooterMenu/WeaponData")]
public class WeaponsData : ScriptableObject
{
    public string weaponName = "";
    public Sprite icon;
    public float damage = 25f;
    public float fireRate = 8f;
    public float range = 100f;
    public int magazineSize = 30;

    public AudioClip fireSound;
    public AudioClip reloadSound;
}
