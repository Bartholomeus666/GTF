using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEngine.Events;

public class LiveManager : MonoBehaviour
{
    [SerializeField] private GameObject[] lives = new GameObject[3];
    [SerializeField] private GameObject YouDiedPanel;

    public bool LoseLife()
    {
        if (lives[0] != null)
        {
            Destroy(lives[0]);
            return false;
        }
        else if (lives[1] != null)
        {
            Destroy(lives[1]);
            return false;
        }
        else if (lives[2] != null)
        {
            Destroy(lives[2]);
            YouDiedPanel.SetActive(true);

            return true;
        }
        else { return true; }
    }
}
