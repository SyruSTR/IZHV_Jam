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
        Run,
        Eat
    }

    private CharacterController _characterController;
    [SerializeField] private Transform bugNest;
    [SerializeField] private float nestRadius;
    [SerializeField] private Vector3 _pointToMove;

    [SerializeField] private float normalSpeed;
    [SerializeField] private float runSpeed;
    private Transform _player;
    private State _animState;
    private Coroutine _idleCoroutine;

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

    // Update is called once per frame
    private Vector3 offset;
    void Update()
    {
        switch (_animState)
        {
                case State.Start:
                    Vector2 randomCircle = UnityEngine.Random.insideUnitCircle * nestRadius;
                    _pointToMove = new Vector3(randomCircle.x, transform.position.y, randomCircle.y);
                    offset = _pointToMove - transform.position;
                    _animState = State.Move;
                    break;
                case State.Eat:
                    break;
                case State.Move:
                    offset = _pointToMove - transform.position;
                    if (offset.magnitude > .1f)
                    {
                        offset = offset.normalized * normalSpeed;
                        _characterController.Move(offset * Time.deltaTime);
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
                case State.Run:
                    break;
        }
    }

    IEnumerator BugIdle()
    {
        yield return new WaitForSeconds(UnityEngine.Random.Range(1.0f, 2.0f));
        _animState = State.Start;
        _idleCoroutine = null;
    }
}
