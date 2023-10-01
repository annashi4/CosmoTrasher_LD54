using System;
using UnityEngine;

public class GrapplingGun : MonoBehaviour
{
    [Header("Scripts Ref:")]
    public GrapplingRope grappleRope;
    public PickUper _pickUper;
    public LineRenderer _lineRenderer;

    [Header("Layers Settings:")]
    [SerializeField] private bool grappleToAll = false;
    [SerializeField] private int grappableLayerNumber = 9;
    [SerializeField] private int _pickableLayer = 6;
    [SerializeField] private int ignoreLayer = 10;

    [Header("Main Camera:")]
    public Camera m_camera;

    [Header("Transform Ref:")]
    public Transform gunHolder;
    public Transform gunPivot;
    public Transform firePoint;

    [Header("Physics Ref:")]
    public SpringJoint2D m_springJoint2D;
    public Rigidbody2D m_rigidbody;

    [Header("Rotation:")]
    [SerializeField] private bool rotateOverTime = true;
    [Range(0, 60)] [SerializeField] private float rotationSpeed = 4;

    [Header("Distance:")]
    [SerializeField] private bool hasMaxDistance = false;

    private enum LaunchType
    {
        Transform_Launch,
        Physics_Launch
    }

    [Header("Launching:")]
    [SerializeField] private bool launchToPoint = true;
    [SerializeField] private LaunchType launchType = LaunchType.Physics_Launch;
    [SerializeField] private float launchSpeed = 1;
    [SerializeField] private float _forceOnGrab;

    [Header("No Launch To Point")]
    [SerializeField] private bool autoConfigureDistance = false;
    [SerializeField] private float targetDistance = 3;
    [SerializeField] private float targetFrequncy = 1;

    [HideInInspector] public Vector2 grapplePoint;
    [HideInInspector] public Vector2 grappleDistanceVector;
    
    private bool _isPicking;
    private Rigidbody2D _pickingItem;

    public event Action<Rigidbody2D> OnPickingItem;
    public event Action<Rigidbody2D> OnReleaseItem;

    private void Start()
    {
        grappleRope.enabled = false;
        m_springJoint2D.enabled = false;

        m_camera = Camera.main;

        _pickUper.OnPickUp += ReleaseAndPickItem;
        DeathCircle.Instance.OnOut += Release;
    }

