using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class SC_CursorTrail : MonoBehaviour
{
    public Color trailColor = new Color(1, 0, 0.38f);
    public float distanceFromCamera = 12;
    public float startWidth = 0.1f;
    public float endWidth = 0f;
    public float trailTime = 0.24f;

    Transform trailTransform;
    Camera thisCamera;
    public TrailRenderer trail;


    // Start is called before the first frame update
    void Start()
    {
        thisCamera = GetComponent<Camera>();

        GameObject trailObj = new GameObject("Mouse Trail");
        trailTransform = trailObj.transform;
        trail = trailObj.AddComponent<TrailRenderer>();
        trail.time = -1f;
        MoveTrailToCursor(Input.mousePosition);
        trail.time = trailTime;
        trail.startWidth = startWidth;
        trail.endWidth = endWidth;
        trail.numCapVertices = 2;
        trail.sharedMaterial = new Material(Shader.Find("Unlit/Color"));
        trail.sharedMaterial.color = trailColor;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButton(0)){
            trail.enabled = true;
            MoveTrailToCursor(Input.mousePosition);
        }
        else if (Input.GetMouseButtonUp(0))
        {
            trail.enabled = false;
        }

    }

    void MoveTrailToCursor(Vector3 screenPosition)
    {
        trailTransform.position = thisCamera.ScreenToWorldPoint(new Vector3(screenPosition.x, screenPosition.y, distanceFromCamera));

    }

    
}
