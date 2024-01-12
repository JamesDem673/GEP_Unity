using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

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


    public Canvas itemTag;
    public TMP_Text thisSlotNamePopUp;
    public TMP_Text thisSlotDescPopUp;

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
            thisSlotQuantityText.text = heldItem.currentQuantity.ToString();
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
}

