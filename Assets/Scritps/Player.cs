using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    [SerializeField] float moveSpeed = 5.0f;
    [SerializeField] int Hp = 10;
    [SerializeField] GameObject HpBar;
    [SerializeField] Text scoreText;
    int score = 0;
    float scoreTime = 0;
    GameObject currentFloor;

    AudioSource deathSound;
    [SerializeField] GameObject replayButton;

    void Start()
    {
        deathSound = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.RightArrow))
        {
            transform.Translate(moveSpeed * Time.deltaTime, 0, 0);
            GetComponent<SpriteRenderer>().flipX = false;
            GetComponent<Animator>().SetBool("Run", true);
        }

        else if (Input.GetKey(KeyCode.LeftArrow))
        {
            transform.Translate(-moveSpeed * Time.deltaTime, 0, 0);
            GetComponent<SpriteRenderer>().flipX = true;
            GetComponent<Animator>().SetBool("Run", true);
        }
        else
        {
            GetComponent<Animator>().SetBool("Run", false);
        }

        UpdatScore();
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Normal")
        {
            if (other.contacts[0].normal == new Vector2(0, 1.0f))
            {
                Debug.Log("撞到第一種階梯");
                currentFloor = other.gameObject;
                ModifyHp(1);
                other.gameObject.GetComponent<AudioSource>().Play();
            }   
        }
        if (other.gameObject.tag == "Nails")
        {
            if (other.contacts[0].normal == new Vector2(0, 1.0f))
            {
                Debug.Log("撞到第二種階梯");
                currentFloor = other.gameObject;
                ModifyHp(-3);
                GetComponent<Animator>().SetTrigger("hurt");
                other.gameObject.GetComponent<AudioSource>().Play();
            }
        }
        if (other.gameObject.tag == "Ceiling")
        {
            Debug.Log("碰到天花板了");
            currentFloor.GetComponent<BoxCollider2D>().enabled = false;
            ModifyHp(-3);
            GetComponent<Animator>().SetTrigger("hurt");
            other.gameObject.GetComponent <AudioSource>().Play();
        }
        if (other.gameObject.tag == "Bounce")
        {
            if (other.contacts[0].normal == new Vector2(0, 1.0f))
            {
                Debug.Log("Touch Bounce");
                currentFloor = other.gameObject;

                // 彈跳效果
                Rigidbody2D rb = GetComponent<Rigidbody2D>();
                rb.velocity = new Vector2(rb.velocity.x, 10.0f); // 向上彈

                ModifyHp(1);
            }
        }

        if (other.gameObject.tag == "Void")
        {
            Debug.Log("Touch Void");

            // 可選：讓玩家直接掉下去
            currentFloor = null;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "DeathLine")
        {
            Debug.Log("你輸了");
            deathSound.Play();
            Time.timeScale = 0.0f;
            replayButton.SetActive(true);
        }
    }

    void ModifyHp(int num)
    {
        Hp += num;
        if (Hp > 10)
        {
            Hp = 10;
        }
        else if (Hp <= 0)
        {
            Hp = 0;
            deathSound.Play();
            Time.timeScale = 0.0f;
            replayButton.SetActive(true);
        }
        UpdateHpBar();
    }

    void UpdateHpBar()
    {
        for (int i = 0; i < HpBar.transform.childCount; i++)
        {
            if (Hp > i)
            {
                HpBar.transform.GetChild(i).gameObject.SetActive(true);
            }
            else
            {
                HpBar.transform.GetChild(i).gameObject.SetActive(false);
            }
        }
    }

    void UpdatScore()
    {
        scoreTime += Time.deltaTime;
        if (scoreTime > 2.0f)
        {
            score++;
            scoreTime = 0;
            scoreText.text = "地下" + score.ToString() + "層";
        }
    }

    public void Replay()
    {
        Time.timeScale = 1.0f;
        SceneManager.LoadScene("SampleScene");
    }
}
