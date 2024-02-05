using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextValue : MonoBehaviour
{
    public float tempoDestroy = 0.3f;

    private void Start()
    {
        Destroy(transform.parent.gameObject, tempoDestroy);
        transform.position += new Vector3(Random.Range(-1f, 1f), Random.Range(0f, 2f), Random.Range(-1f, 1f));
    }
}
