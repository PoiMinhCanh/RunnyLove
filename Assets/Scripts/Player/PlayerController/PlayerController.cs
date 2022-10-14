using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private List<GameObject> gameObjs;

    // Script
    public ShootDirectionController ShootDirectionController;
    public PlayerAttack PlayerAttack;
    public PlayerMovement PlayerMovement;
    // Components
    public LineRenderer lineRenderer;


    private void Awake()
    {
        ShootDirectionController = GetComponent<ShootDirectionController>();
        PlayerAttack = GetComponent<PlayerAttack>();
        PlayerMovement = GetComponent<PlayerMovement>();
        lineRenderer = GetComponent<LineRenderer>();
    }

    public void toggleComponents(bool flag)
    {
        ShootDirectionController.enabled = flag;
        PlayerAttack.enabled = flag;
        PlayerMovement.enabled = flag;
        lineRenderer.enabled = flag;

        gameObjs.ForEach(gameObj =>
        {
            gameObj.SetActive(flag);
        });
    }

}
