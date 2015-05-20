using System;
using Assets.Resources.Code.Interfaces;
using UnityEngine;

namespace Assets.Resources.Code.GameLogic.IngameObjects.PieRoll
{

    public enum PieRollState
    {
        BeginRoll,
        Completed,
        Rolling,
        EndRoll,
        Selection,
        OutOfView
    }

    public class PieRollerController : MonoBehaviour, IDebuggable
    {
        public LayerMask SliceLayerMask;
        private bool m_hasTouched;
        private bool m_hasClicked = false;
        private bool m_startingTouchRoutine = false;
        private float m_angle; private float m_baseAngle;
        private float m_startTime;
        private float totalTimeTakeToSpin;
        private Vector3 direction;
        private Vector3 m_prevDirection;
        private Vector3 m_currDirection;
        private float m_sumOfAngles;
        private float m_angularVelocity;
        public float MaxSpeed;
        public float SlowDownSpeed;
        private Pie m_Pie;

        private PieRoller m_PieRollerModel;

        public PieRollState m_CurrentState = PieRollState.BeginRoll;

        public bool IsDebugging;

        private void Update()
        {
            if (m_CurrentState == PieRollState.BeginRoll)
            {
                MouseHandler();
            }
            switch (m_CurrentState)
            {
                case PieRollState.BeginRoll:
                    MouseHandler();
                    break;
                case PieRollState.Rolling:
                    RotationHandler();
                    break;
            }

        }
        private void RotationHandler()
        {
            m_angularVelocity = Mathf.Lerp(m_angularVelocity, 0, Time.deltaTime);

            float sign = Mathf.Sign(m_angularVelocity);
            if (Mathf.Abs(m_angularVelocity) >= MaxSpeed)
            {
                m_angularVelocity = MaxSpeed * sign;
            }


            Vector3 currentAngles = transform.rotation.eulerAngles;
            float currentAngle = currentAngles.z;
            currentAngle += m_angularVelocity;
            currentAngles.z = currentAngle;
            transform.rotation = Quaternion.Euler(currentAngles);
            if (Mathf.Abs(m_angularVelocity) < 0.1f)
            {
                if (SlowingDownOnHiddenSlice())
                {
                    m_angularVelocity += sign * 0.2f;
                }
                else
                {
                    m_angularVelocity = 0;
                    MPieRollerModel.HidePieSlice(ReturnPieSliceIndex());
                    ChangeState(PieRollState.Selection);
                }

            }
        }

        private int ReturnPieSliceIndex()
        {
            BoxCollider selectorCollider = MPie.MSelectorGameObject.GetComponent<BoxCollider>();
            Ray camRay = new Ray(UnityEngine.Camera.main.transform.position, selectorCollider.center + selectorCollider.gameObject.transform.position - UnityEngine.Camera.main.transform.position);
            RaycastHit hitInfo;
            Physics.Raycast(camRay, out hitInfo, 1000f,SliceLayerMask);
            
            return hitInfo.collider.gameObject.GetComponent<PieSlice>().index;
        }
        /// <summary>
        /// cast a ray from the camera to the small point on the selector game object. if it hits a pie slice collider, then we are about to slow 
        /// down on an existing pie slice. Otherwise, we are slowing down on a non existing one. 
        /// </summary>
        /// <returns></returns>
        private bool SlowingDownOnHiddenSlice()
        {
            BoxCollider selectorCollider = MPie.MSelectorGameObject.GetComponent<BoxCollider>();
            Ray camRay = new Ray(UnityEngine.Camera.main.transform.position, selectorCollider.center + selectorCollider.gameObject.transform.position - UnityEngine.Camera.main.transform.position);
            return !Physics.Raycast(camRay, 10000f, SliceLayerMask);

        }
        private bool isTouchingObject()
        {
#if UNITY_ANDROID
            if (Input.touchCount > 0)
            {
                Vector3 touchPos = UnityEngine.Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position);
                touchPos.z = 10;
                RaycastHit touchHit;
                Ray touchRay = UnityEngine.Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
                if (Physics.Raycast(touchRay, out touchHit, 20f, SliceLayerMask))
                {
                    m_hasTouched = true;
                    return true;
                }

            }
#endif
#if UNITY_EDITOR

