using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [Header("Movement")]  //헤더를 적어줘서 타이틀을 만들어줌 (구분)
    public float moveSpeed;  //스피드
    private Vector2 curMovementInput;  //인풋액션에서 받아온 값을 넣어줄 곳
    public float jumpPower;
    public LayerMask groundLayerMask; //플레이어는 레이어마스크에서 빼준다

    [Header("Look")]    //마우스 따라 이동 (근데 지금은 캐릭터 말고 카메라가)
    public Transform cameraContainer;
    public float minXLook;
    public float maxXLook;
    private float camCurXRot; //인풋액션에서 마우스에 받아온 델타값을 저기다 계산해서 넣어줌
    public float lookSensitivity; //회전 민감도

    private Vector2 mouseDelta;

    [HideInInspector]
    public bool canLook = true;

    private Rigidbody _rigidbody;  //리지드바디 받아옴

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();  //리지드바디 받아옴
    }

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;  //마우스 모양 숨기기 (커서 숨기기)
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void LateUpdate()
    {
        if (canLook)
        {
            CameraLook();
        }
    }

    public void OnMoveInput(InputAction.CallbackContext context)  // context 현재상태 받아오기
    {
        if (context.phase == InputActionPhase.Performed) // Phase 분기점 - Started (키가 눌렸을때)는 한번만 작동해서 Performed로 바꿔줌
        {
            curMovementInput = context.ReadValue<Vector2>();
        }
        else if (context.phase == InputActionPhase.Canceled)
        {
            curMovementInput = Vector2.zero;
        }
    }

    public void OnLookInput(InputAction.CallbackContext context)  //마우스 따라 보기~ 
    {
        mouseDelta = context.ReadValue<Vector2>();
    }

    public void OnJumpInput(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed && IsGrounded())
        {
            _rigidbody.AddForce(Vector2.up * jumpPower, ForceMode.Impulse); // 순간적으로 힘을 확 줘서 점프
        }
    }

    private void Move()  //캐릭터 실제 이동
    {
        Vector3 dir = transform.forward * curMovementInput.y + transform.right * curMovementInput.x;  //w a s d 움직임 상 하 좌 우
        dir *= moveSpeed;
        dir.y = _rigidbody.velocity.y;  // y값 초기화

        _rigidbody.velocity = dir;  //방향값
    }

    void CameraLook()   //캐릭터를 돌리지 말고 일단 카메라만 돌려보자 
    {
        camCurXRot += mouseDelta.y * lookSensitivity;   //y값 * 민감도  (캐릭터가 좌우로 움직이려면 y축 기준으로 움직이기 때문에 X값에 y값을 넣어줘야함)
        camCurXRot = Mathf.Clamp(camCurXRot, minXLook, maxXLook); // 최소값보다 작아지면 최소값 반환, 최댓값보다 많아지면 최댓값 반환
        cameraContainer.localEulerAngles = new Vector3(-camCurXRot, 0, 0); // 로컬좌표로 돌려줌 (-cam~인 이유 실제로는 내렸을때 x값이 +가 되야해서 - 부호로 바꿔준고임)

        transform.eulerAngles += new Vector3(0, mouseDelta.x * lookSensitivity, 0); // 위아래 돌리는건 캐릭터 각도로 조정
    }

    bool IsGrounded() // 이게 지금 그라운드에 붙었는지 아닌지 체크
    {
        Ray[] rays = new Ray[4] //책상다리 4개 생각하면 됨
        {
            new Ray(transform.position + (transform.forward * 0.2f) + (transform.up * 0.01f), Vector3.down),
            new Ray(transform.position + (-transform.forward * 0.2f) + (transform.up * 0.01f), Vector3.down),
            new Ray(transform.position + (transform.right * 0.2f) + (transform.up * 0.01f), Vector3.down),
            new Ray(transform.position + (-transform.right * 0.2f) +(transform.up * 0.01f), Vector3.down)
        };

        for (int i = 0; i < rays.Length; i++)
        {
            if (Physics.Raycast(rays[i], 0.1f, groundLayerMask))
            {
                return true;
            }
        }

        return false;
    }

    public void ToggleCursor(bool toggle)
    {
        Cursor.lockState = toggle ? CursorLockMode.None : CursorLockMode.Locked;
        canLook = !toggle;
    }
}
