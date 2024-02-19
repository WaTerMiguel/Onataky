using UnityEngine;

public class CameraSeguindo : MonoBehaviour
{
    public Transform target;
    [SerializeField] private Vector3 reposicao;
    public float veloc;

    private void FixedUpdate()
    {
        transform.position = Vector3.Lerp(target.position + reposicao, transform.position, veloc);
    }
}
