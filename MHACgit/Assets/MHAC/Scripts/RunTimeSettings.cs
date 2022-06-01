using Assets.MHAC.Modules;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class RunTimeSettings : MonoBehaviour
{
    public ModuleSettingsSO ModuleSettingsSO;
    private List<Joystick> joySticks = new List<Joystick>();
    private List<SteeringWheel> SteeringWheels = new List<SteeringWheel>();

    void Start()
    {
        joySticks = ModuleSettingsSO.modules.OfType<Joystick>().ToList();
        SteeringWheels = ModuleSettingsSO.modules.OfType<SteeringWheel>().ToList();
    }

    private void Awake()
    {
        joySticks.Count();
    }
}
