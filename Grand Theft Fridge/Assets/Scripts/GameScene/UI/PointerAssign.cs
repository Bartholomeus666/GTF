using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointerAssign : MonoBehaviour
{
    private void Start()
    {
        GameObject[] pointers = GameObject.FindGameObjectsWithTag("Pointer");

        for (int i = 0; i < pointers.Length; i++)
        {
            Pointer pointerscript = pointers[i].GetComponent<Pointer>();

            if(pointerscript.IsAssigned == true)
            {

            }
            else if(pointerscript.IsAssigned == false)
            {
                pointerscript.IsAssigned = true;
                pointerscript.Player = this.gameObject;
                break;
            }
        }
    }
}
