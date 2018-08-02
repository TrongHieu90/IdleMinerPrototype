using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElevatorUnload : MonoBehaviour {

    [SerializeField] private CollectingElevator _collectingElevator;
    [SerializeField] private float _amountElevatorHold = 0;

    public float AmountElevatorHold
    {
        get
        {
            return _amountElevatorHold;
        }

        set
        {
            _amountElevatorHold = value;
        }
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("ElevatorWorker"))
        {
            AmountElevatorHold += _collectingElevator.AmountElevatorCollect;
            _collectingElevator.AmountElevatorCollect = 0;
        }
    }

    void OnGUI()
    {
        var position = Camera.main.WorldToScreenPoint(gameObject.transform.position);
        GUI.contentColor = Color.white;

        GUI.Label(new Rect(position.x, Screen.height - position.y, 30, 20), _amountElevatorHold.ToString(), "box");

    }
}
