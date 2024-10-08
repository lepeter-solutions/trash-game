using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupScript : MonoBehaviour
{
    // Start is called before the first frame update

    public static GameObject currentItem;

    [SerializeField] private GameObject itemPivot;

    private static bool justPickedUp = false;


    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (currentItem != null)
        {
            goToHand();

            if (PlayerRaycast.SelectedBin == null)
            {
                checkUserEventsWhileHolding();
            }
            
            justPickedUp = false;

        }
    }

    void checkUserEventsWhileHolding()
    {
        if (Input.GetKeyDown(KeyCode.E) && !justPickedUp)
        {
            Debug.Log("Dropping item after pressing E");
            currentItem.AddComponent<Rigidbody>();
            currentItem = null;
        }
        else if (Input.GetMouseButtonDown(0))
        {
            ThrowCurrentItem();
        }
    }



    void goToHand()
    {
        Debug.Log("Currently holding an item: " + currentItem.name);
        currentItem.transform.position = itemPivot.transform.position;
        currentItem.transform.rotation = itemPivot.transform.rotation;
    }

    public static void ThrowCurrentItem()
    {
        if (currentItem != null)
        {
            // Add Rigidbody if it doesn't exist
            Rigidbody rb = currentItem.GetComponent<Rigidbody>();
            if (rb == null)
            {
                rb = currentItem.AddComponent<Rigidbody>();
            }

            // Calculate the throw direction based on the crosshair position
            Camera mainCamera = Camera.main;
            Ray ray = mainCamera.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));
            Vector3 throwDirection;

            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                throwDirection = (hit.point - mainCamera.transform.position).normalized;
            }
            else
            {
                throwDirection = mainCamera.transform.forward;
            }

            // Calculate the throw force
            Vector3 throwForce = throwDirection * 10f; // Adjust the force as needed

            // Apply force to throw the item
            rb.AddForce(throwForce, ForceMode.Impulse);

            // Clear the currentItem reference
            currentItem = null;
        }
    }

    public static void ItemPickup(GameObject pickedUpItem)
    {
        Debug.Log("Item picked up");
        if (currentItem == null)
        {
            justPickedUp = true;
            currentItem = pickedUpItem;
            //remove rigidbody
            Rigidbody rb = currentItem.GetComponent<Rigidbody>();
            if (rb != null)
            {
                Destroy(rb);
            }
            return;
        }


    }
}
