using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Runtime.InteropServices;
using UnityEngine;

public class CreateTransection : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        //     
        //            CreateMintModel.Response nftResponse = await EVM.CreateMint(chain, network, account, to, cid);

        // if (nftResponse != null)
        //     {
        //         string chainId = await EVM.ChainId(chain, network, "");
        //         // private key of account
        //         string privateKey = "ADD_YOUR_PRIVATE_KEY";
        //         string transaction = await EVM.CreateTransaction(chain, network, nftResponse.tx.account,
        //         nftResponse.tx.to, nftResponse.tx.value, nftResponse.tx.data, nftResponse.tx.gasPrice, nftResponse.tx.gasLimit);
        //         string signature = Web3PrivateKey.SignTransaction(privateKey, transaction, chainId);
        //         string responseBroadcast = await EVM.BroadcastTransaction(chain, network, nftResponse.tx.account, nftResponse.tx.to, nftResponse.tx.value, nftResponse.tx.data, signature,
        //         nftResponse.tx.gasPrice, nftResponse.tx.gasLimit, "");
        //     }

    }


    // Update is called once per frame
    void Update()
    {

    }
}
