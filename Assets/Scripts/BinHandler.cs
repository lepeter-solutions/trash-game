using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BinHandler : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public static void binHandler(GameObject bin, GameObject currentItem)
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            Destroy(currentItem);   
        }
        
    }
}
