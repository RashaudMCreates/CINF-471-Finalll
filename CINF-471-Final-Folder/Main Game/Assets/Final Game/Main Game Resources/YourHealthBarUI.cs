using UnityEngine;
using UnityEngine.UI;

public class YourHealthBarUI : MonoBehaviour
{
    public float Health, MaxHealth, Width, Height;

    [SerializeField]
    private RectTransform healthBar;

    public void SetMaxHealth2(float maxHealth) 
    {
        MaxHealth = maxHealth;
    }

    public void SetHealth2(float health) 
    {
        Health = health;
        float newWidth = (Health/MaxHealth) * Width;

        healthBar.sizeDelta = new Vector2(newWidth, Height);
    }
}
