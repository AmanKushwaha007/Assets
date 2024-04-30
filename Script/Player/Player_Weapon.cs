using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;

public class Player_Weapon : MonoBehaviour
{
    public Transform WeaponHolder, WeaponPoistion, Weapon_Drop_Transform;
    public Waypoints_Manager WPM;
    public int current_Weapon_Index;
    public int Available_Weapon_Place, MaxWeaponHandle;
    public bool isPickUped, isHoldingWeapon;

    public List<GameObject> bulletsPool;
    public int max_Bullet_Handle, bullet_Pointer, point;
    public GameObject bulletParent, Lefthand, RightHand;

    public float force;

    
    
    RaycastHit hit;



    [HideInInspector] public Player_Movement PM;
    [SerializeField] public List<Weapon_Script.Weapon_Details> List_Weapons_Detail;
    [HideInInspector] public Weapon_Script.Weapon_Details Current_Collided_Wepon_Details;
    [HideInInspector] public Weapon_Script.Weapon_Details Holding_Weapon_Details;
    [HideInInspector] public bool isFireing, isBurstFired;

    public int tempBurstCount;

    public float NextFire, burstFireControle, burstNextFire, lastFire, lineRendererInterval;


    public void Buttons()
    {
        if (Input.GetKeyDown("e") && Check_Space_For_Weapon() && Available_Weapon_Place < MaxWeaponHandle)
        {
            isPickUped = true;
            Equip();
        }
        if (Input.GetKeyDown("f"))
        {
            Drop();
        }


        switch(Holding_Weapon_Details.ShootingType)
        {
            case('A'):
                isFireing = Input.GetKey(KeyCode.Mouse1);
                break;
            case ('C'):
                isBurstFired = Input.GetKey(KeyCode.Mouse1);
                break;
            case ('S'):
            case ('B'):
                isFireing = Input.GetKeyDown(KeyCode.Mouse1);
                break;
            default:
                isFireing = false;
                break;
        }
        

        if (Input.GetKeyDown(KeyCode.R) && !Holding_Weapon_Details.isReloading) Reload();

        if (Input.GetKeyDown(KeyCode.Alpha1)) Holding_Gun(0);
        else if (Input.GetKeyDown(KeyCode.Alpha2)) Holding_Gun(1);
        else if (Input.GetKeyDown(KeyCode.Alpha3)) Holding_Gun(2);
        else if (Input.GetKeyDown(KeyCode.Alpha4)) Holding_Gun(3);
        else if (Input.GetKeyDown(KeyCode.Alpha4)) Holding_Gun(4);
    }


