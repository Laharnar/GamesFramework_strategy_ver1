using System;
using System.Collections.Generic;
using UnityEngine;
public class AgentGroup:MonoBehaviour {
    public static Dictionary<string, AgentGroup> groups = new Dictionary<string, AgentGroup>();

    
    public float alignmentWeight = 1f;
    public float cohesionWeight = 1f;
    public float separationWeight = 1f;
    public float effectDistance = float.MaxValue;
    public List<Agent> agentArray = new List<Agent>();


    private void Update() {
        for (int i = 0; i < agentArray.Count; i++) {
            agentArray[i].alignmentWeight = alignmentWeight;
            agentArray[i].cohesionWeight = cohesionWeight;
            agentArray[i].separationWeight = separationWeight;

        }
    }
}

public class Agent : MonoBehaviour {
    public CommandData data;
    public float speed = 10f;

    public float alignmentWeight = 1f;
    public float cohesionWeight = 1f;
    public float separationWeight = 1f;
    public AgentGroup groupData;

    private void Start() {
        StringData faction = GetComponent<FactionAccess>().faction;

        MakeNewAgentGroup(faction);
    }

    private void MakeNewAgentGroup(StringData faction) {
        AgentGroup group;
        if (!AgentGroup.groups.ContainsKey("AGENTS_" + faction.GetValue())) {
            GameObject go = new GameObject("AGENTS_" + faction.GetValue());
            group = go.AddComponent<AgentGroup>();
            AgentGroup.groups.Add("AGENTS_" + faction.GetValue(), group);
        } else {
            group = AgentGroup.groups["AGENTS_" + faction.GetValue()];
        }
        group.agentArray.Add(this);
        groupData = group;
    }

    void Update() {
        Agent agent = this;
        var alignment = ComputeAlignment(agent, groupData.effectDistance);
        var cohesion = ComputeCohesion(agent, groupData.effectDistance);
        var separation = ComputeSeparation(agent, groupData.effectDistance);

        Vector3 move = new Vector3(alignment.x + cohesion.x + separation.x,
            alignment.y + cohesion.y + separation.y,
            alignment.z + cohesion.z + separation.z);
        //agent.velocity.x += alignment.x + cohesion.x + separation.x;
        //agent.velocity.y += alignment.y + cohesion.y + separation.y;

        //agent.velocity.normalize* AGENT_SPEED;

        move = speed * new Vector3(alignment.x * alignmentWeight + cohesion.x * cohesionWeight + separation.x * separationWeight,
            alignment.y * alignmentWeight + cohesion.y * cohesionWeight + separation.y * separationWeight,
            alignment.z * alignmentWeight + cohesion.z * cohesionWeight + separation.z * separationWeight) *Time.deltaTime;

        agent.transform.Translate(move);
        //agent.velocity.x += alignment.x * alignmentWeight + cohesion.x * cohesionWeight + separation.x * separationWeight;
        //agent.velocity.y += alignment.y * alignmentWeight + cohesion.y * cohesionWeight + separation.y * separationWeight;
    }

    public Vector3 ComputeAlignment(Agent myAgent,float dist) {
        // compute direction fix
        List<Agent> agentArray = myAgent.groupData.agentArray;
        Vector3 v = new Vector3();
        var neighborCount = 0;

        foreach (Agent agent in agentArray) {
            if (agent != myAgent) {
                if (Vector3.Distance(myAgent.transform.position, agent.transform.position) < dist) {
                    v.x += data.activeDir.dir.x;// agent.velocity.x;
                    v.y += data.activeDir.dir.y;// agent.velocity.y;
                    v.z += data.activeDir.dir.z;// agent.velocity.y;
                    neighborCount++;
                }
            }
        }

        if (neighborCount == 0)
            return v;

        v.x /= neighborCount;
        v.y /= neighborCount;
        v.z /= neighborCount;
        v = v.normalized;
        return v;
    }
    public Vector3 ComputeCohesion(Agent myAgent, float dist) {
        // compute center vector
        List<Agent> agentArray = myAgent.groupData.agentArray;
        Vector3 v = new Vector3();
        var neighborCount = 0;

        foreach (Agent agent in agentArray) {
            if (agent != myAgent) {
                if (Vector3.Distance(myAgent.transform.position, agent.transform.position) < dist) {
                    v.x += agent.transform.position.x;
                    v.y += agent.transform.position.y;
                    v.z += agent.transform.position.z;
                    neighborCount++;
                }
            }
        }

        if (neighborCount == 0)
            return v;

        v.x /= neighborCount;
        v.y /= neighborCount;
        v.z /= neighborCount;
        v = new Vector3(v.x - myAgent.transform.position.x, v.y - myAgent.transform.position.y, v.z - myAgent.transform.position.z);
        v = v.normalized;
        return v;
    }
    public Vector3 ComputeSeparation(Agent myAgent, float dist) {
        // compute away vector
        List<Agent> agentArray = myAgent.groupData.agentArray;
        Vector3 v = new Vector3();
        var neighborCount = 0;

        foreach (Agent agent in agentArray) {
            if (agent != myAgent) {
                if (Vector3.Distance(myAgent.transform.position, agent.transform.position) < 300) {
                    v.x += agent.transform.position.x - myAgent.transform.position.x;
                    v.y += agent.transform.position.y - myAgent.transform.position.y;
                    v.z += agent.transform.position.z - myAgent.transform.position.z;
                    neighborCount++;
                }
            }
        }

        if (neighborCount == 0)
            return v;

        v.x *= -1;
        v.y *= -1;
        v.z *= -1;
        v = new Vector3(v.x - myAgent.transform.position.x, v.y - myAgent.transform.position.y, v.z - myAgent.transform.position.z);
        v = v.normalized;
        return v;
    }

}