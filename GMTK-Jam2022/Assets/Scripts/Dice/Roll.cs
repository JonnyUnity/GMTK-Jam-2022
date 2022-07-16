using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Roll : MonoBehaviour
{

    private Rigidbody _rigidbody;
    private Transform _transform;

    private bool _thrown;

    public bool HasLanded { get; private set; }

    [SerializeField] private int[] _faceValues;
    [SerializeField] private DieSide[] _dieSides;

    public int DieValue { get; private set; }

    private float _fudgeTorque;
    private float _fudgeForce;

    private Die _die;

    private void Awake()
    {
        
        _transform = transform;
        _rigidbody = GetComponent<Rigidbody>();
        _rigidbody.useGravity = false;
        _rigidbody.isKinematic = false;
        DieValue = 0;

        _fudgeTorque = Random.Range(50f, 100f);
        _fudgeForce = Random.Range(50f, 100f);

    }


public void Init(Die die)
    {
        _die = die;
        _faceValues = die.FaceValues;
    }


    // Update is called once per frame
    void Update()
    {
        //if (Input.GetMouseButtonDown(0) && !_thrown)
        //{
        //    RollDie();
        //}
        //if (HasLanded)
        //    return;
        //if (!_thrown)
        //    return;


        //if (_rigidbody.IsSleeping() && _thrown)
        //{

        //    if (!HasDieLanded())
        //    {
        //        // add torque to move die onto a face...
        //        //_rigidbody.AddTorque(Random.Range(50, 200), Random.Range(50, 200), Random.Range(50, 200));
        //        //_rigidbody.AddForce(Random.Range(50, 200), Random.Range(50, 200), Random.Range(50, 200));

        //        _rigidbody.AddTorque(_fudgeTorque, _fudgeTorque, _fudgeTorque);
        //        _rigidbody.AddForce(_fudgeForce, 50, _fudgeForce);

        //        _fudgeForce *= 2;
        //        _fudgeTorque *= 2;


        //    }
        //    else
        //    {
        //        HasLanded = true;
        //        _rigidbody.isKinematic = true;
        //        //Debug.Log(gameObject.name + " - " + DieValue);
        //    }

            
        //}

    }

    private void FixedUpdate()
    {
        if (HasLanded)
            return;
        if (!_thrown)
            return;


        if (_rigidbody.IsSleeping() && _thrown)
        {

            if (!HasDieLanded())
            {
                // add torque to move die onto a face...
                //_rigidbody.AddTorque(Random.Range(50, 200), Random.Range(50, 200), Random.Range(50, 200));
                //_rigidbody.AddForce(Random.Range(50, 200), Random.Range(50, 200), Random.Range(50, 200));

                _rigidbody.AddTorque(_fudgeTorque, _fudgeTorque, -_fudgeTorque);
                _rigidbody.AddForce(_fudgeForce, 50, -_fudgeForce);

                _fudgeForce *= 2;
                _fudgeTorque *= 2;


            }
            else
            {
                HasLanded = true;
                _rigidbody.isKinematic = true;
                //Debug.Log(gameObject.name + " - " + DieValue);
            }


        }


    }


    public DiceSpawn GetDieData()
    {
        return new DiceSpawn
        {
            DieValue = DieValue,
            Transform = _transform
        };
    }


    public void RollDie()
    {

        if (HasLanded)
            return;

        _thrown = true;
        _rigidbody.useGravity = true;
        _rigidbody.isKinematic = false;
        _rigidbody.AddTorque(Random.Range(10, 200), Random.Range(10, 200), -Random.Range(10, 200));
        _rigidbody.AddForce(Random.Range(100, 200), Random.Range(100, 200), -Random.Range(100, 200));

    }


    private bool HasDieLanded()
    {

        for (int i = 0; i < _dieSides.Length; i++)
        {
            if (_dieSides[i].OnGround)
            {
                DieValue = _faceValues[i];
                _die.SetRolledData(DieValue, _transform);
                return true;
            }
        }

        return false;

    }

}