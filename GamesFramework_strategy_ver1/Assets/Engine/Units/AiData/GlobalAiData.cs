using System.Collections.Generic;
public class GlobalAiData {

    /// <summary>
    /// Global Ai library with all objects in scene, by ai tag.
    /// </summary>
    public static Dictionary<string, List<TreeBehaviour>> aiLib = new Dictionary<string, List<TreeBehaviour>>();

    public static TreeBehaviour[] GetAIFromLib(string tag) {
        return aiLib[tag].ToArray();
    }

    public static void AddToLib(string asKey, TreeBehaviour targeter) {
        string key = asKey;
        if (!aiLib.ContainsKey(key))
            aiLib.Add(key, new List<TreeBehaviour>());
        aiLib[key].Add(targeter);
    }

    public static AITargeter[] GetAllTargetersByTag(string tag) {
        List<AITargeter> allC = new List<AITargeter>();
        if (aiLib.ContainsKey(tag)) {
            TreeBehaviour[] c = aiLib[tag].ToArray();
            foreach (var item in c) {
                AITargeter[] utl = item.GetAiSourcesByTag(tag);
                allC.AddRange(utl);
            }
        }
        return allC.ToArray();
    }

    internal static AITargeter[] GetAiSourcesByTagUnderRoot(TreeBehaviour source, string tag) {
        List<AITargeter> result = new List<AITargeter>();
        if (aiLib.ContainsKey(tag)) {
            result.AddRange(source.GetAiSourcesByTag(tag));
        }
        return result.ToArray();
    }

}
