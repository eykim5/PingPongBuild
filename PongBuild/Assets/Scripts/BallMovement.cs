using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof (BallController2D))]
public class BallMovement : MonoBehaviour {

    public float moveSpeed = 6f;
    public float maxAngle = 75f;
    public float difficulty = 1.0001f;

    Vector2 velocity;
    float velocitySmoothing;

    int vertInt = 1;
    int horiInt = -1;
    float currentAngle = 0;
    float origMoveSpeed;

    BallController2D bController2D;
    GameObject gameManager;
    Setup setup;

	void Start () {
        bController2D = GetComponent<BallController2D>();
        bController2D.setMaxAngle(maxAngle);

        gameManager = GameObject.Find("GameManager");
        setup = gameManager.GetComponent<Setup>();

        origMoveSpeed = moveSpeed;
	}

    void FixedUpdate() {
        if (bController2D.collisions.above || bController2D.collisions.below)
        {
            vertInt *= -1;
        }

        if (bController2D.collisions.left || bController2D.collisions.right)
        {
            currentAngle = bController2D.getCurrentAngle();
            horiInt *= -1;
            vertInt = 1;
        }

        moveSpeed *= difficulty;

        Debug.Log(moveSpeed);

        CalculateVelocity();

        bController2D.Move(velocity * Time.deltaTime);
	}

    void CalculateVelocity()
    {
        velocity.x = horiInt * moveSpeed * Mathf.Cos((currentAngle * Mathf.PI) / 180);
        velocity.y = vertInt * moveSpeed * Mathf.Sin((currentAngle * Mathf.PI) / 180);
    }

    void OnTriggerEnter2D(Collider2D endZone)
    {
        if (endZone.tag == "LeftWall")
        {
            setup.IncrementP2Score();
        }
        else if (endZone.tag == "RightWall")
        {
            setup.IncrementP1Score();
        }

        Invoke("Restart", 2f);
    }

    void Restart()
    {
        horiInt = Random.Range(0, 2);
        currentAngle = Random.Range(0, 60);

        if (horiInt == 0)
        {
            horiInt = -1; 
        }

        moveSpeed = origMoveSpeed;
        origMoveSpeed += 0.4f;
        transform.position = new Vector3(0f, 0f, -1f);

        velocity.x = horiInt * moveSpeed * Mathf.Cos((currentAngle * Mathf.PI) / 180);
        velocity.y = vertInt * moveSpeed * Mathf.Sin((currentAngle * Mathf.PI) / 180);

        setup.RestartGame();
    }
}
