using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamagable  //�ҿ� ������ ������
{
    void TakePhysicalDamage(int damageAmount);
}

public class PlayerCondition : MonoBehaviour, IDamagable
{
    public UICondition uiCondition;

    Condition health { get { return uiCondition.health; } }
    Condition hunger { get { return uiCondition.hunger; } }
    Condition stamina { get { return uiCondition.stamina; } }

    public float noHungerHealthDecay; 
    public event Action onTakeDamage;

    private void Update()
    {
        hunger.Subtract(hunger.passiveValue * Time.deltaTime); //time.deltatime ��⸶�� �ӵ��� �ٸ� �� �ֱ� ������ �����ð����� ����
        stamina.Add(stamina.passiveValue * Time.deltaTime);

        if (hunger.curValue <= 0.0f) //������� ü�µ���
        {
            health.Subtract(noHungerHealthDecay * Time.deltaTime);
        }

        if (health.curValue <= 0.0f)
        {
            Die();
        }
    }

    public void Heal(float amount)
    {
        health.Add(amount);
    }

    public void Eat(float amount)
    {
        hunger.Add(amount);
    }

    public void Die()
    {
        Debug.Log("�÷��̾ �׾���.");
    }

    public void TakePhysicalDamage(int damageAmount)
    {
        health.Subtract(damageAmount);  //�ｺ���� ��������ŭ ����ش�
        onTakeDamage?.Invoke(); // ������ ������ ��ϵ� ��� ȣ��
    }
}
