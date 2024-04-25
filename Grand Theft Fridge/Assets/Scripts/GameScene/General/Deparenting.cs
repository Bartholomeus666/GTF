using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deparenting : MonoBehaviour
{
    private void Start()
    {
        transform.SetParent(null, true);
    }
}
