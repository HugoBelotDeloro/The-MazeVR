using UnityEngine;

public class PlayerCursor : MonoBehaviour
{
    public GameObject LookingGameObject;
    void Update()
    {
        Vector3 PlayerPosition = transform.position;
        
        Ray InteractionRay = new Ray(PlayerPosition,transform.forward);
        
        RaycastHit InteractionRayHit;
        
        float InteractionRayLength = 2.0f;
        
        Vector3 InteractionRayEndPoint = transform.forward * InteractionRayLength+PlayerPosition;
        
        Debug.DrawLine(PlayerPosition,InteractionRayEndPoint);
        
        bool HitFound = Physics.Raycast(InteractionRay, out InteractionRayHit, InteractionRayLength);
        if (HitFound)
        {
            GameObject HitGameObject = InteractionRayHit.transform.gameObject;
            
            string HitFeedBack = HitGameObject.name;

            LookingGameObject = HitGameObject;
        }
        else
        {
            LookingGameObject = null;
        }
    }
}

