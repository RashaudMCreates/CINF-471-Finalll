using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class Enemy : MonoBehaviour
{
    public Rigidbody rb;
    public Vector3 targetPosition = new Vector3(-4.6873f, 6.357f, -9.1297f);
    public float forceStrength = 10f;
    public Animator anim;
    private float timer = 0f;
    private float timer2 = 0f;
    public float thrust;

    public GameObject EnemyLeftFist;
    public GameObject EnemyRightFist;

    //test
    public GameObject LossMenu;
    //test

    public float Health, MaxHealth;
    [SerializeField]
    private YourHealthBarUI healthBar;

    public AudioSource knockout;

    public AudioSource ContactHit;

    private bool hasPlayedKnockout = false;

    void Start() 
    {
        healthBar.SetMaxHealth2(MaxHealth);
        LossMenu.SetActive(false);
        Time.timeScale = 1f;
    }

    void Update()
    {
        timer += Time.deltaTime;
        timer2 += Time.deltaTime;

        if (timer >= 4f)
        {
            anim.Play("PUNCH");
            timer = 0;
            rb.AddRelativeForce(Vector3.forward * thrust);
        }

        if (timer2 >= 6f)
        {
            anim.Play("PUNCH2");
            timer2 = 0;
            rb.AddRelativeForce(Vector3.forward * thrust);
        }


        if (transform.position.y < 0 && !hasPlayedKnockout)
        {
            hasPlayedKnockout = true;
            StartCoroutine(PlayKnockoutAndLoadScene());
        }
    }

    private IEnumerator PlayKnockoutAndLoadScene()
    {
    knockout.Play();
    Time.timeScale = 0.5f;

    // Wait for the knockout audio to finish
    while (knockout.isPlaying)
    {
        yield return null;
    }

    // Optionally reset time scale before loading
    Time.timeScale = 1f;
    SceneManager.LoadScene(4);
    }

    void FixedUpdate()
    {
        Vector3 direction = (targetPosition - rb.position).normalized;
        rb.AddForce(direction * forceStrength);
    }

    public void SetHealth2(float healthChange)
    {
        Health += healthChange;
        Health = Mathf.Clamp(Health, 0, MaxHealth);

        healthBar.SetHealth2(Health);
    }

    private void OnCollisionEnter(Collision collision)
    {
        Player enemy = collision.collider.GetComponent<Player>();
        Rigidbody enemyRb = collision.collider.GetComponent<Rigidbody>();

        if (collision.contacts[0].thisCollider.gameObject == EnemyLeftFist)
        {
            // Check if the object it hit does NOT have an Enemy component
            if (collision.collider.GetComponent<Player>() != null)
            {
                ContactHit.Play();
                Debug.Log("LeftFist hit a player object!");
                SetHealth2(-40f); // or whatever action you want
            }
            if (Health <= 0)
            {
                LaunchEnemy(enemyRb);
            }
        }
        else if (collision.contacts[0].thisCollider.gameObject == EnemyRightFist)
        {
            // Check if the object it hit does NOT have an Enemy component
            if (collision.collider.GetComponent<Player>() != null)
            {
                ContactHit.Play();
                Debug.Log("RightFist hit a player object HARD!");
                SetHealth2(-40f); // or whatever action you want
            }
            if (Health <= 0)
            {
                LaunchEnemy(enemyRb);
            }
        }
    }

    private void LaunchEnemy(Rigidbody enemyRb)
    {
        enemyRb.constraints = RigidbodyConstraints.None; // Unfreeze all
        enemyRb.AddForce(Vector3.up * 1000f); // Adjust force to taste
        enemyRb.AddTorque(Random.insideUnitSphere * 500f); // Optional: adds random spin

        LossMenu.SetActive(true);
        Time.timeScale = 0.3f;
    }
}
