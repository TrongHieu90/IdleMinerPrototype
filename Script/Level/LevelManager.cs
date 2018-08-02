using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour {

#region Singleton
    private static LevelManager _instance;
    public static LevelManager Instance
    {
        get
        {
            if(_instance == null)
            {
                _instance = FindObjectOfType<LevelManager>();
            }
            return _instance;
        }
        
    }
#endregion

    [SerializeField] private Transform[] _allLevelChildren; //array to store all the level as gameobject
    public Transform[] AllLevelChildren
    {
        get
        {
            return _allLevelChildren;
        }

        set
        {
            _allLevelChildren = value;
        }
    }

    [SerializeField] private float _unlockPrice;
    private FinalUnloadLoc _finalUnloadLoc;

    void Start () {
        _finalUnloadLoc = FindObjectOfType<FinalUnloadLoc>();
        AllLevelChildren = new Transform[transform.childCount];
        LevelIteration();
        SetLevelInactiveOnStart();
    }

	void Update () {
		
	}

    private void LevelIteration()
    {
        int i = 0; 
        foreach(Transform child in transform)
        {
            _allLevelChildren[i] = child;
            i += 1;
        }
    }

    public bool HasNextLevel(Transform _currentLevel)
    {
        
        int _levelIndex = System.Array.IndexOf(_allLevelChildren, _currentLevel);
        //return true if a level as another level after and is active, false otherwise
        return _levelIndex < _allLevelChildren.Length -1 
                && _allLevelChildren[_levelIndex+1].gameObject.activeInHierarchy ? true : false;

    }

    public void SetLevelInactiveOnStart()
    {
        for (int i = 0; i < _allLevelChildren.Length; i++)
        {
            if(i != 0) //exclude the first level
            {
                _allLevelChildren[i].gameObject.SetActive(false);
            }
        }
    }

    private void OnGUI()
    {
        var position = Camera.main.WorldToScreenPoint(gameObject.transform.position);

        GUI.contentColor = Color.white;
        GUI.backgroundColor = Color.blue;
        GUI.skin.button.fontSize = 8;

        for (int i = 0; i < _allLevelChildren.Length; i++)
        {
            if (i != 0) //exclude the first level
            {
                if(!_allLevelChildren[i].gameObject.activeInHierarchy)
                {
                    //GUI.Label(new Rect(position.x, Screen.height - position.y - 30, 40, 20), "Unlock Level");
                    if (GUI.Button(new Rect(position.x - 50, Screen.height - position.y + 150, 150, 50), "Unlock Level: " + _unlockPrice))
                    {
                        if(CurrencyManager.Instance.TotalCurrency >= _unlockPrice)
                        {
                            _allLevelChildren[i].gameObject.SetActive((true));
                            _finalUnloadLoc.FinalAmount -= _unlockPrice;
                            _unlockPrice += 5; //increase unlock price the deeper the level runs down
                        }
                        
                    }
                }
            }
        }
    }
}
