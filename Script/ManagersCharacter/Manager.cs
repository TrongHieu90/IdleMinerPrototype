using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager : AbstractManagers {

    private string _onGuiText;
    private bool _showButton; //showing the ability's button to press on screen

    [SerializeField] private float _abilityDuration; // the duration of manager's ability
    [SerializeField] private float _abilityCoolDownDuration;
    private float _abilityTimer;
    private float _abilityCoolDownTimer;
    
    private float _cachedMovementSpeed;
    private float _cachedCapacity;
    private float _cachedCollectRate;

    private bool _canActivate; //allow an ability to be activated
    private bool _canDeactivate;

    public enum ManagerAbilities
    {
        FasterMovementSpeed,
        FasterCollectRate,
        BiggerCapacity
    };

    public ManagerAbilities _currentManagerAbility;

    public enum AbilityStatus
    {
        Ready, //this is the start of ability. The skill button will be pressable.
        Active, // cant press skill button. A duration timer will be calculated for skill effect.
        Charging //skill duration ends. A cooldown appears to calculate when the skill will be ready again.
    };

    public AbilityStatus _abilityStatus;

    void OnEnable()
    {
        _defaultWorker.HasManager = true;
    }

    protected override void Start () {
        base.Start();
        _abilityStatus = AbilityStatus.Ready;

        //caching all the data to class's local data
        _abilityTimer = _abilityDuration;
        _abilityCoolDownTimer = _abilityCoolDownDuration;
        _cachedMovementSpeed = _defaultWorker.MovementSpeed;
        _cachedCapacity = _defaultWorker.Capacity;
        _cachedCollectRate = _defaultWorker.CollectRate;

        RandomAbility();
    }

	void Update () {
        AbilityHandle();
	}

    private string RandomAbility()
    {
        _currentManagerAbility = (ManagerAbilities)Random.Range(0, System.Enum.GetValues(typeof(ManagerAbilities)).Length);

        switch (_currentManagerAbility)
        {
            case ManagerAbilities.FasterMovementSpeed:
                _onGuiText = "Run"; //update from text to icon texture here
                break;
            case ManagerAbilities.BiggerCapacity:
                _onGuiText = "Capacity";
                break;
            case ManagerAbilities.FasterCollectRate:
                _onGuiText = "Collect";
                break;
        }
        return _onGuiText;
    }

    private void AbilityHandle()
    {
        switch(_abilityStatus)
        {
            case AbilityStatus.Ready:
                _showButton = true;
                _canActivate = true;
                break;

            case AbilityStatus.Active:

                ActivateAbility();
                _abilityDuration -= Time.deltaTime;

                if (_abilityDuration < 0)
                {
                    _abilityStatus = AbilityStatus.Charging;
                    _abilityDuration = _abilityTimer;
                    _canDeactivate = true;
                }
                break;

            case AbilityStatus.Charging:

                DeactivateAbility();
                _abilityCoolDownDuration -= Time.deltaTime;

                if(_abilityCoolDownDuration < 0)
                {
                    _abilityStatus = AbilityStatus.Ready;
                    _abilityCoolDownDuration = _abilityCoolDownTimer;                  
                }
                break;
        }
    }


    private void ActivateAbility()
    {
        if(_canActivate)
        {
            switch (_currentManagerAbility)
            {
                case ManagerAbilities.FasterMovementSpeed:
                    ImproveMovementSpeed();
                    break;

                case ManagerAbilities.BiggerCapacity:
                    ImproveCapacity();
                    break;

                case ManagerAbilities.FasterCollectRate:
                    ImproveCollectRate(); ;
                    break;
            }
            _canActivate = false;
        }
        
    }

    private void DeactivateAbility()
    {
        if(_canDeactivate)
        {
            switch (_currentManagerAbility)
            {
                case ManagerAbilities.FasterMovementSpeed:
                    _defaultWorker.MovementSpeed = _cachedMovementSpeed;
                    break;

                case ManagerAbilities.BiggerCapacity:
                    _defaultWorker.Capacity = _cachedCapacity;
                    break;

                case ManagerAbilities.FasterCollectRate:
                    _defaultWorker.CollectRate = _cachedCollectRate;
                    break;
            }
            _canDeactivate = false;
        }     
    }

    protected override float ImproveMovementSpeed()
    {
        base.ImproveMovementSpeed();
        return _defaultWorker.MovementSpeed *= 2f;
    }

    protected override float ImproveCollectRate()
    {
        base.ImproveCollectRate();
        return _defaultWorker.CollectRate *= 2f;
    }

    protected override float ImproveCapacity()
    {
        base.ImproveCapacity();
        return _defaultWorker.Capacity *= 2f;
    }

    void OnGUI()
    {
        var position = Camera.main.WorldToScreenPoint(gameObject.transform.position);

        GUI.contentColor = Color.white;
        GUI.backgroundColor = Color.blue;
        GUI.skin.button.fontSize = 8;

        switch(_abilityStatus)
        {
            case AbilityStatus.Ready:
                if (GUI.Button(new Rect(position.x, Screen.height - position.y - 30, 40, 20), _onGuiText))
                {
                    _showButton = false;
                    _abilityStatus = AbilityStatus.Active;
                }
                break;

            case AbilityStatus.Active:
                int i = Mathf.RoundToInt(_abilityDuration);
                GUI.Label(new Rect(position.x, Screen.height - position.y - 30, 40, 20), i.ToString(), "box");
                break;

            case AbilityStatus.Charging:
                int j = Mathf.RoundToInt(_abilityCoolDownDuration);
                GUI.Label(new Rect(position.x, Screen.height - position.y - 30, 40, 20), "cd: " + j.ToString(), "box");
                break;
        }    
    }
}
