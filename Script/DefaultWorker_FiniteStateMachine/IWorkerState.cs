using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IWorkerState
{
    void Execute();
    void Enter(DefaultWorker cartWorker);
    void Exit();
    void OnTriggerEnter(Collider other);
    void OnTriggerStay(Collider other);
    void OnTriggerExit(Collider other);
}
