namespace NetAF.Example.Lighthouse.Assets
{
    /// <summary>
    /// Shared variable and note keys used to track the state of the lighthouse puzzles.
    /// </summary>
    internal static class GameState
    {
        internal const string OilCanFilled = "OIL_CAN_FILLED";
        internal const string MatchesDry = "MATCHES_DRY";
        internal const string ClockworkWound = "CLOCKWORK_WOUND";
        internal const string LampReservoirFilled = "LAMP_RESERVOIR_FILLED";
        internal const string LampLit = "LAMP_LIT";

        private const string TrueValue = "TRUE";

        internal static bool IsSet(string key)
        {
            var game = NetAF.Logic.GameExecutor.ExecutingGame;
            return game != null && string.Equals(game.VariableManager.Get(key), TrueValue, System.StringComparison.OrdinalIgnoreCase);
        }

        internal static void Set(string key)
        {
            NetAF.Logic.GameExecutor.ExecutingGame?.VariableManager.Add(key, TrueValue);
        }
    }
}
