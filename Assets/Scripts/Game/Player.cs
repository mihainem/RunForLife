using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float speed = 6f;
    [SerializeField] private float jumpForce = 4.5f;
    private bool jump;
    private float horrizontalMovementUnit = 3f;
    private Vector3 lastPosition;

    private Animator _animator;
    private LayerMask enemyMask;
    private Weapon gun;
    private Rigidbody _rigidbody;
    private Transform _transform;
    private bool startMovement = false;
    void Awake()
    {
        _transform = transform;
        _rigidbody = GetComponent<Rigidbody>();
        _animator = GetComponent<Animator>();
        gun = GetComponentInChildren<Weapon>();
    }
    private void Start()
    {
        enemyMask = LayerMask.GetMask("Enemy");
    }

    public void SetMovement(bool active)
    {
        _animator.SetBool("Moving", active);
        startMovement = active;
    }

    private void FixedUpdate()
    {
        if (!startMovement)
            return;

        //continuously move forward
        _rigidbody.MovePosition(_transform.position + Vector3.forward * speed * Time.deltaTime);

        //if player hit a wall;
        if (_rigidbody.position.z == lastPosition.z && jump && IsGrounded())
        {
            _rigidbody.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }

        //if player jumped off the platforms
        if (_transform.position.y <= -10f)
        {
            GameManager.Instance.FailFinishedLevel();
        }

        if (jump)
        {
            _rigidbody.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            jump = false;
        }

        float velocityY = Mathf.Min(_rigidbody.velocity.y, 8f); // Mathf.Clamp(_rigidbody.velocity.y, 0f, 5f);
        _rigidbody.velocity = new Vector3(_rigidbody.velocity.x, velocityY, _rigidbody.velocity.z);

        lastPosition = _rigidbody.position;
    }

    internal void StartPlay()
    {
        _transform.position = new Vector3(0, 0.5f, 5f);
        SetMovement(true);
    }

    private bool IsGrounded()
    {
        return Math.Abs(_rigidbody.velocity.y) <= 0.01f;
    }

    private void AimToClosestEnemy()
    {
        var closestEnemies = Physics.OverlapSphere(transform.position, 10, enemyMask);
        if (closestEnemies.Length <= 0)
            return;
        Vector3 closestEnemyPos = closestEnemies[0].transform.position;
        float minDistance = (closestEnemyPos - transform.position).magnitude;
        foreach (Collider enemy in closestEnemies)
        {
            if ((enemy.transform.position - transform.position).magnitude < minDistance)
            {
                minDistance = (enemy.transform.position - transform.position).magnitude;
                closestEnemyPos = enemy.transform.position;
            }
        }

        transform.LookAt(closestEnemyPos);
    }

    public void Shoot()
    {
        gun.Shoot();
    }

    private void OnEnable()
    {
        InputController.OnTap += InputController_OnTap;
        InputController.OnSwipeLeft += InputController_OnSwipeLeft;
        InputController.OnSwipeRight += InputController_OnSwipeRight;
        InputController.OnSwipeUp += InputController_OnSwipeUp;
        InputController.OnSwipeDown += InputController_OnSwipeDown;
    }

    private void OnDisable()
    {
        InputController.OnTap -= InputController_OnTap;
        InputController.OnSwipeLeft -= InputController_OnSwipeLeft;
        InputController.OnSwipeRight -= InputController_OnSwipeRight;
        InputController.OnSwipeUp -= InputController_OnSwipeUp;
        InputController.OnSwipeDown -= InputController_OnSwipeDown;
    }

    private void InputController_OnSwipeDown()
    {
        Debug.Log("On Swipe down not implemented");
    }

    private void InputController_OnSwipeUp()
    {
        if (!jump && IsGrounded())
        {
            jump = true;
        }
    }

    private void InputController_OnSwipeRight()
    {
        if (IsGrounded() && transform.position.x < 1)
        {
            transform.Translate(Vector3.right * horrizontalMovementUnit);
        }
    }

    private void InputController_OnSwipeLeft()
    {
        if (IsGrounded() && transform.position.x > -1)
        {
            transform.Translate(Vector3.left * horrizontalMovementUnit);
        }
    }


    private void InputController_OnTap()
    {
        AimToClosestEnemy();
        _animator.SetTrigger("Shooting");
    }
}
