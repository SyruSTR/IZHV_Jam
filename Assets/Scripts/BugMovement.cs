using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

public class BugMovement : MonoBehaviour
{
    private enum State
    {
        Start,
        Move,
        Idle,
        Scare,
        Run,
        Eat
    }

    private CharacterController _characterController;
    [SerializeField] private Transform bugNest;
    [SerializeField] private float nestRadius;
     private Vector3 _pointToMove;

    [SerializeField] private float normalSpeed;
    [SerializeField] private float runSpeed;
    [SerializeField] private Transform _player;
    [SerializeField] private float scareLength = 0.5f;
    private Vector3 _offset;
    private State _animState;
    private Coroutine _idleCoroutine;
    private Vector3 _runVector;

    // Start is called before the first frame update
    private void Awake()
    {
        _characterController = GetComponent<CharacterController>();
    }

    void Start()
    {
        _animState = State.Start;
        _pointToMove = Vector3.zero;
    }
    
    void Update()
    {
        Vector3 offsetToPlayer = transform.position - _player.position;
        offsetToPlayer.y = .0f;
        if (offsetToPlayer.magnitude < scareLength && _animState != State.Run)
        {
            _animState = State.Scare;
        }
        switch (_animState)
        {
                case State.Start:
                    Vector2 randomCircle = UnityEngine.Random.insideUnitCircle * nestRadius;
                    _pointToMove = new Vector3(randomCircle.x, transform.position.y, randomCircle.y);
                    _offset = _pointToMove - transform.position;
                    _animState = State.Move;
                    break;
                case State.Eat:
                    break;
                case State.Move:
                    _offset = _pointToMove - transform.position;
                    if (_offset.magnitude > .1f)
                    {
                        _offset = _offset.normalized * normalSpeed;
                        _characterController.Move(_offset * Time.deltaTime);
                    }
                    else
                    {
                        _animState = State.Idle;
                    }
                    break;
                case State.Idle:
                    if (_idleCoroutine == null)
                    {
                        _idleCoroutine = StartCoroutine(BugIdle());
                    }
                        
                    break;
                case State.Scare:
                    //bug near nest or no
                    //x^2 + y^2 < r^2
                    if (transform.position.x * transform.position.x + transform.position.y * transform.position.y <
                        nestRadius * nestRadius)
                    {
                        _runVector = Vector3.Normalize(offsetToPlayer) * runSpeed;
                    }
                    else
                    {
                        _runVector = Vector3.Normalize(-offsetToPlayer) * runSpeed;
                    }
                    
                    StartCoroutine(RunFromThePlayer());
                    break;
                case State.Run:
                    _characterController.Move( _runVector * Time.deltaTime);
                    break;
        }
    }

    IEnumerator BugIdle()
    {
        yield return new WaitForSeconds(UnityEngine.Random.Range(1.0f, 2.0f));
        _animState = State.Start;
        _idleCoroutine = null;
    }

    IEnumerator RunFromThePlayer()
    {
        _animState = State.Run;
        yield return new WaitForSeconds(1.0f);
        _animState = State.Start;
    }
}
