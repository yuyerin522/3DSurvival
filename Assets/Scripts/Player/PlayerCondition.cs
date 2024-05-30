using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamagable  //불에 닿으면 데미지
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
        hunger.Subtract(hunger.passiveValue * Time.deltaTime); //time.deltatime 기기마다 속도가 다를 수 있기 때문에 일정시간으로 조정
        stamina.Add(stamina.passiveValue * Time.deltaTime);

        if (hunger.curValue <= 0.0f) //배고프면 체력딸림
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
        Debug.Log("플레이어가 죽었다.");
    }

    public void TakePhysicalDamage(int damageAmount)
    {
        health.Subtract(damageAmount);  //헬스에서 데미지만큼 깎아준다
        onTakeDamage?.Invoke(); // 데미지 받으면 등록된 기능 호출
    }
}
