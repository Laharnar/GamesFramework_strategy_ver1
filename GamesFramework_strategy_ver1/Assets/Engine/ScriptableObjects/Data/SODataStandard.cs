/// <summary>
/// Base class for different scriptable obejct data types, like floats, ints game objects, etc.
/// For example if you need arrays in scriptable objects, or on components, just make a new class derived from this class.
/// Editors have to be written for subclasses.
/// Can be extended to save into file.
/// </summary>
public abstract class SODataStandard :UnityEngine.ScriptableObject{

    public abstract object GetValue();// optionally implement get value to always return first var, via reflection, unsafe
}