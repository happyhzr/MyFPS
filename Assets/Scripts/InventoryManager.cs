using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour, IGameManager
{
    public ManagerStatus status { get; private set; }
    public string equippedItem { get; private set; }

    private Dictionary<string, int> items;

    public void Startup()
    {
        Debug.Log("inventory manager starting...");
        status = ManagerStatus.Started;
        items = new Dictionary<string, int>();
    }

    public void AddItem(string name)
    {
        if (items.ContainsKey(name))
        {
            items[name] += 1;
        }
        else
        {
            items[name] = 1;
        }
        DisplayItems();
    }

    public List<string> GetItemList()
    {
        List<string> list = new List<string>(items.Keys);
        return list;
    }

    public int GetItemCount(string name)
    {
        if (items.ContainsKey(name))
        {
            return items[name];
        }
        return 0;
    }

    public bool EquipItem(string name)
    {
        if (items.ContainsKey(name) && equippedItem != name)
        {
            equippedItem = name;
            Debug.Log($"equipped {name}");
            return true;
        }
        equippedItem = null;
        Debug.Log("unequipped");
        return false;
    }

    public bool ConsumeItem(string name)
    {
        if (items.ContainsKey(name))
        {
            items[name]--;
            if (items[name] == 0)
            {
                items.Remove(name);
            }
        }
        else
        {
            Debug.Log($"cannot consume {name}");
            return false;
        }
        DisplayItems();
        return true;
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void DisplayItems()
    {
        string itemDisplay = "Items: ";
        foreach (KeyValuePair<string, int> item in items)
        {
            itemDisplay += item.Key + " (" + item.Value + ") ";
        }
        Debug.Log(itemDisplay);
    }
}
