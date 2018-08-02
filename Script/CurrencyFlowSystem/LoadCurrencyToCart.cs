using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadCurrencyToCart : MonoBehaviour {

    [SerializeField] private ElevatorUnload _elevatorUnload;
    [SerializeField] private float _amountToLoadToCart;
    [SerializeField] private DefaultWorker _defaultWorker;

    public float AmountToLoadToCart
    {
        get
        {
            return _amountToLoadToCart;
        }

        set
        {
            _amountToLoadToCart = value;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("LoadingPoint"))
        {
            if(_elevatorUnload.AmountElevatorHold >= _defaultWorker.Capacity)
            {
                _amountToLoadToCart = _defaultWorker.Capacity;
                _elevatorUnload.AmountElevatorHold -= _amountToLoadToCart;
            }
            else
            {
                _amountToLoadToCart = _elevatorUnload.AmountElevatorHold;
                _elevatorUnload.AmountElevatorHold = 0;
            }

        }
    }

    void OnGUI()
    {
        var position = Camera.main.WorldToScreenPoint(gameObject.transform.position);
        GUI.contentColor = Color.white;

        GUI.Label(new Rect(position.x, Screen.height - position.y, 30, 20), _amountToLoadToCart.ToString(), "box");

    }
}
