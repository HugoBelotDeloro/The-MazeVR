using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCursor : MonoBehaviour
{
    void Update()
    {
        Vector3 PlayerPosition = transform.position;
        Vector3 FDirection = transform.forward+PlayerPosition;
        
        Ray InteractionRay = new Ray(PlayerPosition,transform.forward);
        
        RaycastHit InteractionRayHit;
        
        float InteractionRayLength = 5.0f;
        
        Vector3 InteractionRayEndPoint = transform.forward * InteractionRayLength+PlayerPosition;
        
        Debug.DrawLine(PlayerPosition,InteractionRayEndPoint);
        
        bool HitFound = Physics.Raycast(InteractionRay, out InteractionRayHit, InteractionRayLength);
        if (HitFound)
        {
            GameObject HitGameObject = InteractionRayHit.transform.gameObject;
            
            string HitFeedBack = HitGameObject.name;
            
            
        }
    }
}
