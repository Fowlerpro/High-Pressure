using UnityEngine;
[ExecuteAlways]
public class HallwayTilesExec : MonoBehaviour
{

    public WallGroup walls = new WallGroup();
    public CornerGroup corners = new CornerGroup();
    public PropGroup props = new PropGroup();
    public floorOptions floor;
    public ceilingOptions ceiling;
    private void OnValidate()
    {
        //when the check box is enabled or disabled run update walls which should delete walls when something is off
#if UNITY_EDITOR
        UnityEditor.EditorApplication.delayCall -= UpdateWalls;
        UnityEditor.EditorApplication.delayCall += UpdateWalls;
#endif
    }
    //Walls
    [System.Serializable]
    public class WallGroup
    {
        public leftWallOptions leftWall;
        public rightWallOptions rightWall;
        public northWallOptions northWall;
        public southWallOptions southWall;
    }
    //Corners
    [System.Serializable]
    public class CornerGroup
    {
        public leftCornerOptions leftCorner;
        public rightCornerOptions rightCorner;
        public northCornerOptions northCorner;
        public southCornerOptions southCorner;
    }
    //Props
    [System.Serializable]
    public class PropGroup
    {
        public northPropOptions northProp;
        public rightPropOptions rightProp;
        public leftPropOptions leftProp;
        public southPropOptions southProp;
        public ceilingPropOptions ceilingProp;
    }
    private void UpdateWalls()
    {
        if (this == null || Application.isPlaying)
            return;
        //If statement to stop objects from spawning in other scenes
#if UNITY_EDITOR
    if (UnityEditor.EditorUtility.IsPersistent(this))
        return;

    if (UnityEditor.SceneManagement.PrefabStageUtility.GetCurrentPrefabStage() != null)
        return;

    if (!gameObject.scene.IsValid() || !gameObject.scene.isLoaded)
        return;
#endif
        //Left Wall
        if (walls.leftWall != null)
        {
            if (walls.leftWall.enabled)
                walls.leftWall.Spawn();
            else
                walls.leftWall.RemoveWall();
        }

        //Right Wall
        if (walls.rightWall != null)
        {
            if (walls.rightWall.enabled)
                walls.rightWall.Spawn();
            else
                walls.rightWall.RemoveWall();
        }

        //NorthWall
        if (walls.northWall != null)
        {
            if (walls.northWall.enabled)
                walls.northWall.Spawn();
            else
                walls.northWall.RemoveWall();
        }
        //SouthWall
        if (walls.southWall != null)
        {
            if (walls.southWall.enabled)
                walls.southWall.Spawn();
            else
                walls.southWall.RemoveWall();
        }

        //Floor
        if (floor != null)
        {
            if (floor.enabled)
                floor.Spawn();
            else
                floor.RemoveFloor();
        }
        //Ceiling
        if (ceiling != null)
        {
            if (ceiling.enabled)
                ceiling.Spawn();
            else
                ceiling.RemoveCeiling();
        }
        //Left Corner
        if (corners.leftCorner != null)
        {
            if (corners.leftCorner.enabled)
                corners.leftCorner.Spawn();
            else
                corners.leftCorner.RemoveWall();
        }
        //Right Corner
        if (corners.rightCorner != null)
        {
            if (corners.rightCorner.enabled)
                corners.rightCorner.Spawn();
            else
                corners.rightCorner.RemoveWall();
        }
        //North Corner
        if (corners.northCorner != null)
        {
            if (corners.northCorner.enabled)
                corners.northCorner.Spawn();
            else
                corners.northCorner.RemoveWall();
        }
        //SouthCorner
        if (corners.southCorner != null)
        {
            if (corners.southCorner.enabled)
                corners.southCorner.Spawn();
            else
                corners.southCorner.RemoveWall();
        }
        //NorthProp
        if (props.northProp != null)
        {
            if (props.northProp.enabled)
                props.northProp.Spawn();
            else
                props.northProp.RemoveProp();
        }
        //SouthProp
        if (props.southProp != null)
        {
            if (props.southProp.enabled)
                props.southProp.Spawn();
            else
                props.southProp.RemoveProp();
        }
        //LeftProp
        if (props.leftProp != null)
        {
            if (props.leftProp.enabled)
                props.leftProp.Spawn();
            else
                props.leftProp.RemoveProp();
        }

        //RightProp
        if (props.rightProp != null)
        {
            if (props.rightProp.enabled)
                props.rightProp.Spawn();
            else
                props.rightProp.RemoveProp();
        }
        //CeilingProp
        if (props.ceilingProp != null)
        {
            if (props.ceilingProp.enabled)
                props.ceilingProp.Spawn();
            else
                props.ceilingProp.RemoveProp();
        }
    }
    //Left Wall Settings
    [System.Serializable]
    public class leftWallOptions
    {
        public bool enabled = true;
        public Transform leftWallSpot;

