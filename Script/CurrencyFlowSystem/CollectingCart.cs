using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectingCart : MonoBehaviour {

    public float _amountInCart;
    [SerializeField] private DefaultWorker _defaultWorker;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("NormalWorker"))
        {
            _amountInCart += _defaultWorker.Capacity;
        }
    }

    void OnGUI()
    {
        var position = Camera.main.WorldToScreenPoint(gameObject.transform.position);
        GUI.contentColor = Color.white;

        GUI.Label(new Rect(position.x, Screen.height - position.y, 30, 20), _amountInCart.ToString(), "box");
       
    }
}
