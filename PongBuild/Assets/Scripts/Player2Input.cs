using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player2Input : MonoBehaviour {

    Player player;

    // Use this for initialization
    void Start()
    {
        player = GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 dInput = new Vector2(0, Input.GetAxisRaw("Vertical2"));
        player.SetDirectionalInput(dInput);
    }
}
