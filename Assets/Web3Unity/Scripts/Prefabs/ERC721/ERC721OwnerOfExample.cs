using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ERC721OwnerOfExample : MonoBehaviour
{
    async void Start()
    {
         string chain = "ethereum";
        string network = "mainnet";
        string contract = "0xbc61AC336D5558ccb9C304f62a7433e866d1c7e9";
        string tokenId = "1";

        string ownerOf = await ERC721.OwnerOf(chain, network, contract, tokenId);
      //  print("print name "+ownerOf);
    }
}
