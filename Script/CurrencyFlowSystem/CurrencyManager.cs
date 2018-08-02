using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurrencyManager : MonoBehaviour {

#region Singleton

    private static CurrencyManager _instance;
    public static CurrencyManager Instance
    {
        get { return _instance; }
    }

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
            return;
        }

        _instance = this;
        DontDestroyOnLoad(this.gameObject);
    }

    #endregion
    //Singleton to access the final currency anywhere. Used for displaying total final amount as well as other information 

    [SerializeField] private FinalUnloadLoc _finalUnloadLoc;
    [SerializeField] private float _totalCurrency;
    public float TotalCurrency { get { return _totalCurrency; } set { _totalCurrency = value; } }

    void Update () {
        _totalCurrency = _finalUnloadLoc.FinalAmount;
	}
	
    void OnGUI()
    {
        GUI.Label(new Rect(10, 10, 100, 20), "Total Gold: " + _totalCurrency.ToString());
    }
}
