using UnityEngine;

public class GameManager : MonoBehaviour
{
    #region Singleton

    public static GameManager instance;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(this.gameObject);
        }
    }
    #endregion

    int totalCoins = 0;
    public int _coinsRedeem;

    public bool redeemedAvalaunche=false;
    public void updateTotalCoins(int amount)
    {
        totalCoins += amount;
        Debug.Log("total coin"+ totalCoins);
    }
    public int getTotalCoins()
    {
        return totalCoins;
    }

    public void ResetData()
    {
        GameManager.instance.redeemedAvalaunche = false;
        totalCoins = 0;
    }

}
