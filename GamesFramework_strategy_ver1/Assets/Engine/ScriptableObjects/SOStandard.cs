using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New unit stat", menuName = "Framework/Units/New unit stat", order = 1)]
public class SOStandard :ScriptableObject{
    public string statName="NoName";
    public SODataStandard data;
}
