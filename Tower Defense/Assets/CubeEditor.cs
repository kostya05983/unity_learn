using UnityEngine;

[ExecuteInEditMode]
[SelectionBase]
[RequireComponent((typeof(WayPoint)))]
public class CubeEditor : MonoBehaviour
{
    private WayPoint _wayPoint;

    private void Awake()
    {
        _wayPoint = GetComponent<WayPoint>();
    }

    void Update()
    {
        SnapToGrid();
        UpdateLabel();
    }

    private void SnapToGrid()
    {
        int gridSize = _wayPoint.GetGridSize();
        transform.position = new Vector3(
            _wayPoint.GetGridPos().x * gridSize,
            0f,
            _wayPoint.GetGridPos().y * gridSize
        );
    }

    private void UpdateLabel()
    {
        TextMesh _textMesh = GetComponentInChildren<TextMesh>();
        var labelText = _wayPoint.GetGridPos().x  + "," + _wayPoint.GetGridPos().y ;
        _textMesh.text = labelText;
        gameObject.name = labelText;
    }
}