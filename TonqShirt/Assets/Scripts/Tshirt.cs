using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tshirt : MonoBehaviour
{
    private LineRenderer _lineRend;
    private MeshFilter _meshFilter;
    private Mesh _mesh;
    private MeshRenderer _meshRenderer;
    private List<Vector3> _positions;
    private int[] _triangles;
    private const int _splineNumber = 9;

    [SerializeField] private Model _model;

    [SerializeField] private float _shoulderWidth;
    [SerializeField] private float _girth;
    [SerializeField] private float _sleeveLength;

    public Vector3 neckCenter;
    [SerializeField] private ModelSelectButton _nowButton;

    [SerializeField] private float _neckWidth;
    [SerializeField] private float _sleeveWidth;
    [SerializeField] private float _sideAngle;
    [SerializeField] private float _hight;
    [SerializeField] private float _sleeveInnerOuterRatio;
    [SerializeField] private float _lineWidth;
    [SerializeField] private float _neckSize;


    [SerializeField] private float _maxShoulderWidth;
    [SerializeField] private float _minShoulderWidth;

    [SerializeField] private float _maxGirth;
    [SerializeField] private float _minGirth;

    [SerializeField] private float _maxSleeveLength;
    [SerializeField] private float _minSleeveLength;

    [SerializeField] Slider _shoulderSlider;
    [SerializeField] Slider _girthSlider;
    [SerializeField] Slider _sleeveSlider;



    void Start()
    {
        _lineRend = gameObject.GetComponent<LineRenderer>();
        _positions = new List<Vector3>();
        _mesh = new Mesh();
        _meshFilter = gameObject.GetComponent<MeshFilter>();
        _meshRenderer = gameObject.GetComponent<MeshRenderer>();
        CreateTriangles();
        SetLineRenderer();
        SetButton(_nowButton);
    }

    void Update()
    {
    }

    void SetLineRenderer()
    {
        _lineRend.numCornerVertices = 10;
        _lineRend.startWidth = _lineWidth;
        _lineRend.endWidth = _lineWidth;
    }

    private void DrawTshirt()
    {
        UpdateTshirtPoints();
        _lineRend.positionCount = _positions.Count();
        _lineRend.SetPositions(_positions.ToArray());

        CreateMesh();
    }

    private void UpdateTshirtPoints()
    {
        float sideRadian = _sideAngle / 180 * Mathf.PI;
        float sin = Mathf.Sin(sideRadian);
        float cos = Mathf.Cos(sideRadian);
        _positions = new List<Vector3>();

        Vector3 rightNeck = new Vector3(neckCenter.x + _neckWidth / 2, neckCenter.y);
        Vector3 leftNeck = new Vector3(neckCenter.x - _neckWidth / 2, neckCenter.y);

        Vector3 rightShoulder = new Vector3(neckCenter.x + _shoulderWidth /2, neckCenter.y);
        Vector3 leftShoulder = new Vector3(neckCenter.x - _shoulderWidth /2, neckCenter.y);

        Vector3 outerRightSleeve = new Vector3(rightShoulder.x + _sleeveLength * sin, rightShoulder.y - _sleeveLength * cos);
        Vector3 outerLeftSleeve = new Vector3(leftShoulder.x - _sleeveLength * sin, leftShoulder.y - _sleeveLength * cos);

        Vector3 innerRightSleeve = new Vector3(outerRightSleeve.x - _sleeveWidth * cos, outerRightSleeve.y - _sleeveWidth * sin);
        Vector3 innerLeftSleeve = new Vector3(outerLeftSleeve.x + _sleeveWidth * cos, outerLeftSleeve.y - _sleeveWidth * sin);

        float innerLength = _sleeveInnerOuterRatio * _sleeveLength;
        Vector3 rightSide = new Vector3(innerRightSleeve.x - innerLength * sin, innerRightSleeve.y + innerLength * cos);
        Vector3 leftSide = new Vector3(innerLeftSleeve.x + innerLength * sin, innerLeftSleeve.y + innerLength * cos);

        Vector3 waistCenter = new Vector3(neckCenter.x, neckCenter.y - _hight);
        Vector3 rightWaist = new Vector3(waistCenter.x + _girth / 2, waistCenter.y);
        Vector3 leftWaist = new Vector3(waistCenter.x - _girth / 2, waistCenter.y);

        _positions.Add(rightNeck);
        _positions.Add(rightShoulder);
        _positions.Add(outerRightSleeve);
        _positions.Add(innerRightSleeve);
        _positions.Add(rightSide);
        _positions.Add(rightWaist);
        _positions.Add(leftWaist);
        _positions.Add(leftSide);
        _positions.Add(innerLeftSleeve);
        _positions.Add(outerLeftSleeve);
        _positions.Add(leftShoulder);
        _positions.Add(leftNeck);
        SplineNeck(rightNeck, leftNeck);
    }

    private void SplineNeck(Vector3 rightNeck, Vector3 leftNeck)
    {
        Vector3 belowNeck = neckCenter - new Vector3(0, _neckSize);
        for (int i = 0; i < _splineNumber; i++)
        {
            _positions.Add(CalculateRouteCurve(leftNeck, rightNeck, belowNeck, (float)(i+1)/_splineNumber));
        }
    }

    private Vector3 CalculateRouteCurve(Vector3 start, Vector3 end, Vector3 control, float t)
    {
        Vector3 Q0 = Vector3.Lerp(start, control, t);
        Vector3 Q1 = Vector3.Lerp(control, end, t);
        return Vector3.Lerp(Q0, Q1, t);
    }

    private void CreateTriangles()
    {
        const int triangleNum = 3 * (_splineNumber + 12 - 2);
        _triangles = new int[triangleNum] { 0,1,4, 1,2,3, 1,3,4, 11,7,10, 10,8,9, 10,7,8,
                                            4,20,0, 4,19,20, 4,18,19, 4,17,18, 4,16,17,
                                            7,11,12, 7,12,13, 7,13,14, 7,14,15, 7,15,16,
                                            4,5,16, 6,7,16, 5,6,16
                                            };
    }

    private void CreateMesh()
    {
        _mesh.SetVertices(_positions);
        _mesh.SetTriangles(_triangles, 0);
        _meshFilter.mesh = _mesh;
    }

    public void ToggleMeshVisibility()
    {
        _meshRenderer.enabled = !_meshRenderer.enabled;
    }

    public void OnChangeShoulderWidth()
    {
        _shoulderWidth = Easing(_shoulderSlider.value, _minShoulderWidth, _maxShoulderWidth);
        DrawTshirt();
    }

    public void OnChangeGirth()
    {
        _girth = Easing(_girthSlider.value, _minGirth, _maxGirth);
        DrawTshirt();
    }

    public void OnChangeSleeveLength()
    {
        _sleeveLength = Easing(_sleeveSlider.value, _minSleeveLength, _maxSleeveLength);
        DrawTshirt();
    }

    private static float Easing(float t, float min, float max)
    {
        return max * t + min * (1 - t);
    }

    public void OnMoveModel(bool direction)
    {
        if (direction)
        {
            SetButton(_nowButton.rightButton);
        }
        else
        {
            SetButton(_nowButton.leftButton);
        }
    }

    public void SetButton(ModelSelectButton button)
    {
        _nowButton.SetColor(false);
        _nowButton = button;
        _nowButton.SetColor(true);
        _model.UpdateImage(_nowButton.imgId);
        neckCenter = _nowButton.neckCenter;
        DrawTshirt();
    }
}
