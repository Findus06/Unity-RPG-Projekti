using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerMotor))]
public class PlayerController : MonoBehaviour
{
    public Interactables focus;
    public LayerMask movementMask;
    Camera cam;
    PlayerMotor motor;

    // Start is called before the first frame update
    void Start()
    {
       cam = Camera.main; 
       motor = GetComponent<PlayerMotor>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 100, movementMask))
            {
                Debug.Log("We hit " + hit.collider.name + " " + hit.point);
                // Move our player to what we hit
                motor.MoveToPoint(hit.point);
                // Stop focusing any objects
                removeFocus();
            }

        }

        if (Input.GetMouseButtonDown(1))
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 100))
            {
                // Check if we hit an interactable
                Interactables interactable = hit.collider.GetComponent<Interactables>();
                Debug.Log("We focus on " + hit.collider.name + " " + hit.point);
              
                // If we did set it as our focus
                  if (interactable != null){
                    
                    SetFocus(interactable);

                }
            }


        }
    }
    void SetFocus(Interactables newFocus){
        if (newFocus != focus){
            if (focus != null){
                focus.OnDefocused();

            }
            
            focus = newFocus;
            motor.FollowTarget(newFocus);

        }
        
        newFocus.OnFocused(transform);

    }
    void removeFocus(){
        if (focus != null) {
            focus.OnDefocused();
        }
        
        focus = null;
        motor.StopFollowingTarget();
    }


}
