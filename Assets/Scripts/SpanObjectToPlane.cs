using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation; 
using UnityEngine.XR.ARSubsystems; 

[RequireComponent(typeof(ARRaycastManager))]
// we'll be calling some functions from this class

public class SpanObjectToPlane : MonoBehaviour
{
// the first one is we'll store a reference to our raycast manager 
// I'll simply call this one raycast manager we'll also to keep track of any objects
// that we have spawned for this example we'll just going to be working with a single object 
// we'll improve on this and implement 
//
    private ARRaycastManager raycastManager; 

// we'll create a private game object called spawned object 
    private GameObject spawnedObject; 

// next we'll also a variable for the prefab that we'll be placing into the world
// this will be off type game object and I'll call this one place of all prefab
// and of course if u wanted to keep the code a little bit tidier or at least a littel bit safe
    [SerializeField]
    public GameObject PlaceablePrefab; 

    static List<ARRaycastHit> s_Hits = new List<ARRaycastHit>();

    private void Awake() 
    {
        raycastManager = GetComponent<ARRaycastManager>();

    }

    bool TryGetTouchPosition(out Vector2 touchPosition)
    {
        if(Input.touchCount > 0)
        {
            touchPosition = Input.GetTouch(0).position; 
            return true; 

        }

        touchPosition = default; 
        return false; 

    }

    private void Update()
    {
        if (!TryGetTouchPosition(out Vector2 touchPosition))
        {
            return; 
        }

        if (raycastManager.Raycast(touchPosition, s_Hits, TrackableType.PlaneWithinPolygon))
        {
            var hitPose = s_Hits[0].pose; 
            if (spawnedObject == null) 
            {
                spawnedObject = Instantiate(PlaceablePrefab, hitPose.position, hitPose.rotation);

            }
            else 
            {
                spawnedObject.transform.position = hitPose.position; 
                spawnedObject.transform.rotation = hitPose.rotation; 
            }

        }
    }




}

