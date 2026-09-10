using System;
using UnityEngine;
using YGOVR.Gameplay;
using YGOVR.Multiplayer;

namespace YGOVR.Demo
{
    public class PlayableDemoController : MonoBehaviour
    {
        private const string LocalPlayerId = "PlayerOne";
        private const string OpponentPlayerId = "PlayerTwo";

        [SerializeField] private Transform demoRoot;
        [SerializeField] private DuelPlaymatLayout playmatLayout;
        [SerializeField] private DuelFeatureConfig featureConfig;
        [SerializeField] private LifePointManager lifePointManager;
        [SerializeField] private MultiplayerWorldSession worldSession;
        [SerializeField] private PlayerPresenceAnchor localPresence;
        [SerializeField] private PlayerPresenceAnchor opponentPresence;
        [SerializeField] private DuelCardInteraction[] demoCards = Array.Empty<DuelCardInteraction>();

        private int selectedCardIndex;
        private int selectedSlotIndex;
        private bool selectedCardHovered;
        private DuelSide selectedSide = DuelSide.Near;
        private int selectedFeatureIndex;
        private DuelFeature[] allFeatures = Array.Empty<DuelFeature>();

        private void Awake()
        {
            allFeatures = (DuelFeature[])Enum.GetValues(typeof(DuelFeature));
            EnsureSystems();
            InitializeDemoState();
        }

        private void Update()
        {
            HandleKeyboardInput();
            UpdatePresenceSimulation();
        }

        private void OnGUI()
        {
            GUI.Label(new Rect(10, 10, 900, 300),
                $"YGOVR Demo Controls\n" +
                $"H Host | J Join | P Add Opponent | L Leave Local\n" +
                $"1/2 Damage/Heal P1 | 3/4 Damage/Heal P2 | R Toggle Local Ready | O Toggle Opponent Spectator\n" +
                $"Tab Next Card | C Hover Highlight/Zoom | Z Next Monster Slot | X Switch Side\n" +
                $"[/] Feature Select | F Toggle Feature\n\n" +
                $"Session: {(worldSession != null ? worldSession.SessionCode : "-")} Players: {(worldSession != null ? worldSession.ConnectedPlayers.Count : 0)}\n" +
                $"LP P1: {(lifePointManager != null ? lifePointManager.PlayerOne.CurrentLifePoints : 0)} | LP P2: {(lifePointManager != null ? lifePointManager.PlayerTwo.CurrentLifePoints : 0)}\n" +
                $"Selected Feature: {(allFeatures.Length > 0 ? allFeatures[selectedFeatureIndex].ToString() : "-")} = {(allFeatures.Length > 0 && featureConfig != null ? featureConfig.IsEnabled(allFeatures[selectedFeatureIndex]) : false)}\n" +
                $"Selected Card: {selectedCardIndex + 1}, Side: {selectedSide}, Slot: {selectedSlotIndex}");
        }

        private void EnsureSystems()
        {
            if (demoRoot == null)
            {
                demoRoot = new GameObject("Demo Root").transform;
            }

            if (playmatLayout == null)
            {
                playmatLayout = DuelPlaymatLayout.CreateRuntimeDefault();
            }

            if (featureConfig == null)
            {
                featureConfig = ScriptableObject.CreateInstance<DuelFeatureConfig>();
            }

            if (lifePointManager == null)
            {
                lifePointManager = new GameObject("Life Point Manager").AddComponent<LifePointManager>();
            }

            if (worldSession == null)
            {
                worldSession = new GameObject("Multiplayer World Session").AddComponent<MultiplayerWorldSession>();
            }

            EnsurePlaymatVisual();
            EnsurePresenceAnchors();
            EnsureDemoCards();
        }

        private void EnsurePlaymatVisual()
        {
            var playmat = GameObject.CreatePrimitive(PrimitiveType.Plane);
            playmat.name = "Lifesize Duel Playmat";
            playmat.transform.SetParent(demoRoot, false);
            playmat.transform.localScale = new Vector3(playmatLayout.PlaymatWidth / 10f, 1f, playmatLayout.PlaymatLength / 10f);
            playmat.transform.localPosition = Vector3.zero;
        }

