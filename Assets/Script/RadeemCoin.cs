using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RadeemCoin : MonoBehaviour
{

    public Button _radeemcoinButton;

    void Start()
    {
        _radeemcoinButton.onClick.AddListener(redeamCoinFn);
    }
    void redeamCoinFn()
    {
        UiManager.instance.radeemPenal.SetActive(true);
        PlayerController.instance.canMove = false;
        PlayerController.instance.StopPlayer();
    }
}
