using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class RaycastController2D : MonoBehaviour {

    //  Purpose: Generates Raycast origin points based on Gameobjects with BoxCollider2D.

    [HideInInspector]
    public BoxCollider2D collider2D;
    public const float skinWidth = 0.015f;
    public RaycastOrigins rOrig;
    public LayerMask collisionMask;

    const float rayDist = .09f;

    [HideInInspector]
    public int horiRayCount;
    [HideInInspector]
    public int vertRayCount;

    [HideInInspector]
    public float horiRaySpace;
    [HideInInspector]
    public float vertRaySpace;

    public virtual void Awake()
    {
        collider2D = GetComponent<BoxCollider2D>(); 
    }

    public virtual void Start()
    {
        calculateRaycastSpace();
    }

    public void updateRaycastOrigins()
    {
        Bounds bounds = collider2D.bounds;
        bounds.Expand(skinWidth * -2);

        rOrig.topLeft = new Vector2(bounds.min.x, bounds.max.y);
        rOrig.topRight = new Vector2(bounds.max.x, bounds.max.y);
        rOrig.botLeft = new Vector2(bounds.min.x, bounds.min.y);
        rOrig.botRight = new Vector2(bounds.max.x, bounds.min.y);
        
    }

    public void calculateRaycastSpace()
    {
        Bounds bounds = collider2D.bounds;
        bounds.Expand(skinWidth * -2);

        float boundsWidth = bounds.size.x;
        float boundsHeight = bounds.size.y;

        horiRayCount = Mathf.RoundToInt(boundsHeight / rayDist);
        vertRayCount = Mathf.RoundToInt(boundsWidth / rayDist);

        horiRaySpace = bounds.size.y / (horiRayCount - 1);
        vertRaySpace = bounds.size.x / (vertRayCount - 1);
    }

    public struct RaycastOrigins
    {
        public Vector2 topLeft, topRight;
        public Vector2 botLeft, botRight;
    }
}
