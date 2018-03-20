using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainBehavior : MonoBehaviour
{
    public static LayerMask CharacterLayer = 1 << 10, EnemyLayer = 1 << 8, WallLayer = 1 << 11, BlocksLayer = 1 << 12, TargetLayer = 1 << 9,LeaderLayer=1<<13;

    string PreviousScene;
   public void GoPreviousScene()
    {
       /* print(PreviousScene);

        LoadingScreenManager.Instance.GoToScene(PreviousScene);
        */
    }
    // Update is called once per frame

    public void GoToScene(string SceneName)
    {
        LoadingScreenManager.Instance.GoToScene(SceneName);
    }
    public void OpenScreen()
    {
        LoadingScreenManager.Instance.Open();
    }
    public static void DrawCube(Vector3 position, Quaternion rotation, Vector3 scale)
    {
        Matrix4x4 cubeTransform = Matrix4x4.TRS(position, rotation, scale);
        Matrix4x4 oldGizmosMatrix = Gizmos.matrix;

        Gizmos.matrix *= cubeTransform;

        Gizmos.DrawCube(Vector3.zero, Vector3.one);

        Gizmos.matrix = oldGizmosMatrix;
    }

}