        public GameObject leftPrefab;
        [SerializeField, HideInInspector]
        public GameObject newLeftWall;
        public void Spawn()
        {
            if (newLeftWall != null)
                return;
            if (leftPrefab == null || leftWallSpot == null)
                return;
            if (leftWallSpot.childCount > 0)
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
    //Right Wall Settings
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
            if (rightWallSpot.childCount > 0)
                return;
            //Debug.Log("spawning Right wall");
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
    //North Wall Settings
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
            if (northWallSpot.childCount > 0)
                return;
            //Debug.Log("spawning North wall");
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
    //South Wall Settings
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
            if (southWallSpot.childCount > 0)
                return;
            //Debug.Log("spawning South wall");
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
    //FloorSettings
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
            if (floorSpot.childCount > 0)
                return;
            //Debug.Log("spawning Floor");
            newFloor = Instantiate(floorPrefab, floorSpot);

            newFloor.transform.localPosition = Vector3.zero;
            newFloor.transform.localRotation = Quaternion.identity;
        }
        public void RemoveFloor()
        {
            if (newFloor == null)
                return;

            Debug.Log("Removing Floor");

            DestroyImmediate(newFloor);
            newFloor = null;
        }
    }
    //CeilingSettings
    [System.Serializable]
    public class ceilingOptions
    {
        public bool enabled = true;
        public Transform ceilingSpot;

        public GameObject ceilingPrefab;
        public GameObject newCeiling;
        public void Spawn()
        {
            if (newCeiling != null)
                return;
            if (ceilingPrefab == null || ceilingSpot == null)
                return;
            if (ceilingSpot.childCount > 0)
                return;
            //Debug.Log("Spawning Ceiling");
            newCeiling = Instantiate(ceilingPrefab, ceilingSpot);

            newCeiling.transform.localPosition = Vector3.zero;
            newCeiling.transform.localRotation = Quaternion.identity;
        }
        public void RemoveCeiling()
        {
            if (newCeiling == null)
                return;

            Debug.Log("Removing Ceiling");

            DestroyImmediate(newCeiling);
            newCeiling = null;
        }
    }
    //Left Corner Settings
    [System.Serializable]
    public class leftCornerOptions
    {
        public bool enabled = true;
        public Transform leftCornerSpot;

        public GameObject leftCornerPrefab;
        public GameObject newLeftCorner;
        public void Spawn()
        {
            if (newLeftCorner != null)
                return;
            if (leftCornerPrefab == null || leftCornerSpot == null)
                return;
            if (leftCornerSpot.childCount > 0)
                return;
            //Debug.Log("spawning Left Corner");
            newLeftCorner = Instantiate(leftCornerPrefab, leftCornerSpot);

            newLeftCorner.transform.localPosition = Vector3.zero;
            newLeftCorner.transform.localRotation = Quaternion.identity;
        }
        public void RemoveWall()
        {
            if (newLeftCorner == null)
                return;

            Debug.Log("Removing Left Corner wall");

            DestroyImmediate(newLeftCorner);
            newLeftCorner = null;
        }
    }
    //Right Corner Settings
    [System.Serializable]
    public class rightCornerOptions
    {
        public bool enabled = true;
        public Transform rightCornerSpot;

