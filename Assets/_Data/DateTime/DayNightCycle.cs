using System;
using System.Security.Cryptography;
using UnityEngine;

public class DayNightCycle : SaiSingleton<DayNightCycle>
{
    [Header("Time Settings")]
    [SerializeField] protected float dayLengthInMinutes = 7f;
    [SerializeField] protected int startHour = 7;
    [SerializeField] protected int currentHour;
    [SerializeField] protected int currentMinute;

    [Header("Sunlight Settings")]
    [SerializeField] protected Light directionalLight;
    [SerializeField] protected Gradient lightColorGradient;
    [SerializeField] protected AnimationCurve lightIntensityCurve;
    [SerializeField] protected float timeOfDayNormalized;
    [SerializeField] protected float timeElapsed;
    [SerializeField] protected float secondsPerDay;
    [SerializeField] protected TimeOfDay currentTimeOfDay;
    [SerializeField] protected int currentDay = 1;

    protected override void Start()
    {
        this.secondsPerDay = this.dayLengthInMinutes * 60;
        this.UpdateTime();
        //this.ResetAniCurve();
    }

    private void Update()
    {
        SimulateTime();
        UpdateLighting();
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
        timeElapsed += Time.deltaTime;
        float timeInDay = (timeElapsed / secondsPerDay) * 1440;
        currentHour = (int)(timeInDay / 60) % 24;
        currentMinute = (int)(timeInDay % 60);

        if (timeInDay >= 1440)
        {
            timeElapsed -= secondsPerDay; // Reset elapsed time for a new day
            currentDay++; // Increment the day counter
        }

        if (currentHour >= 6 && currentHour < 12)
        {
            currentTimeOfDay = TimeOfDay.Morning;
        }
        else if (currentHour >= 12 && currentHour < 17)
        {
            currentTimeOfDay = TimeOfDay.Noon;
        }
        else if (currentHour >= 17 && currentHour < 20)
        {
            currentTimeOfDay = TimeOfDay.Evening;
        }
        else
        {
            currentTimeOfDay = TimeOfDay.Night;
        }
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
        float initialTime = startHour * 60f;
        timeElapsed = (initialTime / 1440f) * secondsPerDay;
    }

    public string GetFormattedTime()
    {
        string label = LocalizationManager.Instance.GetTranslation("Day");
        return $"{label} {this.currentDay} - {currentHour:D2}:{currentMinute:D2}";
    }

    public string GetTimeOfDay()
    {
        return currentTimeOfDay.ToString();
    }
}
