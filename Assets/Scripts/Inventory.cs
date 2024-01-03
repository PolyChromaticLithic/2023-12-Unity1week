using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using DG.Tweening;
using System;
using TMPro;
using UnityEngine.UI;
using System.Linq;

public class Inventory : MonoBehaviour
{
    [SerializeField] private EventSystem eventSystem;
    [SerializeField] private ItemSlot[] itemSlots;
    [SerializeField] private TextMeshProUGUI nameText;
    [SerializeField] private TextMeshProUGUI descriptionText;
    [SerializeField] private Button useButton;
    [SerializeField] private Button putButton;
    private int selectionIndex = 0;
    [SerializeField] private RectTransform inventoryRectTransform;

    [NonSerialized] public List<ItemData> itemDatas = new();

    static public Inventory Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
    }
    

    public int SelectionIndex
    {
        get
        {
            return selectionIndex;
        }
        set
        {
            itemSlots[selectionIndex].IsSelected = false;
            selectionIndex = value;
            itemSlots[selectionIndex].IsSelected = true;
            nameText.text = itemDatas[selectionIndex].Name;
            descriptionText.text = itemDatas[selectionIndex].Description;
            useButton.interactable = itemDatas[selectionIndex].IsUsable;
            putButton.interactable = itemDatas[selectionIndex].IsDiscardable;
        }
    }

    public ItemData SelectionItemData
    {
        get
        {
            return itemDatas[selectionIndex];
        }
    }

    public void Open()
    {
        selectionIndex = 0;
        foreach (var item in itemSlots)
        {
            item.IsSelected = false;
        }
        nameText.text = "スロットを選択してください。";
        descriptionText.text = "";
        useButton.interactable = false;
        putButton.interactable = false;
        inventoryRectTransform.DOScaleX(1f, 0.2f);
        
    }

    public void Close()
    {
        itemSlots[selectionIndex].IsSelected = false;
        inventoryRectTransform.DOScaleX(0f, 0.2f);
        Player.canMove = true;
        Player.isInventoryOpening = false;
    }

    public void UpdateInventory()
    {
        itemDatas.Sort((a, b) =>
        {
            if (a.ID == 0)
            {
                return 1;
            }
            else if (b.ID == 0)
            {
                return -1;
            }
            else
            {
                return a.ID - b.ID;
            }
        });
        for (int i = 0; i < itemSlots.Length; i++)
        {
            itemSlots[i].ItemData = itemDatas[i];
        }
        Player.instance.SPEED = 1f - itemDatas.Where(x => x.ID != 0).Count() * 0.1f;
    }

    public void AddItem(int id, int amount)
    {
        ItemData itemData = itemDatas.Find(itemData => itemData.ID == id);
        if (itemData == null)
        {
            itemData = new ItemData(id, amount);
            itemDatas.Add(itemData);
        }
        else
        {
            itemData.amount += amount;
        }
        UpdateInventory();
    }

    public void RemoveItem(int id, int amount)
    {
        ItemData itemData = itemDatas.Find(itemData => itemData.ID == id);
        if (itemData == null)
        {
            return;
        }
        else
        {
            itemData.amount -= amount;
            if (itemData.amount <= 0)
            {
                itemDatas.Remove(itemData);
            }
        }
        UpdateInventory();
    }

    public void UseItem()
    {
        ItemData itemData = itemDatas[selectionIndex];
        if (itemData.IsUsable)
        {
            itemData.UseAction[itemDatas[selectionIndex].ID]();
            RemoveItem(itemData.ID, 1);
        }
    }

    void Start()
    {
        for (int i = 0; i < itemSlots.Length; i++)
        {
            itemDatas.Add(ItemData.Empty);
            itemSlots[i].ItemData = itemDatas[i];
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //GameObject型の変数を宣言　ボタンオブジェクトを入れる箱
    private GameObject button_ob;

    public void Select()
    {
        //押されたボタンのオブジェクトをイベントシステムのcurrentSelectedGameObject関数から取得　
        button_ob = eventSystem.currentSelectedGameObject;
        SelectionIndex = button_ob.GetComponent<ItemSlot>().slotIndex;
    }
}
