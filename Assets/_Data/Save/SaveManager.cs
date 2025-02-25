using System;
using System.Collections;
using UnityEngine;

public class SaveManager : SaiSingleton<SaveManager>
{
    [SerializeField] protected Vector3 playerPosition;
    public Vector3 PlayerPosition { get { return playerPosition; } }

    [SerializeField] protected Vector3 playerRotation;
    public Vector3 PlayerRotation { get { return playerRotation; } }

    [SerializeField] protected int currentPointIndex;
    public int CurrentPointIndex { get { return currentPointIndex; } }


    [SerializeField] protected string playerPosName = "playerPosition";
    [SerializeField] protected string playerRotName = "playerRotation";
    [SerializeField] protected string currentPointIndexName = "currentPointIndex";

    [SerializeField] protected bool canSaveData = true;

    public Action OnLoadSuccess;
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

    protected override void ResetValue()
    {
        base.ResetValue();
        this.playerPosName = "playerPosition";
        this.playerRotName = "playerRotation";
        this.currentPointIndexName = "currentPointIndex";
    }

    protected virtual void FixedUpdate()
    {
        //this.Saving();
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

    protected virtual void Saving()
    {
        if (!this.canSaveData) return;

        this.playerPosition = PlayerCtrl.Instance.transform.position;
        SaveSystem.SetVector3(this.playerPosName, this.playerPosition);

        this.playerRotation = PlayerCtrl.Instance.transform.rotation.eulerAngles;
        SaveSystem.SetVector3(this.playerRotName, this.playerRotation);

        this.currentPointIndex = PlayerCtrl.Instance.Moving.CurrentPointIndex();
        SaveSystem.SetInt(this.currentPointIndexName, this.currentPointIndex);

        Debug.LogWarning("Saving");
    }

    protected virtual void LoadSaveGame()
    {
        Debug.LogWarning("LoadSaveGame");
        this.playerPosition = SaveSystem.GetVector3(this.playerPosName);
        this.playerRotation = SaveSystem.GetVector3(this.playerRotName);
        this.currentPointIndex = SaveSystem.GetInt(this.currentPointIndexName);

        GameManager.Instance.DoneLoadSaveGame();
        this.OnLoadSuccess?.Invoke();
        this.canSaveData = true;
    }


}
