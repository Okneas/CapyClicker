using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Entity/Prestige Upgrade")]

public class PrestigeUpgrade : Upgrades
{
    public List<int> conditionsId; 
    public string stringEffectOnGoldenCapyProfit;
    public int effectOnConditions;
    public UBigNumber effectOnGoldenCapyProfit;

    public override void DoUpgrade()
    {
        UpgradeJob job = () => { };
        base.DoUpgrade();
        if (effectOnConditions != 0)
        {
            job += ChangeEffectOnConditions;
            job.Invoke();
            job -= ChangeEffectOnConditions;
        }
        // Если улучшение имеет эффект при нажатии на капитулу для определенного предмета, то:
        if (effectOnGoldenCapyProfit != UBigNumber.ConvertToBigNumber(0))
        {
            job += ChangeEffectOnConditions;
            job.Invoke();
            job -= ChangeEffectOnConditions;
        }
    }

    public void CheckConditions()
    {
        if (conditionsId.Contains(-1) && !(GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().PrestigeAllUpgradesObtained.Contains(this)))
        {
            active = true;
        }
        int count = 0;
        foreach (PrestigeUpgrade i in GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().PrestigeAllUpgradesObtained)
        {
            for(int j = 0; j < conditionsId.Count; j++)
            {
                if(i.id == conditionsId[j])
                {
                    count++;
                }
            }
        }
        if(conditionsId.Count == count)
        {
            active = true;
        }
    }

    public void ChangeEffectOnGoldenCapyProfit()
    {
        GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().profitOfgoldenCapy += effectOnGoldenCapyProfit;
    }
    // Метод, который вызывается при изменении эффекта при нажатии на предмет
    public void ChangeEffectOnConditions()
    {
        GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().maxConditionsForUpgrades += effectOnConditions;
    }
}
