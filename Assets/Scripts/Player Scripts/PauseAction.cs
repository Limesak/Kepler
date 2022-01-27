// GENERATED AUTOMATICALLY FROM 'Assets/Scripts/Player Scripts/PauseAction.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

namespace AsteroidBelt.Player_Scripts
{
    public class @PauseAction : IInputActionCollection, IDisposable
    {
        public InputActionAsset asset { get; }
        public @PauseAction()
        {
            asset = InputActionAsset.FromJson(@"{
    ""name"": ""PauseAction"",
    ""maps"": [
        {
            ""name"": ""Pause"",
            ""id"": ""9b4fbeab-ac6b-4085-9948-f9800cb20170"",
            ""actions"": [
                {
                    ""name"": ""PauseGame"",
                    ""type"": ""Button"",
                    ""id"": ""75a81f73-8d8a-4d28-8952-38286518f75b"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""e47e0df0-89b4-4040-824d-56e43f436a85"",
                    ""path"": ""<Gamepad>/start"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""PauseGame"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""ffea5534-f76f-4fde-a7aa-afc38654ec19"",
                    ""path"": ""<Keyboard>/escape"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""PauseGame"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
            // Pause
            m_Pause = asset.FindActionMap("Pause", throwIfNotFound: true);
            m_Pause_PauseGame = m_Pause.FindAction("PauseGame", throwIfNotFound: true);
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

        // Pause
        private readonly InputActionMap m_Pause;
        private IPauseActions m_PauseActionsCallbackInterface;
        private readonly InputAction m_Pause_PauseGame;
        public struct PauseActions
        {
            private @PauseAction m_Wrapper;
            public PauseActions(@PauseAction wrapper) { m_Wrapper = wrapper; }
            public InputAction @PauseGame => m_Wrapper.m_Pause_PauseGame;
            public InputActionMap Get() { return m_Wrapper.m_Pause; }
            public void Enable() { Get().Enable(); }
            public void Disable() { Get().Disable(); }
            public bool enabled => Get().enabled;
            public static implicit operator InputActionMap(PauseActions set) { return set.Get(); }
            public void SetCallbacks(IPauseActions instance)
            {
                if (m_Wrapper.m_PauseActionsCallbackInterface != null)
                {
                    @PauseGame.started -= m_Wrapper.m_PauseActionsCallbackInterface.OnPauseGame;
                    @PauseGame.performed -= m_Wrapper.m_PauseActionsCallbackInterface.OnPauseGame;
                    @PauseGame.canceled -= m_Wrapper.m_PauseActionsCallbackInterface.OnPauseGame;
                }
                m_Wrapper.m_PauseActionsCallbackInterface = instance;
                if (instance != null)
                {
                    @PauseGame.started += instance.OnPauseGame;
                    @PauseGame.performed += instance.OnPauseGame;
                    @PauseGame.canceled += instance.OnPauseGame;
                }
            }
        }
        public PauseActions @Pause => new PauseActions(this);
        public interface IPauseActions
        {
            void OnPauseGame(InputAction.CallbackContext context);
        }
    }
}
