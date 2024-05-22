using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonManagerStats : MonoBehaviour
{
    public void LoadGameAgain()
    {
        SceneManager.LoadScene("GameScene");
    }
}
