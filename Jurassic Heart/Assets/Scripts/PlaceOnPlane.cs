using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

/// <summary>
/// Listens for touch events and performs an AR raycast from the screen touch point.
/// AR raycasts will only hit detected trackables like feature points and planes.
///
/// If a raycast hits a trackable, the <see cref="placedPrefab"/> is instantiated
/// and moved to the hit position.
/// </summary>
[RequireComponent(typeof(ARRaycastManager))]
public class PlaceOnPlane : MonoBehaviour
{
    [SerializeField]
    [Tooltip("Instantiates this prefab on a plane at the touch location.")]
    Ship m_PlacedPrefab;

    public ShipViewExploder shipViewExploder;
    
    /// <summary>
    /// The prefab to instantiate on touch.
    /// </summary>
    public Ship placedPrefab
    {
        get { return m_PlacedPrefab; }
        set { m_PlacedPrefab = value; }
    }

    /// <summary>
    /// The object instantiated as a result of a successful raycast intersection with a plane.
    /// </summary>
    public Ship spawnedObject { get; private set; }

    private GameObject spawnedObjectHolder;
    void Awake()
    {
        m_RaycastManager = GetComponent<ARRaycastManager>();
        spawnedObjectHolder = new GameObject();
    }

    bool TryGetTouchPosition(out Vector2 touchPosition)
    {
#if UNITY_EDITOR
        if (Input.GetMouseButton(0))
        {
            var mousePosition = Input.mousePosition;
            touchPosition = new Vector2(mousePosition.x, mousePosition.y);
            return true;
        }
        
#else
        if (Input.touchCount > 0 )
        {
            touchPosition = Input.GetTouch(0).position;
            return true;
        }
#endif

        touchPosition = default;
        return false;
    }

    private float lastHitTime = 0;
    void Update()
    {
        if (spawnedObject == null)
        {
            if (!TryGetTouchPosition(out Vector2 touchPosition))
                return;

            if (Time.time > lastHitTime + 1 &&
                m_RaycastManager.Raycast(touchPosition, s_Hits, TrackableType.PlaneWithinPolygon))
            {

                lastHitTime = Time.time;
                // Raycast hits are sorted by distance, so the first one
                // will be the closest hit.
                var hitPose = s_Hits[0].pose;

                if (spawnedObject == null)
                {
                    spawnedObject = Instantiate(m_PlacedPrefab, hitPose.position + Vector3.up/2, Quaternion.Euler(Vector3.up));
                    spawnedObject.transform.parent = spawnedObjectHolder.transform;
                    shipViewExploder.ship = spawnedObject;
                    shipViewExploder.Init();
                }
                else
                {
                    spawnedObject.transform.position = hitPose.position;
                    spawnedObject.transform.rotation = Quaternion.Euler(Vector3.up);
                }

                StartCoroutine(GenericCoroutines.DoAfterFrames(() => { }, 2));
            }
        }
    }

    public void RemoveSpawnedObject()
    {
        foreach (Transform transform1 in spawnedObjectHolder.transform)
        {
            Destroy(transform1.gameObject);
        }
        Destroy(spawnedObject.gameObject);
        //spawnedObject = null;
    }

    static List<ARRaycastHit> s_Hits = new List<ARRaycastHit>();

    ARRaycastManager m_RaycastManager;
}
