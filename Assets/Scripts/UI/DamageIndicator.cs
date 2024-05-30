using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DamageIndicator : MonoBehaviour
{
    public Image image;
    public float flashSpeed;

    private Coroutine coroutine; //�ڷ�ƾ�� ���� ���� �ڷ�ƾ ������ ����

    private void Start()
    {
        CharacterManager.Instance.Player.condition.onTakeDamage += Flash; //�÷��̾� ����ǿ� �ִ� ontakedamage�� �÷��ø� �������
    }

    public void Flash() //��� ��Ÿ���� ������°�
    {
        if (coroutine != null) //�̹� �ڷ�ƾ�� �������϶�
        {
            StopCoroutine(coroutine); //������ �ִ� �ڷ�ƾ ��ž~
        }

        image.enabled = true;
        image.color = new Color(1f, 105f / 255f, 105f / 255f);
        coroutine = StartCoroutine(FadeAway());
    }

    private IEnumerator FadeAway()  //�ڷ�ƾ ���
    {
        float startAlpha = 0.3f;
        float a = startAlpha;

        while (a > 0.0f) // �ѹ� ������ �� ��������, ������ ��������
        {
            a -= (startAlpha / flashSpeed) * Time.deltaTime;
            image.color = new Color(1f, 105f / 255f, 105f / 255f, a);
            yield return null;
        }

        image.enabled = false;  //������ �̹��� ���ֱ�
    }
}
