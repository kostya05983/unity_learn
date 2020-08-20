using UnityEngine;

[ExecuteInEditMode]
[SelectionBase]
public class CubeEditor : MonoBehaviour
{
    [SerializeField] [Range(1f, 20f)] float gridSize = 10f;


    private TextMesh _textMesh;

    void Start()
    {
    }

    void Update()
    {
        Vector3 snapPos;


        snapPos.x = Mathf.RoundToInt(transform.position.x / gridSize) * gridSize;
        snapPos.y = Mathf.RoundToInt(transform.position.y / gridSize) * gridSize;

        snapPos.z = Mathf.RoundToInt(transform.position.z / gridSize) * gridSize;
        transform.position = new Vector3(snapPos.x, snapPos.y, snapPos.z);

        _textMesh = GetComponentInChildren<TextMesh>();
        var labelText = snapPos.x / gridSize + "," + snapPos.z / gridSize;
        _textMesh.text = labelText;
        gameObject.name = labelText;
    }
}