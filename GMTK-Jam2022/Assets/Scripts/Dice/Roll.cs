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


    private void Awake()
    {
        _transform = transform;
        _rigidbody = GetComponent<Rigidbody>();
        _rigidbody.useGravity = false;
    }


    // Start is called before the first frame update
    void Start()
    {
        


    }

    // Update is called once per frame
    void Update()
    {
        //if (Input.GetMouseButtonDown(0) && !_thrown)
        //{
        //    RollDie();
        //}

        if (_rigidbody.IsSleeping() && _thrown)
        {

            if (!HasDieLanded())
            {
                // add torque to move die onto a face...
                _rigidbody.AddTorque(Random.Range(50, 200), Random.Range(50, 200), Random.Range(50, 200));
                _rigidbody.AddForce(Random.Range(50, 200), Random.Range(50, 200), Random.Range(50, 200));
            }
            else
            {
                HasLanded = true;
                Debug.Log(gameObject.name + " - " + DieValue);
            }

            
        }

    }

    public DiceSpawn GetDieData()
    {
        return new DiceSpawn
        {
            DieValue = DieValue,
            Position = new Vector2(_transform.position.x, _transform.position.z)
        };
    }


    public void RollDie()
    {

        if (HasLanded)
            return;

        _thrown = true;
        _rigidbody.useGravity = true;
        _rigidbody.AddTorque(Random.Range(10, 200), Random.Range(10, 200), Random.Range(10, 200));
        _rigidbody.AddForce(Random.Range(10, 200), Random.Range(10, 200), Random.Range(10, 200));

    }


    private bool HasDieLanded()
    {

        bool hasDieLanded = false;

        for (int i = 0; i < _dieSides.Length; i++)
        {
            if (_dieSides[i].OnGround)
            {
                DieValue = _faceValues[i];
                hasDieLanded = true;
                break;
            }
        }

        return hasDieLanded;

    }


}
