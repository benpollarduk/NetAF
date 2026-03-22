using NetAF.Logic;
using NetAF.Persistence.Json;
using System;
using System.Data;
using System.IO;
using System.Linq;

namespace NetAF.Persistence
{
    /// <summary>
    /// Provides a manager for restore points.
    /// </summary>
    public static class RestorePointManager
    {
        #region StaticProperties

        /// <summary>
        /// Get or set the name to use for auto saves.
        /// </summary>
        public static string AutoFileName { get; set; } = "Auto";

        /// <summary>
        /// Get or set the extension to use for saves.
        /// </summary>
        public static string Extension { get; set; } = "netaf";

        /// <summary>
        /// Get or set the root directory for saves.
        /// </summary>
        public static string RootDirectory { get; set; } = DefaultRootDirectory;

        /// <summary>
        /// Get the default root directory for saves.
        /// </summary>
        public static string DefaultRootDirectory => Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "NetAF");

        #endregion

        #region StaticMethods

        /// <summary>
        /// Get the directory that is used for restore points for a game.
        /// </summary>
        /// <param name="game">The game.</param>
        /// <returns>The directory used for retsore points for the provided game.</returns>
        public static string GetRestorePointDirectory(Game game)
        {
            return Path.Combine(RootDirectory, game.Info.Name);
        }

        /// <summary>
        /// Autosave a game.
        /// </summary>
        /// <param name="game">The game to save.</param>
        /// <param name="restorePoint">The created restore point.</param>
        /// <param name="message">A message detailing the result of the save, if the save was unsuccessful. If the save was successful this will be empty.</param>
        /// <returns>True if the save was successful, else false.</returns>
        public static bool Save(Game game, out RestorePoint restorePoint, out string message)
        {
            return Save(game, AutoFileName, out restorePoint, out message);
        }

        /// <summary>
        /// Save a game.
        /// </summary>
        /// <param name="game">The game to save.</param>
        /// <param name="name">The name of the restore point.</param>
        /// <param name="restorePoint">The created restore point.</param>
        /// <param name="message">A message detailing the result of the save, if the save was unsuccessful. If the save was successful this will be empty.</param>
        /// <returns>True if the save was successful, else false.</returns>
        public static bool Save(Game game, string name, out RestorePoint restorePoint, out string message)
        {
            if (game == null)
            {
                restorePoint = null;
                message = "No game to save.";
                return false;
            }

            restorePoint = RestorePoint.Create(name, game);
            var path = GetFilePath(game, name);

            return JsonSave.ToFile(path, restorePoint, out message);
        }

        /// <summary>
        /// Try and load a restore point.
        /// </summary>
        /// <param name="path">The path to the restore point.</param>
        /// <param name="restorePoint">The loaded restore point.</param>
        /// <param name="message">A message detailing the result of the load, if the load was unsuccessful. If the load was successful this will be empty.</param>
        /// <returns>True if the load was successful, else false.</returns>
        private static bool TryLoad(string path, out RestorePoint restorePoint, out string message)
        {
            if (!File.Exists(path))
            {
                restorePoint = null;
                message = "No file found.";
                return false;
            }

            return JsonSave.FromFile(path, out restorePoint, out message);
        }

        /// <summary>
        /// Get the available restore point names for a game.
        /// </summary>
        /// <param name="game">The game to get the restore points for.</param>
        /// <returns>An array containing all the available restore points for a game.</returns>
        public static string[] GetAvailableRestorePointNames(Game game)
        {
            var path = GetRestorePointDirectory(game);

            if (!Path.Exists(path))
                return [];

            var files = Directory.GetFiles(path, $"*.{Extension}", SearchOption.TopDirectoryOnly);
            return [.. files.Select(x => Path.GetFileNameWithoutExtension(x))];
        }

        /// <summary>
        /// Get the available restore points for a game.
        /// </summary>
        /// <param name="game">The game to get the restore points for.</param>
        /// <returns>An array containing all the available restore points for a game.</returns>
        public static RestorePoint[] GetAvailableRestorePoints(Game game)
        {
            var fileNames = GetAvailableRestorePointNames(game);

            return [.. fileNames.Select(x =>
            {
                var path = GetFilePath(game, x);
                return TryLoad(path, out var restorePoint, out _) ? restorePoint : null;
            }).Where(x => x != null)];
        }

        /// <summary>
        /// Try and apply the auto save.
        /// </summary>
        /// <param name="game">The game to apply the restore point to.</param>
        /// <param name="message">A message detailing the result of the load, if the load was unsuccessful. If the load was successful this will be empty.</param>
        /// <returns>True if the load was successful else false.</returns>
        public static bool Apply(Game game, out string message)
        {
            return Apply(game, AutoFileName, out message);
        }

        /// <summary>
        /// Try and apply a restore point.
        /// </summary>
        /// <param name="game">The game to apply the restore point to.</param>
        /// <param name="name">The name of the restore point.</param>
        /// <param name="message">A message detailing the result of the load, if the load was unsuccessful. If the load was successful this will be empty.</param>
        /// <returns>True if the load was successful, else false.</returns>
        public static bool Apply(Game game, string name, out string message)
        {
            var path = GetFilePath(game, name);

            if (!TryLoad(path, out var restorePoint, out message) || restorePoint == null)
                return false;

            game.RestoreFrom(restorePoint.Game);

            return true;
        }

        /// <summary>
        /// Determine if a restore point exists.
        /// </summary>
        /// <param name="game">The game.</param>
        /// <param name="name">The name of the restore point.</param>
        /// <returns>True if the restore point exists, else false.</returns>
        public static bool Exists(Game game, string name)
        {
            var path = GetFilePath(game, name);
            return TryLoad(path, out _, out _);
        }

        /// <summary>
        /// Get a file path.
        /// </summary>
        /// <param name="game">The game.</param>
        /// <param name="name">The name of the restore point.</param>
        /// <returns>The full path.</returns>
        public static string GetFilePath(Game game, string name)
        {
            return Path.Combine(GetRestorePointDirectory(game), $"{name ?? string.Empty}.{Extension ?? string.Empty}");
        }

        #endregion
    }
}
