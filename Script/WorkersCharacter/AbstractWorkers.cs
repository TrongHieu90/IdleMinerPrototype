using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbstractWorkers : MonoBehaviour {

    [SerializeField] protected GameObject _worker;

    [SerializeField] protected float _movementSpeed; //base movement speed of all workers
    [SerializeField] protected bool _facingRight; //used to set direction of worker. Also used to set up down direction of elevator worker
    [SerializeField] protected bool _canMove; //allow the worker to move (exit idle states)
    public float MovementSpeed { get { return _movementSpeed; } set { _movementSpeed = value; } }
    public bool FacingRight { get { return _facingRight; } }
    public bool CanMove { get { return _canMove; } set { _canMove = value; } }
    
    protected bool _cartLoadingPoint;
    public bool CartLoadingPoint { get { return _cartLoadingPoint; }  set { _cartLoadingPoint = value; } }

    [SerializeField] protected bool _hasManager;
    public bool HasManager { get { return _hasManager; } set { _hasManager = value; } }

    [SerializeField] protected float _collectRate = 1;
    [SerializeField] protected float _capacity; //maximum amount a worker can carry
    public float CollectRate { get { return _collectRate; } set { _collectRate = value; } }
    public float Capacity { get { return _capacity; } set { _capacity = value; } }

    protected virtual void Start ()
    {
		
	}

	void Update ()
    {
		
	}

    public virtual void ChangeDirection() 
    {
        //flip the side of direction of moving workers
    }
}
