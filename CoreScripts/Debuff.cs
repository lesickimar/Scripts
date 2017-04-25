using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Debuff
{
    

	public DEBUFF ID;
    public EnemySpellInfo einfo;
	public int duration;
	public int maxDuration;
	public GameObject icon=null;
	public float multiplier = 1f;
    public Soldier myParent;
    private int gap;
	
	public Debuff (DEBUFF _ID, Soldier _myParent)
	{
		ID = _ID;
        myParent = _myParent;
	}

    protected void InitDebuff()
    {
        maxDuration = einfo.DoTgap * einfo.ticksCount;
        duration = maxDuration;
        gap = einfo.DoTgap;
    }
	
	public void Refresh(int _value)
	{
		// 0 - odswiez do pelnego czasu
		if (_value == 0)
		{
			duration = maxDuration;
		}
		else
		{
			duration += _value;
		}
        RefreshAction();
        if (multiplier > 1f)
            icon.transform.GetChild(0).GetComponent<Text>().text = ((int)multiplier).ToString();
        else
            icon.transform.GetChild(0).GetComponent<Text>().text = "";        
        
	}

    public virtual void RefreshAction()
    {

    }

    public virtual void Execute()
    {
        
    }
	
	public void Update()
	{
        if (myParent.isDead)
        {
            Remove();
        }
        else
        {
            duration = Mathf.Max(0, --duration);
            if (icon != null)
            {
                icon.transform.GetChild(1).GetComponent<Image>().fillAmount = (float)duration / (float)maxDuration;
            }
            if (duration % gap == 0)
                Execute();
        }
    }

    public void Remove()
    {
        duration = 0;
        Object.Destroy(icon);
    }
}

