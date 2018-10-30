using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Setup : MonoBehaviour {

    public Camera mainCam;

    public BoxCollider2D topWall, botWall;
    public BoxCollider2D leftWall, rightWall;

    public Transform player1, player2;

    float startingPos1, startingPos2;

    public GUISkin textSkin;

    int p1Score, p2Score;

    void Start()
    {
        startingPos1 = mainCam.ScreenToWorldPoint(new Vector3(Screen.width, 0f, 0f)).x * -1f + .5f;
        startingPos2 = mainCam.ScreenToWorldPoint(new Vector3(Screen.width, 0f, 0f)).x - .5f;

        topWall.size = new Vector2(mainCam.ScreenToWorldPoint(new Vector3(Screen.width * 2f, 0f, 0f)).x, 1f);
        topWall.offset = new Vector2(0f, mainCam.ScreenToWorldPoint(new Vector3(0f, Screen.height, 0f)).y + 0.5f);

        botWall.size = new Vector2(mainCam.ScreenToWorldPoint(new Vector3(Screen.width * 2, 0f, 0f)).x, 1f);
        botWall.offset = new Vector2(0f, mainCam.ScreenToWorldPoint(new Vector3(0f, 0f, 0f)).y - 0.5f);

        leftWall.size = new Vector2(1f, mainCam.ScreenToWorldPoint(new Vector3(0f, Screen.height * 2f, 0f)).y);
        leftWall.offset = new Vector2(mainCam.ScreenToWorldPoint(new Vector3(0f, 0f, 0f)).x - 0.5f, 0f);

        rightWall.size = new Vector2(1f, mainCam.ScreenToWorldPoint(new Vector3(0f, Screen.height * 2f, 0f)).y);
        rightWall.offset = new Vector2(mainCam.ScreenToWorldPoint(new Vector3(Screen.width, 0f, 0f)).x + 0.5f, 0f);

        player1.position = new Vector3(startingPos1, 0f, -1f);
        player2.position = new Vector3(startingPos2, 0f, -1f);

        p1Score = p2Score = 0;
    }

    public void IncrementP1Score()
    {
        p1Score++;
    }

    public void IncrementP2Score()
    {
        p2Score++;
    }

    void OnGUI()
    {
        GUI.skin = textSkin;
        GUI.Label(new Rect(Screen.width / 2 - 150, 20, 100, 100), "" + p1Score);
        GUI.Label(new Rect(Screen.width / 2 + 150, 20, 100, 100), "" + p2Score);
    }

    public void RestartGame()
    {
        player1.position = new Vector3(startingPos1, 0f, -1f);
        player2.position = new Vector3(startingPos2, 0f, -1f);


    }
}
