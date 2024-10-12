using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace AKRGS.Framework
{
    public class StatesHandler : MonoBehaviour
    {
        #region Serialized variable
        [SerializeField] EStates m_states;
        #endregion

        #region private variable
        private static StatesHandler s_instance;
        Dictionary<EStates, List<Action<bool>>> m_actionsHolder = new Dictionary<EStates, List<Action<bool>>>();
        List<Action<bool>> m_listAction = new List<Action<bool>>();
        #endregion

        #region public variables
        public static EStates CurrentState => s_instance.m_states;
        #endregion

        private void Awake()
        {
            s_instance = this;
        }

        private IEnumerator Start()
        {
            yield return new WaitForEndOfFrame();
            SetState_Internal(CurrentState);
        }

        /// <summary>
        /// To register/unregister methods to get update on state change
        /// Add method and set desired state to get StateChange update
        /// </summary>
        /// <param name="a_state"></param>
        /// <param name="a_action"></param>
        #region Register/Unregister
        public static void Register(EStates a_state, Action<bool> a_action)
        {
            s_instance.Register_Internal(a_state, a_action);
        }

        public static void UnRegister(EStates a_state, Action<bool> a_action)
        {
            s_instance.UnRegister_Internal(a_state, a_action);
        }

        void Register_Internal(EStates a_state, Action<bool> a_action)
        {
            List<Action<bool>> l_lstAction = (m_actionsHolder.ContainsKey(a_state)) ? m_actionsHolder[a_state] : new List<Action<bool>>();

            l_lstAction.Add(a_action);
            m_actionsHolder[a_state] = l_lstAction;
        }

        void UnRegister_Internal(EStates a_state, Action<bool> a_action)
        {
            if (m_actionsHolder.ContainsKey(a_state))
            {
                List<Action<bool>> l_lstAction = m_actionsHolder[a_state];
                l_lstAction.Remove(a_action);
                m_actionsHolder[a_state] = l_lstAction;
            }
        }
        #endregion

        public static void SetState(EStates a_state)
        {
            s_instance.SetState_Internal(a_state);
        }

        void SetState_Internal(EStates a_state)
        {
            m_states = a_state;

            //set prev state actions false
            foreach (Action<bool> l_prevAction in m_listAction)
            {
                l_prevAction?.Invoke(false);
            }

            m_listAction.Clear();

            if (!m_actionsHolder.ContainsKey(a_state))
                return;

            m_listAction.AddRange(m_actionsHolder[a_state]);

            //set curr state actions true
            foreach (Action<bool> l_currAction in m_listAction)
            {
                l_currAction?.Invoke(true);
            }
        }
    }
}
