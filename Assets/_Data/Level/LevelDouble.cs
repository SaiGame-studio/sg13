using UnityEngine;

namespace Sai
{
    public abstract class LevelDouble : LevelAbstract
    {

        [Header("Double")]
        [SerializeField] protected float baseNumber = 10;
        [SerializeField] protected float baseMin = 1.3f;
        [SerializeField] protected float baseMax = 1.0712f;
        [SerializeField] protected float baseLimit = 100;
        [SerializeField] protected float numberMulti = 1f;


        public override float GetNextLevelExp()
        {

            //= ROUND(number * base_min ^ MIN(B9, hp_base_limit) * hp_base_max ^ MAX(0, B9 - hp_base_limit), trunc)
            this.nextLevelExp = this.baseNumber * Mathf.Pow(baseMin, Mathf.Min(this.currentLevel, baseLimit)) * Mathf.Pow(baseMax, Mathf.Max(0, this.currentLevel - baseLimit));
            this.nextLevelExp *= this.numberMulti;

            return this.nextLevelExp;
        }


        public override void SetLevel(int level)
        {
            base.SetLevel(level);
            this.GetNextLevelExp();
        }
    }
}