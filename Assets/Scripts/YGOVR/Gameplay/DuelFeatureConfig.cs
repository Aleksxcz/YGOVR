using UnityEngine;

namespace YGOVR.Gameplay
{
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
    }
}
