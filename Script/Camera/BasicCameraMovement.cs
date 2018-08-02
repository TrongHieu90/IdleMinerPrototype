using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicCameraMovement : MonoBehaviour {

    [SerializeField] private float _movementSpeed;
    private float _maximumCeiling; //camera wont go up pass this point
    private float _minimumFloor; //camera wont go down pass this point

	// Use this for initialization
	void Start () {
        _maximumCeiling = 9.8f;
        _minimumFloor = -55.6f;
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S))
        {
            if(transform.position.y >= _minimumFloor)
            {
                transform.Translate(new Vector3(0, -_movementSpeed * Time.deltaTime, 0));
            }           
        }
        if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.X))
        {
            if(transform.position.y <= _maximumCeiling)
            {
                transform.Translate(new Vector3(0, _movementSpeed * Time.deltaTime, 0));
            }           
        }
    }
}
