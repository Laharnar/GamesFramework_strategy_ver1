using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public abstract class SOTree : ScriptableObject {
    public static object source;
    public abstract NodeResult Execute();
    public bool isActive = true;

}