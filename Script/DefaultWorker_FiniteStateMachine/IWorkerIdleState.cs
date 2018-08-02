using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IWorkerIdleState : IWorkerState {

    private DefaultWorker _defaultWorker;

    public void Enter(DefaultWorker defaultWorker)
    {
        _defaultWorker = defaultWorker;
        _defaultWorker.CanMove = false;
    }

    public void Execute()
    {
        //Debug.Log("Idling");
        SwitchToMoveState();
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

    private void SwitchToMoveState()
    {
        if(_defaultWorker.CanMove)
        {
            _defaultWorker.ChangeState(new IWorkerMoveState());
            _defaultWorker.CanMove = false;
        }
    }
}
