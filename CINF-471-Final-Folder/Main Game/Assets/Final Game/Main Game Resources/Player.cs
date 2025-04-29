using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour
{
    public GameObject Player1;
    public GameObject LeftFist;
    public GameObject RightFist;

    public Rigidbody rb;
    public Vector3 targetPosition = new Vector3(-2.04f, 6.02f, -9.12f);
    public float forceStrength = 10f;

    public float Health, MaxHealth;

    [SerializeField]
    private HealthBarUI healthBar;

    private bool isBusy = false;

    //test//
    public GameObject EnemyFist;
    public GameObject EnemyFist2;
    //test//

    private bool Uppering = false;


    void Start() 
    {
        healthBar.SetMaxHealth(MaxHealth);
        Time.timeScale = 1f;

        //test//
        //test//
    }

    void FixedUpdate()
    {
        Vector3 direction = (targetPosition - rb.position).normalized;
        rb.AddForce(direction * forceStrength);
    }

    void Update() 
    {
        Debug.Log("Uppering is: " + Uppering);

        if(Input.GetKeyDown(KeyCode.Mouse0))
        {
            DoLAttack();
        }
        else if(Input.GetKeyDown(KeyCode.Mouse1))
        {
            DoRAttack();
        }
        else if(Input.GetKeyDown(KeyCode.A))
        {
            LDodge();
        }
        else if(Input.GetKeyDown(KeyCode.D))
        {
            RDodge();
        }
        else if(Input.GetKeyDown(KeyCode.Mouse4))
        {
            DoLHook();
        }
        else if(Input.GetKeyDown(KeyCode.Mouse3))
        {
            DoRHook();
        }
        else if(Input.GetKeyDown(KeyCode.Mouse2))
        {
            DoUpper();
        }

        if (!isBusy)
        {
            Uppering = false;
        }
    }

    private IEnumerator PlayWithCooldown(string triggerName, float delay)
    {
        isBusy = true;
        Player1.GetComponent<Animator>().SetTrigger(triggerName);
        yield return new WaitForSeconds(delay);
        isBusy = false;
    }

    public void DoLAttack()
    {
        if (!isBusy)
            StartCoroutine(PlayWithCooldown("LPUNCH", .25f));
    }

    public void DoRAttack()
    {
        if (!isBusy)
            StartCoroutine(PlayWithCooldown("RPUNCH", .25f));
    }

    public void LDodge()
    {
        if (!isBusy)
            StartCoroutine(PlayWithCooldown("LSTRAFE", .75f));
    }

    public void RDodge()
    {
        if (!isBusy)
            StartCoroutine(PlayWithCooldown("RSTRAFE", .75f));
    }

    public void DoLHook()
    {
        if (!isBusy)
            StartCoroutine(PlayWithCooldown("LHOOK", .5f));
    }

    public void DoRHook()
    {
        if (!isBusy)
            StartCoroutine(PlayWithCooldown("RHOOK", .5f));
    }

    public void DoUpper()
    {
        if (!isBusy)
            StartCoroutine(PlayWithCooldown("UPPER", 4f));
            Uppering = true;
    }

    public void SetHealth(float healthChange)
    {
        Health += healthChange;
        Health = Mathf.Clamp(Health, 0, MaxHealth);

        healthBar.SetHealth(Health);
    }


    private void OnCollisionEnter(Collision collision)
    {
        Enemy enemy = collision.collider.GetComponent<Enemy>();
        Rigidbody enemyRb = collision.collider.GetComponent<Rigidbody>();

        if (collision.contacts[0].thisCollider.gameObject == LeftFist)
        {
            // Check if the object it hit does NOT have an Enemy component
            if (collision.collider.GetComponent<Enemy>() != null)
            {
                Debug.Log("LeftFist hit a enemy object!");
                SetHealth(-20f); // or whatever action you want
                Vector3 pushDirection = (enemy.transform.position - transform.position).normalized;
                enemyRb.AddForce(pushDirection * 150f);
            }

            if (Health <= 0)
            {
                LaunchEnemy(enemyRb);
            }
        }
        else if (collision.contacts[0].thisCollider.gameObject == RightFist)
        {
            // Check if the object it hit does NOT have an Enemy component
            if (collision.collider.GetComponent<Enemy>() != null)
            {
                Debug.Log("RightFist hit a enemy object HARD!");
                SetHealth(-40f); // or whatever action you want
                Vector3 pushDirection = (enemy.transform.position - transform.position).normalized;
                enemyRb.AddForce(pushDirection * 200f);

                if (Uppering)
                {
                    SetHealth(-30f);
                    enemyRb.AddForce(pushDirection * 300f);
                }
            }

            if (Health <= 0)
            {
                LaunchEnemy(enemyRb);
                EnemyFist.SetActive(false);
                EnemyFist2.SetActive(false);
            }
        }
    }

    public void LaunchEnemy(Rigidbody enemyRb)
    {
        enemyRb.constraints = RigidbodyConstraints.None; // Unfreeze all
        enemyRb.AddForce(Vector3.up * 1500f); // Adjust force to taste
        enemyRb.AddTorque(Random.insideUnitSphere * 500f); // Optional: adds random spin
    }
}