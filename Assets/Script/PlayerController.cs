using UnityEngine;
using System.Collections.Generic;
using static UnityEditor.Searcher.SearcherWindow.Alignment;
using SceneState;

public class PlayerController : CharacterStatus
{
    [SerializeField] Camera _camera; 
    Rigidbody _rb;
    [SerializeField] bool _isGround;
    [SerializeField, Range(0.0f, 5.0f)] private float _accelerationRatio = 1.0f;
    [Range(0.0f, 20.0f)] public float _speedMax = 5.0f;
    [Range(0.0f, 20.0f)] public float _turnSpeed = 5.0f;
    Vector3 _velocity = Vector3.zero;

    [SerializeField, Range(0.0f, 20.0f)] private float _jumpRatio = 5.0f;
    [SerializeField] List<GameObject> _jumpList = new List<GameObject>();
    [SerializeField] List<GameObject> _onGroundList = new List<GameObject>();

    private void Start()
    {
        if (!_camera) _camera = Camera.main;
        _rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        // �J�����̐��ʕ����ƉE�������擾�iY���͖����j
        Vector3 cameraForward = _camera.transform.forward;
        cameraForward.y = 0;
        cameraForward.Normalize();

        Vector3 cameraRight = _camera.transform.right;
        cameraRight.y = 0;
        cameraRight.Normalize();

        // �J������̈ړ��������쐬
        Vector3 direction = cameraForward * vertical + cameraRight * horizontal;
        direction.Normalize(); // �΂߈ړ��ł����x�����ɂȂ�悤�ɐ��K��

        _velocity = direction * _accelerationRatio;

        if (Input.GetButtonDown("Jump") && _isGround)
        {
            JumpForce(_jumpRatio);
            _isGround = false;
        }

        if (Input.GetKeyDown(KeyCode.U)) ApplyHp(-10);
    }
    private void FixedUpdate()
    {
        if (_velocity.magnitude > 0.0f)
        {
            _rb.AddForce(_velocity, ForceMode.VelocityChange);
            _velocity = Vector3.zero;
        }

        // ���x�̕ۑ�
        Vector3 velocity = new Vector3(_rb.linearVelocity.x, 0.0f, _rb.linearVelocity.z);

        // �ő呬�x����
        if (velocity.magnitude > _speedMax)
        {
            velocity = velocity.normalized * _speedMax;

            _rb.linearVelocity = velocity + new Vector3(0.0f, _rb.linearVelocity.y, 0.0f);
        }

        velocity = _rb.linearVelocity;

        // 停止中は回転しない
        if (velocity.sqrMagnitude < 0.01f)
            return;

        // Y軸方向に限定した回転用方向（XZ平面に投影）
        Vector3 flatDirection = new Vector3(velocity.x, 0, velocity.z);
        if (flatDirection.sqrMagnitude < 0.001f)
            return;

        // 目標のY軸回転
        Quaternion targetRotation = Quaternion.LookRotation(flatDirection, Vector3.up);

        // 現在の回転
        Quaternion currentRotation = _rb.rotation;

        // Y軸だけ補間するようにEuler角を取り出して補間
        float targetY = targetRotation.eulerAngles.y;
        float currentY = currentRotation.eulerAngles.y;
        float smoothY = Mathf.LerpAngle(currentY, targetY, _turnSpeed * Time.fixedDeltaTime);

        // 新しい回転を設定（Y軸のみ変化）
        Quaternion smoothRotation = Quaternion.Euler(0, smoothY, 0);

        // 回転を適用
        _rb.MoveRotation(smoothRotation);
    }

    public void JumpForce(float force)
    {
        Vector3 jumpForce = new Vector3(0.0f, force, 0.0f);
        _rb.AddForce(jumpForce, ForceMode.Impulse);

        foreach (GameObject go in _jumpList)
        {
            Vector3 position = transform.position + go.transform.position;
            GameObject jumpObject = Instantiate(go, position, Quaternion.identity);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            _isGround = true;

            foreach (GameObject go in _onGroundList)
            {
                Vector3 position = transform.position + go.transform.position;
                GameObject onGround = Instantiate(go, position, Quaternion.identity);
            }
        }
    }

    public override bool ApplyHp(int apply)
    {
        bool death = base.ApplyHp(apply);

        if (death)
        {
            // 死んだときの判定を書く
            GameManager.INSTANCE.SetState(SceneState.State.Over);
        }

        return death;
    }
}
