using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillManager : MonoBehaviour
{
    public static SkillManager instance;

    public DashSkill Dash {  get; private set; }
   public CloneSkill Clone {  get; private set; }
    public SwordSkill Sword {  get; private set; }
    [HideInInspector]public BlackholeSkill Blackhole {  get; private set; }
    public CrystalSkill Crystal {  get; private set; }
    public ParrySkil Parry {  get; private set; }
     public DodgeSkill Dodge { get; private set; }

    private void Awake()
    {
        if(instance != null)
        {
            Destroy(instance.gameObject);
        }
        else
        {
            instance = this;
        }
    }

    private void Start()
    {
        Dash =GetComponent<DashSkill>();
        Clone =GetComponent<CloneSkill>();
        Sword =GetComponent<SwordSkill>();
        Blackhole = GetComponent<BlackholeSkill>();
        Crystal = GetComponent<CrystalSkill>();
        Parry = GetComponent<ParrySkil>();
        Dodge = GetComponent<DodgeSkill>();

    }
}
