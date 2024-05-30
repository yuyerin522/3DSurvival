using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayNightCycle : MonoBehaviour
{
    [Range(0.0f, 1.0f)]
    public float time;  // �ð� 0����~100���ΰ� �Ǵ°��̴�
    public float fullDayLength;
    public float startTime = 0.4f; //time�� 0.5�϶� ����
    private float timeRate;
    public Vector3 noon; // ���� (90,0,0)

    [Header("Sun")]
    public Light sun;
    public Gradient sunColor; //�׶���Ʈ�� ������ �ٲ�
    public AnimationCurve sunIntensity;

    [Header("Moon")]
    public Light moon;
    public Gradient moonColor;
    public AnimationCurve moonIntensity;

    [Header("Other Lighting")]
    public AnimationCurve lightingIntensityMultiplier;   //�� ����
    public AnimationCurve reflectionIntensityMultiplier; //�ݻ�

    private void Start()
    {
        timeRate = 1.0f / fullDayLength;  
        time = startTime;
    }

    private void Update()
    {
        time = (time + timeRate * Time.deltaTime) % 1.0f;

        UpdateLighting(sun, sunColor, sunIntensity);      //Sunȣ��
        UpdateLighting(moon, moonColor, moonIntensity);   //Moonȣ��

        RenderSettings.ambientIntensity = lightingIntensityMultiplier.Evaluate(time);  // ���ð� �����Ϸ��� RenderSettings. �������
        RenderSettings.reflectionIntensity = reflectionIntensityMultiplier.Evaluate(time);

    }

    void UpdateLighting(Light lightSource, Gradient colorGradiant, AnimationCurve intensityCurve)
    {
        float intensity = intensityCurve.Evaluate(time);

        lightSource.transform.eulerAngles = (time - (lightSource == sun ? 0.25f : 0.75f)) * noon * 4.0f;  //Sun time 0.5 �����϶� 90���� �Ǿ��ϴµ� 360*0.5�� 180���� ���ͼ� 0.25�� ����
        lightSource.color = colorGradiant.Evaluate(time);                                                 //Noon �϶� 90���� �����ִµ� 90�� 0.25���ص� 90�� �ƴϴ� 4 �� �� ������
        lightSource.intensity = intensity;

        GameObject go = lightSource.gameObject; //�ذ� �Ѿ��� �������
        if (lightSource.intensity == 0 && go.activeInHierarchy)  //��Ⱑ 0�� �ƴµ�, ���̾��Ű���� �����ִٸ�
            go.SetActive(false);                                 //���ּ���
        else if (lightSource.intensity > 0 && !go.activeInHierarchy)  //��Ⱑ 0���� ū��, ���̾��Ű���� ������������
            go.SetActive(true);                                       //���ּ���
    }
}
