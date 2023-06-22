using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;
using JoyconLib;

public class DataCollector : MonoBehaviour
{
    private static readonly Joycon.Button[] buttons =
        Enum.GetValues( typeof( Joycon.Button ) ) as Joycon.Button[];

    private List<Joycon>    joycons;
    private Joycon          joyconL;
    private Joycon          joyconR;
    [SerializeField] private int mode;

    private StreamWriter sw;


    void Start()
    {
        joycons = JoyconManager.Instance.j;

        if ( joycons == null || joycons.Count <= 0 ) return;

        joyconL = joycons.Find( c =>  c.isLeft );
        joyconR = joycons.Find( c => !c.isLeft );

        if (joyconL == null)
        {
            Debug.Log("Couldn't find JoyconL");
        }
        if (joyconR == null)
        {
            Debug.Log("Couldn't find JoyconR");
        }

        mode = 1;
        sw = new StreamWriter(@"VectorData.csv", false, Encoding.GetEncoding("Shift_JIS"));
    }

    void Update()
    {
        if (joyconR.GetButtonDown(Joycon.Button.DPAD_RIGHT))
        {
            AddVectorData();
        }

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            mode = 1; // shoulder
            Debug.Log(mode);
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            mode = 2; // sleeve
            Debug.Log(mode);
        }

        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            mode = 3; // girth
            Debug.Log(mode);
        }

        if (Input.GetKeyDown(KeyCode.Return))
        {
            sw.Close();
            Debug.Log("Saved!");
        }
    }

    void AddVectorData()
    {
        Quaternion q = joyconR.GetVector();
        string[] add_str_list = new string[5];
        add_str_list[0] = q.w.ToString();
        add_str_list[1] = q.x.ToString();
        add_str_list[2] = q.y.ToString();
        add_str_list[3] = q.z.ToString();
        add_str_list[4] = mode.ToString();
        string add_str = string.Join(",", add_str_list);
        sw.WriteLine(add_str);
        Debug.Log(add_str);
    }
}
