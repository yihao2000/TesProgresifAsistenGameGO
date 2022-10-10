using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{

    //Panel
    PickupInformation pickupInformation;


    Player player;
    Player contact;

    public int damage;
    public int attackSpeed;
    public string name;

    bool isHeld = false;

    float colliderRadius = 0.5f;

    public Vector3 PickPosition;
    public Vector3 PickRotation;


    void Start()
    {
        pickupInformation = GameObject.Find("PickupInformation").GetComponent<PickupInformation>();
    }

    public void Drop()
    {
     
        isHeld = false;
        Vector3 lastPos = new Vector3(gameObject.transform.parent.transform.position.x, gameObject.transform.parent.transform.position.y - 1, gameObject.transform.parent.transform.position.z);
        gameObject.transform.parent = null;
        gameObject.transform.position = lastPos;

        

        gameObject.transform.localRotation = Quaternion.Euler(90,
            gameObject.transform.localRotation.eulerAngles.y,
            gameObject.transform.localRotation.eulerAngles.z
           );


        player = null;
        contact = null;


    }

    public void SetPosition()
    {
        transform.localPosition = PickPosition;
        transform.localEulerAngles = PickRotation;
    }

    public void Take()
    {
            HidePanel();
            player = contact;
            Weapon otherWeapon = GameObject.Find("RIGHT_WEAPON_COMBAT_SLOT").transform.GetComponentInChildren<Weapon>();

            if(otherWeapon != null)
            {
            Debug.Log("Masuk Ada Weapon Lain");
                otherWeapon.Drop();
            }

                isHeld = true;

                player.holdWeapon();
                player.GetComponent<CapsuleCollider>().radius = colliderRadius;

                gameObject.transform.parent = GameObject.Find("RIGHT_WEAPON_COMBAT_SLOT").transform;


                SetPosition();

                player.damage = damage;
                player.attackDuration = attackSpeed;
    }

    void Update()
    {
        if(isHeld == false)
        {
            SpinAround();
        }
        if(contact != null && player == null){

            if (Input.GetKeyDown(KeyCode.E))
            {
                
                Take();
            }
              
        }

    }


    void ShowPanel()
    {
        pickupInformation.SetWeaponName(name);
        pickupInformation.ShowPanel();
    }

    void HidePanel()
    {
        pickupInformation.HidePanel();
    }
    void SpinAround()
    {
        if(transform.rotation.y < 360)
        {
            transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y + 0.5f, transform.rotation.eulerAngles.z);
        }
        else
        {
            transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, 0, transform.rotation.eulerAngles.z);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.GetComponentInChildren<Player>() != null && player==null)
        {
            ShowPanel();
            contact = other.GetComponentInChildren<Player>();             
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.GetComponentInChildren<Player>() != null && player ==null)
        {
            HidePanel();
            contact = null;
        }
    }



}
