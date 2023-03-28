using System.Collections;
using System.Collections.Generic;
using System.Data;
using Unity.VisualScripting;
using UnityEngine;

public class ScaleSpriteWithScreen : MonoBehaviour
{
    private ScaleSpriteWithScreen _instance;

    public enum ScaleWith
    {
        Width, Height
    }

    public ScaleWith scaleWith;

    public float scaleFactor;

    public bool destroyAfterStart;
    public bool destroyRenderer;

    private SpriteRenderer _spriteRenderer;

    private Sprite _sprite;

    // Start is called before the first frame update
    void Start()
    {
        _instance = this;

        Scale();

        if (destroyAfterStart)
        {
            Destroy(this);
        }
    }

    void OnDestroy()
    {
        if (destroyRenderer)
        {
            Destroy(_spriteRenderer);
        }
    }

    public void Scale()
    {
        if (!_spriteRenderer)
        {
            _spriteRenderer = GetComponent<SpriteRenderer>();
            _sprite = _spriteRenderer.sprite;
        }

        float newScale = GetNewScale(scaleWith);
        transform.localScale = new Vector3(newScale, newScale, 1f);
    }

    private float GetNewScale(ScaleWith scaleWith)
    {
        float newScale = 1f;

        switch (scaleWith)
        {
            case ScaleWith.Width:
                float textureWidthUnit = _sprite.texture.width / _sprite.pixelsPerUnit;
                newScale = Camera.main.orthographicSize / textureWidthUnit * scaleFactor;
                break;

            case ScaleWith.Height:
                float texureHeightUnit = _sprite.texture.height / _sprite.pixelsPerUnit;
                newScale = Camera.main.orthographicSize * 2f / texureHeightUnit * scaleFactor;
                break;
        }

        return newScale;
    }
}
