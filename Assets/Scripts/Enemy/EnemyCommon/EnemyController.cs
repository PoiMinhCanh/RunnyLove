using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public EnemyType type;

    public void setType(EnemyType type)
    {
        this.type = type;
    }

}
