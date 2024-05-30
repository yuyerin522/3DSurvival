using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [Header("Movement")]  //����� �����༭ Ÿ��Ʋ�� ������� (����)
    public float moveSpeed;  //���ǵ�
    private Vector2 curMovementInput;  //��ǲ�׼ǿ��� �޾ƿ� ���� �־��� ��
    public float jumpPower;
    public LayerMask groundLayerMask; //�÷��̾�� ���̾��ũ���� ���ش�

    [Header("Look")]    //���콺 ���� �̵� (�ٵ� ������ ĳ���� ���� ī�޶�)
    public Transform cameraContainer;
    public float minXLook;
    public float maxXLook;
    private float camCurXRot; //��ǲ�׼ǿ��� ���콺�� �޾ƿ� ��Ÿ���� ����� ����ؼ� �־���
    public float lookSensitivity; //ȸ�� �ΰ���

    private Vector2 mouseDelta;

    [HideInInspector]
    public bool canLook = true;

    private Rigidbody _rigidbody;  //������ٵ� �޾ƿ�

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();  //������ٵ� �޾ƿ�
    }

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;  //���콺 ��� ����� (Ŀ�� �����)
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

    public void OnMoveInput(InputAction.CallbackContext context)  // context ������� �޾ƿ���
    {
        if (context.phase == InputActionPhase.Performed) // Phase �б��� - Started (Ű�� ��������)�� �ѹ��� �۵��ؼ� Performed�� �ٲ���
        {
            curMovementInput = context.ReadValue<Vector2>();
        }
        else if (context.phase == InputActionPhase.Canceled)
        {
            curMovementInput = Vector2.zero;
        }
    }

    public void OnLookInput(InputAction.CallbackContext context)  //���콺 ���� ����~ 
    {
        mouseDelta = context.ReadValue<Vector2>();
    }

    public void OnJumpInput(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed && IsGrounded())
        {
            _rigidbody.AddForce(Vector2.up * jumpPower, ForceMode.Impulse); // ���������� ���� Ȯ �༭ ����
        }
    }

    private void Move()  //ĳ���� ���� �̵�
    {
        Vector3 dir = transform.forward * curMovementInput.y + transform.right * curMovementInput.x;  //w a s d ������ �� �� �� ��
        dir *= moveSpeed;
        dir.y = _rigidbody.velocity.y;  // y�� �ʱ�ȭ

        _rigidbody.velocity = dir;  //���Ⱚ
    }

    void CameraLook()   //ĳ���͸� ������ ���� �ϴ� ī�޶� �������� 
    {
        camCurXRot += mouseDelta.y * lookSensitivity;   //y�� * �ΰ���  (ĳ���Ͱ� �¿�� �����̷��� y�� �������� �����̱� ������ X���� y���� �־������)
        camCurXRot = Mathf.Clamp(camCurXRot, minXLook, maxXLook); // �ּҰ����� �۾����� �ּҰ� ��ȯ, �ִ񰪺��� �������� �ִ� ��ȯ
        cameraContainer.localEulerAngles = new Vector3(-camCurXRot, 0, 0); // ������ǥ�� ������ (-cam~�� ���� �����δ� �������� x���� +�� �Ǿ��ؼ� - ��ȣ�� �ٲ��ذ���)

        transform.eulerAngles += new Vector3(0, mouseDelta.x * lookSensitivity, 0); // ���Ʒ� �����°� ĳ���� ������ ����
    }

    bool IsGrounded() // �̰� ���� �׶��忡 �پ����� �ƴ��� üũ
    {
        Ray[] rays = new Ray[4] //å��ٸ� 4�� �����ϸ� ��
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
