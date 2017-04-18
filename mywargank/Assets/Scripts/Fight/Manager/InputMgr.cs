using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WG
{
    public class InputMgr : MonoBehaviour
    {

        protected static InputMgr _instance = null;

        private Vector3 _lastClickPos = Vector3.zero;

        void Awake()
        {
            _instance = this;
        }

        public static InputMgr getInstance()
        {
            return _instance;
        }

        void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                _lastClickPos = Input.mousePosition;
            }

            if (Input.GetMouseButtonUp(0))
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

                RaycastHit hit;
                if(Physics.Raycast(ray, out hit, 200, 1 << LayerMask.NameToLayer("SubState")))
                {
                    GameObject go = hit.transform.gameObject;
                    SubStrateComponent subStrateComponent = go.GetComponent<SubStrateComponent>();
                    if(subStrateComponent != null)
                    {
                        SubStrateMgr.getInstance().OnSubstrateSelected(subStrateComponent);
                    }
                }

                Debug.DrawRay(ray.origin, ray.direction * 10, Color.yellow);
            }
        }
    }
}
