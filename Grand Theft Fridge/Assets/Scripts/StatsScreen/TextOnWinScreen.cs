using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UIElements;

public class TextOnWinScreen : MonoBehaviour
{
    private TMP_Text _text;

    private void Start()
    {
        _text = GetComponent<TMP_Text>();
        _text.text = $"The winner is Player {PlayerPrefs.GetInt("Winner", 0)}";
    }
}
