using UnityEngine;

public class AttackMaga : MonoBehaviour
{
    public delegate void AcaoAtaqueMaga(bool tendoAtaque);
    public delegate void AtaqueMaga();
    public static event AtaqueMaga OnMagaAttacked;
    public static event AcaoAtaqueMaga OnMagaMoved;
    public static event AcaoAtaqueMaga OnMagaCanAttack;

    void ComecoAnimacaoMaga()
    {
        if (OnMagaAttacked != null && OnMagaCanAttack != null && OnMagaMoved != null)
        {
            OnMagaCanAttack(false);
            OnMagaMoved(false);
        }
    }

    void OnAttackMaga()
    {
        if (OnMagaAttacked != null)
        {
            OnMagaAttacked();
        }
    }

    void OnCanAttackMaga(int i)
    {
        if (OnMagaCanAttack != null)
        {
            if (i == 1) 
            {
                OnMagaCanAttack(true);
            }
            else
            {
                OnMagaCanAttack(false);
            }
        }
    }

    void OnMoveMaga(int i)
    {
        if (OnMagaMoved != null)
        {
            if (i == 1)
            {
                OnMagaMoved(true);
            }
            else
            {
                OnMagaMoved(false);
            }
        }
    }
}