    private void ReleaseAndPickItem(bool success, Garbage garbage)
    {
        Release();
        GameCanvas.Instance.GetScreen<PlayerIndicatorsUIScreen>(UIScreenType.INDICATORS).CompleteTimer(!success);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            SetGrapplePoint();
        }
        else if (Input.GetKey(KeyCode.Mouse0))
        {
            if (grappleRope.enabled)
            {
                RotateGun(grapplePoint, false);
            }
            else
            {
                Vector2 mousePos = m_camera.ScreenToWorldPoint(Input.mousePosition);
                RotateGun(mousePos, true);
            }

            if (launchToPoint && grappleRope.isGrappling)
            {
                if (launchType == LaunchType.Transform_Launch)
                {
                    Vector2 firePointDistnace = firePoint.position - gunHolder.localPosition;
                    Vector2 targetPos = grapplePoint - firePointDistnace;
                    gunHolder.position = Vector2.Lerp(gunHolder.position, targetPos, Time.deltaTime * launchSpeed);
                }
            }

            if (_isPicking)
            {
                grapplePoint = _pickingItem.position;
            }
        }
        else if (Input.GetKeyUp(KeyCode.Mouse0))
        {
            Release();
            GameCanvas.Instance.GetScreen<PlayerIndicatorsUIScreen>(UIScreenType.INDICATORS).DeactivateTimer();
        }
        else
        {
            Vector2 mousePos = m_camera.ScreenToWorldPoint(Input.mousePosition);
            RotateGun(mousePos, true);
        }
    }

    private void Release()
    {
        OnReleaseItem?.Invoke(_pickingItem);
        grappleRope.enabled = false;
        m_springJoint2D.enabled = false;
        m_rigidbody.gravityScale = 0;
        _isPicking = false;
        m_springJoint2D.connectedBody = null;
        _pickingItem = null;

        _lineRenderer.enabled = true;
        
        float range = GameData.Instance.AdditionalRange + PlayerStats.Instance.BaseRange;
        _lineRenderer.SetPosition(1, new Vector3(range, 0, 0));
    }

    void RotateGun(Vector3 lookPoint, bool allowRotationOverTime)
    {
        Vector3 distanceVector = lookPoint - gunPivot.position;

        float angle = Mathf.Atan2(distanceVector.y, distanceVector.x) * Mathf.Rad2Deg;
        if (rotateOverTime && allowRotationOverTime)
        {
            gunPivot.rotation = Quaternion.Lerp(gunPivot.rotation, Quaternion.AngleAxis(angle, Vector3.forward), Time.deltaTime * rotationSpeed);
        }
        else
        {
            gunPivot.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }
    }

    void SetGrapplePoint()
    {
        _lineRenderer.enabled = false;
        float range = PlayerStats.Instance.BaseRange + GameData.Instance.AdditionalRange;
        
        Vector2 distanceVector = m_camera.ScreenToWorldPoint(Input.mousePosition) - gunPivot.position;
        if (Physics2D.Raycast(firePoint.position, distanceVector.normalized, range, ~(1 << ignoreLayer)))
        {
            RaycastHit2D grappableHit = Physics2D.Raycast(firePoint.position, distanceVector.normalized, range, ~(1 <<
                ignoreLayer));
            if (grappableHit.transform.gameObject.layer == grappableLayerNumber || grappleToAll)
            {
                if (Vector2.Distance(grappableHit.point, firePoint.position) <= range || !hasMaxDistance)
                {
                    grapplePoint = grappableHit.point;
                    grappleDistanceVector = grapplePoint - (Vector2)gunPivot.position;
                    grappleRope.enabled = true;
                    _isPicking = false;
                    _pickingItem = null;
                }
            }
            else if (grappableHit.transform.gameObject.layer == _pickableLayer)
            {
                if (Vector2.Distance(grappableHit.point, firePoint.position) <= range || !hasMaxDistance)
                {
                    grapplePoint = grappableHit.point;
                    grappleDistanceVector = grapplePoint - (Vector2)gunPivot.position;
                    grappleRope.enabled = true;
                    _isPicking = true;
                    m_springJoint2D.connectedBody = grappableHit.rigidbody;
                    m_springJoint2D.distance = 0;
                    _pickingItem = grappableHit.rigidbody;
                    OnPickingItem?.Invoke(_pickingItem);
                }
            }

            GameManager.Instance.PlayerRB.AddForce(distanceVector * _forceOnGrab);
        }
    }

    public void Grapple()
    {
        m_springJoint2D.autoConfigureDistance = false;
        if (!launchToPoint && !autoConfigureDistance)
        {
            m_springJoint2D.distance = targetDistance;
            m_springJoint2D.frequency = targetFrequncy;
        }
        if (!launchToPoint)
        {
            if (autoConfigureDistance)
            {
                m_springJoint2D.autoConfigureDistance = true;
                m_springJoint2D.frequency = 0;
            }

            m_springJoint2D.connectedAnchor = grapplePoint;
            m_springJoint2D.enabled = true;
        }
        else
        {
            switch (launchType)
            {
                case LaunchType.Physics_Launch:
                    m_springJoint2D.connectedAnchor = _isPicking ? Vector2.zero : grapplePoint;

                    Vector2 distanceVector = firePoint.position - gunHolder.position;

                    m_springJoint2D.distance = _isPicking ? 0.2f : distanceVector.magnitude;
                    m_springJoint2D.frequency = launchSpeed;
                    m_springJoint2D.enabled = true;
                    break;
                case LaunchType.Transform_Launch:
                    m_rigidbody.gravityScale = 0;
                    m_rigidbody.velocity = Vector2.zero;
                    break;
            }
        }
    }
}