using System;
using System.Collections.Generic;
using UnityEngine;



public interface IBoid {
    Vector3 getVelocity() ;
    float getMaxVelocity() ;
    Vector3 getPosition() ;
    float getMass() ;
}
public enum SteeringMode {
    Seek,
    Flee,
    Arrival,
    Pursuit,
    Evade,
    Wander
}

public partial class SteeringMovement:MonoBehaviour, IBoid {
    public CommandData data;
    public float speed = 10f;

    public Vector3 velocity;
    public float mass = 1;

    // seek vars
    public AITargeter target;

    // wander vars
    const float CIRCLE_DISTANCE = 4;
    const float CIRCLE_RADIUS = 2;
    const float ANGLE_CHANGE = 10;
    Vector3 wanderAngleEuler = new Vector3(0, 0, 0);

    //public SteeringMode mode = SteeringMode.Pursuit;
    Vector3 steering = new Vector3();

    /// <summary>
    /// Executes a single steering calculation
    /// </summary>
    /// <param name="mode"></param>
    public void AttachMove(SteeringMode mode) {
        switch (mode) {
            case SteeringMode.Seek:
                steering += Seek(target.transform.position);
                break;
            case SteeringMode.Flee:
                steering += Flee(target.transform.position);
                break;
            case SteeringMode.Arrival:
                steering += Arrival(target.transform.position, 10);
                break;
            case SteeringMode.Pursuit:
                steering += Pursuit(target.GetComponent<SteeringMovement>());
                break;
            case SteeringMode.Evade:
                steering += Evade(target.GetComponent<SteeringMovement>());
                break;
            case SteeringMode.Wander:
                steering += Wander();
                break;
            default:
                break;
        }
    }
    private void Update() {
        steering = Vector3.zero;//= Seek(target.transform.position); // assuming the character is seeking something
        steering = steering + PathFollowing();


        UpdateSteering();
    }

    public void UpdateSteering() {
        //velocity = Seek(target.position);
        
        steering = Truncate(steering, speed);
        steering = steering / mass;
        velocity = Truncate(velocity + steering, speed);

        transform.forward = velocity;
        transform.Translate(velocity * Time.deltaTime);
        steering = new Vector3();
    }

    Vector3 Seek(Vector3 target) {
        Vector3 desired_velocity = (target - transform.position).normalized *speed;
        Vector3 steering = desired_velocity - velocity;
        Debug.Log(steering);
        return steering;
    }

    Vector3 Flee(Vector3 target) {
        Vector3 desired_velocity = (transform.position- target).normalized * speed;
        Vector3 steering = desired_velocity - velocity;
        return steering;
    }

    Vector3 Arrival(Vector3 target, float slowingRadius) {
        Vector3 desired_velocity = (target - transform.position).normalized * speed;
        Vector3 steering = desired_velocity - velocity;

        // Calculate the desired velocity
        desired_velocity = target - transform.position;
        float distance = desired_velocity.magnitude;

        // Check the distance to detect whether the character
        // is inside the slowing area
        if (distance < slowingRadius) {
            // Inside the slowing area
            desired_velocity = desired_velocity.normalized * speed * (distance / slowingRadius);
        } else {
            // Outside the slowing area.
            desired_velocity = desired_velocity.normalized * speed;
        }

        steering = desired_velocity - velocity;

        return steering;
    }


    Vector3 Pursuit(SteeringMovement target) {
        float distanceBetweenTargetAndPursuer = Vector3.Distance(transform.position, target.transform.position);
        float T = distanceBetweenTargetAndPursuer / speed;
        Vector3 futurePosition = target.transform.position + target.velocity * T;
        return Seek(futurePosition);
    }


    Vector3 Evade(SteeringMovement target) {
        float distanceBetweenTargetAndPursuer = Vector3.Distance(transform.position, target.transform.position);
        float T = distanceBetweenTargetAndPursuer / speed;
        Vector3 futurePosition = target.transform.position + target.velocity * T;
        return Flee(futurePosition);
    }
    Vector3 Wander() {
        // The CIRCLE_DISTANCE constant below is
        // a number defined somewhere else.
        // The code to calculate the circle center:
        Vector3 circleCenter = velocity;
        circleCenter.Normalize();
        circleCenter*=CIRCLE_DISTANCE;

        Vector3 displacement;
        displacement = new Vector3(0, -1, 0);
        displacement*=CIRCLE_RADIUS;
        // Randomly change the vector direction
        // by making it change its current angle
        displacement = Quaternion.Euler(wanderAngleEuler) *displacement;
        // Change wanderAngle just a bit, so it
        // won't have the same value in the
        // next game frame.
        wanderAngleEuler.x += (UnityEngine.Random.Range(0, ANGLE_CHANGE * 0.5f));
        wanderAngleEuler.y += (UnityEngine.Random.Range(0, ANGLE_CHANGE * 0.5f));
        wanderAngleEuler.z += (UnityEngine.Random.Range(0, ANGLE_CHANGE * 0.5f));

        Vector3 wanderForce;
        wanderForce = circleCenter+(displacement);

        return wanderForce;
    }

