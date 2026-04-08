using UnityEngine;

public class MovingFloor : MonoBehaviour
{
    [SerializeField] float moveSpeed = 2.0f;
    [SerializeField] float range = 2.0f;

    float startX;
    int direction = 1;

    void Start()
    {
        startX = transform.position.x;
    }

    void Update()
    {
        transform.Translate(direction * moveSpeed * Time.deltaTime, 0, 0);

        if (Mathf.Abs(transform.position.x - startX) > range)
        {
            direction *= -1; // 反方向移動
        }
    }
}
