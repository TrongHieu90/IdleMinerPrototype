using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectingElevator : MonoBehaviour {

    [SerializeField] private DefaultWorker _defaultWorker;
    [SerializeField] private CollectingCart _collectingCart;
    [SerializeField] private float _amountElevatorCollect = 0;
    private Transform _topLevelParent;

    public float AmountElevatorCollect
    {
        get
        {
            return _amountElevatorCollect;
        }

        set
        {
            _amountElevatorCollect = value;
        }
    }

    // Use this for initialization
    void Start()
    {
        _collectingCart = null;
    }
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    private void ElevatorAmountToCollect()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("LoadingPoint") && _defaultWorker.FacingRight)
        {
            FindTopLevelParent(other.transform);
            _collectingCart = _topLevelParent.transform.Find("CollectBoxHolder/CollectBox").gameObject.GetComponent<CollectingCart>();

            if (_collectingCart._amountInCart == 0)
            {
                AmountElevatorCollect = AmountElevatorCollect;
            }
            else if(_collectingCart._amountInCart >= _defaultWorker.Capacity)
            {
                AmountElevatorCollect += _defaultWorker.Capacity;
                _collectingCart._amountInCart -= _defaultWorker.Capacity;     
            }
            else if(_collectingCart._amountInCart < _defaultWorker.Capacity)
            {
                AmountElevatorCollect += _collectingCart._amountInCart;
                _collectingCart._amountInCart = 0;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("LoadingPoint") && _defaultWorker.FacingRight)
        {
            _collectingCart = null;
        }
    }

    private Transform FindTopLevelParent(Transform _currentLevel)
    {
        //return the Level[i] parent of object
        return _topLevelParent = _currentLevel.parent.parent;
    }

    void OnGUI()
    {
        var position = Camera.main.WorldToScreenPoint(gameObject.transform.position);
        GUI.contentColor = Color.white;

        GUI.Label(new Rect(position.x, Screen.height - position.y, 30, 20), _amountElevatorCollect.ToString(), "box");
    }
}
