using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;


public class PlayerGUI : MonoBehaviour
{
    public static int maxHealth;
    public static int maxEnergy;

    public static int currentHealth;
    public static int currentEnergy;

    private float healthBarWidth;
    private float energyBarWidth;
    private float barHeight;
    private float borderRadius;
    private float barOpacity;

    private PlayerComponentManager manager;
    private void OnGUI()
    {
        // Calcola le dimensioni e la posizione della GUI
        float padding = 10f;
        float healthBarPosX = padding;
        float healthBarPosY = padding;
        float manaBarPosX = padding;
        float manaBarPosY = healthBarPosY + barHeight + padding;

        // Disegna la barra della salute
        DrawColoredBar(new Rect(healthBarPosX, healthBarPosY, healthBarWidth, barHeight), currentHealth, maxHealth, Color.red, "Health");

        // Disegna la barra del mana
        DrawColoredBar(new Rect(manaBarPosX, manaBarPosY, energyBarWidth, barHeight), currentEnergy, maxEnergy, Color.blue, "Energy");
    }

    private void DrawColoredBar(Rect rect, int currentValue, int maxValue, Color color, string labelText)
    {
        // Calcola la percentuale del valore attuale rispetto al valore massimo
        float fillPercentage = (float)currentValue / maxValue;

        // Calcola la larghezza del rettangolo riempito
        float fillWidth = rect.width * fillPercentage;

        // Calcola la dimensione e la posizione del rettangolo di sfondo della barra
        Rect backgroundRect = rect;
        backgroundRect.width += 6f; // Incrementa la larghezza del rettangolo di sfondo
        backgroundRect.height += 6f; // Incrementa l'altezza del rettangolo di sfondo
        backgroundRect.x -= 3f; // Sposta l'origine x del rettangolo di sfondo a sinistra di 2 unità
        backgroundRect.y -= 3f; // Sposta l'origine y del rettangolo di sfondo in alto di 2 unità

        // Disegna il rettangolo di sfondo della barra con angoli stondati
        GUIStyle backgroundStyle = CreateColoredGUIStyle(Color.black);
        GUI.Box(backgroundRect, "", backgroundStyle);

        // Calcola la dimensione e la posizione del rettangolo riempito della barra
        Rect fillRect = new Rect(rect.x, rect.y, fillWidth, rect.height);

        // Disegna il rettangolo riempito della barra con angoli stondati
        GUIStyle fillStyle = CreateColoredGUIStyle(color);
        GUI.Box(fillRect, "", fillStyle);

        // Disegna il testo all'interno della barra
        GUIStyle labelStyle = new GUIStyle(GUI.skin.label);
        labelStyle.alignment = TextAnchor.MiddleCenter;
        GUI.Label(rect, labelText, labelStyle);
    }

    private GUIStyle CreateColoredGUIStyle(Color color)
    {
        GUIStyle style = new GUIStyle(GUI.skin.box);
        style.normal.background = CreateColoredTexture(color, barOpacity);
        style.border = new RectOffset((int)borderRadius, (int)borderRadius, (int)borderRadius, (int)borderRadius);
        return style;
    }


    private Texture2D CreateColoredTexture(Color color, float opacity)
    {
        int textureWidth = 16;
        int textureHeight = 16;

        Texture2D texture = new Texture2D(textureWidth, textureHeight);

        Color[] pixels = new Color[textureWidth * textureHeight];
        for (int x = 0; x < textureWidth; x++)
        {
            for (int y = 0; y < textureHeight; y++)
            {
                if (IsPointInsideCircle(x, y, textureWidth, textureHeight))
                {
                    pixels[y * textureWidth + x] = color;
                    pixels[y * textureWidth + x].a = opacity;
                }
                else
                {
                    pixels[y * textureWidth + x] = Color.clear;
                }
            }
        }

        texture.SetPixels(pixels);
        texture.Apply();

        return texture;
    }

    private bool IsPointInsideCircle(int x, int y, int width, int height)
    {
        float centerX = width / 2f;
        float centerY = height / 2f;
        float radius = Mathf.Min(width, height) / 2f;

        float distance = Mathf.Sqrt((x - centerX) * (x - centerX) + (y - centerY) * (y - centerY));

        return distance <= radius;
    }

    void Start()
    {
        manager = PlayerComponentManager.Instance;

        PlayerFeature health = manager.Features["baseHealth"];
        PlayerFeature energy = manager.Features["baseEnergy"];

        maxHealth = (int)health.BaseValue;
        currentHealth = (int)health.CurrentValue;

        maxEnergy = (int)energy.BaseValue;
        currentEnergy = (int)energy.CurrentValue;

        healthBarWidth = (float)maxHealth * 2;
        energyBarWidth = (float)maxEnergy * 2;

        barHeight = 20f;
        borderRadius = 10f;
        barOpacity = 0.5f;
    }

    private void Update()
    {
        // Assicurati che i valori non scendano al di sotto di uno o al di sopra del massimo
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
        currentEnergy = Mathf.Clamp(currentEnergy, 0, maxEnergy);
    }

    public static int CurrentHealth
    {
        get { return currentHealth; }
        set { currentHealth = value; }
    }

    public static int CurrentEnergy
    {
        get { return currentEnergy; }
        set { currentEnergy = value; }
    }

    public static int MaxHealth
    {
        get { return maxHealth; }
        set { maxHealth = value; }
    }

    public static int MaxEnergy
    {
        get { return maxEnergy; }
        set { maxEnergy = value; }
    }
}
