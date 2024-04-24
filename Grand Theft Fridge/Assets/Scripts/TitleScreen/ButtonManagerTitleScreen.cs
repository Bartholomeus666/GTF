using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonManagerTitleScreen : MonoBehaviour
{
    [SerializeField] private string GameSceneName;


    public void PlayGameButton()
    {
        SceneManager.LoadScene(GameSceneName);
    }
}
