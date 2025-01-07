using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
   public Transform playerPos;
   public float moveSpeed = 3f;

    // Update is called once per frame
    void Update()
    {
        gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, playerPos.position, moveSpeed * Time.deltaTime);
    }
}
