using System.Collections.Generic;
using UnityEngine;
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
public class PlaceBonesInWorld : MonoBehaviour {
	[SerializeField]
	[Tooltip("Instantiates the origin on a plane at the touch location.")]
	GameObject m_PlacedOriginPrefab;

	private bool isOriginSet = false;
	List<ARRaycastHit> s_Hits = new List<ARRaycastHit>();
	ARRaycastManager m_RaycastManager;

	/// <summary>
	/// The prefab to instantiate on touch.
	/// </summary>
	public GameObject placedPrefab {
		get { return m_PlacedOriginPrefab; }
		set { m_PlacedOriginPrefab = value; }
	}

	/// <summary>
	/// The object instantiated as a result of a successful raycast intersection with a plane.
	/// </summary>
	public GameObject spawnedObject { get; private set; }

	void Awake() {
		m_RaycastManager = GetComponent<ARRaycastManager>();
	}

	bool TryGetTouchPosition(out Vector2 touchPosition) {
#if UNITY_EDITOR
		if (Input.GetMouseButton(0)) {
			var mousePosition = Input.mousePosition;
			touchPosition = new Vector2(mousePosition.x, mousePosition.y);
			return true;
		}
#else
        if (Input.touchCount > 0)
        {
            touchPosition = Input.GetTouch(0).position;
            return true;
        }
#endif

		touchPosition = default;
		return false;
	}

	void Update() {
		if (!TryGetTouchPosition(out Vector2 touchPosition))
			return;

		if (!isOriginSet) {
			if (m_RaycastManager.Raycast(touchPosition, s_Hits, TrackableType.PlaneWithinPolygon)) {
				// Raycast hits are sorted by distance, so the first one
				// will be the closest hit.
				var hitPose = s_Hits[0].pose;

				if (spawnedObject == null) {
					spawnedObject = Instantiate(m_PlacedOriginPrefab, hitPose.position, hitPose.rotation);
					isOriginSet = true;
				} else {
					spawnedObject.transform.position = hitPose.position;
				}
			}
			
		} else {
			foreach (Touch touch in Input.touches) {
				if (touch.phase == TouchPhase.Began) {
					// Construct a ray from the current touch coordinates
					Ray ray = Camera.main.ScreenPointToRay(touch.position);
					RaycastHit hit;
					if (Physics.Raycast(ray, out hit, Mathf.Infinity)) {
						if (hit.transform.gameObject.tag == "Dirt") {
							Debug.Log("Dug Dirt");
							hit.transform.GetComponent<DirtRemove>().RemoveDirt();
						}

					}
				}
			}
		}
	}



	
}