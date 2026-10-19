using UnityEngine;
[ExecuteAlways]
public class HallwayTiles : MonoBehaviour
{

    public leftWallOptions leftWall;
    public rightWallOptions rightWall;
    public northWallOptions northWall;
    public southWallOptions southWall;
    public floorOptions floor;
    private void OnValidate()
    {
        //when the check box is enabled or disabled run update walls which should delete walls when something is off
#if UNITY_EDITOR
        UnityEditor.EditorApplication.delayCall -= UpdateWalls;
        UnityEditor.EditorApplication.delayCall += UpdateWalls;
#endif
    }
    private void UpdateWalls()
    {
        if (this == null || Application.isPlaying)
            return;
        if (leftWall != null)
        {
            if (leftWall.enabled)
                leftWall.Spawn();
            else
                leftWall.RemoveWall();
        }

        if (leftWall.enabled)
            leftWall.Spawn();
        else
            leftWall.RemoveWall();
        if (rightWall != null)
        {
            if (rightWall.enabled)
                rightWall.Spawn();
            else
                rightWall.RemoveWall();
        }

        if (rightWall.enabled)
            rightWall.Spawn();
        else
            rightWall.RemoveWall();
        if (northWall != null)
        {
            if (northWall.enabled)
                northWall.Spawn();
            else
                northWall.RemoveWall();
        }

        if (northWall.enabled)
            northWall.Spawn();
        else
            northWall.RemoveWall();
        if (southWall != null)
        {
            if (southWall.enabled)
                southWall.Spawn();
            else
                southWall.RemoveWall();
        }

        if (southWall.enabled)
            southWall.Spawn();
        else
            southWall.RemoveWall();
        if (floor != null)
        {
            if (floor.enabled)
                floor.Spawn();
            else
                floor.RemoveWall();
        }

        if (floor.enabled)
            floor.Spawn();
        else
            floor.RemoveWall();

    }
    [System.Serializable]
    public class leftWallOptions
    {
        public bool enabled = true;
        public Transform leftWallSpot;

        public GameObject leftPrefab;
        [SerializeField, HideInInspector]
        private GameObject newLeftWall;
        public void Spawn()
        {
            if (newLeftWall != null)
                return;
            if (leftPrefab == null || leftWallSpot == null)
                return;
            Debug.Log("spawning left wall");
            newLeftWall = Instantiate(leftPrefab, leftWallSpot);

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
    [System.Serializable]
    public class rightWallOptions
    {
        public bool enabled = true;
        public Transform rightWallSpot;

        public GameObject rightPrefab;
        public GameObject newRightWall;
        public void Spawn()
        {
            if (newRightWall != null)
                return;
            if (rightPrefab == null || rightWallSpot == null)
                return;
            Debug.Log("spawning Right wall");
            newRightWall = Instantiate(rightPrefab, rightWallSpot);

            newRightWall.transform.localPosition = Vector3.zero;
            newRightWall.transform.localRotation = Quaternion.identity;
        }
        public void RemoveWall()
        {
            if (newRightWall == null)
                return;

            Debug.Log("Removing Right wall");

            DestroyImmediate(newRightWall);
            newRightWall = null;
        }
    }
    [System.Serializable]
    public class northWallOptions
    {
        public bool enabled = true;
        public Transform northWallSpot;

        public GameObject northPrefab;
        public GameObject newNorthWall;
        public void Spawn()
        {
            if (newNorthWall != null)
                return;
            if (northPrefab == null || northWallSpot == null)
                return;
            Debug.Log("spawning North wall");
            newNorthWall = Instantiate(northPrefab, northWallSpot);

            newNorthWall.transform.localPosition = Vector3.zero;
            newNorthWall.transform.localRotation = Quaternion.identity;
        }
        public void RemoveWall()
        {
            if (newNorthWall == null)
                return;

            Debug.Log("Removing North wall");

            DestroyImmediate(newNorthWall);
            newNorthWall = null;
        }
    }
    [System.Serializable]
    public class southWallOptions
    {
        public bool enabled = true;
        public Transform southWallSpot;

        public GameObject southPrefab;
        public GameObject newSouthWall;
        public void Spawn()
        {
            if (newSouthWall != null)
                return;
            if (southPrefab == null || southWallSpot == null)
                return;
            Debug.Log("spawning South wall");
            newSouthWall = Instantiate(southPrefab, southWallSpot);

            newSouthWall.transform.localPosition = Vector3.zero;
            newSouthWall.transform.localRotation = Quaternion.identity;
        }
        public void RemoveWall()
        {
            if (newSouthWall == null)
                return;

            Debug.Log("Removing South wall");

            DestroyImmediate(newSouthWall);
            newSouthWall = null;
        }
    }
    [System.Serializable]
    public class floorOptions
    {
        public bool enabled = true;
        public Transform floorSpot;

        public GameObject floorPrefab;
        public GameObject newFloor;
        public void Spawn()
        {
            if (newFloor != null)
                return;
            if (floorPrefab == null || floorSpot == null)
                return;
            Debug.Log("spawning Floor");
            newFloor = Instantiate(floorPrefab, floorSpot);

            newFloor.transform.localPosition = Vector3.zero;
            newFloor.transform.localRotation = Quaternion.identity;
        }
        public void RemoveWall()
        {
            if (newFloor == null)
                return;

            Debug.Log("Removing North wall");

            DestroyImmediate(newFloor);
            newFloor = null;
        }
    }
}