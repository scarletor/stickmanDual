using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class AttackBox : MonoBehaviour
{

    PlayerMoveController control;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "@hitBoxCol")
        {
            collision.gameObject.transform.parent.GetComponent<PlayerMoveController>().GetHit();
            transform.parent.DOPause();
        }
    }


    private void Start()
    {
        control = gameObject.transform.parent.GetComponent<PlayerMoveController>();
    }
}
