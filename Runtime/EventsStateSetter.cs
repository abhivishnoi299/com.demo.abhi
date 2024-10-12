//using AKRGS.Framework.Events;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace AKRGS.Framework
{
    [Serializable]
    public struct SingleValueEventData<T>
    {
        //[SerializeField] RootEvents<T> m_buttonEvent;
        [SerializeField] EStates m_state;

        public void Init()
        {
            //m_buttonEvent.RegisterListener(UpdateState);
        }

        public void Deinit()
        {
            //m_buttonEvent.UnregisterListener(UpdateState);
        }

        private void UpdateState(T a_type)
        {
            StatesHandler.SetState(m_state);
        }
    }

    [Serializable]
    public struct MultiValueEventData<T>
    {
        //[SerializeField] RootEvents<T> m_sliderEvent;
        [SerializeField] EStates[] m_states;

        public void Init()
        {
            //m_sliderEvent.RegisterListener(UpdateState);
        }

        public void Deinit()
        {
            //m_sliderEvent.UnregisterListener(UpdateState);
        }

        private void UpdateState(T a_type)
        {
            if (typeof(T) == typeof(Slider))
                StatesHandler.SetState(m_states[(int)((Slider)(object)a_type).value]);
        }
    }


    public class EventsStateSetter : MonoBehaviour
    {
        [SerializeField] SingleValueEventData<Toggle>[] m_toggleEventData;
        [SerializeField] MultiValueEventData<Slider>[] m_sliderEventData;

        private void Start()
        {
            foreach (var l_eventData in m_toggleEventData)
            {
                l_eventData.Init();
            }

            foreach (var l_eventData in m_sliderEventData)
            {
                l_eventData.Init();
            }
        }

        private void OnDestroy()
        {
            foreach (var l_eventData in m_toggleEventData)
            {
                l_eventData.Deinit();
            }

            foreach (var l_eventData in m_sliderEventData)
            {
                l_eventData.Deinit();
            }
        }
    }
}
