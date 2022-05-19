using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemHero : MonoBehaviour
{
    public int id = 0; //히어로 구분 ID

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Hero"))
        {
            if (collision.GetComponent<HeroMoveController>().isHead)
            {
                HeroManager.hm.AddHero(id);
                Destroy(gameObject);
            }
        }
    }

}
