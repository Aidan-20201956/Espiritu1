using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class InventorySystem : MonoBehaviour
{
    List<Item> playerInventory; 
    int currentInventoryIndex = 0; 
    bool isVisible = false; 
    GameObject inventoryText, inventoryImage, inventoryDescription, inventoryPanel;

    // Start is called before the first frame update
    void Start()
    {
        inventoryText = GameObject.Find("inventoryText");
        inventoryImage = GameObject.Find("inventoryImage");
        inventoryDescription = GameObject.Find("inventoryDescription");
        inventoryPanel = GameObject.Find("inventoryPanel");
        DisplayUI(false);
        playerInventory = new List<Item>();
        playerInventory.Add(new Item(Item.ItemType.MEAT));
        playerInventory.Add(new Item(Item.ItemType.GOLD));
        playerInventory[1].nb = 300;

        checkInventory();

    }

    void checkInventory()
    {
        for (int i = 0;
        i < playerInventory.Count; i++)
        {
            print(playerInventory[i].ItemInfo());
        }
    }

    void DisplayUI(bool toggle)
    {
        inventoryText.SetActive(toggle);
        inventoryPanel.SetActive(toggle);
        inventoryImage.SetActive(toggle);
        inventoryDescription.SetActive(toggle);
    }


    // Update is called once per frame
    void Update()
    {
        if (isVisible)
        {
            DisplayUI(true);
            Item currentItem = playerInventory[currentInventoryIndex];
            GameObject.Find("inventoryText").GetComponent<Text>().text = currentItem.name + "[" + currentItem.nb + "]"; 
            GameObject.Find("inventoryDescription").GetComponent<Text>().text = currentItem.description + "\n\n Press [U] to Select"; 
            GameObject.Find("inventoryImage").GetComponent<RawImage>().texture = currentItem.GetTexture();
            if (Input.GetKeyDown(KeyCode.I)) currentInventoryIndex++;
            if (currentInventoryIndex >= playerInventory.Count)
            {
                currentInventoryIndex = 0; isVisible = false; DisplayUI(false);
            }


        }
        else
        {
            if (Input.GetKeyDown(KeyCode.I)) isVisible = true;
        }


    }
}
