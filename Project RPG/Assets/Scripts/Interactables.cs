using UnityEngine;

public class Interactables : MonoBehaviour
{
   public float radius = 3f;
   public bool isFocus = false;
   Transform player;
   bool hasInteracted = false;
   public Transform interactionTransform;
   public virtual void Interact(){

    Debug.Log("Interacting with " + transform.name);

   }
    void Update()
    {
        if (isFocus && !hasInteracted){


            float distance = Vector3.Distance(player.position, interactionTransform.position);
            if (distance <= radius){


                Interact();
                hasInteracted = true;
            }
        }
    }
   public void OnFocused(Transform playerTransform){

    isFocus =true;
    player = playerTransform;
    hasInteracted = false;
   }
    public void OnDefocused(){

        isFocus = false; 
        player = null;
        hasInteracted = false;

    }
    void OnDrawGizmos()
   {
    Gizmos.color = Color.yellow;
    Gizmos.DrawWireSphere(interactionTransform.position, radius);
   }


}
