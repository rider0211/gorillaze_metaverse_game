using System.Collections;
using System.Numerics;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ERC721Balance : MonoBehaviour
{
    string chain = "Avalanche";
    string network = "mainnet";
    string contract = "0x9c466304100861c8a5864939eb5681233f13c20d";
    string account;
    int first = 500;
    int skip = 0;
    int NftCount = 0;
    int nftBlance = 0;
    public bool _webtest = false;
    public Button _PlayToEarnBtn;
    public bool _TestPlayToEarn = false;

    private void Start()
    {
        //if (_TestPlayToEarn)
        //    _PlayToEarnBtn.interactable = true;
         if (_TestPlayToEarn)
             NftDetials();
    }
    async public void NftDetials()
    {
         if (_TestPlayToEarn) { 
             account = "0x1a9f90c096c59a4bc701635572622659f5c2497c";
        }
        else if (_webtest)
        {
            account = "0x1a9f90c096c59a4bc701635572622659f5c2497c";
        }
        else { 
            account = PlayerPrefs.GetString("Account");
        }
        Debug.Log("Nft account   " + account);
        int balance = await ERC721.BalanceOf(chain, network, contract, account);
        print("owner of ERC721 balance " + balance);
        //nftBlance = balance;
        nftBlance = 1;
        if (nftBlance > 0)
        {
            _PlayToEarnBtn.interactable = true;
            Debug.Log("Totle Nft count :: " + nftBlance);
        }
        // PlayerRewards();}
        // GetAllNFTDat();
    }
    async void GetAllNFTDat()
    {


        string response = await EVM.AllErc721(chain, network, account, contract, first, skip);
        try
        {
            // Debug.Log("response data " + response);

            NFTData[] erc721s = JsonConvert.DeserializeObject<NFTData[]>(response);
            //Debug.Log("reponse ::  " + erc721s.Length);

            foreach (var item in erc721s)
            {
                // print("reponse contract ::   " + item.contract);
                print("reponse tokenId  ::" + item.tokenId);
                // print("reponse  uri     ::   " + item.uri);
                print("reponse  balance::   " + item.balance);
                if (item.balance > 0)
                {
                    nftBlance++;

                }
            }

        }
        catch
        {
            print("Error: " + response);
        }
    }

    public void playToEarnBtnClick()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    void PlayerRewards()
    {

        //     if (nftBlance == 1 && nftBlance < 3)
        //     {
        //         HealthManager.instance.addhealth(1);
        //        // HealthManager.instance.activateShield(true);

        //         Debug.Log(" add life ::  " + 1);
        //     }
        //     else if (nftBlance > 2 && nftBlance < 5)
        //     {
        //         HealthManager.instance.addhealth(2);
        //         Debug.Log(" add life ::  " + 2);
        //     }
        //     else if (nftBlance > 4 && nftBlance < 10)
        //     {
        //         HealthManager.instance.addhealth(3);
        //         Debug.Log(" add life ::  " + 3);
        //     }
        //     else if (nftBlance > 9 && nftBlance < 20)
        //     {
        //         HealthManager.instance.addhealth(4);
        //         Debug.Log(" add life ::  " + 4);
        //     }
        //     else if (nftBlance > 19)
        //     {
        //         HealthManager.instance.addhealth(4);
        //         HealthManager.instance.activateShield(true);
        //         Debug.Log(" add life ::  " + 4);
        //     }
        //    _mainScreenScripts.getplaybutton().interactable=true;
    }
    private class NFTData
    {
        public string contract { get; set; }
        public string tokenId { get; set; }
        public string uri { get; set; }
        public int balance { get; set; }
    }
}
