using UnityEngine;
using System.Collections;

public class Crowdset1 : MonoBehaviour
{
    public float minInterval = 2f;
    public float maxInterval = 5f;
    public float upwardForce = 10f;

    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        if (rb == null)
        {
            Debug.LogError("No Rigidbody found on " + gameObject.name);
            enabled = false;
            return;
        }

        StartCoroutine(ApplyRandomForce());
    }

    IEnumerator ApplyRandomForce()
    {
        while (true)
        {
            float waitTime = Random.Range(minInterval, maxInterval);
            yield return new WaitForSeconds(waitTime);

            rb.AddForce(Vector3.up * upwardForce, ForceMode.Impulse);
        }
    }
}
