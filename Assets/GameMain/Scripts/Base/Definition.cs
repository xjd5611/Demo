public static partial class Definition
{
    public static class AssetPath
    {
        public const string Card = "Assets/GameMain/Prefabs/Card.prefab";
        public const string CharacterPath = "Assets/GameMain/Prefabs/Character/{0}.prefab";
        public const string GetHitFXPath = "Assets/GameMain/Prefabs/FX/GetHitFX.prefab";
    }

    public static class UIPath
    {
        public const string LoadData = "Assets/GameMain/Prefabs/UI/Loading.prefab";
        public const string CharacterUI = "Assets/GameMain/Prefabs/UI/CharacterUI.prefab";
        public const string PlayerInteractUI = "Assets/GameMain/Prefabs/UI/PlayerInteractUI.prefab";
        public const string GameOverUI = "Assets/GameMain/Prefabs/UI/GameOverUI.prefab";
    }

    public static class Group
    {
        public const string UI = "UIGroup";
        public const string Cards = "Cards";
        public const string CharacterGroup = "CharacterGroup";
        public const string FXGroup = "FXGroup";
    }

    public static class RoleState
    {
        public const string Blood = "Blood";
        public const string Coma = "Coma";
        public const string Electrocuted = "Electrocuted";
        public const string Frezon = "Frezon";
        public const string Poisoning = "Poisoning";
    }

    public static class Node
    {
        public const string AllCardPool = "AllCardPool";
        public const string AttackCardPool = "AttackCardPool";
        public const string DefenseCardPool = "DefenseCardPool";
        public const string SkillCardPool = "SkillCardPool";

        public const string PlayerCardsNode = "PlayerCardsNode";
        public const string DeskCardsNode = "DeskCardsNode";
        public const string HandCardsNode = "HandCardsNode";
        public const string GraveCardsNode = "GraveCardsNode";

        public const string RoleNode = "RoleNode";
        public const string MonsterNode = "MonsterNode";
        public const string ActionDataNode = "ActionDataNode";
        public const string GameDataNode = "TurnDataNode";
        public const string MonsterAINode = "MonsterAINode";
    }

    public static class Enum 
    {
        public enum ActionType {µã, ´Ì,Åü ,É¨}
        public enum CardType
        {
            Unknown,
            AttackCard,
            DefenseCard,
            SkillCard,
        }
        public enum CardState
        {
            Unknown,
            InHands,
            InDesk,
            InGrave,
        }
    }

}