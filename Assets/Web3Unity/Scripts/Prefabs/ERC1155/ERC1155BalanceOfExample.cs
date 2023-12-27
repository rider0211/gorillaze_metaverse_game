using System.Collections;
using System.Numerics;
using System.Collections.Generic;
using UnityEngine;

public class ERC1155BalanceOfExample : MonoBehaviour
{

    public string[] Tockenids;
    int NftCount = 0;
    async public void GetNfts()
    {
        string chain = "ethereum";
        string network = "rinkeby";
        string contract = "0x88B48F654c30e99bc2e4A1559b4Dcf1aD93FA656";
        string account = PlayerPrefs.GetString("Account");
        Debug.Log("account getnft ::: " + PlayerPrefs.GetString("Account"));

        for (int i = 0; i < Tockenids.Length; i++)

        {
            string tokenId = Tockenids[i];

            BigInteger balanceOf = await ERC1155.BalanceOf(chain, network, contract, account, tokenId);
            //print(balanceOf);
            if (balanceOf > 0)
            {
                NftCount++;
                Debug.Log("blance of Nft count :: " + NftCount);
            }
        }

        Debug.Log("Nft count :: " + NftCount);
        // if (NftCount == 1)
        // {
        //     GameObject.Find("HealthManager").GetComponent<HealthManager>().addhealth(1);
        //     Debug.Log(" add life ::  " + 1);
        // }
        // else if (NftCount == 2)
        // {
        //     GameObject.Find("HealthManager").GetComponent<HealthManager>().addhealth(2);
        //     Debug.Log(" add life ::  " + 2);
        // }
        // else if (NftCount == 3)
        // {
        //     GameObject.Find("HealthManager").GetComponent<HealthManager>().addhealth(3);
        //     Debug.Log(" add life ::  " + 3);
        // }
        // else if (NftCount == 4)
        // {
        //     GameObject.Find("HealthManager").GetComponent<HealthManager>().addhealth(4);
        //     Debug.Log(" add life ::  " + 4);
        // }
        // else if (NftCount == 5)
        // {
        //     GameObject.Find("HealthManager").GetComponent<HealthManager>().addhealth(4);
        //     Debug.Log(" add life ::  " + 4);
        // }

    }
}