    /// <summary>
    /// Collect All details from Weapon and assing in temprary slot
    /// </summary>
    /// <param name="weapon_Availiable"></param>
    /// <param name="weapon_Prefab"></param>
    /// <param name="weapon_LineRenderer"></param>
    /// <param name="weapon_Fireing_Point"></param>
    public void Current_Collided_Weapon_Details_Assigning(Weapon_Script.Weapon_Details X)
    {
        this.Current_Collided_Wepon_Details = X;
    }

    
    [HideInInspector]
    private bool Check_Space_For_Weapon()
    {
        if (List_Weapons_Detail != null)
        {
            for (int i = 0; i < List_Weapons_Detail.Capacity; i++)
            {
                if (List_Weapons_Detail[i].WeaponAvailiable == false)
                {
                    Available_Weapon_Place = i;
                    return true;
                }
            }

            return false;
        }
        return false;
    }
    
    
    private void Equip()
    {
        if (isPickUped && Current_Collided_Wepon_Details.Weapon_Prefab != null)
        {
            isPickUped = false;
            List_Weapons_Detail[Available_Weapon_Place] = Current_Collided_Wepon_Details;

            //Placeing Weapon at Weapon Position
            GameObject currentWeapon = List_Weapons_Detail[Available_Weapon_Place].Weapon_Prefab;
            currentWeapon.SetActive(false);
            currentWeapon.GetComponent<BoxCollider>().isTrigger = true;
            currentWeapon.GetComponent<Rigidbody>().useGravity = false;
            currentWeapon.GetComponent<Rigidbody>().isKinematic = true;
            currentWeapon.GetComponent<Collider>().enabled = false;
            currentWeapon.transform.parent = WeaponHolder;
            currentWeapon.transform.position = WeaponPoistion.position;
            currentWeapon.transform.rotation = WeaponPoistion.rotation;
            //PMM.Weapon.SetActive(true);
        }
        Current_Collided_Wepon_Details = new Weapon_Script.Weapon_Details();

        //Weapon_Drop_Button.SetActive(Holding_Weapon_Details.WeaponAvailiable);
    }
    
    
    public void Holding_Gun(int x)
    {
        current_Weapon_Index = x;
        for (int i = 0; i < List_Weapons_Detail.Count; i++)
        {
            if (List_Weapons_Detail[i].Weapon_Prefab != null)
            {
                List_Weapons_Detail[i].Weapon_Prefab.SetActive(false);
            }
        }
        if (x != MaxWeaponHandle && List_Weapons_Detail[x].Weapon_Prefab != null)
        {
            isHoldingWeapon = true;
            current_Weapon_Index = x;
            List_Weapons_Detail[x].Weapon_Prefab.SetActive(true);
            Holding_Weapon_Details = List_Weapons_Detail[x];
            Lefthand.transform.position = Holding_Weapon_Details.leftHand.transform.position;
            RightHand.transform.position = Holding_Weapon_Details.rightHand.transform.position;
            //PMM.ChangeControle_GUI("Basic", "Weapon");
            //PMM.Enable_Weapon_Controle(true);
            PM.isBow = false;
        }
        else
        {
            isHoldingWeapon = false;
            current_Weapon_Index = x;
            Holding_Weapon_Details = new Weapon_Script.Weapon_Details();
            PM.isBow = true;
        }

    }
    
    
    /// <summary>
    /// For Drop Holding Weapon
    /// </summary>
    public void Drop()
    {
        if (current_Weapon_Index == -1) return;

        isHoldingWeapon = false;
        List_Weapons_Detail[current_Weapon_Index].Weapon_Prefab.transform.parent = null;
        PM.lineRenderer.enabled = false;
        List_Weapons_Detail[current_Weapon_Index].Weapon_Prefab.GetComponent<BoxCollider>().isTrigger = false;
        List_Weapons_Detail[current_Weapon_Index].Weapon_Prefab.GetComponent<Collider>().enabled = true;
        List_Weapons_Detail[current_Weapon_Index].Weapon_Prefab.GetComponent<Rigidbody>().useGravity = true;
        List_Weapons_Detail[current_Weapon_Index].Weapon_Prefab.GetComponent<Rigidbody>().isKinematic = false;
        List_Weapons_Detail[current_Weapon_Index].Weapon_Prefab.GetComponent<Weapon_Script>().Details = Holding_Weapon_Details;
        List_Weapons_Detail[current_Weapon_Index] = new Weapon_Script.Weapon_Details();
        Holding_Weapon_Details = new Weapon_Script.Weapon_Details();
        current_Weapon_Index = -1;
        //Weapon_Drop_Button.SetActive(Holding_Weapon_Details.WeaponAvailiable);

        for (int i = 0; i < List_Weapons_Detail.Capacity; i++)
        {
            if (List_Weapons_Detail[i].WeaponAvailiable)
            {
                //PMM.Weapon.SetActive(true);
            }
            //else PMM.Weapon.SetActive(false);
        }
    }
    
    
    void Shoot()
    {
        if (Holding_Weapon_Details.bulletType == 'L') LineWeapon();
        else if (Holding_Weapon_Details.bulletType == 'P') ProWeapon();
        else Handed();
    }


