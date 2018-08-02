using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IWorkerLoadingState : IWorkerState {
 
    private DefaultWorker _defaultWorker;

    private float _loadingTimer;
    private float _loadingDuration;

    private bool _canMoveNext; //can move to the next loading point if the worker has elevator tag

    public void Enter(DefaultWorker defaultWorker)
    {
        _defaultWorker = defaultWorker;
    }

    public void Execute()
    {
        //Debug.Log("LoadingStuff");
        LoadingCart();
    }

    public void Exit()
    {
       
    }

    public void OnTriggerEnter(Collider other)
    {
       
    }

    public void OnTriggerExit(Collider other)
    {
        
    }

    public void OnTriggerStay(Collider other)
    {
        // we want the elevator to keep going down if there is next level. It will go up if reaches last level
        //return a bool to be used in LoadingCart function
        if (other.gameObject.CompareTag("LoadingPoint") && _defaultWorker.gameObject.CompareTag("ElevatorWorker"))
        {
            _canMoveNext = LevelManager.Instance.HasNextLevel(other.gameObject.transform.parent.parent);
        }
    }

    private void LoadingCart()
    {
        _loadingTimer += Time.deltaTime;
        _loadingDuration = _defaultWorker.Capacity / _defaultWorker.CollectRate;

        if(_loadingTimer >= _loadingDuration)
        {
            if(_defaultWorker.CartLoadingPoint)
            {
                //change direction of non-elevator worker
                if(!_defaultWorker.gameObject.CompareTag("ElevatorWorker"))
                {
                    _defaultWorker.ChangeDirection();
                }
                //for elevator worker, only change direction when the elevator reaches the last level
                else
                {
                    if(!_canMoveNext)
                    {
                        _defaultWorker.ChangeDirection();
                    }
                }
            }

            _defaultWorker.ChangeState(new IWorkerMoveState());
        }
    }
}
