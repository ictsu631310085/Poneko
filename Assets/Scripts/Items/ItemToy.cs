using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemToy : Item
{
    public float healthAmount;

    private Draggable _draggable;

    private Animator _animator;

    [field: SerializeField]
    private bool _isMoving;

    // Start is called before the first frame update
    void Start()
    {
        _draggable = GetComponent<Draggable>();
        _animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (_isMoving == _draggable.isMoving)
        {
            return;
        }
        
        _isMoving = _draggable.isMoving;
        _animator.SetBool("isMoving", _isMoving);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.TryGetComponent(out Cat cat))
        {
            return;
        }

        cat.target = gameObject;
    }

    void OnTriggerStay2D(Collider2D collision)
    {
        if (!collision.TryGetComponent(out Cat cat) || !_draggable.isMoving)
        {
            return;
        }

        cat.UpdateHealth(healthAmount * Time.deltaTime);
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (!collision.TryGetComponent(out Cat cat))
        {
            return;
        }

        cat.target = null;
    }
}
