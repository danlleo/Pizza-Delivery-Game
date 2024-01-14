//------------------------------------------------------------------------------
// <auto-generated>
//     This code was auto-generated by com.unity.inputsystem:InputActionCodeGenerator
//     version 1.7.0
//     from Assets/Code/Input/GameInput.inputactions
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

public partial class @GameInput: IInputActionCollection2, IDisposable
{
    public InputActionAsset asset { get; }
    public @GameInput()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""GameInput"",
    ""maps"": [
        {
            ""name"": ""Player"",
            ""id"": ""ef3ebdd7-33a9-44ca-a158-abea454b6d53"",
            ""actions"": [
                {
                    ""name"": ""Movement"",
                    ""type"": ""Value"",
                    ""id"": ""e60584a2-7ef5-464a-b9e9-efd82a54cc51"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""Interact"",
                    ""type"": ""Button"",
                    ""id"": ""43258ba1-3de8-415b-a010-412d02c8d963"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Flashlight"",
                    ""type"": ""Button"",
                    ""id"": ""b55aef7c-d3d9-4813-a9e6-0f376baaaf18"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Sprint"",
                    ""type"": ""Button"",
                    ""id"": ""9441a65f-0bfd-4634-9f85-3e50ff4a15dc"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Crouch"",
                    ""type"": ""Button"",
                    ""id"": ""482eaade-a69f-453f-91ec-051bc88b00e8"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Pause"",
                    ""type"": ""Button"",
                    ""id"": ""2702a42d-c6a4-4d13-9aad-6e6f562615c6"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                }
            ],
            ""bindings"": [
                {
                    ""name"": ""WASD"",
                    ""id"": ""bc832366-dc6c-4df7-8171-cf13fa9aec62"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""5efc52e3-3ee3-4d58-9bc5-90fc0d3e18d1"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""8a8e90fe-c4b0-4a2c-b416-527e6e0dcd06"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""94d9d68e-52c9-4a60-94d8-e567db733369"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""3cb4da01-ed7f-48e7-972e-29ae956a5877"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""Arrow Keys"",
                    ""id"": ""0d83145d-e92e-4f6c-a27e-45c52a959eb1"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""e0fc8309-ae1e-4821-b20f-f1b01ca8a69b"",
                    ""path"": ""<Keyboard>/upArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""7d2a94f5-2b83-4bbe-90b5-8df6dd021a1d"",
                    ""path"": ""<Keyboard>/downArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""16c97c29-8c94-44f1-a906-27df261dd8c2"",
                    ""path"": ""<Keyboard>/leftArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""24a3f402-6fdd-4d30-b0c3-b83f9aad561b"",
                    ""path"": ""<Keyboard>/rightArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""33026fb2-17e8-41ec-8241-6398ff091ffe"",
                    ""path"": ""<Keyboard>/e"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Interact"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""623d66ea-fc66-4132-8a60-36b03f42e5dc"",
                    ""path"": ""<Keyboard>/f"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Flashlight"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""0262c126-e3ca-4753-a204-32851f0488d7"",
                    ""path"": ""<Keyboard>/shift"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Sprint"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""23ef92e2-a6e4-4933-9cb7-9a7af66aa4e2"",
                    ""path"": ""<Keyboard>/leftCtrl"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Crouch"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""273cf526-195d-488c-b34b-ba76adc50f0c"",
                    ""path"": ""<Keyboard>/escape"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Pause"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        },
        {
            ""name"": ""UI"",
            ""id"": ""c3952b73-1009-4741-9e82-481fcadb2d94"",
            ""actions"": [
                {
                    ""name"": ""Submit"",
                    ""type"": ""Button"",
                    ""id"": ""4d622a46-be7c-4b93-81b1-a2627a89125e"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""b1b200f7-bfc3-4b2c-a467-097822464472"",
                    ""path"": ""<Keyboard>/e"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Submit"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        },
        {
            ""name"": ""PC"",
            ""id"": ""ed000796-7590-4bdb-a655-e05f8cf7613b"",
            ""actions"": [
                {
                    ""name"": ""Click"",
                    ""type"": ""Button"",
                    ""id"": ""2b096f4e-4ad8-44c4-acc2-f27b80fd59b5"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""e2e96cdf-36fc-4e4d-a7c3-27844db65962"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Click"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        },
        {
            ""name"": ""Tablet"",
            ""id"": ""59b0895f-8455-4909-a51a-81538a40b295"",
            ""actions"": [
                {
                    ""name"": ""TabletPutDown"",
                    ""type"": ""Button"",
                    ""id"": ""dbead56d-eda5-48c1-8ce8-2f01ea20da46"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""88264ffc-1e3f-4a09-884c-f3e948d5aedf"",
                    ""path"": ""<Keyboard>/q"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""TabletPutDown"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        },
        {
            ""name"": ""Keypad"",
            ""id"": ""e762d931-d5d7-450a-863c-1c32303b97a7"",
            ""actions"": [
                {
                    ""name"": ""ButtonPress"",
                    ""type"": ""Button"",
                    ""id"": ""765015bd-b392-4eac-9205-1cfbb4f9c761"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""e8b2e840-e6c6-4e53-8ad2-57b6cf185b0b"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""ButtonPress"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        },
        {
            ""name"": ""PauseMenu"",
            ""id"": ""230b78be-6c29-4c6e-a529-2ce578b4cdc2"",
            ""actions"": [
                {
                    ""name"": ""Unpause"",
                    ""type"": ""Button"",
                    ""id"": ""812efe10-f39d-427a-a970-55efac663973"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""c1f182bc-37f0-4cf4-8053-3a788bcbf467"",
                    ""path"": ""<Keyboard>/escape"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Unpause"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // Player
        m_Player = asset.FindActionMap("Player", throwIfNotFound: true);
        m_Player_Movement = m_Player.FindAction("Movement", throwIfNotFound: true);
        m_Player_Interact = m_Player.FindAction("Interact", throwIfNotFound: true);
        m_Player_Flashlight = m_Player.FindAction("Flashlight", throwIfNotFound: true);
        m_Player_Sprint = m_Player.FindAction("Sprint", throwIfNotFound: true);
        m_Player_Crouch = m_Player.FindAction("Crouch", throwIfNotFound: true);
        m_Player_Pause = m_Player.FindAction("Pause", throwIfNotFound: true);
        // UI
        m_UI = asset.FindActionMap("UI", throwIfNotFound: true);
        m_UI_Submit = m_UI.FindAction("Submit", throwIfNotFound: true);
        // PC
        m_PC = asset.FindActionMap("PC", throwIfNotFound: true);
        m_PC_Click = m_PC.FindAction("Click", throwIfNotFound: true);
        // Tablet
        m_Tablet = asset.FindActionMap("Tablet", throwIfNotFound: true);
        m_Tablet_TabletPutDown = m_Tablet.FindAction("TabletPutDown", throwIfNotFound: true);
        // Keypad
        m_Keypad = asset.FindActionMap("Keypad", throwIfNotFound: true);
        m_Keypad_ButtonPress = m_Keypad.FindAction("ButtonPress", throwIfNotFound: true);
        // PauseMenu
        m_PauseMenu = asset.FindActionMap("PauseMenu", throwIfNotFound: true);
        m_PauseMenu_Unpause = m_PauseMenu.FindAction("Unpause", throwIfNotFound: true);
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
    private readonly InputAction m_Player_Movement;
    private readonly InputAction m_Player_Interact;
    private readonly InputAction m_Player_Flashlight;
    private readonly InputAction m_Player_Sprint;
    private readonly InputAction m_Player_Crouch;
    private readonly InputAction m_Player_Pause;
    public struct PlayerActions
    {
        private @GameInput m_Wrapper;
        public PlayerActions(@GameInput wrapper) { m_Wrapper = wrapper; }
        public InputAction @Movement => m_Wrapper.m_Player_Movement;
        public InputAction @Interact => m_Wrapper.m_Player_Interact;
        public InputAction @Flashlight => m_Wrapper.m_Player_Flashlight;
        public InputAction @Sprint => m_Wrapper.m_Player_Sprint;
        public InputAction @Crouch => m_Wrapper.m_Player_Crouch;
        public InputAction @Pause => m_Wrapper.m_Player_Pause;
        public InputActionMap Get() { return m_Wrapper.m_Player; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(PlayerActions set) { return set.Get(); }
        public void AddCallbacks(IPlayerActions instance)
        {
            if (instance == null || m_Wrapper.m_PlayerActionsCallbackInterfaces.Contains(instance)) return;
            m_Wrapper.m_PlayerActionsCallbackInterfaces.Add(instance);
            @Movement.started += instance.OnMovement;
            @Movement.performed += instance.OnMovement;
            @Movement.canceled += instance.OnMovement;
            @Interact.started += instance.OnInteract;
            @Interact.performed += instance.OnInteract;
            @Interact.canceled += instance.OnInteract;
            @Flashlight.started += instance.OnFlashlight;
            @Flashlight.performed += instance.OnFlashlight;
            @Flashlight.canceled += instance.OnFlashlight;
            @Sprint.started += instance.OnSprint;
            @Sprint.performed += instance.OnSprint;
            @Sprint.canceled += instance.OnSprint;
            @Crouch.started += instance.OnCrouch;
            @Crouch.performed += instance.OnCrouch;
            @Crouch.canceled += instance.OnCrouch;
            @Pause.started += instance.OnPause;
            @Pause.performed += instance.OnPause;
            @Pause.canceled += instance.OnPause;
        }

        private void UnregisterCallbacks(IPlayerActions instance)
        {
            @Movement.started -= instance.OnMovement;
            @Movement.performed -= instance.OnMovement;
            @Movement.canceled -= instance.OnMovement;
            @Interact.started -= instance.OnInteract;
            @Interact.performed -= instance.OnInteract;
            @Interact.canceled -= instance.OnInteract;
            @Flashlight.started -= instance.OnFlashlight;
            @Flashlight.performed -= instance.OnFlashlight;
            @Flashlight.canceled -= instance.OnFlashlight;
            @Sprint.started -= instance.OnSprint;
            @Sprint.performed -= instance.OnSprint;
            @Sprint.canceled -= instance.OnSprint;
            @Crouch.started -= instance.OnCrouch;
            @Crouch.performed -= instance.OnCrouch;
            @Crouch.canceled -= instance.OnCrouch;
            @Pause.started -= instance.OnPause;
            @Pause.performed -= instance.OnPause;
            @Pause.canceled -= instance.OnPause;
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

    // UI
    private readonly InputActionMap m_UI;
    private List<IUIActions> m_UIActionsCallbackInterfaces = new List<IUIActions>();
    private readonly InputAction m_UI_Submit;
    public struct UIActions
    {
        private @GameInput m_Wrapper;
        public UIActions(@GameInput wrapper) { m_Wrapper = wrapper; }
        public InputAction @Submit => m_Wrapper.m_UI_Submit;
        public InputActionMap Get() { return m_Wrapper.m_UI; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(UIActions set) { return set.Get(); }
        public void AddCallbacks(IUIActions instance)
        {
            if (instance == null || m_Wrapper.m_UIActionsCallbackInterfaces.Contains(instance)) return;
            m_Wrapper.m_UIActionsCallbackInterfaces.Add(instance);
            @Submit.started += instance.OnSubmit;
            @Submit.performed += instance.OnSubmit;
            @Submit.canceled += instance.OnSubmit;
        }

        private void UnregisterCallbacks(IUIActions instance)
        {
            @Submit.started -= instance.OnSubmit;
            @Submit.performed -= instance.OnSubmit;
            @Submit.canceled -= instance.OnSubmit;
        }

        public void RemoveCallbacks(IUIActions instance)
        {
            if (m_Wrapper.m_UIActionsCallbackInterfaces.Remove(instance))
                UnregisterCallbacks(instance);
        }

        public void SetCallbacks(IUIActions instance)
        {
            foreach (var item in m_Wrapper.m_UIActionsCallbackInterfaces)
                UnregisterCallbacks(item);
            m_Wrapper.m_UIActionsCallbackInterfaces.Clear();
            AddCallbacks(instance);
        }
    }
    public UIActions @UI => new UIActions(this);

    // PC
    private readonly InputActionMap m_PC;
    private List<IPCActions> m_PCActionsCallbackInterfaces = new List<IPCActions>();
    private readonly InputAction m_PC_Click;
    public struct PCActions
    {
        private @GameInput m_Wrapper;
        public PCActions(@GameInput wrapper) { m_Wrapper = wrapper; }
        public InputAction @Click => m_Wrapper.m_PC_Click;
        public InputActionMap Get() { return m_Wrapper.m_PC; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(PCActions set) { return set.Get(); }
        public void AddCallbacks(IPCActions instance)
        {
            if (instance == null || m_Wrapper.m_PCActionsCallbackInterfaces.Contains(instance)) return;
            m_Wrapper.m_PCActionsCallbackInterfaces.Add(instance);
            @Click.started += instance.OnClick;
            @Click.performed += instance.OnClick;
            @Click.canceled += instance.OnClick;
        }

        private void UnregisterCallbacks(IPCActions instance)
        {
            @Click.started -= instance.OnClick;
            @Click.performed -= instance.OnClick;
            @Click.canceled -= instance.OnClick;
        }

        public void RemoveCallbacks(IPCActions instance)
        {
            if (m_Wrapper.m_PCActionsCallbackInterfaces.Remove(instance))
                UnregisterCallbacks(instance);
        }

        public void SetCallbacks(IPCActions instance)
        {
            foreach (var item in m_Wrapper.m_PCActionsCallbackInterfaces)
                UnregisterCallbacks(item);
            m_Wrapper.m_PCActionsCallbackInterfaces.Clear();
            AddCallbacks(instance);
        }
    }
    public PCActions @PC => new PCActions(this);

    // Tablet
    private readonly InputActionMap m_Tablet;
    private List<ITabletActions> m_TabletActionsCallbackInterfaces = new List<ITabletActions>();
    private readonly InputAction m_Tablet_TabletPutDown;
    public struct TabletActions
    {
        private @GameInput m_Wrapper;
        public TabletActions(@GameInput wrapper) { m_Wrapper = wrapper; }
        public InputAction @TabletPutDown => m_Wrapper.m_Tablet_TabletPutDown;
        public InputActionMap Get() { return m_Wrapper.m_Tablet; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(TabletActions set) { return set.Get(); }
        public void AddCallbacks(ITabletActions instance)
        {
            if (instance == null || m_Wrapper.m_TabletActionsCallbackInterfaces.Contains(instance)) return;
            m_Wrapper.m_TabletActionsCallbackInterfaces.Add(instance);
            @TabletPutDown.started += instance.OnTabletPutDown;
            @TabletPutDown.performed += instance.OnTabletPutDown;
            @TabletPutDown.canceled += instance.OnTabletPutDown;
        }

        private void UnregisterCallbacks(ITabletActions instance)
        {
            @TabletPutDown.started -= instance.OnTabletPutDown;
            @TabletPutDown.performed -= instance.OnTabletPutDown;
            @TabletPutDown.canceled -= instance.OnTabletPutDown;
        }

        public void RemoveCallbacks(ITabletActions instance)
        {
            if (m_Wrapper.m_TabletActionsCallbackInterfaces.Remove(instance))
                UnregisterCallbacks(instance);
        }

        public void SetCallbacks(ITabletActions instance)
        {
            foreach (var item in m_Wrapper.m_TabletActionsCallbackInterfaces)
                UnregisterCallbacks(item);
            m_Wrapper.m_TabletActionsCallbackInterfaces.Clear();
            AddCallbacks(instance);
        }
    }
    public TabletActions @Tablet => new TabletActions(this);

    // Keypad
    private readonly InputActionMap m_Keypad;
    private List<IKeypadActions> m_KeypadActionsCallbackInterfaces = new List<IKeypadActions>();
    private readonly InputAction m_Keypad_ButtonPress;
    public struct KeypadActions
    {
        private @GameInput m_Wrapper;
        public KeypadActions(@GameInput wrapper) { m_Wrapper = wrapper; }
        public InputAction @ButtonPress => m_Wrapper.m_Keypad_ButtonPress;
        public InputActionMap Get() { return m_Wrapper.m_Keypad; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(KeypadActions set) { return set.Get(); }
        public void AddCallbacks(IKeypadActions instance)
        {
            if (instance == null || m_Wrapper.m_KeypadActionsCallbackInterfaces.Contains(instance)) return;
            m_Wrapper.m_KeypadActionsCallbackInterfaces.Add(instance);
            @ButtonPress.started += instance.OnButtonPress;
            @ButtonPress.performed += instance.OnButtonPress;
            @ButtonPress.canceled += instance.OnButtonPress;
        }

        private void UnregisterCallbacks(IKeypadActions instance)
        {
            @ButtonPress.started -= instance.OnButtonPress;
            @ButtonPress.performed -= instance.OnButtonPress;
            @ButtonPress.canceled -= instance.OnButtonPress;
        }

        public void RemoveCallbacks(IKeypadActions instance)
        {
            if (m_Wrapper.m_KeypadActionsCallbackInterfaces.Remove(instance))
                UnregisterCallbacks(instance);
        }

        public void SetCallbacks(IKeypadActions instance)
        {
            foreach (var item in m_Wrapper.m_KeypadActionsCallbackInterfaces)
                UnregisterCallbacks(item);
            m_Wrapper.m_KeypadActionsCallbackInterfaces.Clear();
            AddCallbacks(instance);
        }
    }
    public KeypadActions @Keypad => new KeypadActions(this);

    // PauseMenu
    private readonly InputActionMap m_PauseMenu;
    private List<IPauseMenuActions> m_PauseMenuActionsCallbackInterfaces = new List<IPauseMenuActions>();
    private readonly InputAction m_PauseMenu_Unpause;
    public struct PauseMenuActions
    {
        private @GameInput m_Wrapper;
        public PauseMenuActions(@GameInput wrapper) { m_Wrapper = wrapper; }
        public InputAction @Unpause => m_Wrapper.m_PauseMenu_Unpause;
        public InputActionMap Get() { return m_Wrapper.m_PauseMenu; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(PauseMenuActions set) { return set.Get(); }
        public void AddCallbacks(IPauseMenuActions instance)
        {
            if (instance == null || m_Wrapper.m_PauseMenuActionsCallbackInterfaces.Contains(instance)) return;
            m_Wrapper.m_PauseMenuActionsCallbackInterfaces.Add(instance);
            @Unpause.started += instance.OnUnpause;
            @Unpause.performed += instance.OnUnpause;
            @Unpause.canceled += instance.OnUnpause;
        }

        private void UnregisterCallbacks(IPauseMenuActions instance)
        {
            @Unpause.started -= instance.OnUnpause;
            @Unpause.performed -= instance.OnUnpause;
            @Unpause.canceled -= instance.OnUnpause;
        }

        public void RemoveCallbacks(IPauseMenuActions instance)
        {
            if (m_Wrapper.m_PauseMenuActionsCallbackInterfaces.Remove(instance))
                UnregisterCallbacks(instance);
        }

        public void SetCallbacks(IPauseMenuActions instance)
        {
            foreach (var item in m_Wrapper.m_PauseMenuActionsCallbackInterfaces)
                UnregisterCallbacks(item);
            m_Wrapper.m_PauseMenuActionsCallbackInterfaces.Clear();
            AddCallbacks(instance);
        }
    }
    public PauseMenuActions @PauseMenu => new PauseMenuActions(this);
    public interface IPlayerActions
    {
        void OnMovement(InputAction.CallbackContext context);
        void OnInteract(InputAction.CallbackContext context);
        void OnFlashlight(InputAction.CallbackContext context);
        void OnSprint(InputAction.CallbackContext context);
        void OnCrouch(InputAction.CallbackContext context);
        void OnPause(InputAction.CallbackContext context);
    }
    public interface IUIActions
    {
        void OnSubmit(InputAction.CallbackContext context);
    }
    public interface IPCActions
    {
        void OnClick(InputAction.CallbackContext context);
    }
    public interface ITabletActions
    {
        void OnTabletPutDown(InputAction.CallbackContext context);
    }
    public interface IKeypadActions
    {
        void OnButtonPress(InputAction.CallbackContext context);
    }
    public interface IPauseMenuActions
    {
        void OnUnpause(InputAction.CallbackContext context);
    }
}
