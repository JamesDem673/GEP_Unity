using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;
using Palmmedia.ReportGenerator.Core.Common;

public class Slot : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public bool hovered;
    private Item heldItem;

    private Color opaque = new Color(1, 1, 1, 1);
    private Color transparent = new Color(1, 1, 1, 0);


    private Image thisSlotImage;
    public TMP_Text thisSlotQuantityText;
    private string thisSlotText = "";
    private string thisSlotDesc = "";

    int quantity;

    public Canvas itemTag;
    public TMP_Text thisSlotNamePopUp;
    public TMP_Text thisSlotDescPopUp;

    private GameObject ItemPrefab;
    public GameObject player;



    public void initialiseSlot()
    {
        thisSlotImage = gameObject.GetComponent<Image>();
        thisSlotQuantityText = transform.GetChild(0).GetComponent<TMP_Text>();
        thisSlotImage.sprite = null;
        thisSlotImage.color = transparent;
        setItem(null);
    }

    public void setItem(Item item)
    {
        heldItem = item;

        if (item != null)
        {
            thisSlotImage.sprite = heldItem.icon;
            thisSlotImage.color = opaque;
            thisSlotText = heldItem.name;
            thisSlotDesc = heldItem.description;
            ItemPrefab = heldItem.prefab;

            updateData();
        }
        else
        {
            thisSlotImage.sprite = null;
            thisSlotImage.color = transparent;
            updateData();
        }
    }

    public Item getItem()
    {
        return heldItem;
    }

    public void updateData()
    {
        if (heldItem != null)
        {
            thisSlotQuantityText.text = heldItem.currentQuantity.ToString();
            quantity = heldItem.currentQuantity;
        }
        else
            thisSlotQuantityText.text = "";
    }

    public void OnPointerEnter(PointerEventData pointerEventData)
    {
        hovered = true;

        if (thisSlotText != "")
        {
            itemTag.gameObject.SetActive(true);

            thisSlotNamePopUp.text = thisSlotText.ToString();
            thisSlotDescPopUp.text = thisSlotDesc.ToString();
        }
    }

    public void OnPointerExit(PointerEventData pointerEventData)
    {
        hovered = false;

        itemTag.gameObject.SetActive(false);

        thisSlotNamePopUp.text = "";
        thisSlotDescPopUp.text = "";
    }

    public bool returnHovered()
    {
        return hovered;
    }

    public void Drop(bool all)
    {
        if (all)
        {
            thisSlotQuantityText.text = "0";

            Vector3 playerPos = new Vector3(player.transform.position.x, player.transform.position.y, player.transform.position.z);

            for (int i = 0; i < quantity; i++)
            {
                GameObject newitem = Instantiate(ItemPrefab, player.transform.position + new Vector3(2, 1 + i / 2, 0), Quaternion.identity);
                newitem.GetComponent<Item>().resetQuantity();
                newitem.SetActive(true);
                newitem.AddComponent<Rigidbody>();
            }

            quantity = 0;
        }
        else
        {
            quantity -= 1;

            GameObject newitem = Instantiate(ItemPrefab, player.transform.position + new Vector3(0 + 1, 1, 0), Quaternion.identity);
            newitem.GetComponent<Item>().resetQuantity();
            newitem.SetActive(true);
            newitem.AddComponent<Rigidbody>();

            thisSlotQuantityText.text = quantity.ToString();
            
        }

        if(quantity == 0)
        {
            thisSlotImage = gameObject.GetComponent<Image>();
            thisSlotQuantityText = transform.GetChild(0).GetComponent<TMP_Text>();
            thisSlotImage.sprite = null;
            thisSlotImage.color = transparent;
            setItem(null);
        }
    }
}

