using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReformLogic : MonoBehaviour
{
    [SerializeField] private Collider2D circleCollider;
    [SerializeField] private SpriteRenderer cellShipSprite;
    [SerializeField] private GameObject playerObject;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject == playerObject)
        {
            if (circleCollider != null)
            {
                circleCollider.enabled = true;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject == playerObject)
        {
            if (circleCollider != null)
            {
                circleCollider.enabled = false;
            }
        }
    }
}
