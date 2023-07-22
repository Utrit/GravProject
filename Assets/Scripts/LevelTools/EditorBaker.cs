using UnityEngine;
[RequireComponent(typeof(MeshCollider))]
[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]
public class EditorBaker : MonoBehaviour
{
    
    [SerializeField] private GameObject _spawningObject;
    static int cubeLength = 100;
    static float planeSize = 3f;
    private bool[,,] rawMapSpace = new bool[cubeLength, cubeLength, cubeLength];
    // Start is called before the first frame update
    void Start()
    {
        CreateTwoSpheres();
        CreateMesh();
        OptimizeMesh();
    }
    
    void CheckBorderState(Vector3Int posInLevelMap, Vector3Int direction)
    {
        Vector3Int dirPos = posInLevelMap + direction;
        Vector3 placePos = new Vector3(posInLevelMap.x * planeSize + direction.x * planeSize / 2, posInLevelMap.y * planeSize + direction.y * planeSize / 2, posInLevelMap.z * planeSize + direction.z * planeSize / 2);
        bool state = rawMapSpace[posInLevelMap.x, posInLevelMap.y, posInLevelMap.z];
        bool dirState = rawMapSpace[dirPos.x, dirPos.y, dirPos.z];
        if(state && !dirState)
        {
            Instantiate(_spawningObject, placePos, Quaternion.FromToRotation(Vector3.up,-direction), transform);
        }
    }
    void OptimizeMesh()
    {
        MeshFilter[] meshFilters = GetComponentsInChildren<MeshFilter>();
        CombineInstance[] combines = new CombineInstance[meshFilters.Length];
        for(int i = 0; i < meshFilters.Length; i++)
        {
            combines[i].mesh = meshFilters[i].sharedMesh;
            combines[i].transform = meshFilters[i].transform.localToWorldMatrix;
            meshFilters[i].gameObject.SetActive(false);
        }
        MeshFilter meshFilter = GetComponent<MeshFilter>();
        meshFilter.mesh = new Mesh();
        meshFilter.mesh.CombineMeshes(combines);
        meshFilter.mesh.Optimize();
        GetComponent<MeshCollider>().sharedMesh = meshFilter.mesh;
        gameObject.SetActive(true);
    }
    void CreateMesh()
    {
        for (int x = 1; x < cubeLength - 1; x++)
        {
            for (int y = 1; y < cubeLength - 1; y++)
            {
                for (int z = 1; z < cubeLength - 1; z++)
                {
                    Vector3Int posInMap = new Vector3Int(x, y, z);
                    CheckBorderState(posInMap, Vector3Int.up);
                    CheckBorderState(posInMap, -Vector3Int.up);
                    CheckBorderState(posInMap, Vector3Int.right);
                    CheckBorderState(posInMap, -Vector3Int.right);
                    CheckBorderState(posInMap, new Vector3Int(0, 0, 1));
                    CheckBorderState(posInMap, new Vector3Int(0, 0, -1));
                }
            }

        }
    }



    void CreateTwoSpheres()
    {
        for (int x = 0; x < cubeLength; x++)
        {
            for (int y = 0; y < cubeLength; y++)
            {
                for (int z = 0; z < cubeLength; z++)
                {
                    int ix = x - 15;
                    int iy = y - 15;
                    int iz = z - 15;
                    rawMapSpace[x, y, z] = Mathf.Floor(Mathf.Sqrt(ix * ix + iy * iy + iz * iz)) < 15f;
                    if (!rawMapSpace[x, y, z])
                    {
                        ix = ix - 10;
                        iy = iy - 10;
                        iz = iz - 10;
                        rawMapSpace[x, y, z] = Mathf.Floor(Mathf.Sqrt(ix * ix + iy * iy + iz * iz)) < 15f;
                    }
                }
            }
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
