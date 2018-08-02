using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IWorkerMoveState : IWorkerState {

    private DefaultWorker _defaultWorker;

    private float _movementSpeed;
    private bool _loadingPoint;
    public bool LoadingPoint { get { return _loadingPoint; } set { _loadingPoint = value; } }

    private bool _collectGold;
    public bool CollectGold { get { return _collectGold; } set { _collectGold = value; } }

    public void Enter(DefaultWorker defaultWorker)
    {
        _defaultWorker = defaultWorker;
    }

    public void Execute()
    {
        //Debug.Log("Moving");
        Movement();
    }

    public void Exit()
    {
       
    }

    public void OnTriggerEnter(Collider other)
    {
        //change to loading state if our worker reaches loading point
        if (other.gameObject.CompareTag("LoadingPoint" ))
        {
            if(!_defaultWorker.gameObject.CompareTag("ElevatorWorker"))
            {
                ChangeToLoadingState();
            }
            else
            {
                if(_defaultWorker.FacingRight)
                {
                    ChangeToLoadingState();
                }
            }
            
        }
        //change to unloading state if our worker reaches unloading point
        if(other.gameObject.CompareTag("UnloadingPoint"))
        {
            ChangeToUnloadingState();
        }
    }

    public void OnTriggerExit(Collider other)
    {

    }

    public void OnTriggerStay(Collider other)
    {

    }

    public void Movement()
    {
        _movementSpeed = _defaultWorker.MovementSpeed;

        if(!LoadingPoint)
        {
            if (!_defaultWorker.gameObject.CompareTag("ElevatorWorker"))

                _defaultWorker.transform.position += GetDirectionRightLeft() * Time.deltaTime * _movementSpeed;

            else
            {
                _defaultWorker.transform.position += GetDirectionDownUp() * Time.deltaTime * _movementSpeed;
            }
        }
      
    }

    public Vector3 GetDirectionRightLeft()
    {
        return _defaultWorker.FacingRight ? Vector3.right : Vector3.left;
    }

    public Vector3 GetDirectionDownUp() //also using FacingRight bool to decide up down direction
    {
        return _defaultWorker.FacingRight ? Vector3.down : Vector3.up;
    }

    private void ChangeToLoadingState()
    {
        //Debug.Log("LoadingPoint reached");
        _loadingPoint = true;
        _defaultWorker.CartLoadingPoint = _loadingPoint;
        _defaultWorker.ChangeState(new IWorkerLoadingState());
    }

    private void ChangeToUnloadingState()
    {
        //Debug.Log("UnloadingPoint reached");
        _loadingPoint = true;
        _defaultWorker.CartLoadingPoint = _loadingPoint;
        _defaultWorker.ChangeState(new IWorkerUnloadingState());
    }
}
