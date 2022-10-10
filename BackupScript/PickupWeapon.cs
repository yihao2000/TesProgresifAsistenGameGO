using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupWeapon : MonoBehaviour
{

    public GameObject whatCanIPickup;
    public GameObject playerRightHand;

    private bool isPickable;
  
    // Start is called before the first frame update
    void Start()
    {
        

    }

    // Update is called once per frame
    void Update()
    {

        if(isPickable == true)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                PickUpObject();
            }
            
        }
    }

    public void PickUpObject()
    {
        whatCanIPickup.transform.SetParent(playerRightHand.transform);
        whatCanIPickup.transform.localPosition = new Vector3(0f, 0f, 0f);


    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PickableWeapon"))
        {
            
            whatCanIPickup = other.gameObject;
            Debug.Log("It's Pickable " + other.gameObject.name);

            isPickable = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("PickableWeapon"))
        {

            isPickable = false;
        }
    }


}
