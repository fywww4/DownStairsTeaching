using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] float moveSpeed = 5.0f;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.RightArrow)) transform.Translate(moveSpeed * Time.deltaTime,0 ,0);
        if (Input.GetKeyDown(KeyCode.LeftArrow)) transform.Translate(-moveSpeed * Time.deltaTime, 0, 0);
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.tag == "Floor1")
        {
            Debug.Log("撞到第一種階梯");
        }   
        if (other.gameObject.tag == "Floor2")
        {
            Debug.Log("撞到第二種階梯");
        }
        if (other.gameObject.tag == "DeathLine")
        {
            Debug.Log("你輸了");
        }
    }
}
