using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GoBackToTitle : MonoBehaviour
{ 
    public void GoBack()
    {
        SceneManager.LoadScene("TitelScreen");
    }
}
