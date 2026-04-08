using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Floor : MonoBehaviour
{
    [SerializeField] float moveSpeed = 2.0f;

    void Update()
    {
        transform.Translate(0, moveSpeed * Time.deltaTime, 0);
        if (transform.position.y > 6)
        {
            Destroy(gameObject);
            transform.parent.GetComponent<FloorManager>().SpawnFloor();
        }
    }
}