        public GameObject rightCornerPrefab;
        public GameObject newRightCorner;
        public void Spawn()
        {
            if (newRightCorner != null)
                return;
            if (rightCornerPrefab == null || rightCornerSpot == null)
                return;
            if (rightCornerSpot.childCount > 0)
                return;
            //Debug.Log("spawning Right Corner");
            newRightCorner = Instantiate(rightCornerPrefab, rightCornerSpot);

            newRightCorner.transform.localPosition = Vector3.zero;
            newRightCorner.transform.localRotation = Quaternion.identity;
        }
        public void RemoveWall()
        {
            if (newRightCorner == null)
                return;

            Debug.Log("Removing Right Corner wall");

            DestroyImmediate(newRightCorner);
            newRightCorner = null;
        }
    }
    //North Corner Settings
    [System.Serializable]
    public class northCornerOptions
    {
        public bool enabled = true;
        public Transform northCornerSpot;

        public GameObject northCornerPrefab;
        public GameObject newNorthCorner;
        public void Spawn()
        {
            if (newNorthCorner != null)
                return;
            if (northCornerPrefab == null || northCornerSpot == null)
                return;
            if (northCornerSpot.childCount > 0)
                return;
            //Debug.Log("spawning North Corner");
            newNorthCorner = Instantiate(northCornerPrefab, northCornerSpot);

            newNorthCorner.transform.localPosition = Vector3.zero;
            newNorthCorner.transform.localRotation = Quaternion.identity;
        }
        public void RemoveWall()
        {
            if (newNorthCorner == null)
                return;

            Debug.Log("Removing North Corner wall");

            DestroyImmediate(newNorthCorner);
            newNorthCorner = null;
        }
    }
    //South Corner Settings
    [System.Serializable]
    public class southCornerOptions
    {
        public bool enabled = true;
        public Transform southCornerSpot;

        public GameObject southCornerPrefab;
        public GameObject newSouthCorner;
        public void Spawn()
        {
            if (newSouthCorner != null)
                return;
            if (southCornerPrefab == null || southCornerSpot == null)
                return;
            if (southCornerSpot.childCount > 0)
                return;
            //Debug.Log("spawning South Corner");
            newSouthCorner = Instantiate(southCornerPrefab, southCornerSpot);

            newSouthCorner.transform.localPosition = Vector3.zero;
            newSouthCorner.transform.localRotation = Quaternion.identity;
        }
        public void RemoveWall()
        {
            if (newSouthCorner == null)
                return;

            Debug.Log("Removing Right Corner wall");

            DestroyImmediate(newSouthCorner);
            newSouthCorner = null;
        }
    }
    //North Prop Settings
    [System.Serializable]
    public class northPropOptions
    {
        public bool enabled = true;
        public Transform northPropSpot;

        public GameObject northPropPrefab;
        public GameObject newNorthProp;
        public void Spawn()
        {
            if (newNorthProp != null)
                return;
            if (northPropPrefab == null || northPropSpot == null)
                return;
            if (northPropSpot.childCount > 0)
                return;
#if UNITY_EDITOR
if (UnityEditor.EditorUtility.IsPersistent(northPropSpot))
{
    Debug.LogWarning("North Prop Spot is referencing a prefab asset!");
    return;
}
#endif
            Debug.Log("spawning North Prop");
            newNorthProp = Instantiate(northPropPrefab, northPropSpot);

            newNorthProp.transform.localPosition = Vector3.zero;
            newNorthProp.transform.localRotation = Quaternion.identity;
        }
        public void RemoveProp()
        {
            if (newNorthProp == null)
                return;

            Debug.Log("Removing North Prop");

            DestroyImmediate(newNorthProp);
            newNorthProp = null;
        }
    }
    //South Prop Settings
    [System.Serializable]
    public class southPropOptions
    {
        public bool enabled = true;
        public Transform southPropSpot;

