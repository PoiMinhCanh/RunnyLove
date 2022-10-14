using UnityEngine;

public class CameraController : MonoBehaviour
{
    [Header("Reference Component")]
    [SerializeField] private Camera _camera;
    [SerializeField] private BoxCollider2D confiner;

    // drag camera
    private Vector3 dragPointMouse;
    private Vector3 _originPositionCamera;
    // scroll map by mouse wheel
    private float _originZoomSize = 5f;
    private float _minZoomSize = 5f;
    private float _maxZoomSize;
    private float _zoomSizeStep = 5f;

    // confider boundary camera
    private float mapMinX, mapMaxX, mapMinY, mapMaxY;

    private void Awake()
    {
        // get position of main camera (The first enabled Camera component that is tagged "MainCamera")
        _originPositionCamera = _camera.transform.position;
        // get confiner boundary camera
        // horizontal
        mapMinX = confiner.transform.position.x + confiner.offset.x - confiner.bounds.size.x / 2f;
        mapMaxX = confiner.transform.position.x + confiner.offset.x + confiner.bounds.size.x / 2f;
        // vertical
        mapMinY = confiner.transform.position.y + confiner.offset.y - confiner.bounds.size.y / 2f;
        mapMaxY = confiner.transform.position.y + confiner.offset.y + confiner.bounds.size.y / 2f;

        // calculated max zoom size
        _maxZoomSize = confiner.bounds.size.y / 2f;

    }

    private void LateUpdate()
    {
        // pan camera 
        PanCamera();

        // scroll map by mouse wheel
        zoomCamera();
     
        // click right click
        // return camera
        if (Input.GetMouseButton(1))
        {
            _camera.transform.position = _originPositionCamera;
            _camera.orthographicSize = _originZoomSize;
        }
    
    }

    private void PanCamera()
    {
        // Do lay vi tri tuong doi theo camera nen vi tri van giu nguyen sau moi lan drag
        // dragPointMouse is the same every camera move (because the position calculated based on camera position)

        // click left mouse

        // save position of mouse in world space when drag starts (first time clicked)
        if (Input.GetMouseButtonDown(0))
        {
            dragPointMouse = _camera.ScreenToWorldPoint(Input.mousePosition);
        }

        // calculate distance between drag origin and new position if it is still held down
        if (Input.GetMouseButton(0))
        {
            Vector3 difference = dragPointMouse - _camera.ScreenToWorldPoint(Input.mousePosition);

            //Debug.Log("Origin " + dragPointMouse + " newPosition " + _camera.ScreenToWorldPoint(Input.mousePosition) + " = different " + difference);

            // move the camera by that distance
            //_camera.transform.position += difference;
            _camera.transform.position = ClampCamera(_camera.transform.position + difference);

        }
    }

    private void zoomCamera()
    {
        float newSize = _camera.orthographicSize - Input.GetAxis("Mouse ScrollWheel") * _zoomSizeStep;
        _camera.orthographicSize = Mathf.Clamp(newSize, _minZoomSize, _maxZoomSize);
        _camera.transform.position = ClampCamera(_camera.transform.position);
    }

    private Vector3 ClampCamera(Vector3 targetPosition)
    {
        // get width and height of camera
        float camHeight = _camera.orthographicSize;
        float camWidth = _camera.orthographicSize * _camera.aspect;
        // calculate minX and minY position of camera should be
        // horizontal
        float minX = mapMinX + camWidth;
        float maxX = mapMaxX - camWidth;
        // vertical
        float minY = mapMinY + camHeight;
        float maxY = mapMaxY - camHeight;

        return new Vector3(Mathf.Clamp(targetPosition.x, minX, maxX),
                           Mathf.Clamp(targetPosition.y, minY, maxY),
                           targetPosition.z);
    }
}
