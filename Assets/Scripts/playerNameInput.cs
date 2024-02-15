using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class playerNameInput : MonoBehaviour
{
    public TextMeshProUGUI textInput;
    public TMP_InputField inputField;
    public Text highScoreText;


    public void SavePlayerName()
    {
        string input = textInput.text;
        Debug.Log(input);
        MainManager.Instance.SetPlayerName(input);
    }

    public void LoadPlayerName()
    {
        inputField.text = MainManager.Instance.playerName;
        highScoreText = MainManager.Instance.highScoreText;
    }

}
