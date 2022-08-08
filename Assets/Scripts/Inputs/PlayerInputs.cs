// GENERATED AUTOMATICALLY FROM 'Assets/Scripts/Inputs/PlayerInputs.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class @PlayerInputs : IInputActionCollection, IDisposable
{
    public InputActionAsset asset { get; }
    public @PlayerInputs()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""PlayerInputs"",
    ""maps"": [
        {
            ""name"": ""Game"",
            ""id"": ""317e9c1e-766a-461c-a166-1799f79f9b63"",
            ""actions"": [
                {
                    ""name"": ""FirstSkill"",
                    ""type"": ""Button"",
                    ""id"": ""7147abfa-3323-42ab-a7a9-dd82d63fea45"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""SecondSkill"",
                    ""type"": ""Button"",
                    ""id"": ""a2417224-3ce0-446c-8894-6be25a44d0bb"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""ThirdSkill"",
                    ""type"": ""Button"",
                    ""id"": ""72cc5cd9-bc26-497a-bb5d-4e3019f1ab68"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Movement"",
                    ""type"": ""Button"",
                    ""id"": ""b70ce56f-8bec-4d54-8dbe-fb9c02029c3f"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""09d73aa0-94c9-4bbc-8d91-f2630881a34d"",
                    ""path"": ""<Keyboard>/q"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""KeyboardMouse"",
                    ""action"": ""FirstSkill"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""e9c3faf4-7b0b-4c37-bc2e-e55fedce4940"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""KeyboardMouse"",
                    ""action"": ""SecondSkill"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""6a240c04-3926-4634-9ec2-5706e4ed0108"",
                    ""path"": ""<Keyboard>/e"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""KeyboardMouse"",
                    ""action"": ""ThirdSkill"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""2924edbd-cd2f-42f0-8e27-e7b9a3f078af"",
                    ""path"": ""<Mouse>/rightButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": [
        {
            ""name"": ""KeyboardMouse"",
            ""bindingGroup"": ""KeyboardMouse"",
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
        }
    ]
}");
        // Game
        m_Game = asset.FindActionMap("Game", throwIfNotFound: true);
        m_Game_FirstSkill = m_Game.FindAction("FirstSkill", throwIfNotFound: true);
        m_Game_SecondSkill = m_Game.FindAction("SecondSkill", throwIfNotFound: true);
        m_Game_ThirdSkill = m_Game.FindAction("ThirdSkill", throwIfNotFound: true);
        m_Game_Movement = m_Game.FindAction("Movement", throwIfNotFound: true);
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

    // Game
    private readonly InputActionMap m_Game;
    private IGameActions m_GameActionsCallbackInterface;
    private readonly InputAction m_Game_FirstSkill;
    private readonly InputAction m_Game_SecondSkill;
    private readonly InputAction m_Game_ThirdSkill;
    private readonly InputAction m_Game_Movement;
    public struct GameActions
    {
        private @PlayerInputs m_Wrapper;
        public GameActions(@PlayerInputs wrapper) { m_Wrapper = wrapper; }
        public InputAction @FirstSkill => m_Wrapper.m_Game_FirstSkill;
        public InputAction @SecondSkill => m_Wrapper.m_Game_SecondSkill;
        public InputAction @ThirdSkill => m_Wrapper.m_Game_ThirdSkill;
        public InputAction @Movement => m_Wrapper.m_Game_Movement;
        public InputActionMap Get() { return m_Wrapper.m_Game; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(GameActions set) { return set.Get(); }
        public void SetCallbacks(IGameActions instance)
        {
            if (m_Wrapper.m_GameActionsCallbackInterface != null)
            {
                @FirstSkill.started -= m_Wrapper.m_GameActionsCallbackInterface.OnFirstSkill;
                @FirstSkill.performed -= m_Wrapper.m_GameActionsCallbackInterface.OnFirstSkill;
                @FirstSkill.canceled -= m_Wrapper.m_GameActionsCallbackInterface.OnFirstSkill;
                @SecondSkill.started -= m_Wrapper.m_GameActionsCallbackInterface.OnSecondSkill;
                @SecondSkill.performed -= m_Wrapper.m_GameActionsCallbackInterface.OnSecondSkill;
                @SecondSkill.canceled -= m_Wrapper.m_GameActionsCallbackInterface.OnSecondSkill;
                @ThirdSkill.started -= m_Wrapper.m_GameActionsCallbackInterface.OnThirdSkill;
                @ThirdSkill.performed -= m_Wrapper.m_GameActionsCallbackInterface.OnThirdSkill;
                @ThirdSkill.canceled -= m_Wrapper.m_GameActionsCallbackInterface.OnThirdSkill;
                @Movement.started -= m_Wrapper.m_GameActionsCallbackInterface.OnMovement;
                @Movement.performed -= m_Wrapper.m_GameActionsCallbackInterface.OnMovement;
                @Movement.canceled -= m_Wrapper.m_GameActionsCallbackInterface.OnMovement;
            }
            m_Wrapper.m_GameActionsCallbackInterface = instance;
            if (instance != null)
            {
                @FirstSkill.started += instance.OnFirstSkill;
                @FirstSkill.performed += instance.OnFirstSkill;
                @FirstSkill.canceled += instance.OnFirstSkill;
                @SecondSkill.started += instance.OnSecondSkill;
                @SecondSkill.performed += instance.OnSecondSkill;
                @SecondSkill.canceled += instance.OnSecondSkill;
                @ThirdSkill.started += instance.OnThirdSkill;
                @ThirdSkill.performed += instance.OnThirdSkill;
                @ThirdSkill.canceled += instance.OnThirdSkill;
                @Movement.started += instance.OnMovement;
                @Movement.performed += instance.OnMovement;
                @Movement.canceled += instance.OnMovement;
            }
        }
    }
    public GameActions @Game => new GameActions(this);
    private int m_KeyboardMouseSchemeIndex = -1;
    public InputControlScheme KeyboardMouseScheme
    {
        get
        {
            if (m_KeyboardMouseSchemeIndex == -1) m_KeyboardMouseSchemeIndex = asset.FindControlSchemeIndex("KeyboardMouse");
            return asset.controlSchemes[m_KeyboardMouseSchemeIndex];
        }
    }
    public interface IGameActions
    {
        void OnFirstSkill(InputAction.CallbackContext context);
        void OnSecondSkill(InputAction.CallbackContext context);
        void OnThirdSkill(InputAction.CallbackContext context);
        void OnMovement(InputAction.CallbackContext context);
    }
}
