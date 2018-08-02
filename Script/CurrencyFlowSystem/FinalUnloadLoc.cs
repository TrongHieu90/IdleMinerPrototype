using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalUnloadLoc : MonoBehaviour {

    [SerializeField] private LoadCurrencyToCart _loadCurrencyToCart;
    private float _finalAmount;
    public float FinalAmount
    {
        get
        {
            return _finalAmount;
        }

        set
        {
            _finalAmount = value;
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
        if(other.gameObject.CompareTag("CartWorker"))
        {
            FinalAmount += _loadCurrencyToCart.AmountToLoadToCart;
            _loadCurrencyToCart.AmountToLoadToCart = 0;
        }
    }

    void OnGUI()
    {
        var position = Camera.main.WorldToScreenPoint(gameObject.transform.position);
        GUI.contentColor = Color.white;

        GUI.Label(new Rect(position.x, Screen.height - position.y, 30, 20), FinalAmount.ToString(), "box");

    }
}
