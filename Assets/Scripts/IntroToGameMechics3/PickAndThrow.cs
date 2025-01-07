using UnityEngine;
using System.Collections;
using TMPro;

public class PickAndThrow : MonoBehaviour
{
    public float pickupRange = 9f;
    public float ThrowSpeed = 900f;
    public float GrabSpeed = 8f;
    public Transform camPos;
    public GameObject UIPickup;
    public TextMeshProUGUI pickAndDropText;
    public Transform holdPoint;
    private GameObject heldObject;

    private bool isHolding = false;  
    private Coroutine moveCoroutine;

    private void Update()
    {
        if (heldObject != null)
        {
            pickAndDropText.text = "Press E to Throw";
            if (Input.GetKeyDown(KeyCode.E))
            {
                ThrowObject();
            }

            if (isHolding)
            {
                MoveObjectToHoldPoint();
            }
        }
        else
        {
            pickAndDropText.text = "Press E to Grab";
            if (Input.GetKeyDown(KeyCode.E))
            {
                GrabObject();
            }
        }

        RaycastHit hit;
        if (Physics.Raycast(camPos.position, camPos.forward, out hit, pickupRange))
        {
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

    private void GrabObject()
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

                    isHolding = true;
                    if (moveCoroutine != null) 
                    {
                        StopCoroutine(moveCoroutine);
                    }
                    moveCoroutine = StartCoroutine(MoveToHoldPoint());
                }
            }
        }
    }

    private IEnumerator MoveToHoldPoint()
    {
        while (Vector3.Distance(heldObject.transform.position, holdPoint.position) > 0.05f)  
        {
            heldObject.transform.position = Vector3.MoveTowards(heldObject.transform.position, holdPoint.position, GrabSpeed * Time.deltaTime);
            yield return null;
        }
        heldObject.transform.position = holdPoint.position; 
    }

    private void MoveObjectToHoldPoint()
    {
        if (heldObject != null)
        {
            heldObject.transform.position = Vector3.MoveTowards(heldObject.transform.position, holdPoint.position, GrabSpeed * Time.deltaTime);
        }
    }

    private void ThrowObject()
    {
        if (heldObject != null)
        {
            Rigidbody rb = heldObject.GetComponent<Rigidbody>();
            rb.isKinematic = false;  
            heldObject.transform.SetParent(null);  
            rb.AddForce(camPos.forward * ThrowSpeed, ForceMode.Force);
            heldObject = null;
            isHolding = false; 
        }
    }
}
