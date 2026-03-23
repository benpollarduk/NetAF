using NetAF.Extensions;
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
        public static string AutoName { get; set; } = "Auto";

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
            return Save(game, AutoName, out restorePoint, out message);
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

            if (!TryFindFile(game, name, out var path))
                path = CreateNewFilePath(game);

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
            var restorePoints = GetAvailableRestorePoints(game);
            return [.. restorePoints.Select(x => x.Name)];
        }

        /// <summary>
        /// Get the available restore points for a game.
        /// </summary>
        /// <param name="game">The game to get the restore points for.</param>
        /// <returns>An array containing all the available restore points for a game.</returns>
        public static RestorePoint[] GetAvailableRestorePoints(Game game)
        {
            var paths = GetAllRestorePointPaths(game);

            return [.. paths.Select(x =>
            {
                return TryLoad(x, out var restorePoint, out _) ? restorePoint : null;
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
            return Apply(game, AutoName, out message);
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
            if (!TryFindFile(game, name, out var path))
            {
                message = "Could not find a restore point with the provided name.";
                return false;
            }

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
            return GetAvailableRestorePointNames(game).Any(x => x.InsensitiveEquals(name));
        }

        /// <summary>
        /// Create a new file path.
        /// </summary>
        /// <param name="game">The game.</param>
        /// <returns>The full path.</returns>
        public static string CreateNewFilePath(Game game)
        {
            return Path.Combine(GetRestorePointDirectory(game), $"{CreateFileName(DateTime.Now)}.{Extension ?? string.Empty}");
        }

        /// <summary>
        /// Create a file name, based on a date/time.
        /// </summary>
        /// <param name="dateTime">The date/time to base the file name on.</param>
        /// <returns>The file name.</returns>
        public static string CreateFileName(DateTime dateTime)
        {
            return $"{dateTime.Year:D4}_{dateTime.Month:D2}_{dateTime.Day:D2}_{dateTime.Hour:D2}_{dateTime.Minute:D2}_{dateTime.Second:D2}_{dateTime.Millisecond:D3}";
        }

        /// <summary>
        /// Get all restore point paths for a game.
        /// </summary>
        /// <param name="game">The game to get the restore point paths for.</param>
        /// <returns>An array containing all restore point paths.</returns>
        public static string[] GetAllRestorePointPaths(Game game)
        {
            var directory = GetRestorePointDirectory(game);

            if (!Directory.Exists(directory))
                return [];

            return Directory.GetFiles(directory, $"*.{Extension}", SearchOption.TopDirectoryOnly);
        }

        /// <summary>
        /// Try and find a file from the name of a restore point.
        /// </summary>
        /// <param name="game">The game.</param>
        /// <param name="name">The name of the restore point.</param>
        /// <param name="path">The path to the restore point, if the restore point was found.</param>
        /// <returns>True if the restore point could be found, else false.</returns>
        public static bool TryFindFile(Game game, string name, out string path)
        {
            var paths = GetAllRestorePointPaths(game);

            foreach (var p in paths)
            {
                if (!TryPeekName(p, out var restorePointName) || !restorePointName.InsensitiveEquals(name))
                    continue;

                path = p;
                return true;
            }

            path = string.Empty;
            return false;
        }

        /// <summary>
        /// Try and peek the name of a restore point from a file.
        /// </summary>
        /// <param name="path">The path to the file.</param>
        /// <param name="name">The name, if found.</param>
        /// <returns>True if the name could be found, else false.</returns>
        public static bool TryPeekName(string path, out string name)
        {
            name = string.Empty;

            if (!File.Exists(path))
                return false;

            var content = File.ReadAllText(path);

            // name is right at the end of the file
            var lastIndexOfName = content.LastIndexOf("\"Name\"", StringComparison.OrdinalIgnoreCase);
            if (lastIndexOfName == -1)
                return false;

            var colonIndex = content.IndexOf(':', lastIndexOfName);
            if (colonIndex == -1)
                return false;

            var startQuote = content.IndexOf('"', colonIndex);
            if (startQuote == -1)
                return false;

            var endQuote = content.IndexOf('"', startQuote + 1);
            if (endQuote == -1)
                return false;

            name = content.Substring(startQuote + 1, endQuote - startQuote - 1);
            return true;
        }

        #endregion
    }
}
