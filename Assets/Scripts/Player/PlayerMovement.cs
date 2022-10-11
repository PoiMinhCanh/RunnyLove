using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Properties")]
    [SerializeField] private float speed;

    private void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");

        // Flip player when moving left abd right
        if (horizontalInput > 0.01f)
        {
            // Copy into a variable. We can now make per-value changes.
            var newScale = transform.localScale;
            newScale.x = Mathf.Abs(newScale.x);

            // Copy back into the transform.
            transform.localScale = newScale;
        }
        else if (horizontalInput < -0.01f)
        {
            // Copy into a variable. We can now make per-value changes.
            var newScale = transform.localScale;
            newScale.x = -Mathf.Abs(newScale.x);

            // Copy back into the transform.
            transform.localScale = newScale;
        }
    }

}
