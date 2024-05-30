using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DamageIndicator : MonoBehaviour
{
    public Image image;
    public float flashSpeed;

    private Coroutine coroutine; //코루틴을 쓰기 위해 코루틴 변수를 만듬

    private void Start()
    {
        CharacterManager.Instance.Player.condition.onTakeDamage += Flash; //플레이어 컨디션에 있는 ontakedamage에 플래시를 등록해줌
    }

    public void Flash() //배경 나타났다 사라지는거
    {
        if (coroutine != null) //이미 코루틴이 실행중일때
        {
            StopCoroutine(coroutine); //기존에 있던 코루틴 스탑~
        }

        image.enabled = true;
        image.color = new Color(1f, 105f / 255f, 105f / 255f);
        coroutine = StartCoroutine(FadeAway());
    }

    private IEnumerator FadeAway()  //코루틴 사용
    {
        float startAlpha = 0.3f;
        float a = startAlpha;

        while (a > 0.0f) // 한번 켜졌을 때 빨개지고, 서서히 옅어지게
        {
            a -= (startAlpha / flashSpeed) * Time.deltaTime;
            image.color = new Color(1f, 105f / 255f, 105f / 255f, a);
            yield return null;
        }

        image.enabled = false;  //끝나면 이미지 꺼주기
    }
}
