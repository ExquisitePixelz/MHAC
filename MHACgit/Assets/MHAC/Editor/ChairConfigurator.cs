using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using System.IO;
using Assets.MHAC.Modules;
using System.IO.Ports;

public class ChairConfigurator : EditorWindow
{
    [SerializeField] private SerializebleLists<Module> modules = new SerializebleLists<Module>();
    private ModuleSettingsSO target;
    private GameObject arduinoGO;
    public ArduinoReadThread arduinoScript;
    public string comString;
    public int screenWidth;

    Editor tmpEditor = null;
    [MenuItem("MHAC/Chair Configurator")]
    static void Init()
    {
        GUIContent titleContent = new GUIContent("Chair Configurator");
        Texture icon = AssetDatabase.LoadAssetAtPath<Texture>("Assets/MHAC/Editor/Resources/MHAClogo2.png");
        if (icon != null)
            titleContent = new GUIContent(" Chair Configurator", icon);
        ChairConfigurator w = (ChairConfigurator)EditorWindow.GetWindow<ChairConfigurator>();
        w.titleContent = titleContent;
        w.Show();
    }

    private void OnEnable()
    {
        var result = AssetDatabase.FindAssets("t:ModuleSettingsSO");
        target = AssetDatabase.LoadAssetAtPath(AssetDatabase.GUIDToAssetPath(result[0]), typeof(ModuleSettingsSO)) as ModuleSettingsSO;
        tmpEditor = Editor.CreateEditor(target);
    }

    public void OnGUI()
    {
        drawLogo();

        GUILayout.Label("Configurate Modules", EditorStyles.boldLabel);

        EditorGUILayout.HelpBox("This tool has been made by Yannick Mul in cooperation with Wido Beukhof, Kay Poland, Nathalie Verduin as an assignment for the HKU.", MessageType.Info);

        GUILayout.Space(10);
        if (GUILayout.Button(new GUIContent("Tutorial Chair Configurator", "If you need help, click this button to open a tutorial.")))
        {
            Application.OpenURL("https://youtu.be/ARPqsoiul1I");
        }
        GUILayout.Space(10);

        GUILayout.Label("1. Add modules", EditorStyles.boldLabel);

        if (GUILayout.Button("Add Module"))
        {
            GenericMenu moduleMenu = new GenericMenu();

            moduleMenu.AddItem(new GUIContent("Movement/Joystick"), false, addJoystick);
            moduleMenu.AddItem(new GUIContent("Movement/Steeringwheel"), false, addSteeringwheel);
            moduleMenu.ShowAsContext();
            GUILayout.Space(10);
        }

        tmpEditor.DrawDefaultInspector();

        GUILayout.Space(10);

        GUILayout.Label("2. Add Arduino Serializer", EditorStyles.boldLabel);
        EditorGUILayout.HelpBox("Adds an object to the scene. This object has a script to communicate with Arduino", MessageType.Info);

        GUILayout.Space(10);
        
        if (GUILayout.Button(new GUIContent("Add RunTimeSettings Prefab", "This will import settings on startup. Also handles Arduino Serializing")))
        {
            AddRunTimeSettingsPrefab();
        }

        GUILayout.Space(10);

        GUILayout.Label("3. Change Com port", EditorStyles.boldLabel);
        EditorGUILayout.HelpBox("Com ports can be found by going to Start>Device Manager>Ports", MessageType.Info);
        GUILayout.Space(10);

        comString = GUILayout.TextField(comString);
    }

    public void addJoystick()
    {
        target.AddJoyStickModule();
    }

    public void addSteeringwheel()
    {
        target.AddSteeringdModule();
    }

    public void AddRunTimeSettingsPrefab()
    {
        if (GameObject.FindGameObjectWithTag("RunTimeSettings") is null)
        {
            Debug.Log("Clicked");
            Instantiate(Resources.Load("RunTimeSettings", typeof(GameObject)));

            if (arduinoGO == null)
            {
                arduinoGO = GameObject.FindWithTag("RunTimeSettings");
            }
            arduinoScript = arduinoGO.GetComponent<ArduinoReadThread>();
            arduinoScript.SetComport(comString);
        }
    }

    public void drawLogo()
    {
        Color bgColor = new Color();
        ColorUtility.TryParseHtmlString("#282828", out bgColor);
        EditorGUI.DrawRect(new Rect(0, 0, Screen.width, 90f), bgColor);
        Texture tex = (Texture)EditorGUIUtility.Load("Assets/MHAC/Editor/Resources/MHACbanner-larger.jpg");
        int logoXpos = screenWidth / 2 + tex.width / 2;
        GUI.DrawTexture(new Rect(Screen.width/ 3, 0, tex.width, tex.height), tex, ScaleMode.ScaleToFit);
        GUILayout.Space(95f);

        Debug.Log("logo x pos :" + logoXpos);
        Debug.Log("Screenwidth : " + screenWidth);
        Debug.Log("Texture width : " + tex.width);
    }

    public void OnDestroy()
    {
    }
}
