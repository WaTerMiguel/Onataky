using UnityEngine;

public class PlayerAction : MonoBehaviour
{
    public ActionInputPlayer input;

    private void Awake()
    {
        input = new ActionInputPlayer();
    }

    private void OnEnable()
    {
        input.Enable();
    }

    private void OnDisable()
    {
        input.Disable();
    }
}
