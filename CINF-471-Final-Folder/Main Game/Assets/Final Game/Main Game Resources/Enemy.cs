using UnityEngine;

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


     public float Health, MaxHealth;
    [SerializeField]
    private YourHealthBarUI healthBar;

    void Start() 
    {
        healthBar.SetMaxHealth2(MaxHealth);
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
        if (collision.contacts[0].thisCollider.gameObject == EnemyLeftFist)
        {
            // Check if the object it hit does NOT have an Enemy component
            if (collision.collider.GetComponent<Player>() != null)
            {
                Debug.Log("LeftFist hit a enemy object!");
                SetHealth2(-10f); // or whatever action you want
            }
        }
        else if (collision.contacts[0].thisCollider.gameObject == EnemyRightFist)
        {
            // Check if the object it hit does NOT have an Enemy component
            if (collision.collider.GetComponent<Player>() != null)
            {
                Debug.Log("RightFist hit a enemy object HARD!");
                SetHealth2(-20f); // or whatever action you want
            }
        }
    }
}
