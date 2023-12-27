using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class WalletAddress : MonoBehaviour
{
    public Text _AddressText;

    void Start()
    {
        // Debug.Log("account text address : "+PlayerPrefs.GetString("Account") );
        // _AddressText.text=PlayerPrefs.GetString("Account");
        // _AddressText.text=PlayerPrefs.GetString("Account");
    }

    public void Mainmenu()
    {
        HealthManager.instance.refillHealth();
        GameManager.instance.ResetData();
        SceneManager.LoadScene(1);
    }

}
