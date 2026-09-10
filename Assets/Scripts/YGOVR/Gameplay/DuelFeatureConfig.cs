using UnityEngine;

namespace YGOVR.Gameplay
{
    public enum DuelFeature
    {
        Multiplayer,
        LocalMultiplayer,
        OnlineMultiplayer,
        ArMr,
        RuleVariants,
        RidingDuel,
        ActionDuel,
        ShadowDuel,
        DestinyDraw,
        StormAccess,
        HolographicBattles,
        SpectatorMode,
        AvatarCustomization,
        ProximityVoiceChat,
        VoiceEmotes,
        CardHighlighting,
        InteractiveFields
    }

    [CreateAssetMenu(fileName = "DuelFeatureConfig", menuName = "YGOVR/Gameplay/Duel Feature Config")]
    public class DuelFeatureConfig : ScriptableObject
    {
        [Header("Core Modes")]
        [SerializeField] private bool multiplayerEnabled = true;
        [SerializeField] private bool localMultiplayerEnabled = true;
        [SerializeField] private bool onlineMultiplayerEnabled = true;
        [SerializeField] private bool arMrEnabled;

        [Header("Duel Variants")]
        [SerializeField] private bool ruleVariantsEnabled;
        [SerializeField] private bool ridingDuelEnabled;
        [SerializeField] private bool actionDuelEnabled;
        [SerializeField] private bool shadowDuelEnabled;

        [Header("Gameplay Abilities")]
        [SerializeField] private bool destinyDrawEnabled;
        [SerializeField] private bool stormAccessEnabled;
        [SerializeField] private bool holographicBattlesEnabled;

        [Header("Social Features")]
        [SerializeField] private bool spectatorModeEnabled;
        [SerializeField] private bool avatarCustomizationEnabled;
        [SerializeField] private bool proximityVoiceChatEnabled;
        [SerializeField] private bool voiceEmotesEnabled;

        [Header("Interaction Features")]
        [SerializeField] private bool cardHighlightingEnabled = true;
        [SerializeField] private bool interactiveFieldsEnabled;

        public bool MultiplayerEnabled => multiplayerEnabled;
        public bool LocalMultiplayerEnabled => localMultiplayerEnabled;
        public bool OnlineMultiplayerEnabled => onlineMultiplayerEnabled;
        public bool ArMrEnabled => arMrEnabled;
        public bool RuleVariantsEnabled => ruleVariantsEnabled;
        public bool RidingDuelEnabled => ridingDuelEnabled;
        public bool ActionDuelEnabled => actionDuelEnabled;
        public bool ShadowDuelEnabled => shadowDuelEnabled;
        public bool DestinyDrawEnabled => destinyDrawEnabled;
        public bool StormAccessEnabled => stormAccessEnabled;
        public bool HolographicBattlesEnabled => holographicBattlesEnabled;
        public bool SpectatorModeEnabled => spectatorModeEnabled;
        public bool AvatarCustomizationEnabled => avatarCustomizationEnabled;
        public bool ProximityVoiceChatEnabled => proximityVoiceChatEnabled;
        public bool VoiceEmotesEnabled => voiceEmotesEnabled;
        public bool CardHighlightingEnabled => cardHighlightingEnabled;
        public bool InteractiveFieldsEnabled => interactiveFieldsEnabled;

        public bool IsEnabled(DuelFeature feature)
        {
            return feature switch
            {
                DuelFeature.Multiplayer => multiplayerEnabled,
                DuelFeature.LocalMultiplayer => localMultiplayerEnabled,
                DuelFeature.OnlineMultiplayer => onlineMultiplayerEnabled,
                DuelFeature.ArMr => arMrEnabled,
                DuelFeature.RuleVariants => ruleVariantsEnabled,
                DuelFeature.RidingDuel => ridingDuelEnabled,
                DuelFeature.ActionDuel => actionDuelEnabled,
                DuelFeature.ShadowDuel => shadowDuelEnabled,
                DuelFeature.DestinyDraw => destinyDrawEnabled,
                DuelFeature.StormAccess => stormAccessEnabled,
                DuelFeature.HolographicBattles => holographicBattlesEnabled,
                DuelFeature.SpectatorMode => spectatorModeEnabled,
                DuelFeature.AvatarCustomization => avatarCustomizationEnabled,
                DuelFeature.ProximityVoiceChat => proximityVoiceChatEnabled,
                DuelFeature.VoiceEmotes => voiceEmotesEnabled,
                DuelFeature.CardHighlighting => cardHighlightingEnabled,
                DuelFeature.InteractiveFields => interactiveFieldsEnabled,
                _ => false
            };
        }

        public void SetEnabled(DuelFeature feature, bool enabled)
        {
            switch (feature)
            {
                case DuelFeature.Multiplayer:
                    multiplayerEnabled = enabled;
                    break;
                case DuelFeature.LocalMultiplayer:
                    localMultiplayerEnabled = enabled;
                    break;
                case DuelFeature.OnlineMultiplayer:
                    onlineMultiplayerEnabled = enabled;
                    break;
                case DuelFeature.ArMr:
                    arMrEnabled = enabled;
                    break;
                case DuelFeature.RuleVariants:
                    ruleVariantsEnabled = enabled;
                    break;
                case DuelFeature.RidingDuel:
                    ridingDuelEnabled = enabled;
                    break;
                case DuelFeature.ActionDuel:
                    actionDuelEnabled = enabled;
                    break;
                case DuelFeature.ShadowDuel:
                    shadowDuelEnabled = enabled;
                    break;
                case DuelFeature.DestinyDraw:
                    destinyDrawEnabled = enabled;
                    break;
                case DuelFeature.StormAccess:
                    stormAccessEnabled = enabled;
                    break;
                case DuelFeature.HolographicBattles:
                    holographicBattlesEnabled = enabled;
                    break;
                case DuelFeature.SpectatorMode:
                    spectatorModeEnabled = enabled;
                    break;
                case DuelFeature.AvatarCustomization:
                    avatarCustomizationEnabled = enabled;
                    break;
                case DuelFeature.ProximityVoiceChat:
                    proximityVoiceChatEnabled = enabled;
                    break;
                case DuelFeature.VoiceEmotes:
                    voiceEmotesEnabled = enabled;
                    break;
                case DuelFeature.CardHighlighting:
                    cardHighlightingEnabled = enabled;
                    break;
                case DuelFeature.InteractiveFields:
                    interactiveFieldsEnabled = enabled;
                    break;
            }
        }

        public bool Toggle(DuelFeature feature)
        {
            var updated = !IsEnabled(feature);
            SetEnabled(feature, updated);
            return updated;
        }
    }
}