    const float MAX_SEE_AHEAD = 20f;
    const float MAX_AVOID_FORCE = 1f;
    public Vector3 CollisionAvoidance() {
        Vector3 ahead = transform.position + velocity.normalized * MAX_SEE_AHEAD;
        float dynamic_length = velocity.magnitude / speed;
        ahead = transform.position + velocity.normalized * dynamic_length;
        //Vector3 avoidance_force = ahead - obstacle_center;
        //avoidance_force = avoidance_force.normalize * MAX_AVOID_FORCE;

        CollisionProxy mostThreatening = findMostThreateningObstacle(ahead);
        Vector3 avoidance = new Vector3(0, 0, 0);

        if (mostThreatening != null) {
            avoidance = ahead - mostThreatening.transform.position;

            avoidance = avoidance.normalized * MAX_AVOID_FORCE;
        } else {
            avoidance = Vector3.zero; // nullify the avoidance force
        }

        return avoidance;
    }

    private CollisionProxy findMostThreateningObstacle(Vector3 ahead) {
        CollisionProxy mostThreatening = null;
        RaycastHit hit;
        if (Physics.Raycast(new Ray(transform.position, ahead), out hit))
            mostThreatening = hit.transform.GetComponent<CollisionProxy>();
        return mostThreatening;
    }
    const float LEADER_BEHIND_DIST=5f;
    Vector3 FollowLeader(SteeringMovement leader) {
        Vector3 tv = leader.velocity;
        Vector3 force = new Vector3();

        // Calculate the ahead point
        tv = tv.normalized * LEADER_BEHIND_DIST;
        Vector3 ahead = leader.transform.position +tv;

        // Calculate the behind point
        tv *= -1;
        Vector3 behind = leader.transform.position +tv;

        // If the character is on the leader's sight, add a force
        // to evade the route immediately.
        if (isOnLeaderSight(leader, ahead)) {
            force = force + Evade(leader);
        }

        // Creates a force to arrive at the behind point
        force = force + Arrival(behind, 20); // 50 is the arrive radius

        // Add separation force
        force = force + Separation(GetComponent<Agent>());

        return force;
    }

    int LEADER_SIGHT_RADIUS = 10;
    private bool isOnLeaderSight(SteeringMovement leader, Vector3 leaderAhead) {
        return Vector3.Distance(leaderAhead, transform.position) <= LEADER_SIGHT_RADIUS || Vector3.Distance(leader.transform.position, transform.position) <= LEADER_SIGHT_RADIUS;
    }

    const int MAX_SEPARATION = 1;
    const float SEPARATION_RADIUS = float.MaxValue;
    public Vector3 Separation(Agent myAgent) {
        // compute away vector
        List<Agent> agentArray = myAgent.groupData.agentArray;
        Vector3 v = new Vector3();
        var neighborCount = 0;

        foreach (Agent agent in agentArray) {
            if (agent != myAgent && Vector3.Distance(myAgent.transform.position, agent.transform.position) < SEPARATION_RADIUS) {
                v.x += agent.transform.position.x - myAgent.transform.position.x;
                v.y += agent.transform.position.y - myAgent.transform.position.y;
                v.z += agent.transform.position.z - myAgent.transform.position.z;
                neighborCount++;
            }
        }


        if (neighborCount != 0) {
            v /= neighborCount;
            v *= -1;
        }
        v = v.normalized * MAX_SEPARATION;

        return v;
    }

    private bool LineIntersectsCircle(Vector3 ahead, Vector3 ahead2, CollisionProxy obstacle) {
        // the property "center" of the obstacle is a Vector3D.
        return Vector3.Distance(obstacle.transform.position, ahead) <= obstacle.GetSize() || 
            Vector3.Distance(obstacle.transform.position, ahead2) <= obstacle.GetSize();
    }

