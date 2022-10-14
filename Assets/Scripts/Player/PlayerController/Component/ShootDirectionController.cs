using UnityEngine;

public class ShootDirectionController : MonoBehaviour
{
    [Header("Reference")]
    [SerializeField] private Transform startPoint;

    private LineRenderer lineRenderer;
    
    private float _radius = 3.6f;

    private float _maxAngle = 90;
    private float _minAngle = 8;
    private float _incrementUnitAngle = 0.5f;

    private float angle = 8; // from _minAngle to maxAngle
    
    private float incrementUnitTime = 0.05f;
    private float incrementUnitTimeCounter = 0;

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
            incrementUnitTimeCounter -= Time.deltaTime;
            if (incrementUnitTimeCounter <= 0)
            {
                angle = Mathf.Min(angle + _incrementUnitAngle, _maxAngle);
                // reset increment unit time counter
                incrementUnitTimeCounter = incrementUnitTime;
                //Debug.Log(angle);
                setEndPoint();
            }
        }

        if (Input.GetKeyUp(KeyCode.UpArrow))
        {
            incrementUnitTimeCounter = 0;
        }

        if (Input.GetKey(KeyCode.DownArrow))
        {
            incrementUnitTimeCounter -= Time.deltaTime;
            if (incrementUnitTimeCounter <= 0)
            {
                angle = Mathf.Max(angle - _incrementUnitAngle, _minAngle);
                // reset increment unit time counter
                incrementUnitTimeCounter = incrementUnitTime;
                //Debug.Log(angle);
                setEndPoint();
            }
        }

        if (Input.GetKeyUp(KeyCode.DownArrow))
        {
            incrementUnitTimeCounter = 0;
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
