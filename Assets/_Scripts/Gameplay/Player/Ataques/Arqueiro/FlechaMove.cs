using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlechaMove : MonoBehaviour
{
    [SerializeField] public Transform alvoFlecha;
    [SerializeField] private Rigidbody rb;
    [SerializeField] public float speed;

    private void Start() 
    {
        rb = GetComponent<Rigidbody>();
        StartCoroutine(DestroyFlecha());
    }

    private void FixedUpdate() 
    {
        rb.velocity = transform.forward * speed;
    }

    IEnumerator DestroyFlecha()
    {
        yield return new WaitForSeconds(3.5f);
        Destroy(this.gameObject);
    }
}
