using NetAF.Assets.Characters;
using NetAF.Conversations;
using NetAF.Conversations.Instructions;
using NetAF.Utilities;

namespace NetAF.Example.Lighthouse.Assets.NPCs
{
    /// <summary>
    /// Martha, the old keeper's widow who still haunts the cottage. She offers clues
    /// to the lighthouse puzzles through conversation.
    /// </summary>
    public class Martha : IAssetTemplate<NonPlayableCharacter>
    {
        #region Constants

        internal const string Name = "Martha";
        private const string Description = "An old woman in a grey shawl sits by the dead hearth, barely more than a shadow. She was the last keeper's wife, they say, and she never did leave Gull Rock.";

        #endregion

        #region Implementation of IAssetTemplate<NonPlayableCharacter>

        /// <inheritdoc/>
        public NonPlayableCharacter Instantiate()
        {
            var conversation = new Conversation
            (
                new Paragraph("Oh, thank the Lord. A keeper come at last. The lamp's been dark three nights now."),
                new Paragraph("Read his logbook if you doubt me - it's all written down plain. He kept it on the cottage table."),
                new Paragraph("The oil store's locked. He always hid that key under the old mooring rope out on the jetty."),
                new Paragraph("And mind - the clockwork up the stair is seized solid. You'll want his winding handle; it's here by my chair where he left it."),
                new Paragraph("One thing more: don't trust damp matches. Dry a box on the lamp-room stove before you strike for the wick.", new First())
            );

            return new NonPlayableCharacter(Name, Description, conversation: conversation);
        }

        #endregion
    }
}
