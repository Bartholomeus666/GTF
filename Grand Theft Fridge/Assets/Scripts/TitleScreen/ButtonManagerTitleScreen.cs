using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonManagerTitleScreen : MonoBehaviour
{
    [SerializeField] private string GameSceneName;
    [SerializeField] private string ControlsSceneName;


    public void PlayGameButton()
    {
        SceneManager.LoadScene(GameSceneName);
    }
    public void ControlsButton()
    {
        SceneManager.LoadScene(ControlsSceneName);
    }
}
