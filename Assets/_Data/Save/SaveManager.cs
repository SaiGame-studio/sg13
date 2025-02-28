using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveManager : SaiBehaviour
{
    [SerializeField] protected PlayerCtrl playerCtrl;
    [SerializeField] protected DayNightCycle dayNightCycle;
    [SerializeField] protected string dataSaveName = "dataSaveName";
    [SerializeField] protected bool canSaveData = true;

    [SerializeField] protected Vector3 playerPosition;
    public Vector3 PlayerPosition { get { return playerPosition; } }

    [SerializeField] protected Vector3 playerRotation;
    public Vector3 PlayerRotation { get { return playerRotation; } }

    [SerializeField] protected int currentPointIndex;
    public int CurrentPointIndex { get { return currentPointIndex; } }

    [SerializeField] protected int currentDay;
    [SerializeField] protected float currentTimeElapsed = -1;
    [SerializeField] protected float hunger;
    [SerializeField] protected float thirst;
    [SerializeField] protected float fiber;
    [SerializeField] protected List<ItemInventory> items = new();

    protected virtual void OnApplicationQuit()
    {
        this.Saving();
    }

    protected override void Start()
    {
        base.Start();
        StartCoroutine(LoadGameAfterStart());
        InvokeRepeating(nameof(this.Saving), 2, 2);
    }

    IEnumerator LoadGameAfterStart()
    {
        yield return new WaitForEndOfFrame();
        LoadSaveGame();
    }

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadPlayerCtrl();
        this.LoadDayNightCycle();
    }

    protected virtual void LoadDayNightCycle()
    {
        if (this.dayNightCycle != null) return;
        this.dayNightCycle = FindAnyObjectByType<DayNightCycle>();
        this.currentDay = 1;
        Debug.LogWarning(transform.name + ": LoadDayNightCycle", gameObject);
    }

    protected virtual void LoadPlayerCtrl()
    {
        if (this.playerCtrl != null) return;
        this.playerCtrl = FindAnyObjectByType<PlayerCtrl>();
        this.playerPosition = this.playerCtrl.transform.position;
        this.playerRotation = this.playerCtrl.transform.eulerAngles;
        this.hunger = this.playerCtrl.Needs.Hunger;
        this.thirst = this.playerCtrl.Needs.Thirst;
        this.fiber = this.playerCtrl.Needs.Fiber;
        Debug.LogWarning(transform.name + ": LoadPlayerCtrl", gameObject);
    }

    protected override void ResetValue()
    {
        base.ResetValue();
        this.dataSaveName = "dataSaveName";
        this.currentTimeElapsed = -1;
    }

    public SaveManager(Vector3 pos)
    {
        playerPosition = pos;
    }

    // Convert the object to a JSON string
    public string ToJsonString()
    {
        return JsonUtility.ToJson(this);
    }

    public void FromJsonString(string jsonString)
    {
        JsonUtility.FromJsonOverwrite(jsonString, this);
    }

    protected virtual void Saving()
    {
        if (!this.canSaveData) return;

        this.playerPosition = this.playerCtrl.transform.position;
        this.playerRotation = this.playerCtrl.transform.rotation.eulerAngles;
        this.currentPointIndex = this.playerCtrl.Moving.CurrentPointIndex();
        this.currentDay = DayNightCycle.Instance.CurrentDay;
        this.currentTimeElapsed = DayNightCycle.Instance.TimeElapsed;
        this.hunger = this.playerCtrl.Needs.Hunger;
        this.thirst = this.playerCtrl.Needs.Thirst;
        this.fiber = this.playerCtrl.Needs.Fiber;
        this.SyncAllItemInInventory();

        string jsonData = this.ToJsonString();
        SaveSystem.SetString(this.dataSaveName, jsonData);
        //Debug.LogWarning(jsonData);

        Debug.LogWarning("Saving");
    }

    protected virtual void LoadSaveGame()
    {
        Debug.LogWarning("LoadSaveGame");
        PlayerCtrl playerCtrl = this.playerCtrl;
        DayNightCycle dayNightCycle = this.dayNightCycle;

        string jsonData = SaveSystem.GetString(this.dataSaveName, "");
        if (jsonData != "") this.FromJsonString(jsonData);

        GameManager.Instance.DoneLoadSaveGame();
        this.canSaveData = true;

        this.playerCtrl = playerCtrl;
        this.dayNightCycle = dayNightCycle;

        this.OnLoadSaveGameSuccess();
    }

    protected virtual void OnLoadSaveGameSuccess()
    {
        this.dayNightCycle.LoadSaveData(this.currentDay, this.currentTimeElapsed);
        this.playerCtrl.Needs.LoadSaveData(this.hunger, this.thirst, this.fiber);
        this.playerCtrl.Moving.LoadSaveData(this.playerPosition, this.playerRotation, this.currentPointIndex);
        InventoryManager.Instance.LoadSaveData(this.items);
    }

    protected virtual void SyncAllItemInInventory()
    {
        this.items = new();
        this.LoadItemInInventory(InventoryManager.Instance.Monies().Items);
        this.LoadItemInInventory(InventoryManager.Instance.Items().Items);
    }

    protected virtual void LoadItemInInventory(List<ItemInventory> items)
    {
        ItemInventory itemExist = null;

        foreach (ItemInventory itemInventory in items)
        {
            itemExist = this.items.Find(item => item.ItemID == itemInventory.ItemID);
            if (itemExist == null || itemExist.ItemID == 0)
            {
                this.items.Add(itemInventory);
                continue;
            }
        }
    }
}
