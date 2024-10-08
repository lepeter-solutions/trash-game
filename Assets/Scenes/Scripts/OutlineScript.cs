using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRaycast : MonoBehaviour
{
    public Camera playerCamera;
    public float raycastDistance = 100f;
    public Color highlightColor = Color.magenta;
    public float outlineWidth = 7.0f;

    private Transform highlightedObj;
    private Transform selection;
    public RaycastHit raycastHit;

    private RaycastHit previousRaycastHit;

    void Update()
    {
        ResetHighlight();
        PerformRaycast();
    }

    void ResetHighlight()
    {
        if (highlightedObj != null && highlightedObj != selection)
        {
            Outline outline = highlightedObj.gameObject.GetComponent<Outline>();
            if (outline != null)
            {
                Debug.Log("Disabling outline for previously highlighted object: " + highlightedObj.name);
                outline.enabled = false;
            }
            highlightedObj = null;
        }
    }

    void PerformRaycast()
    {
        Ray ray = new Ray(playerCamera.transform.position, playerCamera.transform.forward);
        if (Physics.Raycast(ray, out raycastHit, raycastDistance))
        {
            
            Debug.Log("Raycast hit object: " + raycastHit.transform.name);
            Transform hitTransform = raycastHit.transform;

            if (!(previousRaycastHit.transform == hitTransform) )
            {
                Debug.Log("Raycast different object: " + hitTransform.name);
                DifferentObjectHit(hitTransform);
            }

            previousRaycastHit = raycastHit;

            if ((hitTransform.CompareTag("Trash") || hitTransform.CompareTag("Bin")) && hitTransform != selection)
            {
                Debug.Log("Highlighting object: " + hitTransform.name);
                highlightedObj = hitTransform;
                HighlightObject(highlightedObj);
            }
        }
        else{
            DifferentObjectHit(null);
        }
    }

    void HighlightObject(Transform obj)
    {
        Outline outline = obj.gameObject.GetComponent<Outline>();
        if (outline == null)
        {
            outline = obj.gameObject.AddComponent<Outline>();
            outline.OutlineColor = highlightColor;
            outline.OutlineWidth = outlineWidth;
        }
        Debug.Log("Enabling outline for object: " + obj.name);
        outline.enabled = true;

        if (obj.CompareTag("Trash"))
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                PickupScript.ItemPickup(obj.gameObject);

            }
        }
        else if (obj.CompareTag("Bin"))
        {
            // bin script
            TrashCanController.LidController(obj.gameObject);
        
        }
    }

    void DifferentObjectHit(Transform hitTransform)
    {
        Debug.Log("Different object hit detected");
        TrashCanController.CloseCurrentLid();
    }
}