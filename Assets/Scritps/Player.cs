using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] float moveSpeed = 5.0f;

    GameObject currentFloor;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.RightArrow)) transform.Translate(moveSpeed * Time.deltaTime,0 ,0);
        if (Input.GetKeyDown(KeyCode.LeftArrow)) transform.Translate(-moveSpeed * Time.deltaTime, 0, 0);
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.tag == "Normal")
        {
            if (other.contacts[0].normal == new Vector2(0, 1.0f))
            {
                Debug.Log("撞到第一種階梯");
                currentFloor = other.gameObject;
            }
        }   
        if (other.gameObject.tag == "Nails")
        {
            if (other.contacts[0].normal == new Vector2(0, 1.0f))
            {
                Debug.Log("撞到第二種階梯");
                currentFloor = other.gameObject;
            }
        }
        if (other.gameObject.tag == "Ceiling")
        {
            Debug.Log("碰到天花板了");
            currentFloor.GetComponent<BoxCollider2D>().enabled = false;
        }
    }
}
