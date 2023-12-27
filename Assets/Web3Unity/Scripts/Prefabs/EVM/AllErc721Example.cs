using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;

public class AllErc721Example : MonoBehaviour
{
    private class NFTs
    {
        public string contract { get; set; }
        public string tokenId { get; set; }
        public string uri { get; set; }
        public int balance { get; set; }
    }
    int count = 0;
    async void Start()
    {
        string chain = "ethereum";
        string network = "mainnet"; // mainnet ropsten kovan rinkeby goerli
        string account = "0xC8AA5725a0F4cefA6b2E46931994B30194E00b5b";
        string contract = "0xbc61AC336D5558ccb9C304f62a7433e866d1c7e9";
        int first = 500;
        int skip = 0;
        string response = await EVM.AllErc721(chain, network, account, contract, first, skip);
        try
        {
            Debug.Log("response data " + response);

            NFTs[] erc721s = JsonConvert.DeserializeObject<NFTs[]>(response);
            Debug.Log("reponse ::  " + erc721s.Length);

            foreach (var item in erc721s)
            {
                print("reponse contract ::   " + item.contract);
                print("reponse tokenId  ::" + item.tokenId);
                print("reponse  uri     ::   " + item.uri);
                print("reponse  balance::   " + item.balance);
                if (item.balance > 0)
                {
                    count++;
                }
            }

        }
        catch
        {
            print("Error: " + response);
        }
    }
}
