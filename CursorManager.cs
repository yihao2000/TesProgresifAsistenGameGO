using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorManager : MonoBehaviour
{
    // Start is called before the first frame update
    bool isLocked;

    // Update is called once per frame

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        isLocked = true;
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftAlt))
        {
            if (isLocked)
            {
                Cursor.lockState = CursorLockMode.None;
                isLocked = false;
            }
            else
            {
                Cursor.lockState = CursorLockMode.Locked;
                isLocked = true;
            }
            
        }
    }
}
