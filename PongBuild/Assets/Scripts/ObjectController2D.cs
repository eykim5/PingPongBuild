using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectController2D : RaycastController2D {

    //  Purpose: Generates raycasts, detects collisions and activates what happens when they are triggered.

    public CollisionsInfo collisions;

    [HideInInspector]
    public Vector2 playerInput;

    public void Move(Vector2 moveAmount)
    {
        Move(moveAmount, Vector2.zero);
    }

    public virtual void Move(Vector2 moveAmount, Vector2 input)
    {
        updateRaycastOrigins();
        collisions.Reset();

        collisions.moveAmountOld = moveAmount;
        playerInput = input;

        WallCollision(ref moveAmount);

        transform.Translate(moveAmount);

    }

    public virtual void WallCollision(ref Vector2 moveAmount)
    {
        float directionY = Mathf.Sign(moveAmount.y);
        float rayLength = Mathf.Abs(moveAmount.y) + skinWidth;


        for (int i = 0; i < vertRayCount; ++i)
        {
            Vector2 rayOrigin = (directionY == -1) ? rOrig.botLeft : rOrig.topLeft;
            rayOrigin += Vector2.right * (vertRaySpace * i);
            RaycastHit2D hit = Physics2D.Raycast(rayOrigin, Vector2.up * directionY, rayLength, collisionMask);

            Debug.DrawRay(rayOrigin, Vector2.up * directionY, Color.red);

            if (hit)
            {
                moveAmount.y = (hit.distance - skinWidth) * directionY;
                rayLength = hit.distance;

                collisions.above = directionY == 1;
                collisions.below = directionY == -1;
            }
        }

    }

    public struct CollisionsInfo
    {
        public bool above, below;
        public bool left, right;

        public Vector2 moveAmountOld;

        public void Reset()
        {
            above = below = false;
            left = right = false;
        }
    }
}
