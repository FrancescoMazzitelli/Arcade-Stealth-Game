using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEngine.EventSystems;


public class ShopManager : MonoBehaviour
{
    public int points;
    public int gems;
    public TMP_Text pointsUI;
    public TMP_Text gemsUI;
    private List<ShopItem> shopItems;
    private List<GameObject> shopPanelsGO;
    private List<ShopTemplate> shopPanels;
    private List<Button> buttons;

    private GameObject contents;

    // Start is called before the first frame update
    void Start()
    {
        shopPanelsGO = new List<GameObject>();
        shopPanels = new List<ShopTemplate> ();
        shopItems = new List<ShopItem>();
        buttons = new List<Button>();

        //----------------------------------------------------------------------------------------------------------------//

        /* Recupero gli ScriptableObjects (I tab all'interno del quale vengono compilate tutte le informazioni
        sull'oggetto da acquistare) */
        string path = "Assets/GUI/ShopScripts/ScriptableObjects";
        DirectoryInfo dir = new DirectoryInfo(path);
        FileInfo[] info = dir.GetFiles("*.asset");

        foreach (FileInfo f in info)
        {
            ShopItem item = (ShopItem)AssetDatabase.LoadAssetAtPath(path + "/" + f.Name, typeof(ShopItem));
            shopItems.Add(item);
        }

        //----------------------------------------------------------------------------------------------------------------//

        /* Recupero tutti i GameObject relativi ai pannelli dove vengono mostrate le informazioni all'utente */
        contents = GameObject.Find("Contents");
        Transform parentTransform = contents.transform;
        for (int i = 0; i < parentTransform.childCount; i++)
        {
            Transform childTransform = parentTransform.GetChild(i);
            GameObject childObject = childTransform.gameObject;
            shopPanelsGO.Add(childObject);
        }

        //----------------------------------------------------------------------------------------------------------------//

        /* Recupero gli script: ShopTemplate associati ai GameObject cercati prima e tutti i Bottoni */
        foreach (GameObject panel in shopPanelsGO)
        {
            ShopTemplate script = panel.GetComponent<ShopTemplate>();
            Button button = panel.GetComponentInChildren<Button>();
            shopPanels.Add(script);
            buttons.Add(button);
        }

        //----------------------------------------------------------------------------------------------------------------//

        /* Snippet per attivare un numero di pannelli pari a quelli degli ShopItem (Sempre i menu dove il Game Designer 
         * può comodamente impostare tutti i parametri dell'oggetto acquistabile) */
        for (int i = 0; i<shopItems.Count; i++)
        {
            shopPanelsGO[i].SetActive(true);
        }
        pointsUI.text = "Points: " + points.ToString();
        gemsUI.text = "Processor parts: " + gems.ToString();
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

    public void AddGems()
    {
        gems += 50;
        gemsUI.text = "Processor parts: " + gems.ToString();
        CheckPurchaseable();
    }

    public void CheckPurchaseable()
    {
        for(int i=0; i<shopItems.Count; i++)
        {
            if(points >= shopItems[i].price)
                buttons[i].interactable = true;
            else
                buttons[i].interactable = false;
        }
    }

    public void LoadPanels()
    {
        for(int i=0; i < shopItems.Count; i++)
        {
            shopPanels[i].title.text = shopItems[i].title;
            shopPanels[i].description.text = shopItems[i].description;
            shopPanels[i].price.text = shopItems[i].price.ToString();
        }
    }

    public void PurchaseItem(int buttonIndex)
    {
        if(points  >= shopItems[buttonIndex].price)
        {
            points = points - shopItems[buttonIndex].price;
            pointsUI.text = "Points: " + points.ToString();
            CheckPurchaseable();
        }
    }
}
