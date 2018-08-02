using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IWorkerUnloadingState : IWorkerState {

    private DefaultWorker _defaultWorker;

    private float _unloadingTimer;
    private float _unloadingDuration;

    public void Enter(DefaultWorker cartWorker)
    {
        //Debug.Log("UnloadingState");
        _defaultWorker = cartWorker;
    }

    public void Execute()
    {
        if (_defaultWorker.CompareTag("NormalWorker"))
        {
            //normal worker doesnt have unloading duration so the state is switched immediately
            SwitchState();
        }
        if (_defaultWorker.CompareTag("ElevatorWorker") || _defaultWorker.CompareTag("CartWorker"))
        {
            //other types of workers have unloading duration
            UnloadingCart();
        }
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
        
    }
    
    private void UnloadingCart()
    {
        _unloadingTimer += Time.deltaTime;
        _unloadingDuration = _defaultWorker.Capacity / _defaultWorker.CollectRate;

        if(_unloadingTimer >= _unloadingDuration)
        {
            SwitchState();
        }
    }

    private void SwitchState()
    {
        _defaultWorker.ChangeDirection();
        if (_defaultWorker.HasManager)
        {
            _defaultWorker.ChangeState(new IWorkerMoveState());
        }
        else
        {
            _defaultWorker.ChangeState(new IWorkerIdleState());
        }
    }
}
