// GENERATED AUTOMATICALLY FROM 'Assets/Scripts/Controller.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class @Controller : IInputActionCollection, IDisposable
{
    public InputActionAsset asset { get; }
    public @Controller()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""Controller"",
    ""maps"": [
        {
            ""name"": ""Slider"",
            ""id"": ""032387aa-2da3-4779-a4f1-4bf9459e3ed6"",
            ""actions"": [
                {
                    ""name"": ""Move"",
                    ""type"": ""PassThrough"",
                    ""id"": ""2eb80f4a-2095-42b1-9bc3-c0c56e3f5d8b"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""TouchScreen"",
                    ""type"": ""Button"",
                    ""id"": ""ebf9f193-a0fb-4a93-932a-71048474aaed"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": ""SidewaysKeyboard"",
                    ""id"": ""9de09c42-635a-4d84-9496-17938a97591d"",
                    ""path"": ""1DAxis"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""negative"",
                    ""id"": ""9c1448d7-f791-4863-9071-090cc55da5e2"",
                    ""path"": ""<Keyboard>/leftArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""positive"",
                    ""id"": ""4f462959-b1b0-41a4-96db-eb434de0b96a"",
                    ""path"": ""<Keyboard>/rightArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""SidewaysScreen"",
                    ""id"": ""218555c0-695c-462f-9b80-e49ac73a66d6"",
                    ""path"": ""1DAxis"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""negative"",
                    ""id"": ""7b67e005-ecbd-4dcd-a7ac-5bc392482675"",
                    ""path"": ""<Gamepad>/buttonWest"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""positive"",
                    ""id"": ""ac642e42-0c8c-43b3-846d-a7b2186c8129"",
                    ""path"": ""<Gamepad>/buttonEast"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""0324807e-8b89-4fe2-8b0b-96e3d54d86e2"",
                    ""path"": ""<Touchscreen>/primaryTouch/indirectTouch"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""TouchScreen"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // Slider
        m_Slider = asset.FindActionMap("Slider", throwIfNotFound: true);
        m_Slider_Move = m_Slider.FindAction("Move", throwIfNotFound: true);
        m_Slider_TouchScreen = m_Slider.FindAction("TouchScreen", throwIfNotFound: true);
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

    // Slider
    private readonly InputActionMap m_Slider;
    private ISliderActions m_SliderActionsCallbackInterface;
    private readonly InputAction m_Slider_Move;
    private readonly InputAction m_Slider_TouchScreen;
    public struct SliderActions
    {
        private @Controller m_Wrapper;
        public SliderActions(@Controller wrapper) { m_Wrapper = wrapper; }
        public InputAction @Move => m_Wrapper.m_Slider_Move;
        public InputAction @TouchScreen => m_Wrapper.m_Slider_TouchScreen;
        public InputActionMap Get() { return m_Wrapper.m_Slider; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(SliderActions set) { return set.Get(); }
        public void SetCallbacks(ISliderActions instance)
        {
            if (m_Wrapper.m_SliderActionsCallbackInterface != null)
            {
                @Move.started -= m_Wrapper.m_SliderActionsCallbackInterface.OnMove;
                @Move.performed -= m_Wrapper.m_SliderActionsCallbackInterface.OnMove;
                @Move.canceled -= m_Wrapper.m_SliderActionsCallbackInterface.OnMove;
                @TouchScreen.started -= m_Wrapper.m_SliderActionsCallbackInterface.OnTouchScreen;
                @TouchScreen.performed -= m_Wrapper.m_SliderActionsCallbackInterface.OnTouchScreen;
                @TouchScreen.canceled -= m_Wrapper.m_SliderActionsCallbackInterface.OnTouchScreen;
            }
            m_Wrapper.m_SliderActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Move.started += instance.OnMove;
                @Move.performed += instance.OnMove;
                @Move.canceled += instance.OnMove;
                @TouchScreen.started += instance.OnTouchScreen;
                @TouchScreen.performed += instance.OnTouchScreen;
                @TouchScreen.canceled += instance.OnTouchScreen;
            }
        }
    }
    public SliderActions @Slider => new SliderActions(this);
    public interface ISliderActions
    {
        void OnMove(InputAction.CallbackContext context);
        void OnTouchScreen(InputAction.CallbackContext context);
    }
}