        private void EnsurePresenceAnchors()
        {
            localPresence = CreatePresenceAnchor("Local Presence Anchor", LocalPlayerId, new Vector3(0f, 0f, -0.8f));
            opponentPresence = CreatePresenceAnchor("Opponent Presence Anchor", OpponentPlayerId, new Vector3(0f, 0f, 0.8f));
        }

        private PlayerPresenceAnchor CreatePresenceAnchor(string objectName, string playerId, Vector3 localPosition)
        {
            var root = new GameObject(objectName);
            root.transform.SetParent(demoRoot, false);
            root.transform.localPosition = localPosition;

            var head = GameObject.CreatePrimitive(PrimitiveType.Sphere).transform;
            head.name = "Head";
            head.SetParent(root.transform, false);
            head.localScale = Vector3.one * 0.2f;
            head.localPosition = new Vector3(0f, 1.7f, 0f);

            var leftHand = GameObject.CreatePrimitive(PrimitiveType.Cube).transform;
            leftHand.name = "LeftHand";
            leftHand.SetParent(root.transform, false);
            leftHand.localScale = new Vector3(0.1f, 0.1f, 0.1f);
            leftHand.localPosition = new Vector3(-0.2f, 1.4f, 0.2f);

            var rightHand = GameObject.CreatePrimitive(PrimitiveType.Cube).transform;
            rightHand.name = "RightHand";
            rightHand.SetParent(root.transform, false);
            rightHand.localScale = new Vector3(0.1f, 0.1f, 0.1f);
            rightHand.localPosition = new Vector3(0.2f, 1.4f, 0.2f);

            var anchor = root.AddComponent<PlayerPresenceAnchor>();
            anchor.AssignPlayer(playerId);

            return anchor;
        }

        private void EnsureDemoCards()
        {
            if (demoCards.Length > 0)
            {
                return;
            }

            demoCards = new DuelCardInteraction[2];
            for (var i = 0; i < demoCards.Length; i++)
            {
                var card = GameObject.CreatePrimitive(PrimitiveType.Cube);
                card.name = $"Demo Card {i + 1}";
                card.transform.SetParent(demoRoot, false);
                card.transform.localScale = new Vector3(0.2f, 0.02f, 0.3f);
                demoCards[i] = card.AddComponent<DuelCardInteraction>();
            }

            MoveCardToMonsterZone(0, DuelSide.Near, 0);
            MoveCardToMonsterZone(1, DuelSide.Far, 0);
        }

        private void InitializeDemoState()
        {
            lifePointManager.ConfigurePlayers(LocalPlayerId, OpponentPlayerId, 8000);
            worldSession.HostSession(LocalPlayerId, "Local Duelist");
            worldSession.RegisterOrUpdatePlayer(OpponentPlayerId, "Remote Duelist");
        }

        private void HandleKeyboardInput()
        {
            if (Input.GetKeyDown(KeyCode.H))
            {
                worldSession.HostSession(LocalPlayerId, "Local Duelist");
            }

            if (Input.GetKeyDown(KeyCode.J))
            {
                worldSession.JoinSession(worldSession.SessionCode, LocalPlayerId, "Guest Duelist");
            }

            if (Input.GetKeyDown(KeyCode.P))
            {
                worldSession.RegisterOrUpdatePlayer(OpponentPlayerId, "Remote Duelist");
            }

            if (Input.GetKeyDown(KeyCode.L))
            {
                worldSession.LeaveSession(LocalPlayerId);
            }

            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                lifePointManager.ApplyDamage(LocalPlayerId, 500);
            }

            if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                lifePointManager.RestoreLifePoints(LocalPlayerId, 500);
            }

