using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Pause_Menu_Manager : MonoBehaviour
{
    public Slider Sensitivity;
    public GameObject Pause, Pause_Menu, Play, Basic, Weapon,Weapon_Controle, Vahical, Map;
    public bool isBreakApplied;

    public Player_Rotation PR;
    [SerializeField]
    public List<GameObject> GUI_GameObject;

    public Player_Weapon PW;
    bool pause;


    private void Start()
    {
        ChangeControle_GUI("Basic", "");
        pause = false;
    }


    /// <summary>
    /// For Weapon controle GameObject Active
    /// it take's true and false as per activate or not 
    /// </summary>
    /// <param name="isTrue"></param>
    public void Enable_Weapon_Controle(bool isTrue)
    {
        Weapon_Controle.SetActive(isTrue);
    }


    /// <summary>
    /// It Change Sensitivity after change in pause menu
    /// </summary>
    public void SensitivityOnChanged()
    {
        PR.Sensitivity = Sensitivity.value;
    }


    public void Pause_button()
    {
        pause = !pause;
        Pause.SetActive(!pause);
        Pause_Menu.SetActive(pause);
    }


    /// <summary>
    /// For making Controle on GUI buttons 
    /// does it should show or not 
    /// </summary>
    /// <param name="Name"></param>
    /// <param name="AnotherName"></param>
    public void ChangeControle_GUI(string Name, string AnotherName)
    {
        for(int i = 0; i < GUI_GameObject.Count; i++)
        {
            if (GUI_GameObject[i].name == Name)
                GUI_GameObject[i].SetActive(true);
            else if (GUI_GameObject[i].name == AnotherName)
                GUI_GameObject[i].SetActive(true);
            else
                GUI_GameObject[i].SetActive(false);
        }
    }


    /// <summary>
    /// Applying Brak in Vahical
    /// </summary>
    /// <param name="X"></param>
    public void Break(bool X)
    {
        isBreakApplied = X;
    }
}