            Vector3 touchPos1 = UnityEngine.Camera.main.ScreenToWorldPoint(Input.mousePosition);
            touchPos1.z = 10;
            RaycastHit hit;
            Ray ray = UnityEngine.Camera.main.ScreenPointToRay(Input.mousePosition);
            //_ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
            if (Physics.Raycast(ray, out hit, 20f, SliceLayerMask))
            {

                m_hasTouched = true;
                return true;

            }

#endif
            return false;
        }
        void MouseHandler()
        {

            if (isTouchingObject() || m_hasTouched)
            {
                if (Input.GetMouseButtonDown(0))
                {
                    m_prevDirection = Input.mousePosition - UnityEngine.Camera.main.WorldToScreenPoint(transform.position);
                    m_sumOfAngles = 0;
                    m_baseAngle = Mathf.Atan2(m_prevDirection.y, m_prevDirection.x) * Mathf.Rad2Deg;
                    m_baseAngle -= Mathf.Atan2(transform.right.y, transform.right.x) * Mathf.Rad2Deg;
                    m_startTime = Time.time;

                    m_startingTouchRoutine = true;
                    m_hasClicked = true;

                }
                else if (Input.GetMouseButton(0)) //dragging
                {
                    if (m_startingTouchRoutine)
                    {
                        m_startingTouchRoutine = false;
                    }
                    else
                    {
                        m_prevDirection = m_currDirection;
                    }
                    m_currDirection = Input.mousePosition - UnityEngine.Camera.main.WorldToScreenPoint(transform.position);
                    m_angle = Mathf.Atan2(m_currDirection.y, m_currDirection.x) * Mathf.Rad2Deg - m_baseAngle;
                    transform.rotation = Quaternion.AngleAxis(m_angle, Vector3.forward);
                    Vector3 crossProduct = Vector3.Cross(m_prevDirection, m_currDirection); //get the sign of the angle
                    float sign = (crossProduct.z < 0) ? -1f : 1f;
                    m_sumOfAngles += Vector2.Angle(m_prevDirection, m_currDirection) * sign;
                }
                else if (m_hasClicked)
                {
                    m_hasTouched = false;
                    m_hasClicked = false;

                    totalTimeTakeToSpin = Time.time - m_startTime;
                    m_angularVelocity = m_sumOfAngles / totalTimeTakeToSpin;
                    if (totalTimeTakeToSpin < 0.25f)
                    {
                        totalTimeTakeToSpin = Time.time;

                        m_hasTouched = true;
                        m_hasClicked = true;
                    }
                    else
                    {
                        ChangeState(PieRollState.Rolling);
                    }

                }
            }

        }

        private void BeginSlowDownRotation()
        {
            m_angularVelocity = Mathf.Lerp(m_angularVelocity, 0, Time.deltaTime);

            float sign = Mathf.Sign(m_angularVelocity);
            if (Mathf.Abs(m_angularVelocity) >= MaxSpeed)
            {
                m_angularVelocity = MaxSpeed * sign;
            }


            Vector3 currentAngles = transform.rotation.eulerAngles;
            float currentAngle = currentAngles.z;
            currentAngle += m_angularVelocity;
            currentAngles.z = currentAngle;
            transform.rotation = Quaternion.Euler(currentAngles);
            if (Mathf.Abs(m_angularVelocity) < 0.2f)
            {
                m_angularVelocity = 0;
            }
        }

        public void ChangeState(PieRollState newState)
        {
            switch (newState)
            {
                case PieRollState.BeginRoll:
                    MPie.EnableAllPieSliceColliders(); //when we need to roll, we should ensure that all the colliders are enabled
                    break;
                    case PieRollState.Rolling:
                    MPie.DisableCollidersWithoutRenders(); //as soon as the pie starts to roll, we should disable all the colliders without any renderers
                    break;
            }    
            m_CurrentState = newState;
        }

        

        private void OnStateChange(PieRollState newState)
        {

        }
        public bool InDebugMode
        {
            get { return IsDebugging; }
            set
            {
                throw new NotImplementedException();
            }
        }



        public DebugLogger Logger
        {
            get { return DebugLogger.Instance; }
        }

        public Pie MPie
        {
            get
            {
                if (m_Pie == null)
                {
                    m_Pie = GetComponent<Pie>();
                }
                return m_Pie;
            }
            set { m_Pie = value; }
        }

        public PieRoller MPieRollerModel
        {
            get
            {
                if (m_PieRollerModel == null)
                {
                    m_PieRollerModel = GetComponent<PieRoller>();
                }
                return m_PieRollerModel;
            }
            set { m_PieRollerModel = value; }
        }
    }

}
