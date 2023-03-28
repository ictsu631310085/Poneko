using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Draggable : MonoBehaviour
{
    [Header("Info")]
    [SerializeField]
    private Vector2 initialPosition;

    [SerializeField]
    private float deltaX, deltaY;

    public bool isPickable;
    public bool isPickUp;
    public bool isMoving;

    public GameObject validDrop;
    public Transform dropPlace;

    private Collider2D _collider;

    // Start is called before the first frame update
    void Start()
    {
        _collider = GetComponent<Collider2D>();

        initialPosition = transform.position;

        isPickable = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount > 0 && isPickable) //!locked
        {
            Touch touch = Input.GetTouch(0);
            Vector2 touchPos = Camera.main.ScreenToWorldPoint(touch.position);

            switch (touch.phase)
            {
                case TouchPhase.Began:
                    if (_collider == Physics2D.OverlapPoint(touchPos))
                    {
                        deltaX = touchPos.x - transform.position.x;
                        deltaY = touchPos.y - transform.position.y;

                        isPickUp = true;

                        SetOtherDraggablesPickable(false);
                    }
                    break;

                case TouchPhase.Moved:
                    if (_collider == Physics2D.OverlapPoint(touchPos))
                    {
                        transform.position = new Vector2(touchPos.x - deltaX, touchPos.y - deltaY);

                        isMoving = true;
                    }
                    break;

                case TouchPhase.Stationary:
                    if (_collider == Physics2D.OverlapPoint(touchPos))
                    {
                        isMoving = false;
                    }
                    break;

                case TouchPhase.Ended:
                    if (!isPickUp)
                    {
                        return;
                    }

                    if (!dropPlace)
                    {
                        transform.position = new Vector2(initialPosition.x, initialPosition.y);
                    }
                    else
                    {
                        transform.position = new Vector2(dropPlace.position.x, dropPlace.position.y);
                        //locked = true;
                    }

                    isPickUp = false;
                    isMoving = false;

                    SetOtherDraggablesPickable(true);
                    break;
            }
        }
    }

    private void SetOtherDraggablesPickable(bool value)
    {
        Draggable[] draggables = FindObjectsOfType<Draggable>();
        foreach (var item in draggables)
        {
            if (item != this)
            {
                item.isPickable = value;
            }
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        //if (locked)
        //return;

        if (isPickUp && collision.TryGetComponent(out DropPlace dropPlace))
        {
            validDrop = dropPlace.gameObject;
            this.dropPlace = dropPlace.dropPosition;
        }
    }

    void OnTriggerStay2D(Collider2D collision)
    {
        if (isPickUp && validDrop == null && collision.TryGetComponent(out DropPlace dropPlace))
        {
            validDrop = dropPlace.gameObject;
            this.dropPlace = dropPlace.dropPosition;
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out DropPlace dropPlace))
        {
            validDrop = null;
            this.dropPlace = null;
        }
    }

    public void ReturnToInitPosition()
    {
        transform.position = initialPosition;
    }
}
