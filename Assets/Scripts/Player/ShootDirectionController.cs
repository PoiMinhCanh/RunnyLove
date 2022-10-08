using UnityEngine;

public class ShootDirectionController : MonoBehaviour
{
    [Header("Reference")]
    [SerializeField] private Transform startPoint;

    private LineRenderer lineRenderer;
    
    private float _radius = 3.6f;
    private int angle = 8; // from 8 to 90C

    public Vector2 direction;

    private void Awake()
    {
        lineRenderer = GetComponent<LineRenderer>();
        // init value
        // set start point
        lineRenderer.SetPosition(0, startPoint.localPosition);
        // set end point
        setEndPoint();
    }



    private void Update()
    {
        if (Input.GetKey(KeyCode.UpArrow))
        {
            angle = Mathf.Min(++angle, 90);
            setEndPoint();
        }

        if (Input.GetKey(KeyCode.DownArrow))
        {
            angle = Mathf.Max(--angle, 8);
            setEndPoint();
        }
    }

    public void setEndPoint()
    {
        // calculate point x of end point
        float posX = startPoint.localPosition.x + _radius * Mathf.Cos(Mathf.Deg2Rad * angle);
        // calculate point y of end point
        float posY = startPoint.localPosition.y + _radius * Mathf.Sin(Mathf.Deg2Rad * angle);
        lineRenderer.SetPosition(1, new Vector2(posX, posY));

        direction = (lineRenderer.GetPosition(1) - lineRenderer.GetPosition(0)).normalized;
    }

}
