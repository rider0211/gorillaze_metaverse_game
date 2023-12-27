using UnityEngine;
using UnityEngine.UI;

public class HealthManager : MonoBehaviour
{
    #region Singleton

    public static HealthManager instance;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else if(instance != this)
        {
            Destroy(this.gameObject);
        }
    }

    #endregion

    [SerializeField]
    int currentHealth = 3, maxHealth = 3;

    public GameObject HealthPanel;

    private void Start()
    {
        currentHealth = maxHealth;
    }

    public void removeHealth()
    {
        currentHealth--;

        HealthPanel.transform.GetChild(currentHealth).gameObject.GetComponent<Image>().sprite = UiManager.instance.emptyHeart;

        if (currentHealth <= 0)
        {
            currentHealth = 0;
        }
    }
    public int getHealth()
    {
        return currentHealth;
    }

    public void refillHealth()
    {
        currentHealth = maxHealth;
    }
}
