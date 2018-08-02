using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnlockManager : MonoBehaviour {

    [SerializeField] private GameObject _managerWorker;
    [SerializeField] private float _unlockPrice;
    private FinalUnloadLoc _finalUnloadLoc;
    private bool _showButton = true;

    private void Start()
    {
        _finalUnloadLoc = FindObjectOfType<FinalUnloadLoc>();
    }

    private void OnGUI()
    {    
        var position = Camera.main.WorldToScreenPoint(gameObject.transform.position);

        GUI.contentColor = Color.white;
        GUI.backgroundColor = Color.yellow;
        GUI.skin.button.fontSize = 8;
        if(_showButton)
        {
            if (GUI.Button(new Rect(position.x, Screen.height - position.y - 30, 80, 20), "Unlock Manager: " + _unlockPrice.ToString()))
            {
                if(CurrencyManager.Instance.TotalCurrency >= _unlockPrice)
                {
                    if (!_managerWorker.activeInHierarchy)
                    {
                        _managerWorker.SetActive(true);
                    }
                    _finalUnloadLoc.FinalAmount -= _unlockPrice;
                    _showButton = false;
                    
                }
                
            }
        }         
              
    }
}
