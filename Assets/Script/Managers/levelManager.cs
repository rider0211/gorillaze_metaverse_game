using UnityEngine;
using UnityEngine.SceneManagement;

public class levelManager : MonoBehaviour
{
    #region Singleton

    public static levelManager instance;

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
    [HideInInspector]
    int coins;

   
    bool isPaused = false;
    bool radeem = true;

    private void Start()
    {
        Time.timeScale = 1f;
    }
    public void updateCoin(int amount)
    {
        coins += amount;
        UiManager.instance.coin_Text.text = coins.ToString();

        if ((coins + GameManager.instance.getTotalCoins()) > GameManager.instance._coinsRedeem && radeem && !GameManager.instance.redeemedAvalaunche)
        {
            Instantiate(UiManager.instance._radeemButton.gameObject, UiManager.instance.gamePlayPanel_GameObject.transform);
            radeem = false;
        }
    }
    public int getCoin()
    {
        return coins;
    }

    #region UI Buttons Funtionality
    public void pauseButton()
    {
        if (isPaused)
        {
            isPaused = false;
            Time.timeScale = 1f;

            UiManager.instance.panels_GameObject.SetActive(false);
        }
        else
        {
            isPaused = true;
            Time.timeScale = 0f;

            UiManager.instance.pausePanel();
        }
    }
    public void retryLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void restartFrom_Level1()
    {
        HealthManager.instance.refillHealth();
        GameManager.instance.ResetData();
        SceneManager.LoadScene(2);

    }
    public void nextLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void option()
    {
        UiManager.instance.optionPanel();
    }
    #endregion
     public void GotoMainmenu()
    {
        HealthManager.instance.refillHealth();
        GameManager.instance.ResetData();
        SceneManager.LoadScene(1);
    }

}
