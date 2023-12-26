using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemSlot : MonoBehaviour
{
    

    [SerializeField] GameObject selection;
    [SerializeField] Image itemImage;
    [SerializeField] TextMeshProUGUI itemAmountText;
    public int slotIndex;
    private ItemData itemData;
    public ItemData ItemData
    {
        get
        {
            return itemData;
        }
        set
        {
            itemData = value;
            itemImage.sprite = itemData.Icon;
            ItemAmount = itemData.amount;
        }
    }
    
    public bool IsSelected
    {
        get 
        { 
            return selection.activeInHierarchy;
        }
        set 
        { 
            selection.SetActive(value);
        }
    }

    public int ItemAmount
    {
        get
        {
            return int.Parse(itemAmountText.text);
        }
        set
        {
            itemAmountText.text = value == 0 ? "" : value.ToString();
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
