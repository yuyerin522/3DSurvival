using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayNightCycle : MonoBehaviour
{
    [Range(0.0f, 1.0f)]
    public float time;  // 시간 0프로~100프로가 되는것이다
    public float fullDayLength;
    public float startTime = 0.4f; //time이 0.5일때 정오
    private float timeRate;
    public Vector3 noon; // 정오 (90,0,0)

    [Header("Sun")]
    public Light sun;
    public Gradient sunColor; //그라디언트로 서서히 바뀜
    public AnimationCurve sunIntensity;

    [Header("Moon")]
    public Light moon;
    public Gradient moonColor;
    public AnimationCurve moonIntensity;

    [Header("Other Lighting")]
    public AnimationCurve lightingIntensityMultiplier;   //빛 조절
    public AnimationCurve reflectionIntensityMultiplier; //반사

    private void Start()
    {
        timeRate = 1.0f / fullDayLength;  
        time = startTime;
    }

    private void Update()
    {
        time = (time + timeRate * Time.deltaTime) % 1.0f;

        UpdateLighting(sun, sunColor, sunIntensity);      //Sun호출
        UpdateLighting(moon, moonColor, moonIntensity);   //Moon호출

        RenderSettings.ambientIntensity = lightingIntensityMultiplier.Evaluate(time);  // 세팅값 접근하려면 RenderSettings. 해줘야함
        RenderSettings.reflectionIntensity = reflectionIntensityMultiplier.Evaluate(time);

    }

    void UpdateLighting(Light lightSource, Gradient colorGradiant, AnimationCurve intensityCurve)
    {
        float intensity = intensityCurve.Evaluate(time);

        lightSource.transform.eulerAngles = (time - (lightSource == sun ? 0.25f : 0.75f)) * noon * 4.0f;  //Sun time 0.5 정오일땐 90도가 되야하는데 360*0.5는 180도가 나와서 0.25를 빼줌
        lightSource.color = colorGradiant.Evaluate(time);                                                 //Noon 일때 90도를 곱해주는데 90에 0.25곱해도 90이 아니니 4 를 또 곱해줌
        lightSource.intensity = intensity;

        GameObject go = lightSource.gameObject; //해가 넘어간경우 꺼줘야함
        if (lightSource.intensity == 0 && go.activeInHierarchy)  //밝기가 0이 됐는데, 하이어라키에선 켜져있다면
            go.SetActive(false);                                 //꺼주세용
        else if (lightSource.intensity > 0 && !go.activeInHierarchy)  //밝기가 0보다 큰데, 하이어라키에서 안켜져있으면
            go.SetActive(true);                                       //켜주세용
    }
}
