using UnityEngine;
using TMPro;

public class PickUpUIChange : MonoBehaviour
{
    public float pickupRange = 2f;  
    public Transform camPos;
    public GameObject UIPickup;   
    public TextMeshProUGUI pickAndDropText;
    public Transform holdPoint;   
    private GameObject heldObject; 

    private void Update()
    {

        if (heldObject != null) 
        {
            pickAndDropText.text = "Press E to Drop";            
            if (Input.GetKeyDown(KeyCode.E)) 
            {
                DropObject();
            }
        }
        else
        {
            pickAndDropText.text = "Press E to Pick Up";            
            if (Input.GetKeyDown(KeyCode.E))
            {
                PickupObject();
            }
        }

        RaycastHit hit;
        if (Physics.Raycast(camPos.position, camPos.forward, out hit, pickupRange))
        {
            // Show the pickup UI if the object is interactable
            if (hit.collider.gameObject.CompareTag("Interactable"))
            {
                UIPickup.SetActive(true);
            }
            else
            {
                UIPickup.SetActive(false); 
            }
        }
        else
        {
            UIPickup.SetActive(false); 
        }
    }

    private void PickupObject()
    {
        RaycastHit hit;

        if (Physics.Raycast(camPos.position, camPos.forward, out hit, pickupRange))
        {
            if (hit.collider.gameObject.CompareTag("Interactable"))
            {

                if (hit.collider.gameObject.GetComponent<Rigidbody>())
                {

                    heldObject = hit.collider.gameObject;
                    Rigidbody rb = heldObject.GetComponent<Rigidbody>();

                    rb.isKinematic = true; 
                    heldObject.transform.SetParent(holdPoint); 
                    heldObject.transform.localPosition = Vector3.zero; 
                }
            }
        }
    }

    private void DropObject()
    {
        if (heldObject != null)
        {
            Rigidbody rb = heldObject.GetComponent<Rigidbody>();
            rb.isKinematic = false; 
            heldObject.transform.SetParent(null); 
            heldObject = null; 
        }
    }
}
