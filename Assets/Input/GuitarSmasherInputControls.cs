//------------------------------------------------------------------------------
// <auto-generated>
//     This code was auto-generated by com.unity.inputsystem:InputActionCodeGenerator
//     version 1.5.1
//     from Assets/Input/GuitarSmasherInputControls.inputactions
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public partial class @GuitarSmasherInputControls: IInputActionCollection2, IDisposable
{
    public InputActionAsset asset { get; }
    public @GuitarSmasherInputControls()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""GuitarSmasherInputControls"",
    ""maps"": [
        {
            ""name"": ""Player"",
            ""id"": ""d8d7fc07-7541-4f54-9bf2-1d9bcf8259ef"",
            ""actions"": [
                {
                    ""name"": ""BlueInput"",
                    ""type"": ""Button"",
                    ""id"": ""ceb98d83-4626-4ca7-83d6-7272dab33c55"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""RedInput"",
                    ""type"": ""Button"",
                    ""id"": ""34d2e8e6-3545-4e58-b42b-72d964d1dd76"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""GreenInput"",
                    ""type"": ""Button"",
                    ""id"": ""39e9179c-6f34-4897-98bc-fd2eeb080489"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""YellowInput"",
                    ""type"": ""Button"",
                    ""id"": ""aca422cf-0276-40b0-86ad-fcb254d67c47"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""DebugInput"",
                    ""type"": ""Button"",
                    ""id"": ""aa00e3fc-185a-4039-9990-2097b86b3735"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""99b41c45-6cd4-4da8-93e7-4d65c5760cc6"",
                    ""path"": ""<Keyboard>/z"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""BlueInput"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""4a93761e-a196-4003-8919-7f6da913fb2a"",
                    ""path"": ""<Keyboard>/x"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""RedInput"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""9876768e-04ce-48e8-8d36-9a20d4ad10c2"",
                    ""path"": ""<Keyboard>/c"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""GreenInput"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""54ab0910-d6b0-45d4-ba59-a2ebc3fb1995"",
                    ""path"": ""<Keyboard>/v"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""YellowInput"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""444abec6-bb22-425c-8456-c66bc06eb131"",
                    ""path"": ""<Keyboard>/f9"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""DebugInput"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": [
        {
            ""name"": ""Keyboard&Mouse"",
            ""bindingGroup"": ""Keyboard&Mouse"",
            ""devices"": [
                {
                    ""devicePath"": ""<Keyboard>"",
                    ""isOptional"": false,
                    ""isOR"": false
                },
                {
                    ""devicePath"": ""<Mouse>"",
                    ""isOptional"": false,
                    ""isOR"": false
                }
            ]
        },
        {
            ""name"": ""Gamepad"",
            ""bindingGroup"": ""Gamepad"",
            ""devices"": [
                {
                    ""devicePath"": ""<Gamepad>"",
                    ""isOptional"": false,
                    ""isOR"": false
                }
            ]
        },
        {
            ""name"": ""Touch"",
            ""bindingGroup"": ""Touch"",
            ""devices"": [
                {
                    ""devicePath"": ""<Touchscreen>"",
                    ""isOptional"": false,
                    ""isOR"": false
                }
            ]
        },
        {
            ""name"": ""Joystick"",
            ""bindingGroup"": ""Joystick"",
            ""devices"": [
                {
                    ""devicePath"": ""<Joystick>"",
                    ""isOptional"": false,
                    ""isOR"": false
                }
            ]
        },
        {
            ""name"": ""XR"",
            ""bindingGroup"": ""XR"",
            ""devices"": [
                {
                    ""devicePath"": ""<XRController>"",
                    ""isOptional"": false,
                    ""isOR"": false
                }
            ]
        }
    ]
}");
        // Player
        m_Player = asset.FindActionMap("Player", throwIfNotFound: true);
        m_Player_BlueInput = m_Player.FindAction("BlueInput", throwIfNotFound: true);
        m_Player_RedInput = m_Player.FindAction("RedInput", throwIfNotFound: true);
        m_Player_GreenInput = m_Player.FindAction("GreenInput", throwIfNotFound: true);
        m_Player_YellowInput = m_Player.FindAction("YellowInput", throwIfNotFound: true);
        m_Player_DebugInput = m_Player.FindAction("DebugInput", throwIfNotFound: true);
    }

    public void Dispose()
    {
        UnityEngine.Object.Destroy(asset);
    }

    public InputBinding? bindingMask
    {
        get => asset.bindingMask;
        set => asset.bindingMask = value;
    }

    public ReadOnlyArray<InputDevice>? devices
    {
        get => asset.devices;
        set => asset.devices = value;
    }

    public ReadOnlyArray<InputControlScheme> controlSchemes => asset.controlSchemes;

    public bool Contains(InputAction action)
    {
        return asset.Contains(action);
    }

    public IEnumerator<InputAction> GetEnumerator()
    {
        return asset.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public void Enable()
    {
        asset.Enable();
    }

    public void Disable()
    {
        asset.Disable();
    }

    public IEnumerable<InputBinding> bindings => asset.bindings;

    public InputAction FindAction(string actionNameOrId, bool throwIfNotFound = false)
    {
        return asset.FindAction(actionNameOrId, throwIfNotFound);
    }

    public int FindBinding(InputBinding bindingMask, out InputAction action)
    {
        return asset.FindBinding(bindingMask, out action);
    }

    // Player
    private readonly InputActionMap m_Player;
    private List<IPlayerActions> m_PlayerActionsCallbackInterfaces = new List<IPlayerActions>();
    private readonly InputAction m_Player_BlueInput;
    private readonly InputAction m_Player_RedInput;
    private readonly InputAction m_Player_GreenInput;
    private readonly InputAction m_Player_YellowInput;
    private readonly InputAction m_Player_DebugInput;
    public struct PlayerActions
    {
        private @GuitarSmasherInputControls m_Wrapper;
        public PlayerActions(@GuitarSmasherInputControls wrapper) { m_Wrapper = wrapper; }
        public InputAction @BlueInput => m_Wrapper.m_Player_BlueInput;
        public InputAction @RedInput => m_Wrapper.m_Player_RedInput;
        public InputAction @GreenInput => m_Wrapper.m_Player_GreenInput;
        public InputAction @YellowInput => m_Wrapper.m_Player_YellowInput;
        public InputAction @DebugInput => m_Wrapper.m_Player_DebugInput;
        public InputActionMap Get() { return m_Wrapper.m_Player; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(PlayerActions set) { return set.Get(); }
        public void AddCallbacks(IPlayerActions instance)
        {
            if (instance == null || m_Wrapper.m_PlayerActionsCallbackInterfaces.Contains(instance)) return;
            m_Wrapper.m_PlayerActionsCallbackInterfaces.Add(instance);
            @BlueInput.started += instance.OnBlueInput;
            @BlueInput.performed += instance.OnBlueInput;
            @BlueInput.canceled += instance.OnBlueInput;
            @RedInput.started += instance.OnRedInput;
            @RedInput.performed += instance.OnRedInput;
            @RedInput.canceled += instance.OnRedInput;
            @GreenInput.started += instance.OnGreenInput;
            @GreenInput.performed += instance.OnGreenInput;
            @GreenInput.canceled += instance.OnGreenInput;
            @YellowInput.started += instance.OnYellowInput;
            @YellowInput.performed += instance.OnYellowInput;
            @YellowInput.canceled += instance.OnYellowInput;
            @DebugInput.started += instance.OnDebugInput;
            @DebugInput.performed += instance.OnDebugInput;
            @DebugInput.canceled += instance.OnDebugInput;
        }

        private void UnregisterCallbacks(IPlayerActions instance)
        {
            @BlueInput.started -= instance.OnBlueInput;
            @BlueInput.performed -= instance.OnBlueInput;
            @BlueInput.canceled -= instance.OnBlueInput;
            @RedInput.started -= instance.OnRedInput;
            @RedInput.performed -= instance.OnRedInput;
            @RedInput.canceled -= instance.OnRedInput;
            @GreenInput.started -= instance.OnGreenInput;
            @GreenInput.performed -= instance.OnGreenInput;
            @GreenInput.canceled -= instance.OnGreenInput;
            @YellowInput.started -= instance.OnYellowInput;
            @YellowInput.performed -= instance.OnYellowInput;
            @YellowInput.canceled -= instance.OnYellowInput;
            @DebugInput.started -= instance.OnDebugInput;
            @DebugInput.performed -= instance.OnDebugInput;
            @DebugInput.canceled -= instance.OnDebugInput;
        }

        public void RemoveCallbacks(IPlayerActions instance)
        {
            if (m_Wrapper.m_PlayerActionsCallbackInterfaces.Remove(instance))
                UnregisterCallbacks(instance);
        }

        public void SetCallbacks(IPlayerActions instance)
        {
            foreach (var item in m_Wrapper.m_PlayerActionsCallbackInterfaces)
                UnregisterCallbacks(item);
            m_Wrapper.m_PlayerActionsCallbackInterfaces.Clear();
            AddCallbacks(instance);
        }
    }
    public PlayerActions @Player => new PlayerActions(this);
    private int m_KeyboardMouseSchemeIndex = -1;
    public InputControlScheme KeyboardMouseScheme
    {
        get
        {
            if (m_KeyboardMouseSchemeIndex == -1) m_KeyboardMouseSchemeIndex = asset.FindControlSchemeIndex("Keyboard&Mouse");
            return asset.controlSchemes[m_KeyboardMouseSchemeIndex];
        }
    }
    private int m_GamepadSchemeIndex = -1;
    public InputControlScheme GamepadScheme
    {
        get
        {
            if (m_GamepadSchemeIndex == -1) m_GamepadSchemeIndex = asset.FindControlSchemeIndex("Gamepad");
            return asset.controlSchemes[m_GamepadSchemeIndex];
        }
    }
    private int m_TouchSchemeIndex = -1;
    public InputControlScheme TouchScheme
    {
        get
        {
            if (m_TouchSchemeIndex == -1) m_TouchSchemeIndex = asset.FindControlSchemeIndex("Touch");
            return asset.controlSchemes[m_TouchSchemeIndex];
        }
    }
    private int m_JoystickSchemeIndex = -1;
    public InputControlScheme JoystickScheme
    {
        get
        {
            if (m_JoystickSchemeIndex == -1) m_JoystickSchemeIndex = asset.FindControlSchemeIndex("Joystick");
            return asset.controlSchemes[m_JoystickSchemeIndex];
        }
    }
    private int m_XRSchemeIndex = -1;
    public InputControlScheme XRScheme
    {
        get
        {
            if (m_XRSchemeIndex == -1) m_XRSchemeIndex = asset.FindControlSchemeIndex("XR");
            return asset.controlSchemes[m_XRSchemeIndex];
        }
    }
    public interface IPlayerActions
    {
        void OnBlueInput(InputAction.CallbackContext context);
        void OnRedInput(InputAction.CallbackContext context);
        void OnGreenInput(InputAction.CallbackContext context);
        void OnYellowInput(InputAction.CallbackContext context);
        void OnDebugInput(InputAction.CallbackContext context);
    }
}
