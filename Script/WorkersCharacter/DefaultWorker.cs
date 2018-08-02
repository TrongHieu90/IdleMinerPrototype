using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefaultWorker : AbstractWorkers {

    private IWorkerState _currentState;
    
    protected override void Start ()
    {
        base.Start();
        ChangeState(new IWorkerIdleState());
	}
	
	void Update ()
    {
        //Debug.Log(_currentState);
        _currentState.Execute();
	}
    
    public void ChangeState(IWorkerState newState)
    {
        if(_currentState != null)
        {
            _currentState.Exit();
        }

        _currentState = newState;
        _currentState.Enter(this);
    }

    private void OnTriggerEnter(Collider other)
    {
        _currentState.OnTriggerEnter(other);
    }

    private void OnTriggerStay(Collider other)
    {
        _currentState.OnTriggerStay(other);
    }

    private void OnTriggerExit(Collider other)
    {
        _currentState.OnTriggerExit(other);
    }

    public override void ChangeDirection()
    {
        base.ChangeDirection();
        _facingRight = !_facingRight;

        if (!_worker.CompareTag("ElevatorWorker"))
        {
            _worker.transform.localScale = new Vector3(_worker.transform.localScale.x,
                                                       _worker.transform.localScale.y * -1,//careful buggy direction from blender import. Supposed to be on x-axis
                                                       _worker.transform.localScale.z);
        }
    }

    private void OnGUI()
    {
        if(_currentState is IWorkerIdleState && !_canMove)
        {
            var position = Camera.main.WorldToScreenPoint(gameObject.transform.position);

            GUI.contentColor = Color.white;
            GUI.backgroundColor = Color.red;
            GUI.skin.button.fontSize = 7;

            if(GUI.Button(new Rect(position.x, Screen.height - position.y - 30, 60, 20), "Click Me"))
            {
                _canMove = true;
            }
        }
    }
}
