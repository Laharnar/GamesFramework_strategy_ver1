using UnityEngine;
/// <summary>
/// Aims sub objects at point. Use on rotating turrets.
/// </summary>
public class AxisSubMovement:MonoBehaviour {

    public Transform xRot, yRot, zRot;
    [HideInInspector] public Vector3 targetPoint;
    public Vector3 targetDir = Vector3.one;
    public Transform target;

    private void Update() {
        Vector3 targetPos = transform.position+ targetDir;
        //Vector3 dir = targetDir;//targetPos - transform.position;//targetDir;
        //targetPoint = transform.position + dir;
        if (xRot) {// untested
            Vector3 pos1 = targetPos;
            pos1.x = xRot.position.x;
            Vector3 xDir = pos1 - xRot.position;
            xRot.right = xDir;
            xRot.transform.localRotation = Quaternion.Euler(xRot.transform.localEulerAngles.x, 0, 0);
        }
        if (yRot) {
            Vector3 pos1 = targetPos;
            pos1.y = yRot.position.y;
            Vector3 yDir = pos1 - yRot.position;
            yRot.right = yDir;
            yRot.transform.localRotation = Quaternion.Euler(0, yRot.transform.localEulerAngles.y, 0);
        }
        if (zRot) {
            Vector3 pos1 = targetPos;
            //pos1.z = transform.position.z;
            Vector3 zDir = pos1 - zRot.position;
            zRot.right = zDir;
            zRot.transform.localRotation = Quaternion.Euler(0, 0, zRot.transform.localEulerAngles.z);
        }
    }

}