using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterManager : MonoBehaviour
{
    private static CharacterManager _instance;
    public static CharacterManager Instance
    {
        get
        {
            if (_instance == null) // instance�� �� �ϰ��
            {
                _instance = new GameObject("CharacerManager").AddComponent<CharacterManager>();  //���� ���������� (����ڵ�)
            }
            return _instance; // ���̴��� �� �ڵ�� ������� �� ��ȯ�ϱ� ������ ������
        }
    }

    private Player _player;
    public Player Player
    {
        get { return _player; }
        set { _player = value; }
    }

    private void Awake()
    {
        if (_instance == null)  // �ν��Ͻ��� ���������
        {
            _instance = this;   //�̹� ���ӿ�����Ʈ�� ��ũ��Ʈ�� ���� ���·� ����Ǳ� ������ ���ӿ�����Ʈ�� ����� �� �ʿ� ���� this �� ���༭ �ڽ��� ��������� ��
            DontDestroyOnLoad(gameObject); //���� �̵��ϴ��� �ı����� �ʰ�, ������ �����ǰ� ��
        }
        else
        {
            if (_instance != this) //�ν��Ͻ��� ���� �ƴҶ�(�ν��Ͻ� �ȿ� �ִ� �Ͱ� ���� �������� �� �ڽ��� �ٸ��ٸ�)
            {
                Destroy(gameObject); // ������� �ı������!
            }
        }
    }
}