        public GameObject southPropPrefab;
        public GameObject newSouthProp;
        public void Spawn()
        {
            if (newSouthProp != null)
                return;
            if (southPropPrefab == null || southPropSpot == null)
                return;
            if (southPropSpot.childCount > 0)
                return;
            //Debug.Log("spawning South Prop");
            newSouthProp = Instantiate(southPropPrefab, southPropSpot);

            newSouthProp.transform.localPosition = Vector3.zero;
            newSouthProp.transform.localRotation = Quaternion.identity;
        }
        public void RemoveProp()
        {
            if (newSouthProp == null)
                return;

            //Debug.Log("Removing South Prop");

            DestroyImmediate(newSouthProp);
            newSouthProp = null;
        }
    }
    //Left Prop Settings
    [System.Serializable]
    public class leftPropOptions
    {
        public bool enabled = true;
        public Transform leftPropSpot;

        public GameObject leftPropPrefab;
        public GameObject newLeftProp;
        public void Spawn()
        {
            if (newLeftProp != null)
                return;
            if (leftPropPrefab == null || leftPropSpot == null)
                return;
            if (leftPropSpot.childCount > 0)
                return;
            //Debug.Log("spawning South Prop");
            newLeftProp = Instantiate(leftPropPrefab, leftPropSpot);

            newLeftProp.transform.localPosition = Vector3.zero;
            newLeftProp.transform.localRotation = Quaternion.identity;
        }
        public void RemoveProp()
        {
            if (newLeftProp == null)
                return;

            //Debug.Log("Removing Left Prop");

            DestroyImmediate(newLeftProp);
            newLeftProp = null;
        }
    }
    //Right Prop Settings
    [System.Serializable]
    public class rightPropOptions
    {
        public bool enabled = true;
        public Transform rightPropSpot;

        public GameObject rightPropPrefab;
        public GameObject newRightProp;
        public void Spawn()
        {
            if (newRightProp != null)
                return;
            if (rightPropPrefab == null || rightPropSpot == null)
                return;
            if (rightPropSpot.childCount > 0)
                return;
            //Debug.Log("spawning Right Prop");
            newRightProp = Instantiate(rightPropPrefab, rightPropSpot);

            newRightProp.transform.localPosition = Vector3.zero;
            newRightProp.transform.localRotation = Quaternion.identity;
        }
        public void RemoveProp()
        {
            if (newRightProp == null)
                return;

            //Debug.Log("Removing Right Prop");

            DestroyImmediate(newRightProp);
            newRightProp = null;
        }
    }
    //ceiling Prop Settings
    [System.Serializable]
    public class ceilingPropOptions
    {
        public bool enabled = true;
        public Transform ceilingPropSpot;

        public GameObject ceilingPropPrefab;
        public GameObject newCeilingProp;
        public void Spawn()
        {
            if (newCeilingProp != null)
                return;
            if (ceilingPropPrefab == null || ceilingPropSpot == null)
                return;
            if (ceilingPropSpot.childCount > 0)
                return;
            //Debug.Log("spawning Right Prop");
            newCeilingProp = Instantiate(ceilingPropPrefab, ceilingPropSpot);

            newCeilingProp.transform.localPosition = Vector3.zero;
            newCeilingProp.transform.localRotation = Quaternion.identity;
        }
        public void RemoveProp()
        {
            if (newCeilingProp == null)
                return;

            //Debug.Log("Removing Ceiling Prop");

            DestroyImmediate(newCeilingProp);
            newCeilingProp = null;
        }
    }
}
