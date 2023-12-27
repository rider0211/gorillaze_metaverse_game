using System;
using System.Collections;

using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;
using System.Numerics;
using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine.UI;

public class UiManager : MonoBehaviour
{
    #region Singleton

    public static UiManager instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }

        else if (instance != this)
        {
            Destroy(this.gameObject);
        }
    }

    #endregion

    [Header("Texts")]
    public Text coin_Text;
    public Text finLevelCoin_Text;
    public Text totalCoins_Text;


    [Header("Panels")]
    public GameObject gamePlayPanel_GameObject;
    public GameObject panels_GameObject;
    public GameObject options_GameObject;
    public GameObject radeemPenal;

    [Header("FX")]
    public GameObject footDust_FX;
    public GameObject pickUpCoin_FX;

    [Header("Image")]
    public Image sfx_Image;
    public Image music_Image;

    [Header("Sprite")]
    public Sprite emptyHeart;
    public Sprite fullHeart;
    public Sprite onImage;
    public Sprite offImage;

    [Header("Objects")]
    public GameObject buttonContainer_GameObject;
    public GameObject HeaderContainer_GameObject;
    public GameObject RadeemCoinFailedText;

    [Header("Camera")]
    public Camera _camera;

    [Header("Buttons")]
    public Button _radeemButton;
    public Button _radeemPenalButton;

    private String token_transfer_abi = "[{\"inputs\":[{\"internalType\":\"contract IERC20\",\"name\":\"_VMGCAddress\",\"type\":\"address\"}],\"stateMutability\":\"nonpayable\",\"type\":\"constructor\"},{\"anonymous\":false,\"inputs\":[{\"indexed\":true,\"internalType\":\"address\",\"name\":\"previousOwner\",\"type\":\"address\"},{\"indexed\":true,\"internalType\":\"address\",\"name\":\"newOwner\",\"type\":\"address\"}],\"name\":\"OwnershipTransferred\",\"type\":\"event\"},{\"inputs\":[],\"name\":\"TransferVMGC\",\"outputs\":[],\"stateMutability\":\"nonpayable\",\"type\":\"function\"},{\"inputs\":[],\"name\":\"owner\",\"outputs\":[{\"internalType\":\"address\",\"name\":\"\",\"type\":\"address\"}],\"stateMutability\":\"view\",\"type\":\"function\"},{\"inputs\":[],\"name\":\"renounceOwnership\",\"outputs\":[],\"stateMutability\":\"nonpayable\",\"type\":\"function\"},{\"inputs\":[{\"internalType\":\"address\",\"name\":\"newOwner\",\"type\":\"address\"}],\"name\":\"transferOwnership\",\"outputs\":[],\"stateMutability\":\"nonpayable\",\"type\":\"function\"}]";

    private String avalanche_testnet_contract_address = "0x84f820eaBB503aF873FB23f063213Df770d4E72a";
    private String avalanche_mainnet_contract_address = "0x021406C218C98aa70bdB5364E3e2eb22Ed39d7CD";

    private String matic_testnet_contract_address = "0x625B7C4cb045c515101eE39B06d4F1075BB30AD6";
    private String matic_mainnet_contract_address = "";


    GameObject tempBtn, tempHdr;
    bool isOptionPanelOpen = false;
    public GameObject[] coinscollection;

    private void Start()
    {
        updatStates();

        HealthManager.instance.HealthPanel = GameObject.FindGameObjectWithTag("HealthPanel");

        for (int i = 0; i < HealthManager.instance.getHealth(); i++)
        {
            HealthManager.instance.HealthPanel.transform.GetChild(i).gameObject.GetComponent<Image>().sprite = fullHeart;
        }

        tempBtn = buttonContainer_GameObject.transform.GetChild(0).gameObject;
        tempHdr = HeaderContainer_GameObject.transform.GetChild(0).gameObject;
        _radeemPenalButton.onClick.AddListener(redeemPenalBtnFn);

    }

    public void pausePanel()
    {
        finLevelCoin_Text.text = levelManager.instance.getCoin().ToString();
        totalCoins_Text.text = GameManager.instance.getTotalCoins().ToString();

        tempBtn.SetActive(false);
        buttonContainer_GameObject.transform.GetChild(0).gameObject.SetActive(true);

        panels_GameObject.SetActive(true);

        tempBtn = buttonContainer_GameObject.transform.GetChild(0).gameObject;
        tempHdr = HeaderContainer_GameObject.transform.GetChild(0).gameObject;
    }
    public void RetryPanel()
    {
        finLevelCoin_Text.text = levelManager.instance.getCoin().ToString();
        totalCoins_Text.text = GameManager.instance.getTotalCoins().ToString();

        tempBtn.SetActive(false);
        tempHdr.SetActive(false);

        buttonContainer_GameObject.transform.GetChild(1).gameObject.SetActive(true);
        HeaderContainer_GameObject.transform.GetChild(1).gameObject.SetActive(true);

        panels_GameObject.SetActive(true);

        tempBtn = buttonContainer_GameObject.transform.GetChild(1).gameObject;
        tempHdr = HeaderContainer_GameObject.transform.GetChild(1).gameObject;
    }
    public void GameOverPanel()
    {
        finLevelCoin_Text.text = levelManager.instance.getCoin().ToString();
        totalCoins_Text.text = GameManager.instance.getTotalCoins().ToString();

        tempBtn.SetActive(false);
        tempHdr.SetActive(false);

        buttonContainer_GameObject.transform.GetChild(2).gameObject.SetActive(true);
        HeaderContainer_GameObject.transform.GetChild(2).gameObject.SetActive(true);

        panels_GameObject.SetActive(true);
        if ((levelManager.instance.getCoin() + GameManager.instance.getTotalCoins()) > GameManager.instance._coinsRedeem && !GameManager.instance.redeemedAvalaunche)
        {
            Debug.Log("redeam coin  :: " + (levelManager.instance.getCoin() + GameManager.instance.getTotalCoins()));
            RadeemCoinFailedText.SetActive(true);
        }
        tempBtn = buttonContainer_GameObject.transform.GetChild(2).gameObject;
        tempHdr = HeaderContainer_GameObject.transform.GetChild(2).gameObject;
    }
    public void LevelCompletePanel()
    {
        finLevelCoin_Text.text = levelManager.instance.getCoin().ToString();
        totalCoins_Text.text = GameManager.instance.getTotalCoins().ToString();

        tempBtn.SetActive(false);
        tempHdr.SetActive(false);

        buttonContainer_GameObject.transform.GetChild(3).gameObject.SetActive(true);
        HeaderContainer_GameObject.transform.GetChild(3).gameObject.SetActive(true);

        panels_GameObject.SetActive(true);

        tempBtn = buttonContainer_GameObject.transform.GetChild(3).gameObject;
        tempHdr = HeaderContainer_GameObject.transform.GetChild(3).gameObject;
    }
    public void optionPanel()
    {
        if (!isOptionPanelOpen)
        {
            isOptionPanelOpen = true;
            options_GameObject.SetActive(true);
        }
        else
        {
            isOptionPanelOpen = false;
            options_GameObject.SetActive(false);
        }
    }

    public void sfx_Button()
    {
        if (AudioManager.instance.canPlaySfx)
        {
            sfx_Image.sprite = offImage;
            AudioManager.instance.canPlaySfx = false;
        }
        else
        {
            sfx_Image.sprite = onImage;
            AudioManager.instance.canPlaySfx = true;
        }

        PlayerPrefs.SetInt("sfx", AudioManager.instance.canPlaySfx ? 1 : 0);
    }
    public void music_Button()
    {
        if (AudioManager.instance.canPlayBg)
        {
            music_Image.sprite = offImage;
            AudioManager.instance.canPlayBg = false;
            AudioManager.instance.bgMusic.Pause();
        }
        else
        {
            music_Image.sprite = onImage;
            AudioManager.instance.canPlayBg = true;
            AudioManager.instance.bgMusic.Play();
        }

        PlayerPrefs.SetInt("bgMusic", AudioManager.instance.canPlayBg ? 1 : 0);
    }
    void updatStates()
    {

        if (AudioManager.instance.canPlaySfx)
        {
            sfx_Image.sprite = onImage;
        }
        else
        {
            sfx_Image.sprite = offImage;
        }

        if (AudioManager.instance.canPlayBg)
        {
            music_Image.sprite = onImage;
            AudioManager.instance.bgMusic.Play();
        }
        else
        {
            music_Image.sprite = offImage;
        }
    }

    string getContractAddress(int networkId)
    {

        switch (networkId)
        {
            case 80001 :
                return matic_testnet_contract_address;
                break;
            case 137:
                return matic_mainnet_contract_address;
                break;
            case 43113:
                return avalanche_testnet_contract_address;
                break;
            case 43114:
                return avalanche_mainnet_contract_address;
                break;
            default:
                return "";
                break;

        }
    }

    public async void redeemPenalBtnFn()
    {

        //StartCoroutine(AvalunchCoinRedeemApi());
        AvalunchCoinRedeemApi();
        // string responsedata = await AvalunchCoinCalling.AvalunchCoinRadeemApi();
        // Debug.Log("respose data::::   " + responsedata);
    }
     
    async void AvalunchCoinRedeemApi()
    {
        // smart contract method to call
        string method = "TransferVMGC";
        // abi in json format
        string abi = "[{\"inputs\":[{\"internalType\":\"contract IERC20\",\"name\":\"_VMGCAddress\",\"type\":\"address\"}],\"stateMutability\":\"nonpayable\",\"type\":\"constructor\"},{\"anonymous\":false,\"inputs\":[{\"indexed\":true,\"internalType\":\"address\",\"name\":\"previousOwner\",\"type\":\"address\"},{\"indexed\":true,\"internalType\":\"address\",\"name\":\"newOwner\",\"type\":\"address\"}],\"name\":\"OwnershipTransferred\",\"type\":\"event\"},{\"inputs\":[],\"name\":\"TransferVMGC\",\"outputs\":[],\"stateMutability\":\"nonpayable\",\"type\":\"function\"},{\"inputs\":[],\"name\":\"owner\",\"outputs\":[{\"internalType\":\"address\",\"name\":\"\",\"type\":\"address\"}],\"stateMutability\":\"view\",\"type\":\"function\"},{\"inputs\":[],\"name\":\"renounceOwnership\",\"outputs\":[],\"stateMutability\":\"nonpayable\",\"type\":\"function\"},{\"inputs\":[{\"internalType\":\"address\",\"name\":\"newOwner\",\"type\":\"address\"}],\"name\":\"transferOwnership\",\"outputs\":[],\"stateMutability\":\"nonpayable\",\"type\":\"function\"}]";
        // address of contract
        int networkId = Web3GL.Network();

        string contract_address = getContractAddress(networkId);

        // array of arguments for contract
        string args = "[]";
        // value in wei
        string value = "0";
        string gasLimit = "";
        // gas price OPTIONAL
        string gasPrice = "";
        // connects to user's browser wallet (metamask) to update contract state
        try
        {
            string response = await Web3GL.SendContract(method, abi, contract_address, args, value, gasLimit, gasPrice);
            if (response != "0") { 
                PlayerController.instance.canMove = true;
                radeemPenal.SetActive(false);
                GameManager.instance.redeemedAvalaunche = true;
                int newcoins = (GameManager.instance.getTotalCoins() + levelManager.instance.getCoin()) - GameManager.instance._coinsRedeem;
                GameManager.instance.ResetData();
                levelManager.instance.updateCoin(-levelManager.instance.getCoin());
                levelManager.instance.updateCoin(newcoins);
            }
            else
            {
                PlayerController.instance.canMove = true;
                radeemPenal.SetActive(false);
                GameManager.instance.redeemedAvalaunche = true;
            }
        }
        catch (Exception e)
        {
            Debug.LogException(e, this);
        }
    }

    IEnumerator AvalunchCoinRedeemApii()
    {
        WWWForm form = new WWWForm();
        form.AddField("module", "account");
        form.AddField("action", "tokentx");
#if UNITY_EDITOR
        form.AddField("address", "0x095A07CA72B9165FC2dfca5BbF75024d464bdfd5");
#else
        form.AddField("address", PlayerPrefs.GetString("Account"));
#endif

        form.AddField("startblock", 0);
        form.AddField("endblock", 2500000);
        form.AddField("sort", "asc");
        form.AddField("apikey", "Y6UGH566KC6QS49NCZAFYFA82IIN5BMCNJ");

        using (UnityWebRequest www = UnityWebRequest.Post("https://api.snowtrace.io/api?", form))
        {
            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.Log(www.error);
            }
            else
            {
                ResponseData _responseapi = JsonUtility.FromJson<ResponseData>(www.downloadHandler.text);
                // ResponseData _responseapi = JsonUtility.FromJson<ResponseData>(www.downloadHandler.data);

                Debug.Log("response    " + www.downloadHandler.text);
                Debug.Log("response status  : " + _responseapi.status);
                Debug.Log("response message  : " + _responseapi.message);
                if (_responseapi.status == "0")
                {
                    PlayerController.instance.canMove = true;
                    radeemPenal.SetActive(false);
                    GameManager.instance.redeemedAvalaunche = true;
                    int newcoins = (GameManager.instance.getTotalCoins() + levelManager.instance.getCoin()) - GameManager.instance._coinsRedeem;
                    GameManager.instance.ResetData();
                    levelManager.instance.updateCoin(-levelManager.instance.getCoin());
                    levelManager.instance.updateCoin(newcoins);

                }
                else
                {
                    PlayerController.instance.canMove = true;
                    radeemPenal.SetActive(false);
                    GameManager.instance.redeemedAvalaunche = true;
                }
            }
        }
    }
}

[System.Serializable]
public class ResponseData
{
    public string status;
    public string message;
    //public List<object> result { get; set; }
}