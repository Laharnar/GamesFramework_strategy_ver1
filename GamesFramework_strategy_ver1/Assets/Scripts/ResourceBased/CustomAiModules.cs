using System;
using UnityEngine;
public class CustomAiModules:MonoBehaviour {

    [ContextMenu("Fighter ai")]
    private void FighterAi() {
        /* fly out, fly on circled path around mothership
         * if target exists, fly to it
         * if in front of target, shoot
         * 
         * */
        Selector root = MkInstance<Selector>();
        Sequence attackTargetBehaviour = MkInstance<Sequence>();
        attackTargetBehaviour.Add(MkInstance<MoveToEnemy>());
        attackTargetBehaviour.Add(MkInstance<InTargetRange>());
        attackTargetBehaviour.Add(MkInstance<SimpleFiring>());

        Sequence followMothershipBehaviour = MkInstance<Sequence>();
        SpawnerTargetCircleMotion pathAroundMothership = MkInstance<SpawnerTargetCircleMotion>();
        followMothershipBehaviour.Add(pathAroundMothership);

        root.Add(attackTargetBehaviour);
        root.Add(followMothershipBehaviour);
        root.name = "FighterMod";
    }

    private T MkInstance<T>() where T:ScriptableObject {
        return ScriptableObject.CreateInstance<T>();
    }
}
