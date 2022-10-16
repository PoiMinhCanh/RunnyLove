using UnityEngine;

public class CameraController : MonoBehaviour
{
    [Header("Reference Component")]
    [SerializeField] private Camera _camera;
    [SerializeField] private BoxCollider2D confiner;
    [SerializeField] private Transform playerTransform;

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

    // speed camera move
    [Header("Set property")]
    [SerializeField] private float speed;

    private void Awake()
    {
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

    private void Start()
    {
        // get position of main camera (The first enabled Camera component that is tagged "MainCamera")
        _originPositionCamera = playerTransform.position;
        _originPositionCamera.z = -10;
        _originPositionCamera = ClampCamera(_originPositionCamera);
    }

    private void LateUpdate()
    {
        // pan camera 
        PanCamera();

        // scroll map by mouse wheel
        zoomCamera();

        // update boundary camera
        _camera.transform.position = ClampCamera(_camera.transform.position);

        // click right click
        // return camera
        if (Input.GetMouseButton(1))
        {
            resetCamera();
        }
    }

    public void resetCamera()
    {
        _camera.transform.position = _originPositionCamera;
        _camera.orthographicSize = _originZoomSize;
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
        if (Input.GetAxis("Mouse ScrollWheel") != 0)
        {
            float newSize = _camera.orthographicSize - Input.GetAxis("Mouse ScrollWheel") * _zoomSizeStep;
            _camera.orthographicSize = Mathf.Clamp(newSize, _minZoomSize, _maxZoomSize);
            _camera.transform.position = ClampCamera(_camera.transform.position);
        }
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

    // move to new position
    // return true if done, else return false
    public bool moveToPosition(Vector3 position)
    {
        // set boundary camera
        if (Vector3.Distance(transform.position, ClampCamera(position)) < 0.1f)
        {
            return true;
        }

        // check done or not
        if (Vector3.Distance(transform.position, position) > 0.1f)
        {
            transform.position = Vector3.MoveTowards(transform.position, position, speed * Time.deltaTime);
            return false;
        }
        // return holder
        return true;
    }

}
