using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Resources.Code.GameLogic.IngameObjects.PieRoll
{
    public class PieRollerTester: MonoBehaviour
    {
        public LayerMask SliceLayerMask;
        private Vector2 _beginTouchPosition;
        Vector2 direction = Vector2.zero;
        private float m_rotationVel;
        public float MaxSpeed;

        private float startTime;
        private float totalTimeTakeToSpin;

        public float frictionCoefficient;

        private float m_baseAngle;
        private float m_angle;
        private Vector2 m_prevPos;
        private Vector3 m_currPos;

        private Vector2 StartingPos;
        private bool m_hasTouched;

        private Vector3 _touchVelocity;
        private float _touchSpeed;
        private float _rollAcceleration;
        private string _outputDebugText = "";
        public bool _inDebugMode = true;
        private UnityEngine.UI.Text m_debugText;
        public float SlowDownSpeed = 3f;


        private Vector3 firstTouchPos;
        private Vector3 deltaTouchPos;
        public GameObject RotationObject;
        public float arrivalSpeed;
        public float stopRadius;
        public float rotationSpeedFactor;

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

    }
}
