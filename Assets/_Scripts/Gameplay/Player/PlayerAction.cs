using UnityEngine;

public class PlayerAction : MonoBehaviour
{
    public ActionInputPlayer input;
    public string tipoDeInput;

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

    public Vector2 MoveValue()
    {
        switch (tipoDeInput)
        {
            case "WASD":
                return input.PlayerWASD.Move.ReadValue<Vector2>();


            case "SETAS":
                return input.PlayerSETAS.Move.ReadValue<Vector2>();
        }

        return Vector2.zero;
    }

    public bool AttackButtonDown()
    {
        switch (tipoDeInput)
        {
            case "WASD":
                return input.PlayerWASD.Tiro.WasPressedThisFrame();


            case "SETAS":
                return input.PlayerSETAS.Tiro.WasPressedThisFrame();
        }
        return false;
    }

    public bool AttackButton()
    {
        switch (tipoDeInput)
        {
            case "WASD":
                return input.PlayerWASD.Tiro.IsPressed();


            case "SETAS":
                return input.PlayerSETAS.Tiro.IsPressed();
        }
        return false;
    }

    public bool Skill01ButtonDown()
    {
        switch (tipoDeInput)
        {
            case "WASD":
                return input.PlayerWASD.Skill01.WasPressedThisFrame();


            case "SETAS":
                return input.PlayerSETAS.Skill01.WasPressedThisFrame();
        }
        return false;
    }

    public bool Skill02ButtonDown()
    {
        switch (tipoDeInput)
        {
            case "WASD":
                return input.PlayerWASD.Skill02.WasPressedThisFrame();


            case "SETAS":
                return input.PlayerSETAS.Skill02.WasPressedThisFrame();
        }
        return false;
    }

    public bool Skill03ButtonDown()
    {
        switch (tipoDeInput)
        {
            case "WASD":
                return input.PlayerWASD.Skill03.WasPressedThisFrame();


            case "SETAS":
                return input.PlayerSETAS.Skill03.WasPressedThisFrame();
        }
        return false;
    }

}
