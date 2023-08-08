using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pawn : ThingWithComp
{
    float angle;
    float eulerAngles;
    public override void Tick()
    {
        base.Tick();
        Vector3 playerPos = Player.GetPlayer.playerObj.transform.position;
        DrawGiz.DrawLine(position,playerPos,Color.green);
        
        //float Distance = Vector2.Distance(position,playerPos);
        Vector2 dir = (playerPos-position);
        dir.Normalize();
        
        angle = Mathf.Atan2(dir.y,dir.x)*Mathf.Rad2Deg;
        
        

        float fovRadius = 3f; // Bán kính FOV
        float fovAngle = 90f; // Góc FOV
        int rayCount = 10; // Số lượng tia để vẽ FOV

        DrawGiz.DrawWireSphere(position,100,fovRadius,0.05f,Color.green);

        float halfFOV = fovAngle / 2f;
        Quaternion leftRayRotation = Quaternion.AngleAxis(-halfFOV, Vector3.forward);
        Quaternion rightRayRotation = Quaternion.AngleAxis(halfFOV, Vector3.forward);

        Vector3 startPoint = obj.transform.position;
        Vector3 leftRayDirection = leftRayRotation * obj.transform.right;
        Vector3 rightRayDirection = rightRayRotation * obj.transform.right;

        DrawGiz.DrawLine(position,obj.transform.position+leftRayDirection * fovRadius,Color.green);
        DrawGiz.DrawLine(position,obj.transform.position+rightRayDirection * fovRadius,Color.green);

        float angleIncrement = fovAngle / rayCount;
        float currentAngle = -halfFOV;

        for (int i = 0; i < rayCount; i++)
        {
            Quaternion rayRotation = Quaternion.AngleAxis(currentAngle, Vector3.forward);
            Vector3 rayDirection = rayRotation * obj.transform.right;
            
            DrawGiz.DrawLine(startPoint,startPoint+rayDirection * fovRadius,Color.green);
            currentAngle += angleIncrement;
        }

        position = Vector2.MoveTowards(position,playerPos,1*Time.deltaTime);
        
    }
    public override void DrawThing(Vector3 pos)
    {
        base.DrawThing(pos);
        obj.transform.position = this.position;
        obj.transform.rotation = Quaternion.Euler(Vector3.forward*angle);
    }
}
