using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemManager : MonoBehaviour
{
    [Serializable]
    public class itemDrawerData
    {
        public GameObject itemPrefab;
        public RawImage itemIcon;
    }

    public itemDrawerData[] items;

    [field: SerializeField]
    private GameObject _itemDrawer;

    [field: SerializeField]
    private GameObject _activeItemObject;

    [field: SerializeField]
    private bool _hasActiveItem;

    // Update is called once per frame
    void Update()
    {
        if (!_hasActiveItem || _activeItemObject != null)
        {
            return;
        }

        foreach (var item in items)
        {
            item.itemIcon.color = Color.white;
        }

        _hasActiveItem = false;
    }

    public void ToggleItemDrawer()
    {
        _itemDrawer.SetActive(!_itemDrawer.activeInHierarchy);
    }

    public void ToggleItem(int index)
    {
        if (_activeItemObject == null)
        {
            SpawnItem(index);
        }
        else
        {
            Item activeItem = _activeItemObject.GetComponent<Item>();
            foreach (var item in items)
            {
                Item getItem = item.itemPrefab.GetComponent<Item>();
                if (activeItem.itemName == getItem.itemName)
                {
                    item.itemIcon.color = Color.white;
                }
            }

            string previousItem = activeItem.itemName;

            Destroy(_activeItemObject);
            _activeItemObject = null;
            _hasActiveItem = false;

            Item getTargetItem = items[index].itemPrefab.GetComponent<Item>();
            if (previousItem != getTargetItem.itemName)
            {
                SpawnItem(index);
            }
        }
    }

    private void SpawnItem(int index)
    {
        _activeItemObject = Instantiate(items[index].itemPrefab);

        items[index].itemIcon.color = Color.black;

        _hasActiveItem = true;
    }
}
