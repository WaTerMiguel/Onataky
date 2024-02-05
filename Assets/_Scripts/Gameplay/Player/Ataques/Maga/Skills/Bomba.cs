using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomba : MonoBehaviour
{

    [SerializeField] float sizeMax, sizeAtual, speed;
    [SerializeField] Light luz;
    [SerializeField] TiroGeral tiro;
    private Vector3 iniSize;
    [SerializeField] Rigidbody rb;

    private void Start()
    {
        iniSize = transform.localScale;

        StartCoroutine(DestroyBomba());
    }

    IEnumerator DestroyBomba()
    {
        yield return new WaitForSeconds(2f);
        Destroy(this.gameObject);
    }

    private void FixedUpdate()
    {
        if (tiro.efeito == true)
        {
            rb.velocity = Vector3.zero;
            tiro.enabled = false;
            sizeAtual = Mathf.Lerp(transform.localScale.x, sizeMax, speed);
            transform.localScale = iniSize * sizeAtual;
            luz.intensity = sizeAtual;
        }
        

    }

}
