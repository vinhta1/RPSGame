using System.Collections.Generic;
using System.Runtime.InteropServices;
using System;
using UnityEngine;
using JoyconUnity;

namespace JoyconUnity
{
    public class JoyconManager : MonoBehaviour
    {
        private static JoyconManager _instance;
        public static JoyconManager Instance => _instance;

        // Settings accessible via Unity
        public bool EnableIMU = true;
        public bool EnableLocalize = true;

        // Different operating systems either do or don't like the trailing zero
        private const ushort VendorId = 0x57e;
        private const ushort VendorIdWithTrailingZero = 0x057e;
        private const ushort LeftJoyconId = 0x2006;
        private const ushort RightJoyconId = 0x2007;
        public List<Joycon> _connectedJoycons = new List<Joycon>();

        public Joycon LeftController => _connectedJoycons.Count > 0 ? _connectedJoycons[0] : null;
        public Joycon RightController => _connectedJoycons.Count > 1 ? _connectedJoycons[1] : null;

        private void Awake()
        {
            Scan();
        }

        private void Start()
        {
            for (int i = 0; i < _connectedJoycons.Count; ++i)
            {
                Debug.Log(i);
                Joycon jc = _connectedJoycons[i];
                byte LEDs = 0x0;
                LEDs |= (byte)(0x1 << i);
                jc.Attach(leds_: LEDs);
                jc.Begin();
            }
        }

        internal void OnApplicationQuit()
        {
            for (int i = 0; i < _connectedJoycons.Count; ++i)
            {
                Joycon jc = _connectedJoycons[i];
                jc.Detach();
            }
        }

        private void Update()
        {
            UpdateJoyconsState();
        }

        private void UpdateJoyconsState()
        {
            foreach (var jc in _connectedJoycons)
                jc.Update();
        }

        #region NonUnityMagic

        private void Scan()
        {
            if (_instance != null) ;
            _instance = this;
            int i = 0;


            bool isLeft = false;
            HIDapi.hid_init();

            IntPtr ptr = HIDapi.hid_enumerate(VendorId, 0x0);
            IntPtr topPtr = ptr;

            if (ptr == IntPtr.Zero)
            {
                ptr = HIDapi.hid_enumerate(VendorIdWithTrailingZero, 0x0);
                if (ptr == IntPtr.Zero)
                {
                    HIDapi.hid_free_enumeration(ptr);
                    Debug.Log("No Joy-Cons found!");
                }
            }

            while (ptr != IntPtr.Zero)
            {
                var enumerate = (hid_device_info)Marshal.PtrToStructure(ptr, typeof(hid_device_info));
                var isValid = false;
                Debug.Log(enumerate.product_id);
                if (enumerate.product_id == LeftJoyconId || enumerate.product_id == RightJoyconId)
                {
                    if (enumerate.product_id == LeftJoyconId)
                    {
                        isValid = true;
                        isLeft = true;
                        Debug.Log("Left Joy-Con connected.");
                    }
                    else if (enumerate.product_id == RightJoyconId)
                    {
                        isValid = true;
                        isLeft = false;
                        Debug.Log("Right Joy-Con connected.");
                    }
                    else
                    {
                        Debug.Log("Non Joy-Con input device skipped.");
                    }
                    if (isValid)
                    {
                        IntPtr handle = HIDapi.hid_open_path(enumerate.path);
                        HIDapi.hid_set_nonblocking(handle, 1);
                        _connectedJoycons.Add(new Joycon(handle, EnableIMU, EnableLocalize & EnableIMU, 0.04f, isLeft));
                    }
                    ++i;
                }
                ptr = enumerate.next;
            }
            HIDapi.hid_free_enumeration(topPtr);
        }

        #endregion
    }
}