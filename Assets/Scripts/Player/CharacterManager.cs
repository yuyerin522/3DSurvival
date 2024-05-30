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
            if (_instance == null) // instance가 널 일경우
            {
                _instance = new GameObject("CharacerManager").AddComponent<CharacterManager>();  //새로 만들어줘야함 (방어코드)
            }
            return _instance; // 널이더라도 위 코드로 만들어준 후 반환하기 때문에 괜찮음
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
        if (_instance == null)  // 인스턴스가 비어있을때
        {
            _instance = this;   //이미 게임오브젝트로 스크립트에 붙은 상태로 실행되기 때문에 게임오브젝트를 만들어 줄 필요 없이 this 만 해줘서 자신을 집어넣으면 됨
            DontDestroyOnLoad(gameObject); //씬을 이동하더라도 파괴되지 않게, 정보가 유지되게 함
        }
        else
        {
            if (_instance != this) //인스턴스가 널이 아닐때(인스턴스 안에 있는 것과 지금 넣으려는 내 자신이 다르다면)
            {
                Destroy(gameObject); // 현재것을 파괴해줘라!
            }
        }
    }
}