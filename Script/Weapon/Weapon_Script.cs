using UnityEngine;

public class Weapon_Script : MonoBehaviour
{
    [System.Serializable]
    public struct Weapon_Details
    {
        [Header("Guns")]
        public GameObject Weapon_Prefab;
        public GameObject Bullet_Hole, Weapon_Fireing_Point, leftHand, rightHand;
        public int maxCapacity, maxAvailale, magCapacity, currentMag;
        public float Damage, fireRate, burstFireRate, reloadingTime, range;
        public bool WeaponAvailiable, isReloading;
        [Tooltip("1) A = Auto, \n2) B = Burst, \n3) S = Single \n4)C = Burst Auto")]
        public char ShootingType;
        [Tooltip("1) P = Projectile \n2) L = Line Renderer \n3) N = None")]
        public char bulletType;

        [Header("If Burst ")]
        public int burstCount;
        public float burstRate;

        [Header("If Projectile")]
        public GameObject bullet;

        [Header("If Extendable")]
        public bool isExtended;
        public int extended_Ammo;

        [Header("Handed")]
        public int x;

    }
    public Weapon_Details Details;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer != 9)
        {
            GetComponent<BoxCollider>().isTrigger = true;
            GetComponent<Rigidbody>().useGravity = false;
            GetComponent<Rigidbody>().isKinematic = true;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        //if (other.gameObject.layer != 9)        {            GetComponent<BoxCollider>().isTrigger = true;            GetComponent<Rigidbody>().useGravity = false;            GetComponent<Rigidbody>().isKinematic = true;        }
    }

    public void changeState()
    {
        
    }
    public void Assign_Weapon_Details(Player_Weapon Player)
    {
        Player.Current_Collided_Weapon_Details_Assigning(this.Details);
    }

    public void Weapon_Details_Assigning(bool Weapon_Availiable, GameObject Weapon_Prefab)
    {

    }
}
