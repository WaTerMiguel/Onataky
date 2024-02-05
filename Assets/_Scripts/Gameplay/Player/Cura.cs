using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cura : MonoBehaviour
{
    public float totalCura;
    public PlayerLife targetCura;
    [SerializeField] GameObject textValue;

    private void FixedUpdate()
    {
        if (targetCura != null)
        {
            targetCura.Cura(totalCura);
            var text = Instantiate(textValue, targetCura.gameObject.transform.position, Quaternion.identity);
            if (text.transform.GetChild(0).gameObject.TryGetComponent(out TextMesh textMesh))
            {
                textMesh.text = totalCura.ToString();
            }
            enabled = false;
        }
    }
}
