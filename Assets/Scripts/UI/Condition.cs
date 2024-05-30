using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Condition : MonoBehaviour
{
    public float curValue; //현재 바 상태값
    public float maxValue; // 최대값
    public float startValue; //시작값
    public float passiveValue; //변화값
    public Image uiBar; //UI를 불러오려면 Using문을 추가해서 UI를 가져와줘야함~

    private void Start()
    {
        curValue = startValue;  //현재값은 시작값
    }

    private void Update()
    {
        //ui 업데이트

        uiBar.fillAmount = GetPercentage(); //가져오는 퍼센트 따라 fillamount 달라짐 (바 길이)
    }

    public void Add(float value) 
    {
        curValue = Mathf.Min(curValue + value, maxValue); //현재 90이고 ,20이 들어왔을 때 더해주면 maxValue보다 크기 때문에 maxValue가 나오게함?
    }

    public void Subtract(float value)
    {
        curValue = Mathf.Max(curValue - value, 0.0f); //빼서 최소보다 작으면 그냥 0
    }

    public float GetPercentage()
    {
        return curValue / maxValue;
    }
}
