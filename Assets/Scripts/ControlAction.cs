// GENERATED AUTOMATICALLY FROM 'Assets/Scripts/ControlAction.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class @ControlAction : IInputActionCollection, IDisposable
{
    public InputActionAsset asset { get; }
    public @ControlAction()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""ControlAction"",
    ""maps"": [
        {
            ""name"": ""PlayerController"",
            ""id"": ""763a8315-d3bc-4159-9365-eb2daf901a8a"",
            ""actions"": [
                {
                    ""name"": ""Move"",
                    ""type"": ""Value"",
                    ""id"": ""36c4189f-7002-4614-a463-a1535b70e2ef"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Jump"",
                    ""type"": ""Button"",
                    ""id"": ""e06445d4-42a0-41bd-ae28-0877cd928818"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Shot1"",
                    ""type"": ""Button"",
                    ""id"": ""60d0b51a-467c-4905-8e1d-10d6d24e5565"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""ShotL"",
                    ""type"": ""Button"",
                    ""id"": ""e526025e-0cae-4436-af1d-569715acf5b4"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""ShotR"",
                    ""type"": ""Button"",
                    ""id"": ""ea9282ff-c7c4-43ac-b6f9-25b9709af55c"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Attack1"",
                    ""type"": ""Button"",
                    ""id"": ""3e3b0e00-2e00-45da-b83d-81fe13e6a4f1"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Jet"",
                    ""type"": ""Button"",
                    ""id"": ""f2f3f5fa-c393-4965-b07b-68217c7d9e45"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""CameraMove"",
                    ""type"": ""Value"",
                    ""id"": ""a13c1dff-7005-4cda-beef-80fb9c74ef19"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""LockOn"",
                    ""type"": ""Button"",
                    ""id"": ""85543fe1-b4c8-4d67-8d27-4426e06b3c37"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""f1394743-2383-45b9-966f-ac1d98918beb"",
                    ""path"": ""<Gamepad>/dpad"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""1d6e7ef8-c4fb-49da-9acd-29c7b8ba081f"",
                    ""path"": ""<Gamepad>/leftStick"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""2D Vector"",
                    ""id"": ""13d8e094-ae03-4a00-abc9-401166f054a7"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""9fb37c6f-31d4-4a6d-891c-0ffddfb5b108"",
                    ""path"": ""<Keyboard>/upArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""a80caef5-9029-42e3-ad00-d2593208ee46"",
                    ""path"": ""<Keyboard>/downArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""a468cf7a-c119-45af-9d08-9280a4ef57a4"",
                    ""path"": ""<Keyboard>/leftArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""7d2a565c-8857-4742-b4eb-16caa7ec9e8d"",
                    ""path"": ""<Keyboard>/rightArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""2D Vector2"",
                    ""id"": ""f5c2fed1-ddd1-476a-ab3d-32ad64e546bd"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""0f54d659-c865-4303-afd0-d5b2cabb6b3d"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""2a15800b-873e-420b-8549-fd2ba03e635e"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""a67d5180-c09d-4681-9a85-fabe5e7eef6f"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""b928ed45-2a0b-4d54-bb97-c4d004394a68"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""7f9377cd-f071-471e-a86f-2757b3b3efdf"",
                    ""path"": ""<Gamepad>/buttonSouth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Jump"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""62b75892-bbda-48c9-9339-fa72c0584a7b"",
                    ""path"": ""<Keyboard>/space"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Jump"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""4c30e28f-bdfd-4bf6-8e28-ca14c5ae62e0"",
                    ""path"": ""<Gamepad>/rightTrigger"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Jump"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""a029cf68-843a-4cd8-8846-f06d64587f7d"",
                    ""path"": ""<Gamepad>/buttonWest"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Shot1"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""3b75b814-e4d2-4709-8e61-966d0eab7bb9"",
                    ""path"": ""<Keyboard>/z"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Shot1"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""a4ae6690-09c8-474c-b5f3-8552ec3297cb"",
                    ""path"": ""<Keyboard>/j"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Shot1"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""c5aa6eee-b333-43b0-832f-a5c2e141d234"",
                    ""path"": ""<Gamepad>/leftShoulder"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""ShotL"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""256931a1-ffc0-4d75-9aba-9146c96e0444"",
                    ""path"": ""<Keyboard>/q"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""ShotL"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""d73836a0-40e4-4112-8a43-c485924f3aee"",
                    ""path"": ""<Gamepad>/rightShoulder"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""ShotR"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""bcc69ddc-a543-46df-acb4-cba952c5e3ba"",
                    ""path"": ""<Keyboard>/e"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""ShotR"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""e51cadd9-12fe-43b9-8723-3529759987c8"",
                    ""path"": ""<Gamepad>/buttonNorth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Attack1"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""107f13ca-9ec1-4f86-a2da-8dde5c3a438e"",
                    ""path"": ""<Keyboard>/x"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Attack1"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""ec5f9452-0660-48ad-841a-e2741ddbf98e"",
                    ""path"": ""<Keyboard>/shift"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Jet"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""d1442c91-3854-411a-9d60-d9239fe133b4"",
                    ""path"": ""<Gamepad>/leftTrigger"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Jet"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""5e06a005-f308-4557-acb9-600ca9337984"",
                    ""path"": ""<Gamepad>/rightStick"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""CameraMove"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""79c81524-addc-4118-9afa-c301a6c1ece9"",
                    ""path"": ""<DualShockGamepad>/rightStickPress"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""LockOn"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // PlayerController
        m_PlayerController = asset.FindActionMap("PlayerController", throwIfNotFound: true);
        m_PlayerController_Move = m_PlayerController.FindAction("Move", throwIfNotFound: true);
        m_PlayerController_Jump = m_PlayerController.FindAction("Jump", throwIfNotFound: true);
        m_PlayerController_Shot1 = m_PlayerController.FindAction("Shot1", throwIfNotFound: true);
        m_PlayerController_ShotL = m_PlayerController.FindAction("ShotL", throwIfNotFound: true);
        m_PlayerController_ShotR = m_PlayerController.FindAction("ShotR", throwIfNotFound: true);
        m_PlayerController_Attack1 = m_PlayerController.FindAction("Attack1", throwIfNotFound: true);
        m_PlayerController_Jet = m_PlayerController.FindAction("Jet", throwIfNotFound: true);
        m_PlayerController_CameraMove = m_PlayerController.FindAction("CameraMove", throwIfNotFound: true);
        m_PlayerController_LockOn = m_PlayerController.FindAction("LockOn", throwIfNotFound: true);
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

    // PlayerController
    private readonly InputActionMap m_PlayerController;
    private IPlayerControllerActions m_PlayerControllerActionsCallbackInterface;
    private readonly InputAction m_PlayerController_Move;
    private readonly InputAction m_PlayerController_Jump;
    private readonly InputAction m_PlayerController_Shot1;
    private readonly InputAction m_PlayerController_ShotL;
    private readonly InputAction m_PlayerController_ShotR;
    private readonly InputAction m_PlayerController_Attack1;
    private readonly InputAction m_PlayerController_Jet;
    private readonly InputAction m_PlayerController_CameraMove;
    private readonly InputAction m_PlayerController_LockOn;
    public struct PlayerControllerActions
    {
        private @ControlAction m_Wrapper;
        public PlayerControllerActions(@ControlAction wrapper) { m_Wrapper = wrapper; }
        public InputAction @Move => m_Wrapper.m_PlayerController_Move;
        public InputAction @Jump => m_Wrapper.m_PlayerController_Jump;
        public InputAction @Shot1 => m_Wrapper.m_PlayerController_Shot1;
        public InputAction @ShotL => m_Wrapper.m_PlayerController_ShotL;
        public InputAction @ShotR => m_Wrapper.m_PlayerController_ShotR;
        public InputAction @Attack1 => m_Wrapper.m_PlayerController_Attack1;
        public InputAction @Jet => m_Wrapper.m_PlayerController_Jet;
        public InputAction @CameraMove => m_Wrapper.m_PlayerController_CameraMove;
        public InputAction @LockOn => m_Wrapper.m_PlayerController_LockOn;
        public InputActionMap Get() { return m_Wrapper.m_PlayerController; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(PlayerControllerActions set) { return set.Get(); }
        public void SetCallbacks(IPlayerControllerActions instance)
        {
            if (m_Wrapper.m_PlayerControllerActionsCallbackInterface != null)
            {
                @Move.started -= m_Wrapper.m_PlayerControllerActionsCallbackInterface.OnMove;
                @Move.performed -= m_Wrapper.m_PlayerControllerActionsCallbackInterface.OnMove;
                @Move.canceled -= m_Wrapper.m_PlayerControllerActionsCallbackInterface.OnMove;
                @Jump.started -= m_Wrapper.m_PlayerControllerActionsCallbackInterface.OnJump;
                @Jump.performed -= m_Wrapper.m_PlayerControllerActionsCallbackInterface.OnJump;
                @Jump.canceled -= m_Wrapper.m_PlayerControllerActionsCallbackInterface.OnJump;
                @Shot1.started -= m_Wrapper.m_PlayerControllerActionsCallbackInterface.OnShot1;
                @Shot1.performed -= m_Wrapper.m_PlayerControllerActionsCallbackInterface.OnShot1;
                @Shot1.canceled -= m_Wrapper.m_PlayerControllerActionsCallbackInterface.OnShot1;
                @ShotL.started -= m_Wrapper.m_PlayerControllerActionsCallbackInterface.OnShotL;
                @ShotL.performed -= m_Wrapper.m_PlayerControllerActionsCallbackInterface.OnShotL;
                @ShotL.canceled -= m_Wrapper.m_PlayerControllerActionsCallbackInterface.OnShotL;
                @ShotR.started -= m_Wrapper.m_PlayerControllerActionsCallbackInterface.OnShotR;
                @ShotR.performed -= m_Wrapper.m_PlayerControllerActionsCallbackInterface.OnShotR;
                @ShotR.canceled -= m_Wrapper.m_PlayerControllerActionsCallbackInterface.OnShotR;
                @Attack1.started -= m_Wrapper.m_PlayerControllerActionsCallbackInterface.OnAttack1;
                @Attack1.performed -= m_Wrapper.m_PlayerControllerActionsCallbackInterface.OnAttack1;
                @Attack1.canceled -= m_Wrapper.m_PlayerControllerActionsCallbackInterface.OnAttack1;
                @Jet.started -= m_Wrapper.m_PlayerControllerActionsCallbackInterface.OnJet;
                @Jet.performed -= m_Wrapper.m_PlayerControllerActionsCallbackInterface.OnJet;
                @Jet.canceled -= m_Wrapper.m_PlayerControllerActionsCallbackInterface.OnJet;
                @CameraMove.started -= m_Wrapper.m_PlayerControllerActionsCallbackInterface.OnCameraMove;
                @CameraMove.performed -= m_Wrapper.m_PlayerControllerActionsCallbackInterface.OnCameraMove;
                @CameraMove.canceled -= m_Wrapper.m_PlayerControllerActionsCallbackInterface.OnCameraMove;
                @LockOn.started -= m_Wrapper.m_PlayerControllerActionsCallbackInterface.OnLockOn;
                @LockOn.performed -= m_Wrapper.m_PlayerControllerActionsCallbackInterface.OnLockOn;
                @LockOn.canceled -= m_Wrapper.m_PlayerControllerActionsCallbackInterface.OnLockOn;
            }
            m_Wrapper.m_PlayerControllerActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Move.started += instance.OnMove;
                @Move.performed += instance.OnMove;
                @Move.canceled += instance.OnMove;
                @Jump.started += instance.OnJump;
                @Jump.performed += instance.OnJump;
                @Jump.canceled += instance.OnJump;
                @Shot1.started += instance.OnShot1;
                @Shot1.performed += instance.OnShot1;
                @Shot1.canceled += instance.OnShot1;
                @ShotL.started += instance.OnShotL;
                @ShotL.performed += instance.OnShotL;
                @ShotL.canceled += instance.OnShotL;
                @ShotR.started += instance.OnShotR;
                @ShotR.performed += instance.OnShotR;
                @ShotR.canceled += instance.OnShotR;
                @Attack1.started += instance.OnAttack1;
                @Attack1.performed += instance.OnAttack1;
                @Attack1.canceled += instance.OnAttack1;
                @Jet.started += instance.OnJet;
                @Jet.performed += instance.OnJet;
                @Jet.canceled += instance.OnJet;
                @CameraMove.started += instance.OnCameraMove;
                @CameraMove.performed += instance.OnCameraMove;
                @CameraMove.canceled += instance.OnCameraMove;
                @LockOn.started += instance.OnLockOn;
                @LockOn.performed += instance.OnLockOn;
                @LockOn.canceled += instance.OnLockOn;
            }
        }
    }
    public PlayerControllerActions @PlayerController => new PlayerControllerActions(this);
    public interface IPlayerControllerActions
    {
        void OnMove(InputAction.CallbackContext context);
        void OnJump(InputAction.CallbackContext context);
        void OnShot1(InputAction.CallbackContext context);
        void OnShotL(InputAction.CallbackContext context);
        void OnShotR(InputAction.CallbackContext context);
        void OnAttack1(InputAction.CallbackContext context);
        void OnJet(InputAction.CallbackContext context);
        void OnCameraMove(InputAction.CallbackContext context);
        void OnLockOn(InputAction.CallbackContext context);
    }
}
