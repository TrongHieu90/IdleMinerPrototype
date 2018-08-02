using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbstractManagers : MonoBehaviour {

    [SerializeField] protected GameObject _manager;  // the manager game object
    [SerializeField] protected DefaultWorker _defaultWorker; //the worker that the manager has influence on 
            
	// Use this for initialization
	protected virtual void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    protected virtual float ImproveMovementSpeed()
    {
        //Manager's ability to improve worker's movement speed
        return _defaultWorker.MovementSpeed;
    }

    protected virtual float ImproveCollectRate()
    {
        //Manager's ability to improve worker's collect rate
        return _defaultWorker.CollectRate;
    }

    protected virtual float ImproveCapacity()
    {
        //Manager's ability to improve worker's capacity
        return _defaultWorker.Capacity;
    }
}
