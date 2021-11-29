using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeviationShootingControl
{
    public static Vector3 GetAfterPos(Vector3 attackerPos,Vector3 targetPos,Vector3 beforePos, float shotSpeed)
    {
        Vector3 currentMoveSpeed = targetPos - beforePos;
        float TargetDistance = Vector3.Distance(attackerPos, targetPos);
        return targetPos + currentMoveSpeed.normalized * TargetDistance / (shotSpeed / Time.deltaTime);
    }
    // 円形予測射撃
    public static Vector3 CirclePrediction(Vector3 attackerPos, Vector3 targetPos, Vector3 beforePos, Vector3 beforePos2, float shotSpeed)
    {
        //3点の角度変化が小さい場合は線形予測に切り替え
        if (Mathf.Abs(Vector3.Angle(targetPos - beforePos, beforePos - beforePos2)) < 0.03)
        {
            return GetAfterPos(attackerPos, targetPos, beforePos, shotSpeed);
        }

        //Unityの物理はm/sなのでm/flameにする
        shotSpeed = shotSpeed * Time.fixedDeltaTime;

        //１、3点から円の中心点を出す
        Vector3 CenterPosition = Circumcenter(targetPos, beforePos, beforePos2);

        //２、中心点から見た1フレームの角速度と軸を出す
        Vector3 axis = Vector3.Cross(beforePos - CenterPosition, targetPos - CenterPosition);
        float angle = Vector3.Angle(beforePos - CenterPosition, targetPos - CenterPosition);

        //３、現在位置で弾の到達時間を出す
        float PredictionFlame = Vector3.Distance(targetPos, attackerPos) / shotSpeed;

        //４、到達時間分を移動した予測位置で再計算して到達時間を補正する。
        for (int i = 0; i < 3; ++i)
        {
            PredictionFlame = Vector3.Distance(RotateToPosition(targetPos, CenterPosition, axis, angle * PredictionFlame), attackerPos) / shotSpeed;
        }

        return RotateToPosition(targetPos, CenterPosition, axis, angle * PredictionFlame);
    }

    //三角形の頂点三点の位置から外心の位置を返す
    public static Vector3 Circumcenter(Vector3 posA, Vector3 posB, Vector3 posC)
    {
        //三辺の長さの二乗を出す
        float edgeA = Vector3.SqrMagnitude(posB - posC);
        float edgeB = Vector3.SqrMagnitude(posC - posA);
        float edgeC = Vector3.SqrMagnitude(posA - posB);

        //重心座標系で計算する
        float a = edgeA * (-edgeA + edgeB + edgeC);
        float b = edgeB * (edgeA - edgeB + edgeC);
        float c = edgeC * (edgeA + edgeB - edgeC);

        if (a + b + c == 0) return (posA + posB + posC) / 3;//0割り禁止
        return (posA * a + posB * b + posC * c) / (a + b + c);
    }

    //目標位置をセンター位置で軸と角度で回転させた位置を返す
    public static Vector3 RotateToPosition(Vector3 v3_target, Vector3 v3_center, Vector3 v3_axis, float f_angle)
    {
        return Quaternion.AngleAxis(f_angle, v3_axis) * (v3_target - v3_center) + v3_center;
    }
}
