using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Condition : MonoBehaviour
{
    public float curValue; //���� �� ���°�
    public float maxValue; // �ִ밪
    public float startValue; //���۰�
    public float passiveValue; //��ȭ��
    public Image uiBar; //UI�� �ҷ������� Using���� �߰��ؼ� UI�� �����������~

    private void Start()
    {
        curValue = startValue;  //���簪�� ���۰�
    }

    private void Update()
    {
        //ui ������Ʈ

        uiBar.fillAmount = GetPercentage(); //�������� �ۼ�Ʈ ���� fillamount �޶��� (�� ����)
    }

    public void Add(float value) 
    {
        curValue = Mathf.Min(curValue + value, maxValue); //���� 90�̰� ,20�� ������ �� �����ָ� maxValue���� ũ�� ������ maxValue�� ��������?
    }

    public void Subtract(float value)
    {
        curValue = Mathf.Max(curValue - value, 0.0f); //���� �ּҺ��� ������ �׳� 0
    }

    public float GetPercentage()
    {
        return curValue / maxValue;
    }
}
