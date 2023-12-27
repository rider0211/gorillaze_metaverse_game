using System;
using System.Numerics;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;
using Newtonsoft.Json;
using System.IO;
public class AvalunchCoinCalling
{

    /*
        public class Response<T> { public T response; }

        private readonly static string host = "https://api.snowtrace.io/api?";
        public static async Task<string> AvalunchCoinRadeemApi()
        {
            WWWForm form = new WWWForm();
            form.AddField("module", "account");
            form.AddField("action", "tokentx");
            // #if UNITY_EDITOR
            form.AddField("address", "0x095A07CA72B9165FC2dfca5BbF75024d464bdfd5");
            // #else
            //    form.AddField("address", PlayerPrefs.GetString("Account"));
            // #endif

            form.AddField("startblock", 0);
            form.AddField("endblock", 2500000);
            form.AddField("sort", "asc");
            form.AddField("apikey", "Y6UGH566KC6QS49NCZAFYFA82IIN5BMCNJ");
            string url = host;
            UnityWebRequest webRequest = UnityWebRequest.Post(url, form);


            await webRequest.SendWebRequest();



            Debug.Log(" webRequest  data :::  " + webRequest.error);


            ResponseData _responseapi = JsonUtility.FromJson<ResponseData>(System.Text.Encoding.UTF8.GetString(webRequest.downloadHandler.data));
            Debug.Log(" _responseapi  data :::  " + _responseapi.message);
            Debug.Log("_responseapi    data :::  " + _responseapi.status);

            Debug.Log("data :::  " + webRequest.downloadHandler.data.ToString());
            // ResponseData _responseapi = JsonUtility.FromJson<ResponseData<string>>(System.Text.Encoding.UTF8.GetString(webRequest.downloadHandler.data));
            Response<string> data = JsonUtility.FromJson<Response<string>>(System.Text.Encoding.UTF8.GetString(webRequest.downloadHandler.data));

            return data.response;
        }*/
}

