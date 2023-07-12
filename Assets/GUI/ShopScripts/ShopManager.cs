using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;


public class ShopManager : MonoBehaviour
{
    public int credits;
    public int gems;
    public TMP_Text creditsUI;
    public TMP_Text gemsUI;
    private List<ShopItem> shopItems;
    private List<GameObject> shopPanelsGO;
    private List<ShopTemplate> shopPanels;
    private List<Button> buttons;

    private GameObject contents;
    private static PlayerComponentManager manager;
    private string activeModifiersPath = "Assets/Player/ActiveModifiers.csv";

    // Start is called before the first frame update
    void Start()
    {
        shopPanelsGO = new List<GameObject>();
        shopPanels = new List<ShopTemplate> ();
        shopItems = new List<ShopItem>();
        buttons = new List<Button>();

        manager = PlayerComponentManager.Instance;

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
        creditsUI.text = "Credits: " + credits.ToString();
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
        credits+=50;
        creditsUI.text = "Credits: " + credits.ToString();
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
        //controlla se hai già il potenziamento
        
        for (int i = 0; i < shopItems.Count; i++)
        {
            if (credits >= shopItems[i].price)
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
        if(credits  >= shopItems[buttonIndex].price)
        {
            //Attiva il modifier
            foreach (PlayerModifier modifier in manager.Modifiers)
            {
                if (modifier.GetName.Equals(shopItems[buttonIndex].title.Trim()))
                {
                    modifier.Enabled = true;
                }
            }

            credits = credits - shopItems[buttonIndex].price;
            creditsUI.text = "Credits: " + credits.ToString();

            /* scrivi sul file per backup e per non perdere i potenziamenti
             * all'avvio di una nuova sessione di gioco */
            WriteToCSV(activeModifiersPath, shopItems[buttonIndex].title.Trim(), true);
            CheckPurchaseable();
        }
    }

    public static void WriteToCSV(string filePath, string name, bool value)
    {
        //Controllo se il modifier è già presente tra quelli attivi e aggiorno la struttura
        Dictionary<PlayerModifier, bool> activeModifiers = manager.ActiveModifiers;
        Dictionary<PlayerModifier, bool> activeModifiersCopy = new Dictionary<PlayerModifier, bool>(activeModifiers);

        if (activeModifiers.Count > 0)
        {
            foreach (KeyValuePair<PlayerModifier, bool> pair in activeModifiersCopy)
            {
                PlayerModifier key = pair.Key;
                if (key.GetName.Equals(name))
                {
                    activeModifiers[key] = value;
                }               
            }

            manager.ActiveModifiers = activeModifiers;
        }
        else
        {
            foreach (PlayerModifier modifier in manager.Modifiers)
            {
                if (modifier.GetName.Equals(name))
                {
                    activeModifiers.Add(modifier, value);
                }
                if (!modifier.GetName.Equals(name))
                {
                    activeModifiers.Add(modifier, false);
                }
            }

            manager.ActiveModifiers = activeModifiers;
        }
       
        //Scrivo sul file
        using (StreamWriter writer = new StreamWriter(filePath))
        {
            foreach (KeyValuePair<PlayerModifier, bool> entry in activeModifiers)
            {
                int intValue = entry.Value ? 1 : 0;
                string line = $"{entry.Key.GetName}, {intValue}";
                writer.WriteLine(line);
            }
        }
    }

    public void NewGame()
    {
        // Provvisorio
        // Qui bisogna collegare lo script di generazione procedurale del livello
        SceneManager.LoadScene("Tutorial");
        PlayerGUI.CurrentHealth = (int)PlayerGUI.MaxHealth;
        PlayerGUI.CurrentEnergy = (int)PlayerGUI.MaxEnergy;
        SampleTeleporterController.Active = true;
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
