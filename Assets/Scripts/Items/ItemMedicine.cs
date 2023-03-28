using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemMedicine : Item
{
    public float healthAmount;

    public float usableHealthThreshold;

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.TryGetComponent(out Cat cat) || !cat.isSick || cat.CurrentHealth / cat.maxHealth > usableHealthThreshold)
        {
            return;
        }

        cat.isSick = false;
        cat.UpdateHealth(healthAmount);

        Destroy(gameObject);
    }
}