            if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                lifePointManager.ApplyDamage(OpponentPlayerId, 500);
            }

            if (Input.GetKeyDown(KeyCode.Alpha4))
            {
                lifePointManager.RestoreLifePoints(OpponentPlayerId, 500);
            }

            if (Input.GetKeyDown(KeyCode.R))
            {
                var isReady = worldSession.SetPlayerReady(LocalPlayerId, !worldSession.LocalPlayer.IsReady);
                if (!isReady)
                {
                    worldSession.RegisterOrUpdatePlayer(LocalPlayerId, "Local Duelist", false, true);
                }
            }

            if (Input.GetKeyDown(KeyCode.O))
            {
                var opponent = FindPlayer(OpponentPlayerId);
                var next = opponent == null || !opponent.IsSpectator;
                worldSession.SetPlayerSpectator(OpponentPlayerId, next);
            }

            if (Input.GetKeyDown(KeyCode.Tab))
            {
                selectedCardIndex = (selectedCardIndex + 1) % demoCards.Length;
            }

            if (Input.GetKeyDown(KeyCode.C))
            {
                if (!featureConfig.IsEnabled(DuelFeature.CardHighlighting))
                {
                    return;
                }

                selectedCardHovered = !selectedCardHovered;
                demoCards[selectedCardIndex].SetHovered(selectedCardHovered);
            }

            if (Input.GetKeyDown(KeyCode.X))
            {
                selectedSide = selectedSide == DuelSide.Near ? DuelSide.Far : DuelSide.Near;
            }

            if (Input.GetKeyDown(KeyCode.Z))
            {
                selectedSlotIndex = (selectedSlotIndex + 1) % 5;
                MoveCardToMonsterZone(selectedCardIndex, selectedSide, selectedSlotIndex);
            }

            if (Input.GetKeyDown(KeyCode.LeftBracket))
            {
                selectedFeatureIndex = selectedFeatureIndex == 0 ? allFeatures.Length - 1 : selectedFeatureIndex - 1;
            }

            if (Input.GetKeyDown(KeyCode.RightBracket))
            {
                selectedFeatureIndex = (selectedFeatureIndex + 1) % allFeatures.Length;
            }

            if (Input.GetKeyDown(KeyCode.F))
            {
                featureConfig.Toggle(allFeatures[selectedFeatureIndex]);
            }
        }

        private MultiplayerPlayerState FindPlayer(string playerId)
        {
            foreach (var player in worldSession.ConnectedPlayers)
            {
                if (string.Equals(player.PlayerId, playerId, StringComparison.OrdinalIgnoreCase))
                {
                    return player;
                }
            }

            return null;
        }

        private void MoveCardToMonsterZone(int cardIndex, DuelSide side, int slotIndex)
        {
            if (cardIndex < 0 || cardIndex >= demoCards.Length)
            {
                return;
            }

            if (!playmatLayout.TryGetZonePosition(side, PlaymatZoneType.Monster, slotIndex, out var localPosition))
            {
                return;
            }

            var cardTransform = demoCards[cardIndex].transform;
            cardTransform.localPosition = localPosition + Vector3.up * 0.02f;
            cardTransform.localRotation = Quaternion.Euler(90f, side == DuelSide.Near ? 180f : 0f, 0f);
        }

        private void UpdatePresenceSimulation()
        {
            var time = Time.time;
            var localHead = new Vector3(-0.15f, 1.7f, -0.8f + Mathf.Sin(time) * 0.03f);
            var localLeft = new Vector3(-0.35f, 1.4f, -0.7f);
            var localRight = new Vector3(0.05f, 1.4f, -0.7f);
            localPresence.ApplyPose(localHead, Quaternion.identity, localLeft, Quaternion.identity, localRight, Quaternion.identity);

            var opponentSpectating = FindPlayer(OpponentPlayerId)?.IsSpectator ?? false;
            var opponentHead = new Vector3(0.15f, opponentSpectating ? 2.2f : 1.7f, 0.8f + Mathf.Sin(time * 1.3f) * 0.03f);
            var opponentLeft = new Vector3(-0.05f, opponentSpectating ? 2f : 1.4f, 0.7f);
            var opponentRight = new Vector3(0.35f, opponentSpectating ? 2f : 1.4f, 0.7f);
            opponentPresence.ApplyPose(opponentHead, Quaternion.identity, opponentLeft, Quaternion.identity, opponentRight, Quaternion.identity);
        }
    }
}
