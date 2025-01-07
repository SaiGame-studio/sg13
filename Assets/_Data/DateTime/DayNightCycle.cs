using System.Data;
using UnityEngine;

public class DayNightCycle : SaiSingleton<DayNightCycle>
{
    [Header("Time Status")]
    [SerializeField] protected bool isSleeping = false;
    [SerializeField] protected bool isEatTime = false;
    public bool IsEatTime { get { return isEatTime; } }

    [Header("Time Settings")]
    [SerializeField] protected float minutePerDay = 5f;
    [SerializeField] protected int startHour = 2;
    [SerializeField] protected int currentHour;
    [SerializeField] protected int currentMinute;
    [SerializeField] protected int currentDay = 1;
    [SerializeField] protected float timeScale = 1f;
    [SerializeField] protected float restTimeScale = 16f;

    [Header("Sunlight Settings")]
    [SerializeField] protected Light directionalLight;
    [SerializeField] protected Gradient lightColorGradient;
    [SerializeField] protected AnimationCurve lightIntensityCurve;
    [SerializeField] protected float timeOfDayNormalized;
    [SerializeField] protected float timeElapsed;
    [SerializeField] protected float secondsPerDay;
    [SerializeField] protected TimeOfDay currentTimeOfDay;

    protected override void Start()
    {
        this.secondsPerDay = this.minutePerDay * 60;
        this.UpdateTime();
    }

    private void Update()
    {
        this.UpdateStatus();
        this.SimulateTime();
        this.UpdateLighting();
    }

    protected override void ResetValue()
    {
        base.ResetValue();
        this.ResetAniCurve();
    }

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadDirectionalLight();
    }

    protected virtual void ResetAniCurve()
    {
        if (this.lightIntensityCurve.keys.Length > 0) return;
        this.lightIntensityCurve = new AnimationCurve(
            new Keyframe(0f, 0f, 4.721271f, 4.721271f),
            new Keyframe(1f, 0.004718781f, -3.072022f, -3.072022f)
            );
        Debug.LogWarning(transform.name + ": ResetAniCurve", gameObject);
    }

    protected virtual void LoadDirectionalLight()
    {
        if (this.directionalLight != null) return;
        this.directionalLight = GetComponentInChildren<Light>();
        Debug.LogWarning(transform.name + ": LoadDirectionalLight", gameObject);
    }

    private void SimulateTime()
    {
        if (this.isSleeping) this.timeScale = this.restTimeScale;
        else this.timeScale = 1;

        timeElapsed += Time.deltaTime * timeScale;
        float timeInDay = (timeElapsed / secondsPerDay) * 1440;
        currentHour = (int)(timeInDay / 60) % 24;
        currentMinute = (int)(timeInDay % 60);

        if (timeInDay >= 1440)
        {
            timeElapsed -= secondsPerDay;
            currentDay++;
        }

        if (currentHour >= 6 && currentHour < 12) currentTimeOfDay = TimeOfDay.Morning;
        else if (currentHour >= 12 && currentHour < 17) currentTimeOfDay = TimeOfDay.Noon;
        else if (currentHour >= 17 && currentHour < 20) currentTimeOfDay = TimeOfDay.Evening;
        else currentTimeOfDay = TimeOfDay.Night;
    }

    private void UpdateLighting()
    {
        if (this.directionalLight == null) return;
        this.timeOfDayNormalized = (this.currentHour * 60f + this.currentMinute) / 1440f;
        this.directionalLight.color = lightColorGradient.Evaluate(this.timeOfDayNormalized);
        this.directionalLight.intensity = lightIntensityCurve.Evaluate(this.timeOfDayNormalized);
    }

    private void UpdateTime()
    {
        float initialTime = this.startHour * 60f;
        this.timeElapsed = (initialTime / 1440f) * this.secondsPerDay;
    }

    public string GetFormattedTime()
    {
        string label = Language.T("Day");
        return $"{label} {this.currentDay} - {currentHour:D2}:{currentMinute:D2}";
    }

    public virtual string GetTimeOfDay()
    {
        return this.currentTimeOfDay.ToString();
    }

    public virtual TimeOfDay GetTime()
    {
        return this.currentTimeOfDay;
    }

    public virtual bool Is(TimeOfDay timeToCheck)
    {
        return this.currentTimeOfDay == timeToCheck;
    }

    public virtual bool IsNight()
    {
        return this.Is(TimeOfDay.Night);
    }

    public virtual void SetTimeScale(float timeScale)
    {
        this.timeScale = timeScale;
    }

    protected virtual void UpdateStatus()
    {
        this.isEatTime = DayNightCycle.Instance.Is(TimeOfDay.Morning);
        this.isSleeping = PlayerNeeds.Instance.IsSleeping();
    }
}
