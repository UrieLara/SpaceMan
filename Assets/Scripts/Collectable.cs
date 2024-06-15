using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CollectableType
{
    healthPotion,
    manaPotion,
    money
}

public class Collectable : MonoBehaviour
{
    public CollectableType type = CollectableType.money;

    SpriteRenderer sprite;
     CircleCollider2D itemCollider;

    GameObject player;

    //bool hasBeenCollected = false;

    public int value = 1;

    private void Start()
    {
        player = GameObject.Find("Player");
    }

    private void Awake()
    {
        sprite = GetComponent<SpriteRenderer>();
        itemCollider = GetComponent< CircleCollider2D>();
    }

    void Show()
    {
        sprite.enabled = true;
        itemCollider.enabled = true;
      //  hasBeenCollected = false;
    }

    void Collect()
    {
       // hasBeenCollected = true;
        Hide();

        switch (this.type)
        {
            case CollectableType.money:
                GameManager.sharedInstance.CollectObject(this);
                GetComponent<AudioSource>().Play();
                break;


            case CollectableType.healthPotion:
                player.GetComponent<PlayerController>().CollectHealth(this.value);
                GetComponent<AudioSource>().Play();
                break;


            case CollectableType.manaPotion:
                player.GetComponent<PlayerController>().CollectMana(this.value);
                GetComponent<AudioSource>().Play();
                break;
            
        }
    }

    void Hide()
    {
        sprite.enabled = false;
        itemCollider.enabled = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            Collect();
        }
    }
}