    public Path path;
    int pathDir = 1;
    public int currentNode;

    private Vector3 PathFollowing() {
        Vector3 target;
        if (path != null && path.GetNodes().Count > 0) {
            List<Vector3> nodes = path.GetNodes();

            target = nodes[currentNode];

            if (Vector3.Distance(transform.position, target) <= 10) {
                currentNode += 1;

                if (currentNode >= nodes.Count || currentNode < 0) {
                    pathDir *= -1;
                    currentNode += pathDir;
                    currentNode = Mathf.Clamp(currentNode, 0, nodes.Count-1);
                }
            }
            return Seek(target);
        }

        return new Vector3();
    }

    public Vector3 SetAngle(Vector3 vector , float value){
        float len = vector.magnitude;
        vector.x = Mathf.Cos(value) * len;
        vector.y = Mathf.Sin(value) * len;
        return vector;
    }

    private Vector3 Truncate(Vector3 vector3, float speed) {
        /*float i = speed / vector3.magnitude;
        i = i < 1.0f ? i : 1.0f;
        return vector3.normalized * speed * i;*/
        if (vector3.magnitude > speed) {
            return vector3.normalized * speed;
        }
        return vector3;
    }

    private void OnDrawGizmosSelected() {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(transform.position, velocity);
        
    }
    private void OnDrawGizmos() {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, 1);
    }

    public Vector3 getVelocity() {
        return velocity;
    }

    public float getMaxVelocity() {
        return speed;
    }

    public Vector3 getPosition() {
        return transform.position;
    }

    public float getMass() {
        return mass;
    }
}

public partial class SteeringMovement {
    int MAX_QUEUE_AHEAD = 5;
    int MAX_QUEUE_RADIUS = 5;
    private SteeringMovement getNeighborAhead() {
        Vector3 ahead = transform.position + velocity.normalized * MAX_SEE_AHEAD;

        int i;
        SteeringMovement ret = null;
        Vector3 qa = velocity;

        qa = qa.normalized * MAX_QUEUE_AHEAD;
     
        ahead = transform.position+qa;

        /*for (i = 0; i<allunits; i++) {
            //SteeringMovement neighbor = FactionUnits.FindClosestEnemy(target).GetComponent<SteeringMovement>();
            float d = Vector3.Distance(ahead, neighbor.transform.position);
         
            if (neighbor != this && d <= MAX_QUEUE_RADIUS) {
                ret = neighbor;
                break;
            }
        }
     */
        Debug.Log("Not implemented");
        return ret;
    }

    private Vector3 Queue() {
        Vector3 v = velocity;
        Vector3 brake  = new Vector3();
        SteeringMovement neighbor = getNeighborAhead();
 
        if (neighbor != null) {
            // velocity *= 0.3f;
            brake.x = -steering.x * 0.8f;
            brake.y = -steering.y * 0.8f;

            v *= -1;
            brake = brake + v;
            brake = brake +Separation(GetComponent<Agent>());
            if (distance(position, neighbor.position) <= MAX_QUEUE_RADIUS) {
                velocity.scaleBy(0.3);
            }
        }
     
        return new Vector3(0, 0);
    }

    Vector3 QueueUpdate() {
        Vector3 steering = Seek(new Vector3()); // seek the doorway
        steering = steering + CollisionAvoidance(); // avoid obstacles
        steering = steering + Queue(); // queue along the way
        return steering,
    }
}

public class SteeringManager {
    public Vector3 steering;
    public IBoid host;
 
    public SteeringManager(IBoid host) {
        this.host = host;
        this.steering = new Vector3(0, 0);
    }

    // The public API (one method for each behavior)
    public void seek(Vector3 target, float slowingRadius= 20) {}
    public void flee(Vector3 target) {}
 
    public void wander() {}
    public void evade(IBoid target) {}
    public void pursuit(IBoid target) {}
 
    // The update method. 
    // Should be called after all behaviors have been invoked
    public void update(){}
 
    // Reset the internal steering force.
    public void reset() {}
 
    // The internal API
    /*private Vector3 doSeek(Vector3 target, float slowingRadius  = 0) {}
    private Vector3 doFlee(Vector3 target) {}
    private Vector3 doWander() {}
    private Vector3 doEvade(IBoid target) {}
    private Vector3 doPursuit(IBoid target)  {}*/
}
