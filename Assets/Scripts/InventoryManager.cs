using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour, IGameManager
{
    public ManagerStatus status { get; private set; }
    public string equippedItem { get; private set; }

    private Dictionary<string, int> items;
    private NetworkService network;

    public void Startup(NetworkService service)
    {
        Debug.Log("inventory manager starting...");
        status = ManagerStatus.Started;
        UpdateData(new Dictionary<string, int>());
        network = service;
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

    public void UpdateData(Dictionary<string, int> items)
    {
        this.items = items;
    }

    public Dictionary<string, int> GetData()
    {
        return items;
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
