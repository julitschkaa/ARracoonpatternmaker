using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class SpawnableManager : MonoBehaviour
{
    [SerializeField]
    ARRaycastManager m_RaycastManager;
    List<ARRaycastHit> m_Hits = new List<ARRaycastHit>();

    [SerializeField]
    GameObject[] spawnablePrefabs;
    
    GameObject[] spawnedObjects;
    GameObject selectedObject;
    int iterator = 0;

    Camera arCam;

    [SerializeField]
    LayerMask mask;

    // Start is called before the first frame update
    void Start()
    {
        selectedObject = null;
        spawnedObjects = new GameObject[spawnablePrefabs.Length];
        arCam = GameObject.Find("AR Camera").GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount == 0)
            return;

        if (Input.GetTouch(0).phase == TouchPhase.Began)
        {
            Ray ray = arCam.ScreenPointToRay(Input.GetTouch(0).position);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, Mathf.Infinity, mask))
                selectedObject = (hit.transform.parent != null) ? hit.transform.parent.gameObject : hit.transform.gameObject;
            else
                selectedObject = null;
        }

        if (m_RaycastManager.Raycast(Input.GetTouch(0).position, m_Hits))
        {
            if (m_Hits[0].trackable is ARPlane plane)
            {
                if (Input.GetTouch(0).phase == TouchPhase.Began)
                {
                    if (selectedObject == null)
                        SpawnPrefab(m_Hits[0].pose.position);
                    else
                        UpdateSelectedObjectPosition(m_Hits[0].pose.position);
                }
                else if (Input.GetTouch(0).phase == TouchPhase.Moved)
                {
                    UpdateSelectedObjectPosition(m_Hits[0].pose.position);
                }
            }
        }
    }

    private void SpawnPrefab(Vector3 spawnPosition)
    {
        if (iterator < spawnablePrefabs.Length)
        {
            spawnedObjects[iterator] = Instantiate(spawnablePrefabs[iterator], spawnPosition, Quaternion.identity);
            iterator++;
        }
    }

    private void UpdateSelectedObjectPosition(Vector3 pos)
    {
        if (selectedObject != null)
            selectedObject.transform.position = pos;
    }
}