using UnityEngine;

public class Player : MonoBehaviour
{
    public GameObject Player1;

    public float Health, MaxHealth;

    [SerializeField]
    private HealthBarUI healthBar;

    void Start() 
    {
        healthBar.SetMaxHealth(MaxHealth);
    }

    void Update() 
    {
        if(Input.GetKeyDown(KeyCode.Mouse0))
        {
            DoLAttack();
            SetHealth(-35f);
        }
        else if(Input.GetKeyDown(KeyCode.Mouse1))
        {
            DoRAttack();
            SetHealth(-20f);
        }
        else if(Input.GetKeyDown(KeyCode.A))
        {
            LDodge();
        }
        else if(Input.GetKeyDown(KeyCode.D))
        {
            RDodge();
        }
    }

    public void DoLAttack()
    {
        Player1.GetComponent<Animator>().SetTrigger("LPUNCH");
    }

    public void DoRAttack()
    {
        Player1.GetComponent<Animator>().SetTrigger("RPUNCH");
    }

    public void LDodge()
    {
        Player1.GetComponent<Animator>().SetTrigger("LSTRAFE");
    }

    public void RDodge()
    {
        Player1.GetComponent<Animator>().SetTrigger("RSTRAFE");
    }

    public void SetHealth(float healthChange)
    {
        Health += healthChange;
        Health = Mathf.Clamp(Health, 0, MaxHealth);

        healthBar.SetHealth(Health);
    }
}