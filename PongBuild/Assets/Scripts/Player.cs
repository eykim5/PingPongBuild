using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof (ObjectController2D))]
public class Player : MonoBehaviour {

    public float moveSpeed = 16f;
    //float origMoveSpeed;
    public float accelTime = .1f;

    Vector2 velocity;
    float velocityYSmoothing;

    ObjectController2D controller;

    Vector2 dInput;

	// Use this for initialization
	void Start () {
        controller = GetComponent<ObjectController2D>();

        //origMoveSpeed = moveSpeed;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        CalculateVelocity();

        controller.Move(velocity * Time.deltaTime, dInput);
	}

    public void SetDirectionalInput (Vector2 input)
    {
        dInput = input;
    }

    void CalculateVelocity()
    {
        float targetVelocityY = dInput.y * moveSpeed;
        velocity.y = Mathf.SmoothDamp(velocity.y, targetVelocityY, ref velocityYSmoothing, accelTime);
    }
}
