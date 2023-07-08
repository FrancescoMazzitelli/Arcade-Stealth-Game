using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShopManager : MonoBehaviour
{
    public int points;
    public TMP_Text pointsUI;
    public ShopItem[] shopItems;
    private List<GameObject> shopPanelsGO;
    public ShopTemplate[] shopPanels;
    public Button[] buttons;

    private GameObject contents;

    // Start is called before the first frame update
    void Start()
    {
        shopPanelsGO = new List<GameObject>();

        string folderPath = "Assets/GUI/ShopScripts/ScriptableObjects";
        ShopItem[] objects = Resources.LoadAll<ShopItem>(folderPath);
        foreach(ShopItem item in objects)
        {
            Debug.Log(item);
        }

        contents = GameObject.Find("Contents");
        Transform parentTransform = contents.transform;
        for (int i = 0; i < parentTransform.childCount; i++)
        {
            Transform childTransform = parentTransform.GetChild(i);
            GameObject childObject = childTransform.gameObject;
            shopPanelsGO.Add(childObject);
        }

        for(int i = 0; i<shopItems.Length; i++)
        {
            shopPanelsGO[i].SetActive(true);
        }
        pointsUI.text = "Points: " + points.ToString();
        LoadPanels();
        CheckPurchaseable();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddPoints()
    {
        points+=50;
        pointsUI.text = "Points: " + points.ToString();
        CheckPurchaseable();
    }

    public void CheckPurchaseable()
    {
        for(int i=0; i<shopItems.Length; i++)
        {
            if(points >= shopItems[i].price)
                buttons[i].interactable = true;
            else
                buttons[i].interactable = false;
        }
    }

    public void LoadPanels()
    {
        for(int i=0; i < shopItems.Length; i++)
        {
            shopPanels[i].title.text = shopItems[i].title;
            shopPanels[i].description.text = shopItems[i].description;
            shopPanels[i].price.text = shopItems[i].price.ToString();
        }
    }

    public void PurchaseItem(int btnNum)
    {
        if(points  >= shopItems[btnNum].price)
        {
            points = points - shopItems[btnNum].price;
            pointsUI.text = "Points: " + points.ToString();
            CheckPurchaseable();
        }
    }
}
