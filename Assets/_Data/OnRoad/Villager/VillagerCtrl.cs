using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


[RequireComponent(typeof(NavMeshAgent))]
public abstract class VillagerCtrl : OnRoadCtrl
{
    [Header("Villager")]
    [SerializeField] protected Transform model;

    [SerializeField] protected NavMeshAgent agent;
    public NavMeshAgent Agent { get { return agent; } }

    [SerializeField] protected Animator animator;
    public Animator Animator { get { return animator; } }

    [SerializeField] protected VillagerMoving moving;
    public VillagerMoving Moving { get { return moving; } }

    [Header("Fate/Karma")]
    [SerializeField] protected float karmaRate = 43;
    [SerializeField] protected float karmaDefaultRate = 43;
    [SerializeField]
    protected List<ItemCode> fateItems = new()
        {
            ItemCode.Water,
            ItemCode.Water1,
            ItemCode.Water2,
            ItemCode.Water3,
            ItemCode.Banana,
            ItemCode.Coconut,
            ItemCode.Grapes1,
            ItemCode.Grapes2,
            ItemCode.Grapes3,
            ItemCode.Bread,
            ItemCode.WaterMelon,
            ItemCode.Cherry,
            ItemCode.Corn,
        };

    [SerializeField]
    protected List<ItemCode> karmaItems = new()
        {
            ItemCode.Meat1,
            ItemCode.Meat2,
            ItemCode.Meat3,
            ItemCode.Gold,
            ItemCode.Wine,
            ItemCode.Fiber1,
        };

    public override string GetName()
    {
        return OnRoadCode.Villager.ToString();
    }

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadNavMeshAgent();
        this.LoadModel();
        this.LoadAnimator();
        this.LoadMoving();
    }
    protected virtual void LoadMoving()
    {
        if (this.moving != null) return;
        this.moving = GetComponentInChildren<VillagerMoving>();
        Debug.Log(transform.name + ": LoadMoving", gameObject);
    }

    protected virtual void LoadNavMeshAgent()
    {
        if (this.agent != null) return;
        this.agent = GetComponent<NavMeshAgent>();
        this.agent.speed = 4f;
        this.agent.angularSpeed = 200f;
        this.agent.acceleration = 150f;
        this.agent.stoppingDistance = 0f;
        this.agent.baseOffset = 0f;
        Debug.Log(transform.name + ": LoadNavMeshAgent", gameObject);
    }

    protected virtual void LoadModel()
    {
        if (this.model != null) return;
        this.model = transform.Find("Model");
        this.model.localPosition = new Vector3(0f, -0.16f, 0f);
        Debug.Log(transform.name + ": LoadModel", gameObject);
    }

    protected virtual void LoadAnimator()
    {
        if (this.animator != null) return;
        this.animator = this.model.GetComponent<Animator>();
        this.animator.applyRootMotion = false;
        Debug.Log(transform.name + ": LoadAnimator", gameObject);
    }

    protected override void Reborn()
    {
        base.Reborn();
        this.GiveButton.gameObject.SetActive(false);
        Vector3 position = this.model.localPosition;
        position.y = 0f;
        this.model.localPosition = position;
    }

    protected override ItemCode RandomItem()
    {
        List<ItemCode> items = this.GetFateOrKarmaItems();
        return this.inventoryManager.RandomItem(items);
    }

    protected virtual List<ItemCode> GetFateOrKarmaItems()
    {
        int playerLevel = PlayerCtrl.Instance.Level.CurrentLevel;
        int karmaPercent = (int)(this.karmaDefaultRate - (playerLevel * 2.5));
        this.karmaRate = karmaPercent;
        int rand = Random.Range(0, 100);
        if (rand < this.karmaRate) return this.itemsGive = this.karmaItems;
        return itemsGive = this.fateItems;
    }
}
