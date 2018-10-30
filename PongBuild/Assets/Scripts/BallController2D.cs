using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController2D : ObjectController2D {

    float maxAngle;
    float currentAngle;

	public override void Start () {
        base.Start();
	}


    public override void Move(Vector2 moveAmount, Vector2 input)
    {
        updateRaycastOrigins();
        collisions.Reset();

        collisions.moveAmountOld = moveAmount;

        VertCollision(ref moveAmount);
        HoriCollision(ref moveAmount);

        transform.Translate(moveAmount);

    }

    public override void WallCollision(ref Vector2 moveAmount)
    {
        return;
    }

    void VertCollision(ref Vector2 moveAmount)
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

    void HoriCollision(ref Vector2 moveAmount)
    {
        float directionX = Mathf.Sign(moveAmount.x);
        float rayLength = Mathf.Abs(moveAmount.x) + skinWidth;


        for (int i = 0; i < horiRayCount; ++i)
        {
            Vector2 rayOrigin = (directionX == -1) ? rOrig.botLeft : rOrig.botRight;
            rayOrigin += Vector2.up * (horiRaySpace * i);
            RaycastHit2D hit = Physics2D.Raycast(rayOrigin, Vector2.right * directionX, rayLength, collisionMask);

            Debug.DrawRay(rayOrigin, Vector2.right * directionX, Color.red);

            if (hit)
            {
                moveAmount.x = (hit.distance - skinWidth) * directionX;
                rayLength = hit.distance;

                currentAngle = calculateBallAngle(hit.collider.transform.position, GetComponent<Transform>().position, hit.collider.bounds.size.y);

                collisions.left = directionX == -1;
                collisions.right = directionX == 1;
            }
        }
    }

    float calculateBallAngle(Vector2 pPos, Vector2 bPos, float cSizeY)
    {
        float angle = 2f * maxAngle * ((bPos.y - pPos.y) / cSizeY);

        if (angle > maxAngle)
        {
            return maxAngle;
        }

        return angle;
    }

    public void setMaxAngle(float angle)
    {
        maxAngle = angle;
    }

    public float getCurrentAngle()
    {
        return currentAngle;
    }

}