    /// <summary>
    /// This is a function which is called after the shoot decision
    /// which is for Line rendrer
    /// </summary>
    void LineWeapon()
    {
        Holding_Weapon_Details.currentMag--;

        PM.lineRenderer.enabled = true;
        PM.lineRenderer.SetPosition(0, Holding_Weapon_Details.Weapon_Fireing_Point.transform.position);

        if (Physics.Raycast(PM.player_Camera.transform.position, PM.player_Camera.transform.forward,
                            out hit, Holding_Weapon_Details.range))
        {
            PM.lineRenderer.SetPosition(1, hit.point);
            if (hit.collider.CompareTag("AI"))
            {
                hit.collider.GetComponent<Enemy_Status>().Take_damage(10);
            }
            else
            {
                BulletInstancer();
            }
        }
        else
        {
            PM.lineRenderer.SetPosition(1, PM.player_Camera.transform.forward.normalized * 100);
        }

        if (Holding_Weapon_Details.currentMag == 0 && Holding_Weapon_Details.maxAvailale > 0)
        {
            Reload();
            Holding_Weapon_Details.isReloading = true;
        }
    }
    
    
    void ProWeapon()
    {
        Holding_Weapon_Details.currentMag--;
        Physics.Raycast(PM.player_Camera.transform.position, PM.player_Camera.transform.forward,
                            out hit, Holding_Weapon_Details.range);
        if (hit.point == Vector3.zero)
        { 
            bulletsPool[point] = Instantiate(Holding_Weapon_Details.bullet,
                                    Holding_Weapon_Details.Weapon_Fireing_Point.transform.position
                        , Quaternion.LookRotation(hit.point), bulletParent.transform);
        }
        else
        {
            bulletsPool[point] = Instantiate(Holding_Weapon_Details.bullet,
                                    Holding_Weapon_Details.Weapon_Fireing_Point.transform.position
                        , Quaternion.LookRotation(PM.player_Camera.transform.forward.normalized * 100), bulletParent.transform);
        }
        point++;

        if (Holding_Weapon_Details.currentMag == 0 && Holding_Weapon_Details.maxAvailale > 0)
        {
            Reload();
            Holding_Weapon_Details.isReloading = true;
        }
    }
    
    
    void Handed()
    {
        Bow();
    }
    
    
    public void Bow()
    {
        
    }
    
    
    public void Reload()
    {
        int requirment = Holding_Weapon_Details.magCapacity - Holding_Weapon_Details.currentMag;
        if (Holding_Weapon_Details.maxAvailale >= requirment)
        {
            Holding_Weapon_Details.currentMag = Holding_Weapon_Details.magCapacity;
            Holding_Weapon_Details.maxAvailale -= requirment;
        }
        else
        {
            Holding_Weapon_Details.currentMag += Holding_Weapon_Details.maxAvailale;
            Holding_Weapon_Details.maxAvailale = 0;
        }
        Invoke("finishReload", Holding_Weapon_Details.reloadingTime);

    }
    
    
    private void finishReload()
    {
        Holding_Weapon_Details.isReloading = false;
    }
    
    
    public void Shoot_Call()
    {
        if (Holding_Weapon_Details.Weapon_Prefab != null )
        {
            if (isBurstFired)
            {
                if(burstFireControle < Time.time && !Holding_Weapon_Details.isReloading && Holding_Weapon_Details.currentMag > 0)
                {
                    burstFireControle = Time.time + Holding_Weapon_Details.fireRate;
                    isFireing = true;
                }
                else
                {
                    isFireing = false;
                }
            }

            if (isFireing) {
                if (NextFire < Time.time && !Holding_Weapon_Details.isReloading && Holding_Weapon_Details.currentMag > 0)
                {
                    lastFire = NextFire;
                    NextFire = Time.time + Holding_Weapon_Details.fireRate;
                    if (Holding_Weapon_Details.ShootingType == 'B' || Holding_Weapon_Details.ShootingType == 'C')
                    {
                        tempBurstCount = 0;
                        burstNextFire = Time.time + Holding_Weapon_Details.burstFireRate;
                    }
                    Shoot();
                }
                else
                {
                    if (Time.time > lastFire + lineRendererInterval) PM.lineRenderer.enabled = false;
                }
            }
            else 
            {
                if (tempBurstCount != -1 && tempBurstCount < Holding_Weapon_Details.burstCount-1 && Holding_Weapon_Details.currentMag > 0)
                {
                    if (burstNextFire < Time.time && !Holding_Weapon_Details.isReloading)
                    {
                        burstNextFire = Time.time + Holding_Weapon_Details.burstFireRate;
                        Shoot();
                        tempBurstCount++;
                    }
                }
                else
                {
                    tempBurstCount = -1;
                }
                if (Time.time > lastFire + lineRendererInterval)PM.lineRenderer.enabled = false;
            }
            

        }
    }

    public void BulletInstancer()
    {
        if(max_Bullet_Handle > bulletsPool.Capacity)
        {
            bulletsPool.Add(Instantiate(Holding_Weapon_Details.Bullet_Hole, hit.point + hit.normal.normalized * 0.01f,
                            Quaternion.LookRotation(hit.normal) ) );
            bullet_Pointer++;
        }
        else
        {
            if(bullet_Pointer >= max_Bullet_Handle)
            {
                bullet_Pointer = 0;
            }
            bulletsPool[bullet_Pointer].transform.position = hit.point + hit.normal.normalized * 0.01f;
            bulletsPool[bullet_Pointer].transform.rotation = Quaternion.LookRotation(hit.normal);
            bullet_Pointer++;
        }
    }
}
