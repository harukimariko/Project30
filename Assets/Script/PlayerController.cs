using UnityEngine;
using System.Collections.Generic;
using static UnityEditor.Searcher.SearcherWindow.Alignment;

public class PlayerController : MonoBehaviour
{
    [SerializeField] Camera _camera; 
    Rigidbody _rb;
    [SerializeField] bool _isGround;
    [SerializeField, Range(0.0f, 5.0f)] private float _accelerationRatio = 1.0f;
    [SerializeField, Range(0.0f, 10.0f)] private float _speedMax = 5.0f;
    Vector3 _velocity = Vector3.zero;

    [SerializeField, Range(0.0f, 10.0f)] public float _jumpRatio = 5.0f;
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

        // カメラの正面方向と右方向を取得（Y軸は無視）
        Vector3 cameraForward = _camera.transform.forward;
        cameraForward.y = 0;
        cameraForward.Normalize();

        Vector3 cameraRight = _camera.transform.right;
        cameraRight.y = 0;
        cameraRight.Normalize();

        // カメラ基準の移動方向を作成
        Vector3 direction = cameraForward * vertical + cameraRight * horizontal;
        direction.Normalize(); // 斜め移動でも速度が一定になるように正規化

        _velocity = direction * _accelerationRatio;

        if (Input.GetButtonDown("Jump") && _isGround)
        {
            JumpForce(_jumpRatio);
            _isGround = false;
        }
    }
    private void FixedUpdate()
    {
        if (_velocity.magnitude > 0.0f)
        {
            _rb.AddForce(_velocity, ForceMode.VelocityChange);
            _velocity = Vector3.zero;
        }

        // 速度の保存
        Vector3 velocity = new Vector3(_rb.linearVelocity.x, 0.0f, _rb.linearVelocity.z);

        // 最大速度制限
        if (velocity.magnitude > _speedMax)
        {
            velocity = velocity.normalized * _speedMax;

            _rb.linearVelocity = velocity + new Vector3(0.0f, _rb.linearVelocity.y, 0.0f);
        }

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
}
