using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.MHAC.Modules;
[CreateAssetMenu(fileName = "ModuleSettingsSO", menuName = "ModuleSettingsSO")]
public class ModuleSettingsSO : ScriptableObject
{
    [SerializeReference] public List<Module> modules = new List<Module>();

    [ContextMenu("Add JoyStick")]
    public void AddJoyStickModule()
    {
        modules.Add(new Joystick());
    }

    [ContextMenu("Add SteeringWheel")]
    public void AddSteeringdModule()
    {
        modules.Add(new SteeringWheel());
    }
}
