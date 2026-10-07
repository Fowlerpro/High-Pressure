using UnityEngine;
[ExecuteAlways]
public class HallwayTiles : MonoBehaviour
{
    public leftWallOptions leftWall;
    private void OnValidate()
    {
        if (leftWall.enabled)
            leftWall.Spawn();
    }

    [System.Serializable]
    public class leftWallOptions
    {
        public bool enabled = true;
        public Transform leftWallSpot;

        public GameObject leftPrefab;
        public void Spawn()
        {
            Debug.Log("spawning left wall");
            public GameObject newLeftWall = Instantiate(leftPrefab, leftWallSpot);

            newLeftWall.transform.localPosition = Vector3.zero;
            newLeftWall.transform.localRotation = Quaternion.identity;
        }
        public void RemoveWall()
        {
            if (newLeftWall == null)
                return;

            Debug.Log("Removing left wall");

            DestroyImmediate(newLeftWall);
            newLeftWall = null;
        }
    }
