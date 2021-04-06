using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/**
* Vincent Tse.
* 2021-02-13
*/
public class GameController : MonoBehaviour
{
    public SoundClip activeSoundClip;
    public AudioSource[] audioSources;

    [Header("Crystal Count")]
    public int crystalCount = 0;

    [Header("Potion Count")]
    public int potionCount = 0;

    public PlayerBehaviour player;

    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<PlayerBehaviour>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void addCrystal()
    {
        crystalCount++;
        player.inventory.AddItem(new Item { itemType = Item.ItemType.StarFragment, amount = 1 });
    }

    public void addPotion()
    {
        potionCount++;
        player.inventory.AddItem(new Item { itemType = Item.ItemType.HealthPotion, amount = 1 });


    }

    public void usePotion()
    {
        potionCount--;
        player.inventory.RemoveItem(new Item { itemType = Item.ItemType.HealthPotion, amount = 1 });
    }

    public int getCrystal()
    {
        return crystalCount;
    }

    public void SaveGame()
    {
        Debug.Log("saving");

        SaveSystem.SaveGame(this);

    }

    
    public void LoadGame()
    {
        GameData data = SaveSystem.LoadGame();

        Debug.Log(data.crystals);
        enabled = false;
        crystalCount = data.crystals;
        potionCount = data.potions;
        enabled = true;

    }
}
