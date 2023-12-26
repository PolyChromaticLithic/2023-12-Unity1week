using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using DG.Tweening;
using System;
using TMPro;

public class Inventory : MonoBehaviour
{
    [SerializeField] private EventSystem eventSystem;
    [SerializeField] private ItemSlot[] itemSlots;
    [SerializeField] private TextMeshProUGUI nameText;
    [SerializeField] private TextMeshProUGUI descriptionText;
    private int selectionIndex = 0;
    [SerializeField] private RectTransform inventoryRectTransform;

    [NonSerialized] public ItemData[] itemDatas = new ItemData[8];

    

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
        nameText.text = "�X���b�g��I�����Ă��������B";
        descriptionText.text = "";
        inventoryRectTransform.DOScaleX(1f, 0.2f);
        
    }

    public void Close()
    {
        itemSlots[selectionIndex].IsSelected = false;
        inventoryRectTransform.DOScaleX(0f, 0.2f);
        Player.canMove = true;
        Player.isInventoryOpening = false;
    }

    void Start()
    {
        for (int i = 0; i < itemDatas.Length; i++)
        {
            itemDatas[i] = ItemData.Empty;
            itemSlots[i].ItemData = itemDatas[i];
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //GameObject�^�̕ϐ���錾�@�{�^���I�u�W�F�N�g�����锠
    private GameObject button_ob;

    public void Select()
    {
        //�����ꂽ�{�^���̃I�u�W�F�N�g���C�x���g�V�X�e����currentSelectedGameObject�֐�����擾�@
        button_ob = eventSystem.currentSelectedGameObject;
        SelectionIndex = button_ob.GetComponent<ItemSlot>().slotIndex;
    }
}
