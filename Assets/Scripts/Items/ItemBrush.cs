using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemBrush : Item
{
    public float healthAmount;

    private Draggable _draggable;

    // Start is called before the first frame update
    void Start()
    {
        _draggable = GetComponent<Draggable>();
    }

    void OnTriggerStay2D(Collider2D collision)
    {
        if (!collision.TryGetComponent(out Cat cat) || !_draggable.isMoving)
        {
            return;
        }
        cat.UpdateHealth(healthAmount * Time.deltaTime);
    }
}
